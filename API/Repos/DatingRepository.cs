using API.Helper;
using API.Models;
using DatingApp.API.Helper;
using DatingApp.API.Helpers;

namespace API.Repos;

public class DatingRepository : IDatingRepository
{
    public void Add<T>(T entity) where T : class
    {
        throw new NotImplementedException();
    }

    public void Delete<T>(T entity) where T : class
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveAll()
    {
        throw new NotImplementedException();
    }

    public Task<PagedList<User>> GetUsers(UserParams userParams)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUser(int id, bool isCurrentUser)
    {
        throw new NotImplementedException();
    }

    public Task<Photo> GetPhoto(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Photo> GetMainPhotoForUser(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<Like> GetLike(int userId, int recipientId)
    {
        throw new NotImplementedException();
    }

    public Task<Message> GetMessage(int id)
    {
        throw new NotImplementedException();
    }

    public Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId)
    {
        throw new NotImplementedException();
    }
}