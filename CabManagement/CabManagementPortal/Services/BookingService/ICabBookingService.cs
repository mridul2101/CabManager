using CabManagementPortal.Services.BookingService.BookingStrategy;
using CabManagementPortal.Services.LocationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace CabManagementPortal.Services.BookingService
{
	internal interface ICabBookingService
	{
		BookingEntity BookCab(City sourceCity, City destCity, Strategy strategy = Strategy.MostIdle);
		bool CompleteRide(int bookingId);

	}
}
