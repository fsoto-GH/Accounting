export type AccountSummary = {
  accountID: number;
  nickName: string | undefined;
  status: boolean;
  type: number;
  totalPurchases: number;
  totalPayments: number;
  netBalance: number;
  countOfTransactions: number;
  personID: number;
  formattedTotalPayments: string;
  formattedTotalPurchases: string;
  formattedNetBalance: string;
};
