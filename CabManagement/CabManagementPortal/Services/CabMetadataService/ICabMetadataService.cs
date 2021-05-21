using System;
using System.Collections.Generic;
using System.Text;

namespace CabManagementPortal.Services.CabMetadataService
{
	internal interface ICabMetadataService
	{
		int Register(string number, string name, long phone, string license);
		CabEntity GetCab(int cabId);
		IList<CabEntity> GetCabs();
	}
}
