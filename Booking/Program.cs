// See https://aka.ms/new-console-template for more information
using Booking;

Console.WriteLine("Hello, World!");
var db = new BookingDB();
var charges = new List<IExtraChargeStrategy>{
  new SeasonalExtraChargeStrategy()
};
var discounts = new List<IDiscounts>
{
  new LoyaltyDiscount(db)
};

IBookingSystem system = new BookingSystem(db, new NotificationService(), new PriceService(charges, discounts));

var availableApartments = system.SearchAvailableBookings
  (ApartmentType.OneBedroom, DateOnly.FromDateTime(DateTime.Parse("2024-04-29 00:00:00")), 
  DateOnly.FromDateTime(DateTime.Parse("2024-05-05 00:00:00")), "asd").ToList();

int num = availableApartments.Count();
var price = availableApartments.First().TotalPrice;

system.Book(availableApartments[0].ApartmentId, 
  DateOnly.FromDateTime(DateTime.Parse("2024-04-29 00:00:00")), 
  DateOnly.FromDateTime(DateTime.Parse("2024-05-05 00:00:00")), "asd");
system.Book(availableApartments[0].ApartmentId,
  DateOnly.FromDateTime(DateTime.Parse("2024-04-29 00:00:00")),
  DateOnly.FromDateTime(DateTime.Parse("2024-05-05 00:00:00")), "asd");
system.Book(availableApartments[2].ApartmentId,
  DateOnly.FromDateTime(DateTime.Parse("2024-04-29 00:00:00")),
  DateOnly.FromDateTime(DateTime.Parse("2024-05-05 00:00:00")), "asd");
system.Book(availableApartments[3].ApartmentId,
  DateOnly.FromDateTime(DateTime.Parse("2024-04-29 00:00:00")),
  DateOnly.FromDateTime(DateTime.Parse("2024-05-05 00:00:00")), "asd");


var availableApartments2 = system.SearchAvailableBookings
  (ApartmentType.OneBedroom, DateOnly.FromDateTime(DateTime.Parse("2024-04-29 00:00:00")),
  DateOnly.FromDateTime(DateTime.Parse("2024-05-05 00:00:00")), "asd");


int num2 = availableApartments2.Count();
var price2 = availableApartments2.First().TotalPrice;




Console.ReadLine();

