using System.Collections.Generic;

namespace BookingSample.AppService.BookingSrv.Dto
{
    public class RoomAvailableInfoDto
    {
        public int RoomId { get; set; }
        public int AvailablePlaces { get; set; }
    }

    public class AvailableInfoDto
    {
        public IEnumerable<RoomAvailableInfoDto> AvailableItems { get; set; }
        public int TotalItems { get; set; }
    }
}