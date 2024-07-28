using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using filip_vaja_MVC.Models;

namespace filip_vaja_MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DbContext _context;

    public HomeController(ILogger<HomeController> logger, DbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Expenses()
    {
        var expensesList = _context.Expenses.ToList();
        var totalExpenses = expensesList.Sum(x => x.Value);

        ViewBag.Expanses = totalExpenses;
        
        return View(expensesList);
    }
    
    public IActionResult CreateEditExpense(int? id)
    {
        if (id != null)
        {
            var expenseInDb = _context.Expenses.SingleOrDefault(x => x.Id == id);
            return View(expenseInDb);
        }
        
        return View();
    }
    
    public IActionResult CreateEditExpenseForm(Expense model)
    {
        if (model.Id == 0)
        {
            _context.Expenses.Add(model);
        }
        else
        {
            _context.Expenses.Update(model);
        }
        _context.SaveChanges();
        
        return RedirectToAction("Expenses");
    }
    
    public IActionResult DeleteExpense(int id)
    {
        var expenseInDb = _context.Expenses.SingleOrDefault(x => x.Id == id);
        _context.Expenses.Remove(expenseInDb);
        _context.SaveChanges();
        
        return RedirectToAction("Expenses");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}