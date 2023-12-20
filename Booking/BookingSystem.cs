using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking
{
  public class BookingSystem : IBookingSystem
  {
    private readonly IBookingDB _bookingDB;
    private readonly INotificationService _notificationService;
    private readonly PriceService _priceService;

    public BookingSystem(IBookingDB bookingDB, INotificationService notificationService, PriceService priceService)
    {
      _bookingDB = bookingDB;
      _notificationService = notificationService;
      _priceService = priceService;
    }

    public Reservation Book(int apartmentId, DateOnly startDate, DateOnly endDate, string userEmail)
    {
      var reservation = _bookingDB.CreateReservation(apartmentId, startDate, endDate, userEmail);
      _notificationService.SendConfirmationEmail(userEmail, reservation);

      return reservation;
    }

    public IEnumerable<Reservation> GetReservations(DateOnly startDate, DateOnly endDate)
    {
      return _bookingDB.GetReservations(startDate, endDate);
    }

    public IEnumerable<AvailableBooking> SearchAvailableBookings(ApartmentType type, DateOnly startDate, DateOnly endDate, string userEmail)
    {
      var apartments = _bookingDB.SearchAvailableApartments(type, startDate, endDate)
        .Select(a => new AvailableApartment { Apartment = a, StartDate = startDate, EndDate = endDate})
        .ToList();

      if (!apartments.Any())
      {
        apartments = GetCloserAvailableApartments(type, startDate, endDate);
      }

      var bookings = CreateBookings(apartments, userEmail);

      return bookings;
    }

    private IEnumerable<AvailableBooking> CreateBookings(IEnumerable<AvailableApartment> availableApartments, string userEmail)
    {
      var bookings = new List<AvailableBooking>();
      foreach(var availableApartment in availableApartments)
      {
        var totalPrice = _priceService.CalculatePrice(availableApartment.Apartment.BasePrice, userEmail, availableApartment.StartDate, availableApartment.EndDate);

        bookings.Add(new AvailableBooking
        {
          ApartmentId = availableApartment.Apartment.Id,
          ApartmentType = availableApartment.Apartment.Type,
          StartDate = availableApartment.StartDate,
          EndDate = availableApartment.EndDate,
          TotalPrice = totalPrice
        });
      }

      return bookings;
    }

    private List<AvailableApartment> GetCloserAvailableApartments(ApartmentType type, DateOnly startDate, DateOnly endDate)
    {
      var found = false;
      var availables = new List<Apartment>();

      var searchStartDate = endDate;
      var searchEndDate = endDate.AddDays(endDate.DayNumber - startDate.DayNumber);

      do
      {

        availables = _bookingDB.SearchAvailableApartments(type, searchStartDate, searchEndDate).ToList();

        found = availables.Any();

        if(!found)
        {
          var delta = searchEndDate.DayNumber - searchStartDate.DayNumber;
          searchStartDate = searchEndDate;
          searchEndDate = searchEndDate.AddDays(delta);
        }

      } while (!found);

      return availables.Select(a => new AvailableApartment { Apartment = a, StartDate = searchStartDate, EndDate = searchEndDate }).ToList() ;
    }

    private class AvailableApartment
    {
      public Apartment Apartment { get; init; }
      public DateOnly StartDate { get; init; }
      public DateOnly EndDate { get; init; }
    }
  }
}
