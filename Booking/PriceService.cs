namespace Booking
{
  public partial class PriceService
  {
    public List<IExtraChargeStrategy> _extraChargeStrategies;
    public List<IDiscounts> _discounts;

    public PriceService(List<IExtraChargeStrategy> extraChargeStrategies, List<IDiscounts> discounts)
    {
      _extraChargeStrategies = extraChargeStrategies;
      _discounts = discounts;
    }

    public decimal CalculatePrice(decimal basePrice, string userEmail, DateOnly startDate, DateOnly endDate)
    {
      var totalPrice = 0m;
      for (var date = startDate; date < endDate; date = date.AddDays(1))
      {
        totalPrice += ApplyExtraCharges(basePrice, date);
      }
      totalPrice = ApplyDiscounts(totalPrice, startDate, endDate, userEmail);
      return totalPrice;
    }

    private decimal ApplyDiscounts(decimal price, DateOnly startDate, DateOnly endDate, string userEmail)
    {
      var result = price;
      foreach (var discount in _discounts)
      {
        result = discount.Apply(result, startDate, endDate, userEmail);
      }
      return result;
    }

    private decimal ApplyExtraCharges(decimal basePrice, DateOnly bookingDate)
    {
      var result = basePrice;
      foreach(var strategy in _extraChargeStrategies)
      {
        result = strategy.ApplyExtraCharge(result, bookingDate);
      }
      return result;
    }
  }
}
