using System.Transactions;

namespace Website.BL.General
{
    public static class Helpers
    {
        public static int? StringToIntDef(string str, int? def)
        {
            if (int.TryParse(str, out int value)) { return value; };
            return def;
        }
        public static Guid? StringToGuidDef(string str)
        {
            if (Guid.TryParse(str, out Guid value)) { return value; };
            return null;
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
