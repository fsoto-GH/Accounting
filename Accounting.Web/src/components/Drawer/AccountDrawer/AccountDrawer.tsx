import { ChangeEvent, useState } from "react";
import Drawer from "../Drawer";
import styles from "../AccountDrawer/AccountDrawer.module.scss";

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
  personID: number;
  accountID?: number;
  nickName?: string;
  type: 1 | 2; // checking | savings
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
    personID: personID,
    accountID: accountID,
    type: 1,
  });

  const handleSubmitClick = () => {};

  function handleFormChange(
    e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>
  ) {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
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
          Nickname:{" "}
        </label>
        <input type="text" maxLength={200} onChange={handleFormChange}></input>

        <label htmlFor="type" className={styles.label}>
          Account Type:{" "}
        </label>
        <select value={formData.type} onChange={handleFormChange}>
          <option value={1}>Checkings</option>
          <option value={2}>Savings</option>
        </select>
      </div>
    </Drawer>
  );
}

export default AccountDrawer;
