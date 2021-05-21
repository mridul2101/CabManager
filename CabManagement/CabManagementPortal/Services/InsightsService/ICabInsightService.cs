using CabManagementPortal.Services.BookingService;
using CabManagementPortal.Services.LocationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace CabManagementPortal.Services.InsightsService
{
	internal interface ICabInsightService
	{
		IList<CabInsightEntity> GetCabInsights(List<int> cabId);
		void UpdateCabInsight(BookingEntity booking);

		long CabIdleTime(int cabId, DateTime start, DateTime end);
		IList<BookingEntity> GetCabHistory(int cabId);
		IList<City> GetCityByDemand();
		void UpdateCityDemandInsights(City city);
	}
}
