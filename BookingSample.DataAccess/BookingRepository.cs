using System;
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
    public class BookingRepository : IBookingRepository
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public BookingRepository(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }

        public void Add(Booking booking)
        {
            using (var con = new SqlConnection(_connectionStringProvider.Provide()))
            {
                con.Execute("Booking_I", booking, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Booking> GetAllReservationBetweenDates(DateTime starTime, DateTime endTime)
        {
            using (var con = new SqlConnection(_connectionStringProvider.Provide()))
            {
                return con.Query<Booking>("Booking_G_AllReservationsBetweenDates",
                    new {StartDate = starTime, EndDate = endTime},
                    commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public IEnumerable<Booking> GetReservationForRoomBetweenDates(int roomId, DateTime starTime, DateTime endTime)
        {
            using (var con = new SqlConnection(_connectionStringProvider.Provide()))
            {
                return con.Query<Booking>("Booking_G_ReservationForRoomBetweenDates",
                    new {RoomId = roomId, StartDate = starTime, EndDate = endTime},
                    commandType: CommandType.StoredProcedure).ToList();
            }
        }
    }
}