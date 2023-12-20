namespace Booking
{
  public class AvailableBooking
  {
    public int ApartmentId { get; set; }
    public ApartmentType ApartmentType { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public decimal TotalPrice { get; set; }
  }
}