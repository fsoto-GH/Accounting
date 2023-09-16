using Accounting.API.Services.Transaction;
using Moq;

namespace Accounting.API.Test.Transaction
{
    public class Tests
    {
        //private ITransactionService? _transactionService;

        [SetUp]
        public void Setup()
        {
            var _transactionService = new Mock<ITransactionService>().Object;
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}