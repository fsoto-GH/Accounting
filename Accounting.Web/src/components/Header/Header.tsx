import styles from "./Header.module.scss";

interface Props {
  id: string;
}

function Header({ id }: Props) {
  return (
    <>
      <div id={id} className={styles.header}>
        <div className={styles.heading}>
          <h1>Accounting</h1>
        </div>

        <div className={styles.rightNavigation}>
          <a href="">Persons</a>
          <a href="">Login</a>
        </div>
      </div>
    </>
  );
}

export default Header;
