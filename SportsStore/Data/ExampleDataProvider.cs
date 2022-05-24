using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Reflection;
using Reflection.Services;
using Reflection.TutorCom.Core.Data;

namespace SportsStore.Data {
    public class CMService : TransactionalService, ICMSService
    {
        public CMService(ISqlDataProvider dataProvider) : base(dataProvider)
        {
        }
    }
    public class ExampleDataProvider : SqlDataProvider, ICMSService {
		public ExampleDataProvider(string connectionString) :
            base(connectionString) { }


		internal void UpdateRazerView(RazerView view) {
			base.ExecuteProc<RazerView>("[dbo].[UpdateRazerView]", view);
		}

		internal void CreateRazerView(RazerView view ){
			base.ExecuteProc<RazerView>("[dbo].[CreateRazerView]", view );
		}

		internal RazerView GetRazerView(int razerViewId) {
			return ExecuteProc<RazerView>("[dbo].[GetRazerView]", new { RazerViewId = razerViewId }).FirstOrDefault();
		}

		internal int GetRazerViewCount() {
			return ExecuteProc<int>("[dbo].[GetRazerViewCount]").FirstOrDefault();
		}

		internal ICollection<RazerView> GetAllRazerViews() {
			return ExecuteProc<RazerView>("[dbo].[GetAllRazerViews]");
        }

        internal RazerView GetRazerView(string Location) {
			return ExecuteProc<RazerView>("[dbo].[GetRazerViewByLocation]", new { Location = Location }).FirstOrDefault();
		}
		internal RazerView GetRazerViewLikeLocation(string Location) {
			return ExecuteProc<RazerView>("[dbo].[GetRazerViewLikeLocation]", new { Location = Location }).FirstOrDefault();
		}



	}
}
