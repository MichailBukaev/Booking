namespace Booking
{
  public interface IDiscounts
  {
    decimal Apply(decimal price, DateOnly startDate, DateOnly endDate, string userEmail);
  }
}