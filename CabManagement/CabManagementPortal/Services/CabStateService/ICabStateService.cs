using CabManagementPortal.Services.LocationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace CabManagementPortal.Services.CabStateService
{
	internal interface ICabStateService
	{
		bool Add(CabStateEntity stateEntity);
		bool Add(IList<CabStateEntity> stateEntities);
		bool UpdateLocation(int cabId, City city);
		bool UpdateState(int cabId);
		
		CabStateEntity GetCabStateEntity(int cabId);
		IList<CabStateEntity> GetCabStateEntity();
		IList<CabStateEntity> GetAvailableCabs(City city);
	}
}
