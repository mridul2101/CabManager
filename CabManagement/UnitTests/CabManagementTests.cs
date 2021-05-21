using CabManagementPortal;
using CabManagementPortal.Services.CabMetadataService;
using CabManagementPortal.Services.CabStateService;
using CabManagementPortal.Services.LocationService;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CabManagementTests
{
	public class CabEntityDemo
	{
		public int CabId;
		public CabEntity Entity;
		public City City;
	}

	public class CabManagementTests
	{
		private ICabManagementController cabManagerService = GlobalInit.CabManagementService;

		private List<CabEntityDemo> cabEntities = new List<CabEntityDemo>()
		{
			new CabEntityDemo(){Entity =  new CabEntity("KA201451", "Ram Kumar", 123456789, "DL1282"), City = City.Bangalore },
			new CabEntityDemo(){Entity =  new CabEntity("AP201351", "Laxman Kumar", 126756789, "DL1342"), City = City.Hyderabad },
			new CabEntityDemo(){Entity =  new CabEntity("TS207251", "Bharath Kumar", 126756789, "DL16522"), City = City.Mysore },
			new CabEntityDemo(){Entity =  new CabEntity("MH283751", "Shat Kumar", 1267582689, "DL167267"), City = City.Mumbai },
			new CabEntityDemo(){Entity =  new CabEntity("MH283851", "Satish Kumar", 9367582689, "DL967267"), City = City.Mumbai },
		};


		[Test]
		public void CabManagerServceTest()
		{
			int id = 1;
			foreach (var cab in cabEntities)
			{
				cab.CabId = cabManagerService.Register(cab.Entity.CabNumber, cab.Entity.DriveName, cab.Entity.PhoneNumber, cab.Entity.License);
				cabManagerService.AddCabState(new CabStateEntity(cab.CabId, cab.City));
			}

			//1
			Assert.Throws<Exception>(() => cabManagerService.BookCab(City.Pune, City.Mumbai));
			Print(id++, cabManagerService.GetCabStateEntity());

			//2
			var booking1 = cabManagerService.BookCab(City.Mumbai, City.Pune);
			Print(id++, cabManagerService.GetCabStateEntity());

			//3
			var booking2 = cabManagerService.BookCab(City.Bangalore, City.Mysore);
			Print(id++, cabManagerService.GetCabStateEntity());

			//3
			var booking2_1 = cabManagerService.BookCab(City.Mysore, City.Bangalore);
			Print(id++, cabManagerService.GetCabStateEntity());

			//4
			var booking3 = cabManagerService.BookCab(City.Hyderabad, City.Mumbai);
			Print(id++, cabManagerService.GetCabStateEntity());

			//5
			Assert.Throws<Exception>(() => cabManagerService.BookCab(City.Hyderabad, City.Mumbai));
			Print(id++, cabManagerService.GetCabStateEntity());

			//6
			cabManagerService.CompleteRide(booking2.Id);
			Print(id++, cabManagerService.GetCabStateEntity());

			//7
			var booking4 = cabManagerService.BookCab(City.Mysore, City.Pune);
			Print(id++, cabManagerService.GetCabStateEntity());

			//8
			cabManagerService.CompleteRide(booking1.Id);
			Print(id++, cabManagerService.GetCabStateEntity());

			//9
			var booking5 = cabManagerService.BookCab(City.Pune, City.Mysore);
			Print(id++, cabManagerService.GetCabStateEntity());

			//10
			cabManagerService.CompleteRide(booking3.Id);
			Print(id++, cabManagerService.GetCabStateEntity());
			
			//11
			cabManagerService.CompleteRide(booking4.Id);
			Print(id++, cabManagerService.GetCabStateEntity());

			//12
			cabManagerService.CompleteRide(booking5.Id);
			Print(id++, cabManagerService.GetCabStateEntity());

			//13
			var booking6 = cabManagerService.BookCab(City.Mysore, City.Bangalore);
			Print(id++, cabManagerService.GetCabStateEntity());

			//14
			Assert.Throws<Exception>(() => cabManagerService.CompleteRide(booking5.Id));

			foreach (var cab in cabEntities)
			{
				var bookings = cabManagerService.GetCabHistory(cab.CabId);
				System.Diagnostics.Debug.WriteLine($"CabId: {cab.CabId}");

				foreach (var bk in bookings)
				{
					System.Diagnostics.Debug.WriteLine($"{bk.Id} {bk.StartCity} {bk.DestinationCity} {bk.IsCompleted}");
				}
			}
			Assert.Pass();
		}

		private void Print(int id, IList<CabStateEntity> entities)
		{
			System.Diagnostics.Debug.WriteLine($"Case {id}");
			foreach (var entity in entities )
			{
				System.Diagnostics.Debug.WriteLine($"{entity.CabId} {entity.State} {entity.CityId}");
			}

			var cityByDemand = string.Join(',', cabManagerService.GetCityByDemand());
			System.Diagnostics.Debug.WriteLine($"City By Demand : {cityByDemand}");

			

			System.Diagnostics.Debug.WriteLine("");
		}
	}
}