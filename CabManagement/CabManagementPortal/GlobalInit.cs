using CabManagementPortal.Services.BookingService;
using CabManagementPortal.Services.BookingService.BookingStrategy;
using CabManagementPortal.Services.CabMetadataService;
using CabManagementPortal.Services.CabStateService;
using CabManagementPortal.Services.InsightsService;
using System;
using System.Collections.Generic;
using System.Text;

namespace CabManagementPortal
{
	public static class GlobalInit
	{
		internal static ICabMetadataService CabMetadataService = new CabMetadataService();
		internal static ICabStateService StateService = new CabStateService();
		internal static ICabInsightService CabInsightService = new CabInsightService();
		internal static ICabBookingService BookingService = new CabBookingService();

		internal static IDictionary<Strategy, IBookingStrategy> CabBookingStrategy = new Dictionary<Strategy, IBookingStrategy>()
		{
			{ Strategy.MostIdle, new MostIdleBookingStrategy() }
		};

		public static ICabManagementController CabManagementService = new CabManagementController();
	}
}
