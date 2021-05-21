using CabManagementPortal.Services.BookingService.BookingStrategy;
using CabManagementPortal.Services.CabStateService;
using CabManagementPortal.Services.InsightsService;
using CabManagementPortal.Services.LocationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CabManagementPortal.Services.BookingService
{
	public class CabBookingService : ICabBookingService
	{
		private int nextId = 0;
		private readonly IList<BookingEntity> bookingEntities;
		private readonly object writeLock = new object();

		private readonly ICabStateService StateService;
		private readonly ICabInsightService insightService;

		public CabBookingService()
		{
			this.bookingEntities = new List<BookingEntity>();
			this.insightService = GlobalInit.CabInsightService;
			this.StateService = GlobalInit.StateService;
		}

		public BookingEntity BookCab(City startCity, City destCity, Strategy strategy = Strategy.MostIdle)
		{
			try
			{
				var availableCabs = this.StateService.GetAvailableCabs(startCity);
				var cabIds = availableCabs.Select(x => x.CabId).ToList();
				var insights = this.insightService.GetCabInsights(cabIds);
				this.insightService.UpdateCityDemandInsights(startCity);

				int cabId = GlobalInit.CabBookingStrategy[strategy].GetCab(cabIds, insights);
				var cab = availableCabs.First(x => x.CabId == cabId);

				BookingEntity booking = null;
				lock (writeLock)
				{
					Interlocked.Increment(ref nextId);
					booking = new BookingEntity(nextId, cabId, startCity, destCity);
					bookingEntities.Add(booking);
					this.StateService.UpdateState(cabId);
					this.insightService.UpdateCabInsight(booking);
				}
				return booking;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public bool CompleteRide(int bookingId)
		{
			try
			{
				var booking = bookingEntities.First(x => x.Id == bookingId);
				if (booking.IsCompleted)
				{
					throw new Exception("Already completed");
				}

				lock (writeLock)
				{
					booking.EndTime = DateTime.UtcNow;
					this.StateService.UpdateState(booking.CabId);
					this.StateService.UpdateLocation(booking.CabId, booking.DestinationCity);
				
					booking.IsCompleted = true;
				}
				return true;
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
