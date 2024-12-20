import { ChangeEvent, useState } from "react";
import styles from "./TransactionGrid.module.scss";

// Remark: while I like this concept of editable grid, we'd have to come to a
type Props = {
  transactionID: number;
  amountString: string;
  handleOnBlur: (data: OnBlurTransactionAmountChange) => void;
};

type ValidationResult = {
  isValid: boolean;
  message: string;
  value: number | undefined;
};

export type OnBlurTransactionAmountChange = {
  transactionID: number;
  value: number | undefined;
};

function TransactionAmountGridCell({
  transactionID,
  amountString,
  handleOnBlur,
}: Props) {
  const [showInput, setShowInput] = useState(false);
  const [validationResult, setValidation] = useState<ValidationResult>({
    isValid: false,
    message: "",
    value: undefined,
  });

  const validateCurrency = (e: ChangeEvent<HTMLInputElement>) => {
    console.debug("Validating...");
    const value = e.currentTarget.value?.trim();

    // first group is the entire match
    // second is the magnituede
    // third is amount
    const validReg = "^(-)?\\$?(\\d*\\.?\\d{0,2}?)$";
    const match = value.match(validReg);

    if (!value || ["$", "-$"].includes(value)) {
      setValidation({
        isValid: false,
        message: "Amount cannot be empty.",
        value: undefined,
      });
    } else if (!match || match.length !== 3) {
      setValidation({
        isValid: false,
        message: "Please enter a valid amount.",
        value: undefined,
      });
    } else if (match.length == 3) {
      setValidation({
        isValid: true,
        message: "",
        value: (match[0] === "-" ? -100 : 100) * Number(match[2]),
      });
    }
    console.debug("Validated...");
  };

  return (
    <td
      key={`transactions-${transactionID}-amount`}
      id={`transactions-${transactionID}-amount`}
      className={styles.alignRight}
      onDoubleClick={() => setShowInput(true)}
    >
      {!showInput && (
        <div>
          {amountString}
          <button role="button"></button>
        </div>
      )}
      {showInput && (
        <div>
          <input
            className={`${styles.alignRight} ${
              validationResult.isValid ? styles.validInput : styles.invalidInput
            }`}
            type="test"
            defaultValue={amountString}
            onChange={validateCurrency}
            onBlur={() => {
              if (validationResult.isValid) {
                setShowInput(false);
                handleOnBlur({
                  transactionID: transactionID,
                  value: validationResult.value,
                });
              }
            }}
          ></input>
          {!validationResult.isValid && (
            <span className={styles.validation}>
              {validationResult.message}
            </span>
          )}
        </div>
      )}
    </td>
  );
}

export default TransactionAmountGridCell;
