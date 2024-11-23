import { Account } from "./Account";

export type PersonAccounts = {
  netBalance: number;
  personID: number;
  totalAccounts: number;
  formattedNetBalance: string;
  accounts: Array<Account>;
};
