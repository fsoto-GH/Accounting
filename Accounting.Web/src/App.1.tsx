import { useEffect, useState } from "react";
import Select from "./components/Select/Select";
import { Person } from "./Models/Person/Person";
import { PersonAccounts } from "./Models/AccountSummary/PersonAccounts";
import { SelectOption } from "./components/Select/SelectOption";
import { Account } from "./Models/AccountSummary/Account";
import { AccountType } from "./Enums/AccountTypes";
import styles from "./App.module.scss";
import TransactionsGrid from "./components/Transactions/TransactionsGrid";
import { PagedTransactions } from "./Models/Transaction/PagedTransactions";
import SummaryComponent from "./components/SummaryComponent/SummaryComponent";
import { AccountSummary } from "./Models/AccountSummary/AccountSummary";
import Pagination from "./components/Pagination/Pagination";
import { urlBase } from "./figureThisOut";
import TransactionDrawer from "./components/TransactionDrawer/TransactionDrawer";

export function App() {
  const RECORD_STEPS: [number, ...number[]] = [5, 25, 50];

  // allPersons -> selectedPerson -> personAccounts
  // selectedAccount -> pagedTransactions
  const [allPersons, setPersons] = useState<Map<number, Person>>(new Map());

  const [selectedPerson, setSelectedPerson] = useState<Person>();
  const [selectedPersonAccounts, setPersonAccounts] =
    useState<PersonAccounts>();

  const [selectedAccount, setSelectedAccount] = useState<Account>();
  const [selectedAccountSummary, setAccountSummary] =
    useState<AccountSummary>();

  const [pagedTransactions, setPagedTransactions] =
    useState<PagedTransactions>();

  const [nameQuery, setNameQuery] = useState<string>();

  const [pagingSettings, setPagingSettings] = useState({
    currPage: 1,
    pagingSize: RECORD_STEPS[1],
  });

  const [refresh, setRefresh] = useState(false);

  const doRefresh = () => setRefresh((prev) => !prev);
  const [drawerState, setDrawerState] = useState<{
    isOpen: boolean;
    selectedTransaction: Transaction | undefined;
  }>({
    isOpen: false,
    selectedTransaction: undefined,
  });

  // GET Persons only on load.
  useEffect(() => {
    console.debug("Fetching persons!");
    fetch(`${urlBase}/v1/Persons`, {
      method: "GET",
    })
      .then((res) => {
        if (!res.ok) {
          throw new Error("Failed to fetch persons.");
        }

        console.debug("Sucessfully fetched persons.");
        return res.json() as Promise<Array<Person>>;
      })
      .then((data) => {
        if (!data) return;

        let res: Map<number, Person> = new Map();
        data.forEach((x) => res.set(x.personID, x));
        setPersons(res);
      })
      .catch((err) => {
        console.error("There was a problem with the fetch operation:", err);
      });
  }, []);

  // GET Accounts for Selected Person
  useEffect(() => {
    const controller = new AbortController();

    if (!selectedPerson || !selectedPerson.personID) return;

    fetch(`${urlBase}/v1/Persons/${selectedPerson?.personID}/Accounts`, {
      method: "GET",
      signal: controller.signal,
    })
      .then((res) => {
        if (!res.ok) {
          throw new Error("Failed to fetch person's accounts.");
        }

        console.debug("Sucessfully fetched person's accounts.");
        return res.json() as Promise<PersonAccounts>;
      })
      .then((data) => {
        setPersonAccounts(data);
      })
      .catch((err) => {
        setPersonAccounts(undefined);
        if (err instanceof DOMException) {
          console.debug("Cancelled previous request for person accounts.");
        } else {
          console.error("There was a problem with the fetch operation:", err);
        }
      });

    return () => controller.abort();
  }, [selectedPerson, refresh]);

  // GET Paged Transaction
  useEffect(() => {
    const controller = new AbortController();

    if (!selectedPerson?.personID || !selectedAccount?.accountID) return;

    const urlParams = new URLSearchParams();
    if (pagingSettings) {
      urlParams.append("page", pagingSettings.currPage.toString());
      urlParams.append("size", pagingSettings.pagingSize.toString());
    }
    if (nameQuery) urlParams.append("nameQuery", nameQuery.trim());

    fetch(
      `${urlBase}/v1/Persons/${selectedPerson.personID}/Accounts/${selectedAccount.accountID}/Transactions?` +
        urlParams,
      {
        method: "GET",
        signal: controller.signal,
      }
    )
      .then((res) => {
        if (!res.ok) {
          throw new Error(
            `Failed to fetch transactions for PersonID/AccountID: ${selectedPerson.personID}/${selectedAccount.accountID}`
          );
        }

        return res.json() as Promise<PagedTransactions>;
      })
      .then((data) => {
        data.transactions.map((x) => (x.date = new Date(x.date)));
        setPagedTransactions(data);
      })
      .catch((err) => {
        setPagedTransactions(undefined);
        console.error("There was a problem with the fetch operation:", err);
      });

    return () => controller.abort();
  }, [
    selectedPerson,
    selectedAccount,
    nameQuery,
    pagingSettings.currPage,
    pagingSettings.pagingSize,
    refresh,
  ]);

  // GET Account Summary
  useEffect(() => {
    const controller = new AbortController();

    if (!selectedPerson?.personID || !selectedAccount?.accountID) return;

    setPagingSettings({ ...pagingSettings, currPage: 1 });
    fetch(
      `${urlBase}/v1/Persons/${selectedPerson?.personID}/Accounts/${selectedAccount?.accountID}`,
      {
        method: "GET",
        signal: controller.signal,
      }
    )
      .then((res) => {
        if (!res.ok) {
          throw new Error("Failed to fetch account summary.");
        }

        console.debug("Sucessfully fetched account summary.");
        return res.json() as Promise<AccountSummary>;
      })
      .then((data) => {
        setAccountSummary(data);
      })
      .catch((err) => {
        console.error("There was a problem with the fetch operation:", err);
      });

    return () => controller.abort();
  }, [selectedPerson, selectedAccount, refresh]);

  const handleSelectedPerson = (value: string) => {
    setPagedTransactions(undefined);
    setSelectedAccount(undefined);
    setSelectedPerson(allPersons.get(Number(value)));
  };

  const renderPersonSelect = () => {
    return (
      <div className={`${styles.input_group}`}>
        <Select
          id="selectPersons"
          className={styles.select}
          placeholderText="Select a person."
          value={selectedPerson?.personID ?? 0}
          options={Array.from(allPersons, ([_, value]) => ({
            value: value.personID,
            displayValue: !value?.middleName
              ? `${value.firstName} ${value.lastName}`
              : `${value.firstName} ${value.middleName} ${value.lastName}`,
          }))}
          onSelectedIndexChanged={(e) => handleSelectedPerson(e.target.value)}
        />
      </div>
    );
  };

  const handleSelectedAccount = (value: string) => {
    setPagedTransactions(undefined);
    setAccountSummary(undefined);
    const newSelection = selectedPersonAccounts?.accounts.find(
      (x) => x.accountID === Number(value)
    );
    setSelectedAccount(newSelection);
  };

  const handleTransactionEditClick = (transactionID: number) => {
    const clickedTrans = pagedTransactions?.transactions.find(
      (x) => x.transactionID === transactionID
    );
    if (!clickedTrans) return;
    setDrawerState({ isOpen: true, selectedTransaction: clickedTrans });
  };

  const renderAccountsSelect = () => {
    return (
      <div className={`${styles.input_group}`}>
        <Select
          id="selectAccounts"
          placeholderText="Select an account."
          value={selectedAccount?.accountID ?? 0}
          options={selectedPersonAccounts?.accounts.map<SelectOption>((x) => {
            return {
              value: x.accountID,
              displayValue: `${x?.nickName ?? AccountType[x.type]}`,
            };
          })}
          onSelectedIndexChanged={(e) => handleSelectedAccount(e.target.value)}
        />
      </div>
    );
  };

  const renderAccountSummary = () => (
    <SummaryComponent
      id="transactionsSummary"
      componentHeaderText="Account Summary"
      summaryLines={
        selectedAccountSummary
          ? [
              {
                key: "Purchases",
                value: selectedAccountSummary.formattedTotalPurchases,
              },
              {
                key: "Payments",
                value: selectedAccountSummary.formattedTotalPayments,
              },
              {
                key: "Balance",
                value: selectedAccountSummary.formattedNetBalance,
              },
            ]
          : []
      }
      className={styles.transactionsSummary}
    ></SummaryComponent>
  );

  const renderTransactionActions = () => {
    return (
      <>
        <div id="left-menu" className={styles.left}>
          <input
            name="description-query"
            placeholder="Search by description"
            onChange={(e) => setNameQuery(e.target.value)}
          ></input>
        </div>
        <div id="right-menu" className={styles.right}></div>
      </>
    );
  };

  const renderTransactionGrid = () => {
    return (
      <TransactionsGrid
        id="transactions"
        pagedTransactions={pagedTransactions}
        className={styles.clear}
        refreshAccountDetails={() => doRefresh()}
        handleTransactionEditClick={handleTransactionEditClick}
      ></TransactionsGrid>
    );
  };

  const renderPersonSummary = () => (
    <SummaryComponent
      id="personAccountSummary"
      componentHeaderText={`${selectedPerson?.firstName}'s Summary`}
      summaryLines={
        selectedPersonAccounts
          ? [
              {
                key: "Accounts Open",
                value: selectedPersonAccounts.accounts
                  .filter((x) => x.status)
                  .length.toString(),
              },
              {
                key: "Accounts Closed",
                value: selectedPersonAccounts.accounts
                  .filter((x) => !x.status)
                  .length.toString(),
              },
              {
                key: "Total Accounts",
                value: `${selectedPersonAccounts.totalAccounts}`,
              },
              {
                key: "Total Net Balance",
                value: `${selectedPersonAccounts?.formattedNetBalance}`,
              },
            ]
          : []
      }
      className={styles.personSummary}
    ></SummaryComponent>
  );

  const renderPersonActions = () => {
    return (
      <>
        <button key="btnAddPerson" className={styles.primary}>
          Add a Person
        </button>
        <button key="btnAddAccount" className={styles.secondary}>
          Add an Account
        </button>
      </>
    );
  };

  function handleTransactionChange() {
    setRefresh();
  }

  return (
    <div id="content" className={styles.content}>
      <div id="selectGroup" className={styles.selectContainer}>
        {renderPersonSelect()}
        {renderAccountsSelect()}
      </div>
      <div key="personActions" className={styles.personActions}>
        {renderPersonActions()}
      </div>
      {selectedPerson && (
        <div className={styles.summaries}>
          {renderPersonSummary()}
          {selectedAccount && renderAccountSummary()}
        </div>
      )}
      {selectedPerson && (
        <div id="transactionActions" className={styles.transactionActions}>
          {selectedPerson && renderTransactionActions()}
        </div>
      )}
      {selectedPerson && (
        <div id="transactionsGrid" className={styles.transactionsGrid}>
          {selectedAccountSummary && (
            <Pagination
              id="pagination"
              pagingSettings={pagingSettings}
              totalRecords={pagedTransactions?.applicableTransactionCount ?? 0}
              pagingSteps={RECORD_STEPS}
              onPageSettingChange={(change) => setPagingSettings(change)}
            ></Pagination>
          )}
          {renderTransactionGrid()}
        </div>
      )}
      {selectedPerson && selectedAccount && drawerState.selectedTransaction && (
        <TransactionDrawer
          id={
            drawerState.selectedTransaction
              ? "edit-transaction"
              : "add-transaction"
          }
          personID={selectedPerson.personID}
          accountID={selectedAccount.accountID}
          isOpen={drawerState.isOpen}
          selectedTransaction={drawerState.selectedTransaction}
          handleDrawerClose={() =>
            setDrawerState({ isOpen: false, selectedTransaction: undefined })
          }
          handleSubmit={handleTransactionChange}
          headerText={
            drawerState.selectedTransaction
              ? "Edit Transaction"
              : "Add a Transaction"
          }
        ></TransactionDrawer>
      )}
    </div>
  );
}
