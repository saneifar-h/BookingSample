using System;
using System.Collections.Generic;
using BookingSample.Domain.Entities;

namespace BookingSample.Domain.Repositories
{
    public interface IBookingRepository
    {
        void Add(Booking booking);
        IEnumerable<Booking> GetAllReservationBetweenDates(DateTime starTime, DateTime endTime);
        IEnumerable<Booking> GetReservationForRoomBetweenDates(int roomId, DateTime starTime, DateTime endTime);
    }
}