namespace Booking
{
  public class Reservation
  {
    public ApartmentType ApartmentType { get; set; }
    public long ApartmentId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string UserEmail { get; set; } = default!;
  }
}