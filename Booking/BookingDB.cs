namespace Booking
{
  public class BookingDB : IBookingDB
  {
    private static List<Reservation> _reservations = new List<Reservation>();
    private static List<Apartment> _hotelInventory = new List<Apartment>
    {
      new Apartment
      {
        Id = 1,
        Type = ApartmentType.OneBedroom,
        BasePrice = 50,
      },
      new Apartment
      {
        Id = 2,
        Type = ApartmentType.OneBedroom,
        BasePrice = 50,
      },
      new Apartment
      {
        Id = 3,
        Type = ApartmentType.OneBedroom,
        BasePrice = 50,
      },
      new Apartment
      {
        Id = 4,
        Type = ApartmentType.OneBedroom,
        BasePrice = 50,
      }
      ,new Apartment
      {
        Id = 5,
        Type = ApartmentType.TwoBedroom,
        BasePrice = 60,
      }
      ,new Apartment
      {
        Id = 6,
        Type = ApartmentType.TwoBedroom,
        BasePrice = 60,
      }
      ,new Apartment
      {
        Id = 7,
        Type = ApartmentType.TwoBedroom,
        BasePrice = 60,
      }
      ,new Apartment
      {
        Id = 8,
        Type = ApartmentType.ThreeBedroom,
        BasePrice = 70,
      }
      ,new Apartment
      {
        Id = 9,
        Type = ApartmentType.ThreeBedroom,
        BasePrice = 70,
      }
      ,new Apartment
      {
        Id = 10,
        Type = ApartmentType.ThreeBedroom,
        BasePrice = 70,
      }
    };

    public Reservation CreateReservation(int apartmentId, DateOnly startDate, DateOnly endDate, string userEmail)
    {
      CheckAvailability(apartmentId, startDate, endDate);
      var apartmentType = _hotelInventory.Single(a => a.Id == apartmentId).Type;
      var reservation = new Reservation
      {
        ApartmentId = apartmentId,
        ApartmentType = apartmentType,
        StartDate = startDate,
        EndDate = endDate,
        UserEmail = userEmail,
      };

      _reservations.Add(reservation);

      return reservation;
    }

    private void CheckAvailability(int apartmentId, DateOnly startDate, DateOnly endDate)
    {
      var foundReservations = _reservations.Where(r => InPeriod(r, startDate, endDate) && r.ApartmentId == apartmentId).ToList();
      
      if(foundReservations.Any())
        throw new Exception("No available apartments");
    }

    public IEnumerable<Reservation> GetReservations(DateOnly startDate, DateOnly endDate)
    {
      return _reservations.Where(r => InPeriod(r, startDate, endDate)).ToList();
    }

    public IEnumerable<Reservation> GetUserReservations(string userEmail, DateOnly startDate, DateOnly endDate)
    {
      return GetReservations(startDate, endDate)
        .Where(r => r.UserEmail == userEmail)
        .ToList();
    }

    public IEnumerable<Apartment> SearchAvailableApartments(ApartmentType type, DateOnly startDate, DateOnly endDate)
    {
      var foundReservations = _reservations.Where(r => InPeriod(r, startDate, endDate));
      foundReservations = foundReservations.Where(r => r.ApartmentType == type);
      var foundIds = foundReservations.Select(r => r.ApartmentId).ToList();

      var apartmentsOfType = _hotelInventory.Where(a => a.Type == type);
      var result = apartmentsOfType.Where(a => !foundIds.Contains(a.Id));
      
      return result.ToList();
    }

    private bool InPeriod(Reservation reservation, DateOnly startDate, DateOnly endDate)
    {
      return 
        (reservation.StartDate >= startDate && reservation.StartDate < endDate) 
        || (reservation.EndDate > startDate && reservation.EndDate <= endDate);
    }
  }
}