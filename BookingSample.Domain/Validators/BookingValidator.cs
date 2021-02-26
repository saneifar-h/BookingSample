using System;
using System.Collections.Generic;
using System.Linq;
using BookingSample.Domain.Entities;
using BookingSample.Domain.Repositories;

namespace BookingSample.Domain.Validators
{
    public interface IAddBookingValidator : IAddValidator<Booking>
    {
    }

    public class AddBookingValidator : IAddBookingValidator
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;

        public AddBookingValidator(IBookingRepository bookingRepository, IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
        }

        public IValidationResult Validate(Booking booking)
        {
            var result = new ValidationResult();
            var reservedBookings =
                _bookingRepository.GetReservationForRoomBetweenDates(booking.RoomId, booking.StartDate,
                    booking.EndDate).ToList();
            result.Add(ValidateDateRange(booking, reservedBookings));
            result.Add(ValidateGender(booking, reservedBookings));
            result.Add(ValidateDatesLessThanYear(booking));
            return result;
        }

        private string ValidateDatesLessThanYear(Booking booking)
        {
            if (booking.StartDate > DateTime.Now.AddYears(1).Date || booking.EndDate > DateTime.Now.AddYears(1).Date)
                return "StartDate and EndDate Must be less than a year";
            if (booking.StartDate < DateTime.Now.Date || booking.EndDate < DateTime.Now.Date)
                return "StartDate and EndDate Must be Greater than a current day";
            return booking.StartDate.Date <= booking.EndDate.Date ? "EndDate Must be Greater than StartDate" : "";
        }

        private string ValidateDateRange(Booking booking, IReadOnlyList<Booking> reservedBookings)
        {
            var room = _roomRepository.Get(booking.RoomId);
            var sumOfPlacesReserved = reservedBookings.Any() ? reservedBookings.Sum(i => i.UsedPlace) : 0;
            var availablePlaces = room.Places - sumOfPlacesReserved;
            return booking.UsedPlace > availablePlaces
                ? $"There is no place to book for this room because needed amount is:{booking.UsedPlace} while available place amount is:{availablePlaces}"
                : string.Empty;
        }

        private string ValidateGender(Booking booking, IReadOnlyList<Booking> reservedBookings)
        {
            return reservedBookings.Any(i => i.Gender != booking.Gender)
                ? "There is no place to book for this room because your gender dose not match other roomates gender"
                : string.Empty;
        }
    }
}