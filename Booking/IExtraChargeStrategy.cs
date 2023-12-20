namespace Booking
{
  public interface IExtraChargeStrategy
  {
    decimal ApplyExtraCharge(decimal basePrice, DateOnly bookingDate);
  }
}
