using cinemaratona.Models;
using cinemaratona.Repositories;

namespace cinemaratona.Services;

public class FriendshipService(FriendshipRepository friendshipRepository)
{
    private readonly FriendshipRepository _friendshipRepository = friendshipRepository;

    public List<Friendship> List()
    {
        return _friendshipRepository.List();
    }

    public Friendship? Include(Friendship friendship_obj)
    {
        return _friendshipRepository.Include(friendship_obj);
    }

    public Friendship? Find(int id)
    {
        return _friendshipRepository.Find(id);
    }

    public Friendship? Delete(int id)
    {
        return _friendshipRepository.Delete(id);
    }

    public Friendship? Update(Friendship friendship_obj)
    {
        var existingFriendship = _friendshipRepository.Find(friendship_obj.Id);
        if (existingFriendship == null) return null;
        
        return _friendshipRepository.Update(friendship_obj);
    }
}