namespace Booking
{
  public class LoyaltyDiscount : IDiscounts
  {
    private readonly IBookingDB _bookingDB;
    
    public LoyaltyDiscount(IBookingDB bookingDB)
    {
      _bookingDB = bookingDB;
    }

    public decimal Apply(decimal price, DateOnly startDate, DateOnly endDate, string userEmail)
    {
      var reservations = _bookingDB.GetUserReservations(userEmail, startDate.AddYears(-1), startDate);
      if (reservations.Any())
        return price * 0.9m;
      return price;
    }
  }
}