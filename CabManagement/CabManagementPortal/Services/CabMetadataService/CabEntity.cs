using System;
using System.Collections.Generic;
using System.Text;

namespace CabManagementPortal.Services.CabMetadataService
{
	public class CabEntity
	{
		public int Id { get; }
		public string CabNumber { get; }
		public string DriveName { get; }
		public long PhoneNumber { get; } 
		public string License { get; }
		public DateTime RegisterTime { get; }
		internal CabEntity(int id, string number, string name, long phone, string license)
		{
			this.Id = id;
			this.CabNumber = number;
			this.DriveName = name;
			this.PhoneNumber = phone;
			this.License = license;
			this.RegisterTime = DateTime.UtcNow;
		}
		public CabEntity(string number, string name, long phone, string license)
		{
			this.CabNumber = number;
			this.DriveName = name;
			this.PhoneNumber = phone;
			this.License = license;
		}
	}
}
