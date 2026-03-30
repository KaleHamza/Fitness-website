using webbir.Interfaces;
using webbir.Models;

namespace webbir.Services;

public class ExerciseService : IExerciseService
{
    private readonly List<Exercise> _exercises = new()
    {
        new Exercise { Id = 1, Name = "Push-up", Description = "A basic upper body exercise", Category = "Strength" },
        new Exercise { Id = 2, Name = "Squats", Description = "Lower body exercise", Category = "Strength" },
        new Exercise { Id = 3, Name = "Running", Description = "Cardiovascular exercise", Category = "Cardio" },
        new Exercise { Id = 4, Name = "Plank", Description = "Core strengthening exercise", Category = "Core" }
    };

    public IEnumerable<Exercise> GetAll() => _exercises;

    public Exercise? GetById(int id) => _exercises.FirstOrDefault(e => e.Id == id);
}