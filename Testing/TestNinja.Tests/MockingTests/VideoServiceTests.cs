using Moq;
using Newtonsoft.Json;
using TestNinja.Mocking;
using TestNinja.Repositories.Interfaces;

namespace TestNinja.Tests.MockingTests;

[TestFixture]
public class VideoServiceTests
{
    private Mock<IFileRepository> _fakeFileRepository = null!;
    private Mock<IVideoRepository> _fakeVideoRepository = null!;
    private VideoService _videoService = null!;

    [SetUp]
    public void Setup()
    {
        _fakeFileRepository = new Mock<IFileRepository>();
        _fakeVideoRepository = new Mock<IVideoRepository>();
        _videoService = new VideoService(_fakeFileRepository.Object, _fakeVideoRepository.Object);
    }

    [Test]
    public void ReadVideoTitle_IfInvalidJson_ReturnErrorString()
    {
        _fakeFileRepository.Setup(s => s.GetAllText(It.IsAny<string>())).Returns("");

        var result = _videoService.ReadVideoTitle();

        Assert.That(result, Does.Contain("error").IgnoreCase);
    }

    [Test]
    public void ReadVideoTitle_IfValidJson_ReturnTitle()
    {
        _fakeFileRepository.Setup(s => s.GetAllText(It.IsAny<string>()))
            .Returns(JsonConvert.SerializeObject(new Video { Title = "this is the same video title" }));

        var result = _videoService.ReadVideoTitle();

        Assert.That(result, Does.Contain("this is the same video title").IgnoreCase);
    }

    private static readonly object[] ProcessedVideos =
    {
        new List<Video>
        {
            new() { Id = 2, IsProcessed = true },
            new() { Id = 4, IsProcessed = true },
            new() { Id = 8, IsProcessed = true }
        },
        new List<Video>()
    };

    [Test]
    [TestCaseSource(nameof(ProcessedVideos))]
    public void GetUnprocessedVideosAsCsv_AllVideosAreProcessedOrHasNoVideo_ReturnEmptyString(List<Video> videos)
    {
        _fakeVideoRepository.Setup(s => s.GetAll()).Returns(videos);

        var result = _videoService.GetUnprocessedVideosAsCsv();

        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void GetUnprocessedVideosAsCsv_WhenCall_ReturnIdsWasSplitWithComa()
    {
        _fakeVideoRepository.Setup(s => s.GetAll()).Returns(new List<Video>
        {
            new() { Id = 2, IsProcessed = true },
            new() { Id = 4, IsProcessed = false },
            new() { Id = 8, IsProcessed = false }
        });

        var result = _videoService.GetUnprocessedVideosAsCsv();
        
        Assert.That(result, Does.Contain("4"));
        Assert.That(result, Does.Contain("8"));
    }
}