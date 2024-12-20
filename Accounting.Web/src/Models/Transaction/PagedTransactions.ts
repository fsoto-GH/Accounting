export type PagedTransactions = {
  accountID: number;
  formattedTotalPayments: string;
  formattedTotalPurchases: string;
  personID: number;
  totalPayments: number;
  totalPurchases: number;
  transactions: Array<Transaction> | Transaction[];
  applicableTransactionCount: number;
};
