using CabManagementPortal.Services.BookingService;
using CabManagementPortal.Services.CabStateService;
using CabManagementPortal.Services.LocationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace CabManagementPortal
{
	public interface ICabManagementController
	{
		int Register(string number, string name, long phone, string license);
		bool AddCabState(IList<CabStateEntity> stateEntities);
		bool AddCabState(CabStateEntity stateEntities);
		IList<CabStateEntity> GetCabStateEntity();

		bool UpdateLocation(int cabId, City city);

		BookingEntity BookCab(City startCity, City destCity);
		bool CompleteRide(int bookingId);
		IList<BookingEntity> GetCabHistory(int cabId);
		IList<City> GetCityByDemand();
	}
}
