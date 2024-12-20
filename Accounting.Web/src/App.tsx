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
import AccountDrawer from "./components/Drawer/AccountDrawer/AccountDrawer";

function App() {
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

  const [selectedTransactionId, setSelectedTransactionId] = useState<
    number | undefined
  >();

  const [pagedTransactions, setPagedTransactions] =
    useState<PagedTransactions>();

  const [nameQuery, setNameQuery] = useState<string>();

  const [pagingSettings, setPagingSettings] = useState({
    currPage: 1,
    pagingSize: RECORD_STEPS[1],
  });

  const [refresh, setRefresh] = useState(false);

  const doRefresh = () => setRefresh((prev) => !prev);
  const [activeDrawerName, setActiveDrawer] = useState<
    "Account" | "Transaction" | "Person" | undefined
  >();

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

    if (!selectedPerson || !selectedPerson.personID) {
      setPersonAccounts(undefined);
      return;
    }

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
      <div className={styles.selectPersons}>
        <Select
          id="selectPersons"
          className={styles.select}
          placeholder="Select a person"
          value={selectedPerson?.personID ?? 0}
          options={Array.from(allPersons, ([_, value]) => ({
            value: value.personID,
            displayValue: !value?.middleName
              ? `${value.firstName} ${value.lastName}`
              : `${value.firstName} ${value.middleName} ${value.lastName}`,
          }))}
          onSelectedIndexChanged={(e) => handleSelectedPerson(e.target.value)}
        />
        <button id="btnAddPerson" key="btnAddPerson" className={styles.primary}>
          Add a Person
        </button>
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
    if (!transactionID) {
      setActiveDrawer(undefined);
      setSelectedTransactionId(undefined);
    } else {
      setActiveDrawer("Transaction");
      setSelectedTransactionId(transactionID);
    }
  };

  const renderAccountsSelect = () => {
    return (
      <div className={styles.selectAccounts}>
        <Select
          id="selectAccounts"
          className={styles.select}
          placeholder="Select an account"
          value={selectedAccount?.accountID ?? 0}
          options={selectedPersonAccounts?.accounts.map<SelectOption>((x) => {
            return {
              value: x.accountID,
              displayValue: `${x?.nickName ?? AccountType[x.type]}`,
            };
          })}
          onSelectedIndexChanged={(e) => handleSelectedAccount(e.target.value)}
        />
        <button
          id="btnAddAccount"
          key="btnAddAccount"
          className={styles.secondary}
          onClick={() => setActiveDrawer("Account")}
          disabled={!selectedPerson}
        >
          Add an Account
        </button>
      </div>
    );
  };

  const renderAccountSummary = () => (
    <SummaryComponent
      id="personAccountSummary"
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
      className={styles.personAccountSummary}
    ></SummaryComponent>
  );

  const renderTransactionActions = () => {
    return (
      <div id="transactionActions" className={styles.transactionActions}>
        <input
          name="description-query"
          placeholder="Search by description"
          className={styles.searchQuery}
          onChange={(e) => setNameQuery(e.target.value)}
          size={100}
          max={200}
        ></input>
        <Pagination
          id="pagination"
          className={styles.pagination}
          pagingSettings={pagingSettings}
          totalRecords={pagedTransactions?.applicableTransactionCount ?? 0}
          pagingSteps={RECORD_STEPS}
          onPageSettingChange={(change) => setPagingSettings(change)}
        ></Pagination>
      </div>
    );
  };

  const renderTransactions = () => {
    return (
      <TransactionsGrid
        id="transactions"
        className={styles.transactions}
        pagedTransactions={pagedTransactions}
        refreshAccountDetails={() => doRefresh()}
        handleTransactionEditClick={handleTransactionEditClick}
      ></TransactionsGrid>
    );
  };

  const renderPersonSummary = () => (
    <SummaryComponent
      id="personSummary"
      componentHeaderText={`${selectedPerson?.firstName}'s Summary`}
      summaryLines={
        selectedPersonAccounts
          ? [
              {
                key: "Accounts Opened",
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

  function submitDrawerCallback() {
    doRefresh();
    setActiveDrawer(undefined);
  }

  return (
    <>
      <div id="selectGroup" className={styles.selectGroup}>
        {renderPersonSelect()}
        {renderAccountsSelect()}
      </div>
      <div id="content" className={styles.content}>
        {/* {selectedAccount && (
          <h2 className={styles.accountName}>
            {`[${
              selectedAccount?.type == AccountType.CHECKING
                ? "Checking"
                : "Savings"
            }] ${selectedAccount?.nickName}`}
          </h2>
        )} */}
        <div id="filtering" className={styles.filtering}>
          {renderTransactionActions()}
        </div>

        <div id="main" className={styles.main}>
          {selectedPerson && (
            <div id="transactionsArea" className={styles.transactionsArea}>
              {renderTransactions()}
            </div>
          )}
          {selectedPerson && (
            <div id="summariesArea" className={styles.summariesArea}>
              {renderPersonSummary()}
              {selectedAccount && renderAccountSummary()}
            </div>
          )}
        </div>

        {selectedPerson && selectedAccount && (
          <TransactionDrawer
            id="edit-transaction"
            headerText="Edit Transaction"
            isOpen={activeDrawerName === "Transaction"}
            accountID={selectedAccount.accountID}
            personID={selectedPerson.personID}
            transactionID={selectedTransactionId ?? 0}
            handleSubmit={submitDrawerCallback}
            handleDrawerClose={() => setActiveDrawer(undefined)}
          ></TransactionDrawer>
        )}
        {selectedPerson && activeDrawerName === "Account" && (
          <AccountDrawer
            id="add-account"
            headerText={`Add Account for ${selectedPerson.firstName}`}
            isOpen={activeDrawerName === "Account"}
            personID={selectedPerson.personID}
            accountID={selectedAccount?.accountID}
            handleDrawerClose={() => setActiveDrawer(undefined)}
            handleSubmit={submitDrawerCallback}
          ></AccountDrawer>
        )}
      </div>
    </>
  );
}

export default App;
