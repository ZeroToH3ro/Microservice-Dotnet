using Microsoft.AspNetCore.Http.HttpResults;

namespace WebPlatform;

public class PlatformRepo : IPlatformRepo
{
    private readonly AppDbContext _context;
    public PlatformRepo(AppDbContext context)
    {
        _context = context;

    }
    public void CreatePlatform(Platform platform)
    {
        if (platform == null)
        {
            throw new ArgumentNullException(nameof(platform));
        }
        _context.Add(platform);
    }

    public IEnumerable<Platform> GetAllPlatforms() => _context.Platforms.ToList();
    public Platform GetPlatformById(int id)
    {
        var platform = _context.Platforms.FirstOrDefault(p => p.Id == id);
        if (platform == null)
        {
            return null;
        }

        return platform;
    }

    public bool SaveChanges()
    {
        return (_context.SaveChanges() >= 0);
    }
}
