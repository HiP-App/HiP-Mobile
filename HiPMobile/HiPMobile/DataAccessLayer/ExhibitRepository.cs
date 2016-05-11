using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using de.upb.hip.mobile.pcl.BusinessLayer.Models;
using de.upb.hip.mobile.pcl.DataLayer;
using SQLite;

namespace de.upb.hip.mobile.pcl.DataAccessLayer
{


    class ExhibitRepository : IDataAccess {

        private HiPAppDataLayer db = null;

        public ExhibitRepository (SQLiteConnection conn)
        {
            db = new HiPAppDataLayer(conn);
        }


        public BusinessEntityBase GetItem (int key)
        {
            return db.GetItem<Exhibit> (key);
        }

        public BusinessEntityBase GetItems()
        {
            return db.GetItems<Exhibit> ();
        }

        public int SaveItem (BusinessEntityBase item)
        {
            return db.SaveItem (item);
        }

        public void DeleteItem (int key)
        {
            db.DeleteItem<Exhibit> (key);
        }

    }
}
