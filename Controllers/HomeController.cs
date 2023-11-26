using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CrudDelicious.Models;

// 1.- Definir uso de Entity Framework
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CrudDelicious.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private MyContext _context;
    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        List<Dish> AllDishes = _context.Dishes.OrderBy(d => d.Name).ToList();
        return View(AllDishes);
    }

    // 1.-Renderiza la página de nuevos platos
    [HttpGet("dishes/new")]
    public IActionResult AddDish()
    {
        return View();
    }

    // 2.-Ruta POST para crear los platos
    [HttpPost("dishes/create")]
    public IActionResult AddNewDish(Dish NewDish)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _context.Add(NewDish);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {

                return View("AddDish");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return View("AddDish");
        }
    }

    // 3.- Muestra ruta "dishes/{id}
    [HttpGet("dishes/{id}")]
    public IActionResult DishesById(int id)
    {

        Dish? DishById = _context.Dishes.FirstOrDefault(d => d.DishId == id);

        if (DishById != null)
        {
            return View("DishesById", DishById);
        }
        else
        {
            return RedirectToAction("Index");
        }

    }

    // 4.- Ruta Edit Dish
    [HttpGet("dishes/{id}/edit")]
    public IActionResult EditDish(int id)
    {

        Dish? DishToEdit = _context.Dishes.FirstOrDefault(d => d.DishId == id);

        if (DishToEdit != null)
        {
            return View(DishToEdit);
        }
        else
        {
            return RedirectToAction("DishesById");
        }
    }

    // 5.-Ruta Actualizar (Post) Dish
    [HttpPost("dishes/{id}/update")]
    public IActionResult UpdateDish(Dish NewDishVersion, int id)
    {
        Dish? OldDish = _context.Dishes.FirstOrDefault(d => d.DishId == id);

        if (ModelState.IsValid)
        {
            if (OldDish != null)
            {
                OldDish.Chef = NewDishVersion.Chef;
                OldDish.Name = NewDishVersion.Name;
                OldDish.Calories = NewDishVersion.Calories;
                OldDish.Tastiness = NewDishVersion.Tastiness;
                OldDish.Description = NewDishVersion.Description;
                OldDish.UpdatedAt = DateTime.Now;

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        return View("EditDish", OldDish);
    }

    // 6.- Ruta Delete (Post) Dish.
    [HttpPost("dishes/{id}/delete")]
    public IActionResult DeleteDish(int id)
    {
        Dish? DishToDelete = _context.Dishes.FirstOrDefault(d => d.DishId == id);

        if (DishToDelete != null)
        {
            _context.Remove(DishToDelete);
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
