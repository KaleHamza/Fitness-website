using webbir.Data;
using webbir.Interfaces;
using webbir.Models;

namespace webbir.Services;

public class CommentService : ICommentService
{
    private const string FileName = "comments.json";
    private readonly JsonStore _jsonStore;
    private readonly List<Comment> _comments;

    public CommentService(JsonStore jsonStore)
    {
        _jsonStore = jsonStore;
        _comments = _jsonStore.Load<Comment>(FileName);
    }

    public IEnumerable<Comment> GetCommentsByPostId(int postId)
        => _comments.Where(c => c.BlogPostId == postId).OrderByDescending(c => c.CreatedDate);

    public int AddComment(int postId, string author, string content, int? userId)
    {
        var newComment = new Comment
        {
            Id = _comments.Any() ? _comments.Max(c => c.Id) + 1 : 1,
            BlogPostId = postId,
            Author = author,
            Content = content,
            UserId = userId,
            CreatedDate = DateTime.Now
        };

        _comments.Add(newComment);
        Persist();

        return newComment.Id;
    }

    private void Persist() => _jsonStore.Save(FileName, _comments);
}