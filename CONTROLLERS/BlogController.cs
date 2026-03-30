using Microsoft.AspNetCore.Mvc;
using webbir.Interfaces;
using webbir.Models;

namespace webbir.Controllers;

public class BlogController : Controller
{
    private readonly IBlogService _blogService;
    private readonly ICommentService _commentService;

    public BlogController(IBlogService blogService, ICommentService commentService)
    {
        _blogService = blogService;
        _commentService = commentService;
    }

    public IActionResult Index()
    {
        var posts = _blogService.GetAll();
        return View(posts);
    }

    public IActionResult Details(int id)
    {
        var post = _blogService.GetById(id);
        if (post == null) return NotFound();
        
        var comments = _commentService.GetCommentsByPostId(id);
        ViewBag.Comments = comments;
        ViewBag.PostId = id;
        
        return View(post);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToAction("Login", "Auth");
        }
        return View();
    }

    [HttpPost]
    public IActionResult Create(string title, string content)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var username = HttpContext.Session.GetString("Username");

        if (userId == null || username == null)
        {
            return RedirectToAction("Login", "Auth");
        }

        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
        {
            ViewBag.Error = "Title and content are required";
            return View();
        }

        _blogService.CreatePost(title, content, username, userId);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult AddComment(int postId, string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return RedirectToAction("Details", new { id = postId });
        }

        var username = HttpContext.Session.GetString("Username");
        var userId = HttpContext.Session.GetInt32("UserId");
        var author = !string.IsNullOrEmpty(username) ? username : "Anonymous";

        _commentService.AddComment(postId, author, content, userId);
        return RedirectToAction("Details", new { id = postId });
    }
}