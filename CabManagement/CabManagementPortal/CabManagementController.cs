using CabManagementPortal.Services.BookingService;
using CabManagementPortal.Services.CabStateService;
using CabManagementPortal.Services.LocationService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http;

namespace CabManagementPortal
{
	public class CabManagementController : ICabManagementController
	{
		public int Register(string number, string name, long phone, string license)
		{
			return GlobalInit.CabMetadataService.Register(number, name, phone, license);
		}

		public bool AddCabState(IList<CabStateEntity> stateEntities)
		{
			return GlobalInit.StateService.Add(stateEntities);
		}
		public bool AddCabState(CabStateEntity stateEntity)
		{
			return GlobalInit.StateService.Add(stateEntity);
		}
		public IList<CabStateEntity> GetCabStateEntity()
		{
			return GlobalInit.StateService.GetCabStateEntity();
		}

		public BookingEntity BookCab(City startCity, City destCity)
		{
			return GlobalInit.BookingService.BookCab(startCity, destCity);
		}

		public bool CompleteRide(int bookingId)
		{
			return GlobalInit.BookingService.CompleteRide(bookingId);
		}

		public bool UpdateLocation(int cabId, City city)
		{
			return GlobalInit.StateService.UpdateLocation(cabId, city);
		}

		public IList<BookingEntity> GetCabHistory(int cabId)
		{
			return GlobalInit.CabInsightService.GetCabHistory(cabId);
		}

		public IList<City> GetCityByDemand()
		{
			return GlobalInit.CabInsightService.GetCityByDemand();
		}
	}
}
