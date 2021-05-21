using CabManagementPortal.Services.LocationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CabManagementPortal.Services.CabStateService
{
	public class CabStateService : ICabStateService
	{
		private readonly IList<CabStateEntity> stateEntities;
		private readonly object writeLock = new object();

		private int stateCount;

		public CabStateService()
		{
			stateEntities = new List<CabStateEntity>();
			this.stateCount = Enum.GetNames(typeof(CabState)).Length; ;
		}

		public bool Add(CabStateEntity stateEntity)
		{
			try
			{
				lock (writeLock)
				{
					stateEntities.Add(stateEntity);
				}
				return true;

			}
			catch (Exception)
			{

				throw;
			}
		}

		public bool Add(IList<CabStateEntity> stateEntities)
		{
			foreach(var entity in stateEntities)
			{
				Add(entity);
			}
			return true;
		}

		public IList<CabStateEntity> GetAvailableCabs(City city)
		{
			try
			{
				return stateEntities.Where(cab => (cab.CityId == city && cab.State == CabState.IDLE)).ToList();
			}
			catch (Exception)
			{

				throw;
			}
		}

		public CabStateEntity GetCabStateEntity(int cabId)
		{
			try
			{
				return stateEntities.First(state => state.CabId == cabId);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public IList<CabStateEntity> GetCabStateEntity()
		{
			try
			{
				return stateEntities;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public bool UpdateLocation(int cabId, City city)
		{
			try
			{
				var stateEntity = this.GetCabStateEntity(cabId);
				lock (stateEntity)
				{
					stateEntity.CityId = city;
					return true; 
				}
			}
			catch (Exception)
			{

				throw;
			}
		}

		public bool UpdateState(int cabId)
		{
			try
			{
				var stateEntity = this.GetCabStateEntity(cabId);
				lock (stateEntity)
				{
					stateEntity.State = (CabState)(((int)stateEntity.State + 1) % this.stateCount);
					return true;
				}
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
