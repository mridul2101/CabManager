using CabManagementPortal.Services.LocationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace CabManagementPortal.Services.InsightsService
{
	public class CabDemandEntity
	{
		public IList<DateTime> RequestTime { get; } 
		public int Count => RequestTime.Count;
		public City SourceCity { get; }
		public CabDemandEntity(City city)
		{
			this.SourceCity = city;
			this.RequestTime = new List<DateTime>() { DateTime.UtcNow };
		}
	}
}
