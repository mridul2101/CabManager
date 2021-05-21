using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CabManagementPortal.Services.CabMetadataService
{
	internal class CabMetadataService : ICabMetadataService
	{
		private int nextId = 0;
		private readonly IList<CabEntity> cabEntities;
		private readonly object writeLock = new object();

		public CabMetadataService()
		{
			cabEntities = new List<CabEntity>();
		}

		public CabEntity GetCab(int cabId)
		{
			try
			{
				return cabEntities.First(cab => cab.Id == cabId);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public IList<CabEntity> GetCabs()
		{
			try
			{
				return cabEntities;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public int Register(string number, string name, long phone, string license)
		{
			try
			{
				lock (writeLock)
				{
					Interlocked.Increment(ref nextId);
					cabEntities.Add(new CabEntity(nextId, number, name, phone, license));
				}
				return nextId;
			}
			catch (Exception ex)
			{
				
				throw;
			}
		}
	}
}
