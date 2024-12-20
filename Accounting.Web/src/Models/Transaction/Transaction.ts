type Transaction = {
  transactionID: number;
  amount: number;
  rollingBalance: number;
  formattedAmount: string;
  formattedRollingBalance: string;
  date: Date;
  description: string;
  type: number;
};
