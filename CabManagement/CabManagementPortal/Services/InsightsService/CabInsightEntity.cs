using CabManagementPortal.Services.BookingService;
using CabManagementPortal.Services.CabMetadataService;
using System;
using System.Collections.Generic;

namespace CabManagementPortal.Services.InsightsService
{
	public class CabInsightEntity
	{
		public int CabId { get; }
		public CabEntity Cab { get; }
		public long IdleTime { get; private set; }
		public IList<BookingEntity> Bookings { get; }
		public CabInsightEntity(CabEntity cab)
		{
			this.CabId = cab.Id;
			this.IdleTime = 0;
			this.Cab = cab;
			this.Bookings = new List<BookingEntity>();
		}

		public void Add(BookingEntity entity)
		{
			TimeSpan timeDiff;
			if(this.Bookings.Count == 0)
			{
				timeDiff = entity.StartTime - Cab.RegisterTime;
			}
			else
			{
				timeDiff = entity.StartTime - Bookings[0].EndTime;
			}

			Bookings.Insert(0, entity);
			IdleTime += timeDiff.Milliseconds;
		}
	}
}
