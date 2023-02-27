using AccountingAPI.DAOs;
using Microsoft.AspNetCore.Mvc;

namespace AccountingAPI.Controllers
{
    [ApiController]
    [Route("v1")]
    public class TransactionController : Controller
    {
        private readonly ITransactionDao _transactionDao;
        public TransactionController(ITransactionDao transactionDao)
        {
            _transactionDao = transactionDao;
        }
    }
}
