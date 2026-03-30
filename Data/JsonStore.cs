using System.Text.Json;

namespace webbir.Data;

public class JsonStore
{
    private readonly IWebHostEnvironment _environment;

    public JsonStore(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    private string GetDataFolder()
    {
        var dataFolder = Path.Combine(_environment.ContentRootPath, "Data");
        Directory.CreateDirectory(dataFolder);
        return dataFolder;
    }

    private string GetFilePath(string fileName)
    {
        return Path.Combine(GetDataFolder(), fileName);
    }

    public List<T> Load<T>(string fileName)
    {
        var path = GetFilePath(fileName);
        if (!File.Exists(path))
            return new List<T>();

        var json = File.ReadAllText(path);
        if (string.IsNullOrWhiteSpace(json))
            return new List<T>();

        try
        {
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }
        catch
        {
            return new List<T>();
        }
    }

    public void Save<T>(string fileName, IEnumerable<T> data)
    {
        var path = GetFilePath(fileName);
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        File.WriteAllText(path, JsonSerializer.Serialize(data.ToList(), options));
    }
}
