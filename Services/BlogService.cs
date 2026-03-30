using webbir.Data;
using webbir.Interfaces;
using webbir.Models;

namespace webbir.Services;

public class BlogService : IBlogService
{
    private const string FileName = "blogposts.json";
    private readonly JsonStore _jsonStore;
    private readonly List<BlogPost> _posts;

    public BlogService(JsonStore jsonStore)
    {
        _jsonStore = jsonStore;
        _posts = _jsonStore.Load<BlogPost>(FileName);

        if (!_posts.Any())
        {
            _posts.AddRange(new[]
            {
                new BlogPost { Id = 1, Title = "Fitness Başlangıç Rehberi", Content = "Fitness yolculuğunuza başlamak için ipuçları: Düzenli egzersiz yapın, beslenmenize dikkat edin, yeterli uyku alın. Başlangıç seviyesinde haftada 3-4 gün antrenman yeterli olabilir.", PublishedDate = DateTime.Now.AddDays(-1), Author = "Admin" },
                new BlogPost { Id = 2, Title = "Beslenme Önerileri", Content = "Sağlıklı beslenme için öneriler: Protein, karbonhidrat ve yağ dengesini sağlayın. Bol su için, sebze ve meyve tüketimi artırın. İşlenmiş gıdalardan kaçının.", PublishedDate = DateTime.Now.AddDays(-2), Author = "Admin" },
                new BlogPost { Id = 3, Title = "Antrenman Motivasyonu", Content = "Motivasyonunuzu yüksek tutmak için hedefler belirleyin, ilerlemenizi takip edin, arkadaşlarınızla birlikte egzersiz yapın. Küçük adımllarla başlayın ve sabırlı olun.", PublishedDate = DateTime.Now.AddDays(-3), Author = "Admin" }
            });
            Persist();
        }
    }

    public IEnumerable<BlogPost> GetAll() => _posts.OrderByDescending(p => p.PublishedDate);

    public BlogPost? GetById(int id) => _posts.FirstOrDefault(p => p.Id == id);

    public int CreatePost(string title, string content, string author, int? userId)
    {
        var newId = _posts.Any() ? _posts.Max(p => p.Id) + 1 : 1;

        var newPost = new BlogPost
        {
            Id = newId,
            Title = title,
            Content = content,
            Author = author,
            UserId = userId,
            PublishedDate = DateTime.Now
        };

        _posts.Add(newPost);
        Persist();

        return newPost.Id;
    }

    private void Persist() => _jsonStore.Save(FileName, _posts);
}
