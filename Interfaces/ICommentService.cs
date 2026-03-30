using webbir.Models;

namespace webbir.Interfaces;

public interface ICommentService
{
    IEnumerable<Comment> GetCommentsByPostId(int postId);
    int AddComment(int postId, string author, string content, int? userId);
}