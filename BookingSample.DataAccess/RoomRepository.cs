using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BookingSample.Domain;
using BookingSample.Domain.Entities;
using BookingSample.Domain.Repositories;
using Dapper;

namespace BookingSample.DataAccess
{
    public class RoomRepository : IRoomRepository
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public RoomRepository(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }

        public void Add(Room room)
        {
            using (var con = new SqlConnection(_connectionStringProvider.Provide()))
            {
                con.Execute("Room_I", room, commandType: CommandType.StoredProcedure);
            }
        }

        public Room Get(int roomId)
        {
            using (var con = new SqlConnection(_connectionStringProvider.Provide()))
            {
                return con.Query<Room>("Room_G", new {roomId}, commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
            }
        }

        public IEnumerable<Room> GetAll()
        {
            using (var con = new SqlConnection(_connectionStringProvider.Provide()))
            {
                return con.Query<Room>("Room_G_All", commandType: CommandType.StoredProcedure).ToList();
            }
        }
    }
}