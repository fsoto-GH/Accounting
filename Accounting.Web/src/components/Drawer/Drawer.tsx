import { FormEvent } from "react";
import styles from "./Drawer.module.scss";
import { BannerValidationResult } from "./Model/BannerValidationResult";

type Props = {
  id: string;
  isOpen: boolean;
  headerText: string;
  bannerGlobalMessage?: BannerValidationResult;
  isLoading: boolean;
  handleSave?: () => void;
  handleClose: () => void;
  children?: React.ReactNode;
};

function Drawer({
  id,
  isOpen,
  children,
  headerText,
  bannerGlobalMessage,
  isLoading,
  handleSave,
  handleClose,
}: Props) {
  function handleSubmitClick(e: FormEvent) {
    e.preventDefault();
    if (handleSave) handleSave();
  }
  const renderDrawer = () => {
    return (
      <form onSubmit={(e) => handleSubmitClick(e)}>
        <div
          id={`${id}-drawer-background`}
          className={styles.drawerBackground}
        ></div>
        <div className={styles.drawerContent}>
          <div>
            {bannerGlobalMessage?.isError !== undefined && (
              <div
                className={
                  bannerGlobalMessage.isError
                    ? styles.isInvalid
                    : styles.isValid
                }
              >
                {bannerGlobalMessage.isError
                  ? bannerGlobalMessage.message
                  : bannerGlobalMessage.success}
              </div>
            )}
          </div>
          <div id={`${id}-drawer-body`} className={styles.drawerBody}>
            <div className={styles.header}>
              <h1>{headerText}</h1>
              <button
                type="submit"
                className={styles.primary}
                disabled={isLoading}
              >
                Submit
              </button>
              <button
                type="button"
                className={styles.secondary}
                onClick={handleClose}
                disabled={isLoading}
              >
                Cancel
              </button>
            </div>
            <div className={styles.body}>{children}</div>
          </div>
        </div>
      </form>
    );
  };
  return isOpen && renderDrawer();
}

export default Drawer;
