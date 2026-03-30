using webbir.Models;

namespace webbir.Interfaces;

public interface IBlogService
{
    IEnumerable<BlogPost> GetAll();
    BlogPost? GetById(int id);
    int CreatePost(string title, string content, string author, int? userId);
}