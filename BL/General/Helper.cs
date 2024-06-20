using System.Transactions;

namespace Website.BL.General
{
    public static class Helpers
    {
        public static int? StringToIntDefl(string str, int? def)
        {
            if (int.TryParse(str, out int value)) { return value; };
            return def;
        }

        public static TransactionScope CreateTransactionScope(int seconds = 6000)
        {
            return new TransactionScope(
                TransactionScopeOption.Required,
                new TimeSpan(0, 0, seconds),
                TransactionScopeAsyncFlowOption.Enabled
                );
        }
    }
}
