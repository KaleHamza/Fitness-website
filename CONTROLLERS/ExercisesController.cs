using Microsoft.AspNetCore.Mvc;
using webbir.Interfaces;
using webbir.Models;

namespace webbir.Controllers;

public class ExercisesController : Controller
{
    private readonly IExerciseService _exerciseService;

    public ExercisesController(IExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
    }

    public IActionResult Index()
    {
        var exercises = _exerciseService.GetAll();
        return View(exercises);
    }

    public IActionResult Details(int id)
    {
        var exercise = _exerciseService.GetById(id);
        if (exercise == null) return NotFound();
        return View(exercise);
    }
}