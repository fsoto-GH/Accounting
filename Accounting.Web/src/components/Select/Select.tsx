import { ChangeEvent } from "react";
import { SelectOption } from "./SelectOption";
import styles from "./Select.module.scss";

type Props = {
  id: string;
  placeholderText: string;
  options: Array<SelectOption> | undefined;
  className?: string;
  size?: number;
  value?: number;
  onSelectedIndexChanged: (e: ChangeEvent<HTMLSelectElement>) => void;
};

function Select({
  id,
  value,
  options,
  onSelectedIndexChanged,
  className,
  size = 1,
  placeholderText,
}: Props) {
  return (
    <>
      <select
        size={size}
        id={id}
        onChange={onSelectedIndexChanged}
        disabled={!options?.length}
        className={`${styles.select} ${className ?? ""}`}
        value={value}
      >
        <option value="">{placeholderText}</option>
        {options &&
          options?.length > 0 &&
          options.map((x) => (
            <option value={x.value} key={x.value}>
              {x.displayValue}
            </option>
          ))}
      </select>
    </>
  );
}

export default Select;
