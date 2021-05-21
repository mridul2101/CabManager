using CabManagementPortal.Services.BookingService;
using CabManagementPortal.Services.CabMetadataService;
using CabManagementPortal.Services.LocationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CabManagementPortal.Services.InsightsService
{
	internal class CabInsightService : ICabInsightService
	{
		private Dictionary<int, CabInsightEntity> Insights;
		private IDictionary<City, CabDemandEntity> CabDemandInsights;
		private readonly object writeLock = new object();

		private readonly ICabMetadataService CabMetadataService;
		public CabInsightService()
		{
			this.CabMetadataService = GlobalInit.CabMetadataService;
			this.Insights = new Dictionary<int, CabInsightEntity>();
			this.CabDemandInsights = new Dictionary<City, CabDemandEntity>();
		}
		public IList<CabInsightEntity> GetCabInsights(List<int> cabId)
		{
			try
			{
				return this.Insights.Where(x => cabId.Contains(x.Key)).Select(x => x.Value).ToList();
			}
			catch (Exception)
			{

				throw;
			}
		}

		public void UpdateCabInsight(BookingEntity booking)
		{
			try
			{
				if (!this.Insights.TryGetValue(booking.CabId, out CabInsightEntity entity))
				{
					entity = new CabInsightEntity(this.CabMetadataService.GetCab(booking.CabId));
					this.Insights.Add(booking.CabId, entity);
				}
				entity.Add(booking);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public long CabIdleTime(int cabId, DateTime start, DateTime end)
		{
			TimeSpan timeDiff;
			if (this.Insights.TryGetValue(cabId, out CabInsightEntity entity))
			{
				return entity.IdleTime;
			}
			else
			{
				var cabMetadata = this.CabMetadataService.GetCab(cabId);
				timeDiff = (DateTime.UtcNow - cabMetadata.RegisterTime);
				return timeDiff.Milliseconds;
			}
		}

		public IList<BookingEntity> GetCabHistory(int cabId)
		{
			if (this.Insights.TryGetValue(cabId, out CabInsightEntity entity))
			{
				return entity.Bookings;
			}
			return new List<BookingEntity>();
		}

		public IList<City> GetCityByDemand()
		{
			return this.CabDemandInsights.OrderByDescending(x => x.Value.Count).Select(x => x.Key).ToList();
		}

		public void UpdateCityDemandInsights(City city)
		{
			if(this.CabDemandInsights.TryGetValue(city, out CabDemandEntity insight))
			{
				insight.RequestTime.Add(DateTime.UtcNow);
			}
			else
			{
				this.CabDemandInsights.Add(city, new CabDemandEntity(city));
			}
		}
	}
}
