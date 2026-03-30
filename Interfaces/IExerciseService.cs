using webbir.Models;

namespace webbir.Interfaces;

public interface IExerciseService
{
    IEnumerable<Exercise> GetAll();
    Exercise? GetById(int id);
}