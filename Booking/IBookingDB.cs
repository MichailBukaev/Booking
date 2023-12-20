namespace Booking
{
  public interface IBookingDB
  {
    Reservation CreateReservation(int apartmentId, DateOnly startDate, DateOnly endDate, string userEmail);
    IEnumerable<Reservation> GetReservations(DateOnly startDate, DateOnly endDate);
    IEnumerable<Apartment> SearchAvailableApartments(ApartmentType type, DateOnly startDate, DateOnly endDate);
    IEnumerable<Reservation> GetUserReservations(string userEmail, DateOnly startDate, DateOnly endDate);
  }
}