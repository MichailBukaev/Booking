using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking
{
  public interface IBookingSystem
  {
    IEnumerable<Reservation> GetReservations(DateOnly startDate, DateOnly endDate);

    IEnumerable<AvailableBooking> SearchAvailableBookings(ApartmentType type, DateOnly startDate, DateOnly endDate, string userEmail);

    Reservation Book(int apartmentId, DateOnly startDate, DateOnly endDate, string userEmail);
  }
}
