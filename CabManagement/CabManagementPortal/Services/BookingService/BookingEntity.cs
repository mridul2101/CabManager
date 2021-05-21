using CabManagementPortal.Services.LocationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace CabManagementPortal.Services.BookingService
{
	public class BookingEntity
	{
		public int Id { get; }
		public int CabId { get; }
		public City StartCity { get; }
		public City DestinationCity { get; }
		public DateTime StartTime { get; }
		public DateTime EndTime { get; set; }
		public bool IsCompleted { get; set; }
		public BookingEntity(int id, int cabId, City startCity, City destCity)
		{
			this.Id = id;
			this.CabId = cabId;
			this.StartCity = startCity;
			this.DestinationCity = destCity;
			this.StartTime = DateTime.UtcNow;
		}

	}
}
