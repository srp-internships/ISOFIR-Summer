using TestNinja.Mocking;

namespace TestNinja.Repositories.Interfaces;

public interface IVideoRepository
{
    IEnumerable<Video> GetAll();
}