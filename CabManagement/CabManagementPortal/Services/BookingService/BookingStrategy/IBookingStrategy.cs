using CabManagementPortal.Services.InsightsService;
using System;
using System.Collections.Generic;
using System.Text;

namespace CabManagementPortal.Services.BookingService.BookingStrategy
{
	internal interface IBookingStrategy
	{
		Strategy Strategy { get; }
		int GetCab(IList<int> availableCabs, IList<CabInsightEntity> cabInsights);
	}

	public enum Strategy
	{
		MostIdle,
		MostBooking,
		MostStar
	}
}
