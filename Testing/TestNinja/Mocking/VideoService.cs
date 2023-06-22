using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TestNinja.Repositories.Interfaces;

namespace TestNinja.Mocking;

public class VideoService
{
    private readonly IFileRepository _fileRepository;
    private readonly IVideoRepository _videoRepository;
    public VideoService(IFileRepository fileRepository, IVideoRepository videoRepository)
    {
        _fileRepository = fileRepository;
        _videoRepository = videoRepository;
    }
    public string ReadVideoTitle()
    {
        var videoJson = _fileRepository.GetAllText("video.json");
        var video = JsonConvert.DeserializeObject<Video>(videoJson);
        return video == null ? "Error parsing the video." : video.Title;
    }

    public string GetUnprocessedVideosAsCsv()
    {
        var videos = _videoRepository.GetAll();
        var videoIds = videos.Where(s => !s.IsProcessed).Select(s => s.Id).ToList();
        
        return string.Join(",", videoIds);
    }
}

public class Video
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsProcessed { get; set; }
}

public class VideoContext : DbContext
{
    public DbSet<Video> Videos { get; set; }
}