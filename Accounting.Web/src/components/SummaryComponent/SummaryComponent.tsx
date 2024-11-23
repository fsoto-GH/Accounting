import { KeyValue } from "./KeyValue";
import styles from "./SummaryComponent.module.scss";
type Props = {
  id: string;
  componentHeaderText: string;
  summaryLines: Array<KeyValue>;
  className?: string | undefined;
};

function SummaryComponent({
  id,
  componentHeaderText,
  summaryLines,
  className,
}: Props) {
  const renderSummaryLines = () =>
    summaryLines.map((line) => (
      <tr key={`${id}-${line.key}-row`}>
        <td id={`${id}-${line.key}-label`} key={`${id}-${line.key}-label`}>
          {line.key}
        </td>
        <td key={`${id}-${line.key}-amount`} className={styles.alignRight}>
          {line.value}
        </td>
      </tr>
    ));

  const renderTransactionSummary = () => (
    <div
      id={`${id}`}
      key={`${id}`}
      className={`${styles.summaryComponent} ${className ?? ""}`}
    >
      <table id={`${id}-table`} key={`${id}-table`} className={styles.table}>
        <tbody id={`${id}-body`} key={`${id}-body`}>
          <tr id={`${id}-heading`} key={`${id}-heading`}>
            <th colSpan={2} key={`${id}-summary`}>
              {componentHeaderText}
            </th>
          </tr>
          {renderSummaryLines()}
        </tbody>
      </table>
    </div>
  );

  return renderTransactionSummary();
}

export default SummaryComponent;
