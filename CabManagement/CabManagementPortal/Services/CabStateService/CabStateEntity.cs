using CabManagementPortal.Services.LocationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace CabManagementPortal.Services.CabStateService
{
	public class CabStateEntity
	{
		public int CabId { get; }
		public CabState State {get;set;}
		public City CityId { get; set; }
		public CabStateEntity(int cabId, City city, CabState cabState = CabState.IDLE)
		{
			this.CabId = cabId;
			this.State = cabState;
			this.CityId = city;
		}
	}

	public enum CabState
	{
		IDLE,
		ON_TRIP
	}
}
