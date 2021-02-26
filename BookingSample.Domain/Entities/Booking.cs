using System;

namespace BookingSample.Domain.Entities
{
    public class Booking
    {
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UsedPlace { get; set; }
        public Gender Gender { get; set; }
    }
}