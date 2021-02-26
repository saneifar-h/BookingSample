using System.Collections.Generic;
using BookingSample.Domain.Entities;

namespace BookingSample.Domain.Repositories
{
    public interface IRoomRepository
    {
        void Add(Room room);
        Room Get(int roomId);
        IEnumerable<Room> GetAll();
    }
}