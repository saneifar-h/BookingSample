using System;
using System.Linq;
using BookingSample.AppService.BookingSrv.Dto;
using BookingSample.Domain.Entities;
using BookingSample.Domain.Repositories;
using BookingSample.Domain.Validators;

namespace BookingSample.AppService.BookingSrv
{
    public interface IBookingService
    {
        void Add(Booking booking);
        AvailableInfoDto GetRoomsAvailability(DateTime starTime, DateTime endTime, int pageNumber, int pageSize = 50);
    }

    public class BookingService : IBookingService
    {
        private readonly IAddBookingValidator _addBookingValidator;
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingService(IRoomRepository roomRepository, IBookingRepository bookingRepository,
            IAddBookingValidator addBookingValidator)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
            _addBookingValidator = addBookingValidator;
        }

        public void Add(Booking booking)
        {
            var validationResult = _addBookingValidator.Validate(booking);

            if (!validationResult.IsValid)
                throw new Exception(validationResult.ErrorString);
            _bookingRepository.Add(booking);
        }

        public AvailableInfoDto GetRoomsAvailability(DateTime starTime, DateTime endTime, int pageNumber,
            int pageSize = 50)
        {
            var allRooms = _roomRepository.GetAll().ToList();
            var allReservation = _bookingRepository.GetAllReservationBetweenDates(starTime, endTime).ToList();
            var lstRoomAvailableInfoDto = allRooms.GroupJoin(allReservation, room => room.Id, book => book.RoomId,
                (room, booking) =>
                {
                    var enumerable = booking.ToList();
                    return new RoomAvailableInfoDto
                    {
                        RoomId = room.Id,
                        AvailablePlaces = room.Places - (enumerable.Any() ? enumerable.Sum(i => i.UsedPlace) : 0)
                    };
                }).Where(i => i.AvailablePlaces > 0).ToList();

            var selected = lstRoomAvailableInfoDto.OrderBy(i => i.RoomId)
                .Skip(pageNumber).Skip(pageNumber * pageSize)
                .Take(pageSize).ToList();

            return new AvailableInfoDto
            {
                AvailableItems = selected, TotalItems = lstRoomAvailableInfoDto.Count,
                TotalPages = (int) Math.Ceiling((decimal)lstRoomAvailableInfoDto.Count / pageSize),
                CurrentPage = pageNumber
            };
        }
    }
}