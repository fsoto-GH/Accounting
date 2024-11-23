import { useEffect, useState } from "react";
import styles from "../Pagination/Pagination.module.scss";

type PagingSettings = {
  currPage: number;
  pagingSize: number;
};

type Props = {
  id: string;
  pagingSettings: PagingSettings;
  totalRecords: number;
  pagingSteps: [number, ...number[]];
  className?: string;
  onPageSettingChange: (changes: PagingSettings) => void;
};

function Pagination({
  id,
  pagingSettings,
  totalRecords,
  pagingSteps,
  className,
  onPageSettingChange,
}: Props) {
  const MIN_PAGE = 1;

  const [currPage, setCurrPage] = useState(pagingSettings.currPage);
  const [pagingSize, setPagingSize] = useState(pagingSettings.pagingSize);

  useEffect(() => {
    onPageSettingChange({
      pagingSize: pagingSize,
      currPage: MIN_PAGE,
    });
  }, [pagingSize]);

  useEffect(() => {
    onPageSettingChange({
      pagingSize: pagingSize,
      currPage: currPage,
    });
  }, [currPage]);

  const getMaxPageVal = () =>
    totalRecords === 0 ? MIN_PAGE : Math.ceil(totalRecords / pagingSize);

  const getSafePageNumber = (value: number): number => {
    const maxPage = getMaxPageVal();
    let page = Math.floor(value);
    if (page < MIN_PAGE) {
      page = MIN_PAGE;
    } else if (page > maxPage) {
      page = maxPage;
    }

    return page;
  };

  return (
    <div id={id} className={`${styles.container} ${className ?? ""}`}>
      <div className={styles.left}>
        <button
          role="button"
          disabled={currPage === MIN_PAGE}
          className={styles.leftArrow}
          onClick={() => setCurrPage(getSafePageNumber(currPage - 1))}
        >
          &lt;
        </button>
        <input
          type="number"
          name={`${id}-page`}
          min={MIN_PAGE}
          max={getMaxPageVal()}
          onChange={(e) =>
            setCurrPage(getSafePageNumber(Number(e.target.value)))
          }
          value={currPage}
        ></input>
        <button
          role="button"
          disabled={currPage === getMaxPageVal()}
          className={styles.rightArrow}
          onClick={() => setCurrPage(getSafePageNumber(currPage + 1))}
        >
          &gt;
        </button>
      </div>
      <div className={styles.right}>
        <label htmlFor={`${id}-size`}>Show: </label>
        <select
          id={`${id}-size`}
          onChange={(e) => {
            setCurrPage(MIN_PAGE);
            setPagingSize(Number(e.target.value));
          }}
          value={pagingSize}
        >
          {pagingSteps.map((val) => (
            <option value={val} key={`${id}-${val}`}>
              {`${val} records`}
            </option>
          ))}
        </select>
      </div>
    </div>
  );
}

export default Pagination;
