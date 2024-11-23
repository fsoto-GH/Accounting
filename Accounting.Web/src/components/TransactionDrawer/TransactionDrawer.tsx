import { TransactionType } from "../../Enums/TransactionType";
import Drawer from "../Drawer/Drawer";
import styles from "./TransactionDrawer.module.scss";
import { ChangeEvent, useState } from "react";
import { BannerValidationResult } from "../Drawer/Model/BannerValidationResult";
import { urlBase } from "../../figureThisOut";

type Props = {
  id: string;
  isOpen: boolean;
  personID: number;
  accountID: number;
  selectedTransaction?: Transaction;
  headerText: string;
  handleDrawerClose: () => void;
  handleSubmit: () => void;
};

type TransactionAmountValidationResult = {
  error?: string;
  value?: number;
};

type ValidationResult = {
  isError: boolean;
  amount?: string;
  success?: string;
};

type TransactionFormData = {
  amount?: string;
  description?: string;
};

function TransactionDrawer({
  id,
  isOpen,
  personID,
  accountID,
  selectedTransaction,
  headerText,
  handleDrawerClose,
  handleSubmit,
}: Props) {
  const [isLoading, setLoading] = useState<boolean>(false);
  const defValidationResult: ValidationResult = { isError: false };
  const [banneError, setBannerError] = useState<BannerValidationResult>();

  const [validationResult, setValidation] =
    useState<ValidationResult>(defValidationResult);
  const [formData, setFormData] = useState<TransactionFormData>({
    amount: selectedTransaction?.formattedAmount,
    description: selectedTransaction?.description,
  });

  // PATCH Transaction
  async function handleTransactionChange(
    personID: number,
    accountID: number,
    patchDto: TransactionPatchDto
  ): Promise<Transaction> {
    const res = await fetch(
      `${urlBase}/v1/Persons/${personID}/Accounts/${accountID}/Transactions/${patchDto.transactionID}`,
      {
        method: "PATCH",
        body: JSON.stringify(patchDto),
        headers: {
          "Content-Type": "application/json",
        },
      }
    );
    if (!res.ok) throw new Error(`An error occurred. Please try again.`);
    return await (res.json() as Promise<Transaction>);
  }

  function validateTransactionAmount(): TransactionAmountValidationResult {
    const value = formData.amount?.trim() ?? `${selectedTransaction?.amount}`;

    // first group is the entire match
    // second is the magnitude
    // third is amount
    const validReg = new RegExp("^(-)?\\$?(\\d*\\.?\\d{0,2}?)$");
    const isMatch = validReg.test(value);
    const isEmpty = !value || ["$", "-$"].includes(value);

    console.debug(isEmpty, isMatch);
    if (isEmpty) {
      return {
        error: "Amount cannot be empty.",
      };
    } else if (!isMatch) {
      return {
        error: "Please enter a valid amount.",
      };
    } else {
      const match = validReg.exec(value)!;
      return {
        value: (match[1] === "-" ? -100 : 100) * Number(match[2]),
      };
    }
  }

  /**
   * Validate that the amount input is a parsable amount.
   * Description does not need any validation on its own.
   * Design choice: PATCH even if there is no perceived change.
   */
  const handleSubmitClick = async () => {
    const amountValidationResult = validateTransactionAmount();

    if (amountValidationResult.error) {
      console.debug("There is an error in the amount input.");
      setValidation({
        isError: true,
        amount: amountValidationResult.error,
      });
    } else if (amountValidationResult.value !== undefined) {
      setLoading(true);
      setValidation((prev) => ({
        ...prev,
        isError: true,
        amount: amountValidationResult.error,
      }));

      handleTransactionChange(personID, accountID, {
        // TODO: rework this for adds
        transactionID: selectedTransaction!.transactionID,
        amount: Math.abs(amountValidationResult.value),
        type:
          amountValidationResult.value < 0
            ? TransactionType.DEBIT
            : TransactionType.CREDIT,
        description: formData.description,
      })
        .then(() => {
          setBannerError({});
        })
        .catch((err: Error) => {
          setBannerError({
            isError: true,
            message: err.message,
          });
        })
        .finally(() => {
          setLoading(false);
          handleSubmit();
        });
    }
  };

  function handleFormChange(
    e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  }

  return (
    selectedTransaction && (
      <Drawer
        id={`${id}`}
        isOpen={isOpen}
        handleClose={() => {
          setFormData({});
          setValidation(defValidationResult);
          handleDrawerClose();
        }}
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
            name="description"
            maxLength={200}
            value={formData.description}
            className={`${styles.formDescription} ${styles.descriptionTextarea}`}
            onChange={handleFormChange}
          ></textarea>

          <label htmlFor="amount" className={styles.amountLabel}>
            Amount:
          </label>
          <div className={styles.transactionAmount}>
            <input
              name="amount"
              type="text"
              value={formData.amount}
              onChange={handleFormChange}
            ></input>
            {validationResult.amount && (
              <span className={styles.invalidAmount}>
                {validationResult.amount}
              </span>
            )}
          </div>
        </div>
      </Drawer>
    )
  );
}

export default TransactionDrawer;
