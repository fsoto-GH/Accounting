import { TransactionType } from "../../Enums/TransactionType";
import Drawer from "../Drawer/Drawer";
import styles from "./TransactionDrawer.module.scss";
import { useEffect, useState } from "react";
import { BannerValidationResult } from "../Drawer/Model/BannerValidationResult";
import { urlBase } from "../../figureThisOut";

type Props = {
  id: string;
  isOpen: boolean;
  personID: number;
  accountID: number;
  transactionID: number;
  headerText: string;
  handleDrawerClose: () => void;
  handleSubmit: () => void;
};

function TransactionDrawer({
  id,
  isOpen,
  personID,
  accountID,
  transactionID,
  headerText,
  handleDrawerClose,
  handleSubmit,
}: Props) {
  const [isLoading, setLoading] = useState<boolean>(false);
  const [banneError, setBannerError] = useState<BannerValidationResult>();
  const [amountError, setAmountError] = useState("");
  const [formData, setFormData] = useState({
    amount: "0",
    description: "",
    transactionID: 0,
    type: 0,
  });

  // PATCH Transaction
  async function handleTransactionSubmit(
    transaction: TransactionDto
  ): Promise<Transaction> {
    const res = await fetch(
      `${urlBase}/v1/Persons/${personID}/Accounts/${accountID}/Transactions/${transactionID}`,
      {
        method: "PATCH",
        body: JSON.stringify(transaction),
        headers: {
          "Content-Type": "application/json",
        },
      }
    );
    if (!res.ok) throw new Error(`An error occurred. Please try again.`);
    return await (res.json() as Promise<Transaction>);
  }

  // GET Transaction Details
  useEffect(() => {
    if (!transactionID) return;
    // only continue if showing
    if (!isOpen) return;
    const controller = new AbortController();
    fetch(
      `${urlBase}/v1/Persons/${personID}/Accounts/${accountID}/Transactions/${transactionID}`,
      {
        method: "GET",
        signal: controller.signal,
      }
    )
      .then((res) => {
        if (!res.ok)
          throw new Error(
            `Failed to fetch transactions for PersonID/AccountID/Transaction: ${personID}/${accountID}/${transactionID}`
          );
        return res.json() as Promise<Transaction>;
      })
      .then((data) => {
        setFormData({
          transactionID: data.transactionID,
          amount: data.formattedAmount,
          description: data.description,
          type: data.type,
        });
      });
    return () => controller.abort();
  }, [transactionID, isOpen]);

  /**
   * Validate that the amount input is a parsable amount.
   * Description does not need any validation on its own.
   * Design choice: PATCH even if there is no perceived change.
   */
  const handleSubmitClick = async () => {
    const validatedAmount = validateAmount(formData.amount);

    if (validatedAmount === undefined) return;

    handleTransactionSubmit({
      amount: Math.abs(validatedAmount * 100),
      transactionID: transactionID,
      description: formData.description,
      type:
        validatedAmount < 0 ? TransactionType.DEBIT : TransactionType.CREDIT,
    }).then((_) => {
      handleSubmit();
    });
  };

  const handleChange = (e: { target: { name: any; value: any } }) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleAmountChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const input = e.target.value.trim();
    validateAmount(input);
    setFormData({ ...formData, amount: input });
  };

  const handleAmountBlur = (e: React.ChangeEvent<HTMLInputElement>) => {
    const input = e.target.value.trim();

    const validatedAmount = validateAmount(input);
    if (validatedAmount !== undefined) {
      const result = new Intl.NumberFormat("en-US", {
        style: "currency",
        currency: "USD",
      }).format(validatedAmount);
      setAmountError("");
      setFormData({ ...formData, amount: result });
    }
  };

  const validateAmount = (input: string) => {
    let whole = "0";
    let decimal = "0";

    if (input.indexOf(".") !== -1) {
      let splitInput = input.split(".");
      if (splitInput.length > 2) {
        setAmountError("Invalid amount.");
        return;
      }
      [whole, decimal] = input.split(".");
      decimal = decimal.length > 0 ? decimal : "00";
      whole = whole.length > 0 ? whole : "0";
    } else {
      whole = input.length > 0 ? input : "0";
    }

    whole = whole.replace(/[$,\.]/g, "");
    // all characters should be numbers now (w possible leading "-")
    const isAllDigits = (str: string) => /^-?\d+$/.test(str);
    if (!isAllDigits(whole) || !isAllDigits(decimal)) {
      setAmountError("Invalid amount.");
      return;
    }

    if (decimal.length > 2) {
      const _decimal = parseInt(decimal);
      decimal = `${Math.round(_decimal / 10)}`;
    }

    let parsedRes = parseInt(whole, 10);
    parsedRes +=
      parsedRes >= 0
        ? parseInt(decimal, 10) / 100
        : -parseInt(decimal, 10) / 100;

    return parsedRes;
  };

  return (
    !!transactionID && (
      <Drawer
        id={`${id}`}
        isOpen={isOpen}
        handleClose={handleDrawerClose}
        headerText={headerText}
        handleSave={handleSubmitClick}
        bannerGlobalMessage={banneError}
        isLoading={isLoading}
      >
        <div className={styles.grid}>
          <label htmlFor="description" className={`${styles.descriptionLabel}`}>
            Description:
          </label>
          <textarea
            id="description"
            name="description"
            maxLength={200}
            value={formData?.description}
            className={`${styles.formDescription} ${styles.descriptionTextarea}`}
            onChange={handleChange}
          ></textarea>

          <label htmlFor="amount" className={styles.amountLabel}>
            Amount:
          </label>
          <div className={styles.transactionAmount}>
            <input
              id="amount"
              name="amount"
              type="text"
              value={formData?.amount}
              onChange={handleAmountChange}
              onBlur={handleAmountBlur}
            ></input>
            {amountError && (
              <span className={styles.invalidAmount}>{amountError}</span>
            )}
          </div>
        </div>
      </Drawer>
    )
  );
}

export default TransactionDrawer;
