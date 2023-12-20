namespace Booking
{
  public interface INotificationService
  {
    void SendConfirmationEmail(string userEmail, object reservation);
  }
}