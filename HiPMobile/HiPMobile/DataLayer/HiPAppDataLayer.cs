using System.Collections.Generic;
using System.Linq;
using de.upb.hip.mobile.pcl.BusinessLayer.Models;
using SQLite;

namespace de.upb.hip.mobile.pcl.DataLayer {
    //this class contain generic object to retrieve and store in DB, using underlying ORM
   
    public class HiPAppDataLayer {
        
        // locker to protect simultations access of diff threads to DB; these could be also done as singltone, but locker seems more
        // sophisticated to me. What do you think?

        static object locker = new object();
        public SQLiteConnection db;

        public HiPAppDataLayer(SQLiteConnection conn) //here we are goind to pass the platform specific connection
        {
            db = conn;
            //all tables should be created manually (these here are just for show case). ORM takes care about is the table created or not.
            db.CreateTable<Exhibit> ();
            db.CreateTable<Route> ();
        }

        public IEnumerable<T> GetItems<T> () where T : BusinessEntityBase, new ()
        {
            lock (locker)
            {
                return (from i in db.Table<T> () select i).ToList ();
            }
        }

        public T GetItem<T> (int id) where T : BusinessEntityBase, new ()
        {
            lock (locker)
            {
                return db.Table<T> ().FirstOrDefault (x => x.ID == id);
            }
        }

        public int SaveItem<T> (T item) where T : BusinessEntityBase
        {
            lock (locker)
            {
                if (item.ID != 0)
                {
                    db.Update(item);
                    return item.ID;
                }
                else {
                    return db.Insert(item);
                }
            }
        }

        public void DeleteItem<T> (int id) where T : BusinessEntityBase, new ()
        {
            lock (locker)
            {
                db.Delete (new T {ID = id});
            }
        }

    }
}