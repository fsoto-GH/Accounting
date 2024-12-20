import { useEffect, useState } from "react";
import Drawer from "../Drawer";
import styles from "../AccountDrawer/AccountDrawer.module.scss";
import { AccountType } from "../../../Enums/AccountTypes";
import { urlBase } from "../../../figureThisOut";
import { Account } from "../../../Models/AccountSummary/Account";

type Props = {
  id: string;
  isOpen: boolean;
  personID: number;
  accountID?: number;
  headerText: string;
  handleDrawerClose: () => void;
  handleSubmit: () => void;
};

type AccountDrawerData = {
  nickName?: string;
  type: number;
};

function AccountDrawer({
  id,
  isOpen,
  personID,
  accountID,
  headerText,
  handleDrawerClose,
  handleSubmit,
}: Props) {
  const [isLoading, setLoading] = useState<boolean>(false);
  const [banneError, setBannerError] = useState();
  const [formData, setFormData] = useState<AccountDrawerData>({
    type: AccountType.CHECKING,
  });

  const handleSubmitClick = async () => {
    handleAccountSubmit({
      nickname: formData.nickName,
      type: formData.type,
    }).then((_) => {
      handleSubmit();
    });
  };

  useEffect(() => {
    if (!accountID) return;
    // only continue if showing
    if (!isOpen) return;
    const controller = new AbortController();
    fetch(`${urlBase}/v1/Persons/${personID}/Accounts/${accountID}`, {
      method: "GET",
      signal: controller.signal,
    })
      .then((res) => {
        if (!res.ok)
          throw new Error(
            `Failed to fetch account for PersonID/AccountID: ${personID}/${accountID}`
          );
        return res.json() as Promise<Account>;
      })
      .then((data) => {
        setFormData({
          nickName: data.nickName,
          type: data.type,
        });
      });
    return () => controller.abort();
  }, [accountID, isOpen]);

  const handleChange = (e: { target: { name: any; value: any } }) => {
    const { name, value } = e.target;
    console.log(name, value);
    setFormData({ ...formData, [name]: value });
  };

  // PATCH Transaction
  async function handleAccountSubmit(account: AccountDto): Promise<AccountDto> {
    const res = accountID
      ? await fetch(`${urlBase}/v1/Persons/${personID}/Accounts/${accountID}`, {
          method: "PATCH",
          body: JSON.stringify(account),
          headers: {
            "Content-Type": "application/json",
          },
        })
      : await fetch(`${urlBase}/v1/Persons/${personID}/Accounts`, {
          method: "POST",
          body: JSON.stringify(account),
          headers: {
            "Content-Type": "application/json",
          },
        });
    if (!res.ok) throw new Error(`An error occurred. Please try again.`);
    return await (res.json() as Promise<Transaction>);
  }

  return (
    <Drawer
      id={`${id}`}
      isOpen={isOpen}
      handleClose={() => {
        handleDrawerClose();
      }}
      headerText={headerText}
      handleSave={handleSubmitClick}
      bannerGlobalMessage={banneError}
      isLoading={isLoading}
    >
      <div className={styles.grid}>
        <label htmlFor="nickName" className={styles.label}>
          Nickname:
        </label>
        <input
          id="nickName"
          name="nickName"
          type="text"
          value={formData.nickName}
          maxLength={200}
          onChange={handleChange}
        ></input>

        <label htmlFor="type" className={styles.label}>
          Account Type:
        </label>
        <select
          id="type"
          name="type"
          value={formData.type}
          onChange={handleChange}
        >
          <option value={AccountType.CHECKING}>Checkings</option>
          <option value={AccountType.SAVINGS}>Savings</option>
        </select>
      </div>
    </Drawer>
  );
}

export default AccountDrawer;
