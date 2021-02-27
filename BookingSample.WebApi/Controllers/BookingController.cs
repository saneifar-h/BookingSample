using System;
using System.Web.Http;
using System.Web.Http.Cors;
using BookingSample.AppService.BookingSrv;
using BookingSample.Domain;
using BookingSample.Domain.Entities;
using BookingSample.WebApi.Models;
using BookingSample.WebApi.Models.AuthSrv;

namespace BookingSample.WebApi.Controllers
{
    [RoutePrefix("api/booking")]
    [EnableCors("*", "*", "*")]
    public class BookingController : BaseApiController
    {
        private readonly IAuthService _authService;
        private readonly IBookingService _bookingService;
        private readonly ILogAdapter _logAdapter;

        public BookingController(ILogAdapter logAdapter, IBookingService bookingService, IAuthService authService)
        {
            _logAdapter = logAdapter;
            _bookingService = bookingService;
            _authService = authService;
        }


        [Route("Get")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                return GetAvailabilityOfRooms(new AvailabilityQueryDto());
            }
            catch (Exception ex)
            {
                _logAdapter.Error(ex);
                return CreateErrorResponseFromException(ex);
            }
        }


        [Route("GetAvailabilityOfRooms")]
        [HttpGet]
        public IHttpActionResult GetAvailabilityOfRooms(AvailabilityQueryDto availabilityQueryDto)
        {
            try
            {
                var availableInfo =
                    _bookingService.GetRoomsAvailability(availabilityQueryDto.StartTime ?? DateTime.Now.Date, availabilityQueryDto.EndTime ?? DateTime.Now.AddDays(10).Date,
                        availabilityQueryDto.PageNumber ?? 0, availabilityQueryDto.PageSize ?? 50);
                return Ok(availableInfo);
            }
            catch (Exception ex)
            {
                _logAdapter.Error(ex);
                return CreateErrorResponseFromException(ex);
            }
        }

        [Route("Add")]
        [HttpPost]
        public IHttpActionResult Add(Booking booking)
        {
            try
            {
                _bookingService.Add(booking);
                return Ok();
            }
            catch (Exception ex)
            {
                _logAdapter.Error(ex);
                return CreateErrorResponseFromException(ex);
            }
        }
    }
}