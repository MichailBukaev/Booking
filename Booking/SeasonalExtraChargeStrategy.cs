namespace Booking
{
  public class SeasonalExtraChargeStrategy : IExtraChargeStrategy
  {
    public decimal ApplyExtraCharge(decimal basePrice, DateOnly bookingDate)
    {
      if (bookingDate.Month >= 10 || bookingDate.Month <= 4)
        return basePrice * 1.1m;
      else if (bookingDate.Month >= 5 && bookingDate.Month <= 9)
        return basePrice * 1.2m;
      else
        return basePrice;
    }
  }
}
