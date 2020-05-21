using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Data {
    public class ExampleDataProvider : SqlDataProvider {
		internal ExampleDataProvider(string connectionString) :
			
            base(connectionString) { }


		internal void UpdateRazerView(RazerView view) {
			base.ExecuteProc("[dbo].[UpdateRazerView]", new { View = view });
		}

		internal void CreateRazerView(RazerView view ){
			base.ExecuteProc<RazerView>("[dbo].[CreateRazerView]", view );
		}

		internal RazerView GetRazerView(int razerViewId) {
			return ExecuteProc<RazerView>("[dbo].[GetRazerView]", new { RazerViewId = razerViewId }).FirstOrDefault();
		}

		internal RazerView GetRazerView(string Location) {
			return ExecuteProc<RazerView>("[dbo].[GetRazerViewByLocation]", new { Location = Location }).FirstOrDefault();
		}
		internal RazerView GetRazerViewLikeLocation(string Location) {
			return ExecuteProc<RazerView>("[dbo].[GetRazerViewLikeLocation]", new { Location = Location }).FirstOrDefault();
		}



	}
}
