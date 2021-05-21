using CabManagementPortal.Services.InsightsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CabManagementPortal.Services.BookingService.BookingStrategy
{
	internal abstract class BaseBookingStrategy : IBookingStrategy
	{
		public abstract Strategy Strategy { get; }

		public int GetCab(IList<int> availableCabs, IList<CabInsightEntity> cabInsights)
		{
			if(availableCabs.Count == 0)
			{
				throw new Exception("No Cab available");
			}

			if (cabInsights.Count == 0)
			{
				return availableCabs.First();
			}

			return GetCabInternal(availableCabs, cabInsights);


		}

		internal abstract int GetCabInternal(IList<int> availableCabs, IList<CabInsightEntity> cabInsights);
	}
	internal class MostIdleBookingStrategy : BaseBookingStrategy
	{
		public override Strategy Strategy => Strategy.MostIdle;

		internal override int GetCabInternal(IList<int> availableCabs, IList<CabInsightEntity> cabInsights)
		{
			CabInsightEntity mostIdleCab = cabInsights.First();
			foreach(var cab in cabInsights)
			{
				if(cab.IdleTime > mostIdleCab.IdleTime)
				{
					mostIdleCab = cab;
				}
			}
			return mostIdleCab.CabId;
		}
	}
}
