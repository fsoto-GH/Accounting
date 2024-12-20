import { MouseEvent } from "react";
import { PagedTransactions } from "../../Models/Transaction/PagedTransactions";
import styles from "./TransactionGrid.module.scss";

type Props = {
  id: string;
  className?: string | undefined;
  pagedTransactions: PagedTransactions | undefined;
  refreshAccountDetails: () => void;
  handleTransactionEditClick: (transactionID: number) => void;
};

function TransactionsGrid({
  id,
  className,
  pagedTransactions,
  handleTransactionEditClick,
}: Props) {
  const displayContextMenu = (
    e: MouseEvent<HTMLTableRowElement>,
    transactionID: number
  ) => {
    e.preventDefault();
    console.debug(transactionID);
  };

  const renderTransactions = () => {
    var options: Intl.DateTimeFormatOptions = {
      year: "numeric",
      month: "2-digit",
      day: "2-digit",
    };

    // TODO: Refactor? Looks weird when you have a loading fetch request.
    if (!pagedTransactions) {
      return (
        <tr className={styles.noData}>
          <td colSpan={4}>Not much to see here!</td>
        </tr>
      );
    }

    return pagedTransactions.transactions.map((transaction) => (
      <tr
        key={`transactions-${transaction.transactionID}-row`}
        id={`transactions-${transaction.transactionID}-row`}
        onContextMenu={(e) => displayContextMenu(e, transaction.transactionID)}
      >
        <td
          key={`transactions-${transaction.transactionID}-date`}
          className={styles.date}
        >
          <button
            type="button"
            className={styles.dateEdit}
            onClick={() =>
              handleTransactionEditClick(transaction.transactionID)
            }
          >
            {transaction.date.toLocaleDateString("en-US", options)}
          </button>
        </td>
        <td
          key={`transactions-${transaction.transactionID}-description`}
          className={styles.description}
        >
          {transaction.description?.length > 100
            ? transaction.description.substring(0, 100)
            : transaction.description}
        </td>
        <td
          key={`transactions-${transaction.transactionID}-amount`}
          className={`${styles.amount} ${styles.alignRight}`}
        >
          {transaction.formattedAmount}
        </td>
        <td
          key={`transactions-${transaction.transactionID}-rollingAmount`}
          className={styles.alignRight}
        >
          {transaction.formattedRollingBalance}
        </td>
      </tr>
    ));
  };

  const renderTransactionGrid = () => {
    return (
      <div id={id} className={`${styles.rounded} ${className ?? ""}`}>
        <table key={`${id}-table`} id={`${id}-table`} className={styles.table}>
          <tbody key={`${id}-body`} id={`${id}-body`}>
            <tr key={`${id}-heading`} id={`${id}-heading`}>
              <th key={`${id}-date`} className={styles.date}>
                Date
              </th>
              <th key={`${id}-description`} className={styles.description}>
                Description
              </th>
              <th
                key={`${id}-amount`}
                className={`${styles.alignRight} ${styles.formattedRollingAmount}`}
              >
                Amount
              </th>
              <th
                key={`${id}-rollingAmount`}
                className={`${styles.alignRight} ${styles.formattedRollingAmount}`}
              >
                Balance
              </th>
            </tr>
            {renderTransactions()}
          </tbody>
        </table>
      </div>
    );
  };

  return renderTransactionGrid();
}

export default TransactionsGrid;
