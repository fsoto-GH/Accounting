using Accounting.API.DAOs.Account;
using Accounting.API.DAOs.Person;
using Accounting.API.DAOs.Transaction;
using Accounting.API.DTOs.Account;
using Accounting.API.DTOs.Person;
using Accounting.API.DTOs.Transaction;
using Accounting.API.Enums;
using Accounting.API.Exceptions.Account;
using Accounting.API.Exceptions.Person;
using Accounting.API.Exceptions.Transaction;
using Accounting.API.Services.Transaction;
using Moq;

namespace Accounting.API.Test.Transaction
{
    public class Tests
    {
        private ITransactionService _transactionService;

        [SetUp]
        public void Setup()
        {
            var _transactionDao = new Mock<ITransactionDao>();
            var _personDao = new Mock<IPersonDao>();
            var _accountDao = new Mock<IAccountDao>();


            _personDao.Setup(x => x.GetAsync(It.Is<int>(i => i == 1))).ReturnsAsync(new PersonDto
            {
                FirstName = "Jake",
                LastName = "Smith",
                MiddleName = null,
                PersonID = 1
            });

            _accountDao.Setup(x => x.GetAsync(It.Is<int>(i => i == 1), It.Is<int>(j => j == 1))).ReturnsAsync(new AccountDto
            {
                PersonID = 1,
                AccountID = 1,
                NickName = "My Account",
                Status = false,
                Type = AccountType.CHECKING
            });

            _transactionDao.Setup(x => x.GetAsync(It.Is<int>(i => i == 1), It.Is<int>(j => j == 1), It.Is<int>(k => k == 1))).ReturnsAsync(new TransactionDto
            {
                TransactionID = 1,
                AccountID = 1,
                PersonID = 1,
                Amount = 10_000,
                Type = TransactionType.CREDIT,
                Description = "My Description",
                Date = new DateTime(2023, 10, 1),
            });
            _transactionService = new TransactionService(_transactionDao.Object, _accountDao.Object, _personDao.Object);

        }

        [Test(Description = "Throws exception on invalid PersonID")]
        public void ExceptionOnInvalidPersonID()
        {
            Assert.ThrowsAsync<NotFoundPersonException>(() =>  _transactionService.GetAsync(2, 1, 1));
        }

        [Test(Description = "Throws exception on invalid AccountID")]
        public void ExceptionOnInvalidAccountID()
        {
            Assert.ThrowsAsync<NotFoundAccountException>(() => _transactionService.GetAsync(1, 2, 1));
        }

        [Test(Description = "Throws exception on invalid TransactionID")]
        public void ExceptionOnInvalidTransactionID()
        {
            Assert.ThrowsAsync<NotFoundTransactionException>(() => _transactionService.GetAsync(1, 1, 2));
        }

        [Test(Description ="Valid call returns correct details.")]
        public async Task CorrectDetailsAreReturnedForValidCall()
        {
            var res = await _transactionService.GetAsync(1, 1, 1);

            Assert.That(res.AccountID, Is.EqualTo(1));
            Assert.That(res.PersonID, Is.EqualTo(1));
            Assert.That(res.TransactionID, Is.EqualTo(1));
            Assert.That(res.Type, Is.EqualTo(TransactionType.CREDIT));
            Assert.That(res.Description, Is.EqualTo("My Description"));
            Assert.That(res.Date, Is.EqualTo(new DateTime(2023, 10, 1)));
            Assert.That(res.Amount, Is.EqualTo(10000));
            Assert.That(res.FormattedAmount, Is.EqualTo("$100.00"));
            Assert.That(res.TransactionTime, Is.EqualTo("2023-10-01T00:00:00"));
        }
    }
}