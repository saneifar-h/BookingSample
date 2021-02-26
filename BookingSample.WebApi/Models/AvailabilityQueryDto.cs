using System;

namespace BookingSample.WebApi.Models
{
    public class AvailabilityQueryDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}