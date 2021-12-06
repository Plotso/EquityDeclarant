namespace EquityDeclarant.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Models.InputModels;
    using Models.ViewModels;
    using Services;

    public class StatementController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRevolutStatementParser _revolutStatementParser;

        public StatementController(ILogger<HomeController> logger, IRevolutStatementParser revolutStatementParser)
        {
            _logger = logger;
            _revolutStatementParser = revolutStatementParser;
        }

        public IActionResult Upload()
        {
            return View(new StatementInputModel());
        }

        public IActionResult Result()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Upload(StatementInputModel statementUpload)
        {
            if (ModelState.IsValid && statementUpload.Statement != null)
            {
                var transactions = await _revolutStatementParser.GetTransactions(statementUpload);
                var debugVar = true;
            }
            return RedirectToAction(nameof(Result));
        }
    }
}