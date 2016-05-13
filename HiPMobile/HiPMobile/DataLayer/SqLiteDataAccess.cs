// /*
//  * Copyright (C) 2016 History in Paderborn App - Universität Paderborn
//  *
//  * Licensed under the Apache License, Version 2.0 (the "License");
//  * you may not use this file except in compliance with the License.
//  * You may obtain a copy of the License at
//  *
//  *      http://www.apache.org/licenses/LICENSE-2.0
//  *
//  * Unless required by applicable law or agreed to in writing, software
//  * distributed under the License is distributed on an "AS IS" BASIS,
//  * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  * See the License for the specific language governing permissions and
//  * limitations under the License.
//  */

using System.Collections.Generic;
using System.Linq;
using de.upb.hip.mobile.pcl.BusinessLayer.Models;
using de.upb.hip.mobile.pcl.Common;
using de.upb.hip.mobile.pcl.Common.Contracts;
using de.upb.hip.mobile.pcl.DataAccessLayer;
using Microsoft.Practices.Unity;
using SQLite;

namespace de.upb.hip.mobile.pcl.DataLayer
{
    /// <summary>
    /// Class encapsulating the access to the SQLite database.
    /// </summary>
    class SqLiteDataAccess : IDataAccess {

        // Singleton pattern
        private static SqLiteDataAccess instance;
        public static SqLiteDataAccess Instance {
            get {
                if (instance == null)
                {
                    // the pcl doesn't know the db path, therefore each platform needs to provide this as 
                    // an implementation of the ISettings interface, allocation of the concrete type is done 
                    // via dependecy injection
                    var settings = IoCManager.UnityContainer.Resolve<ISettings>();
                    instance = new SqLiteDataAccess (settings.GetDbPath ());
                }
                return instance;
            }
            private set { instance = value; }
        }

        /// <summary>
        /// Constructorfor this object. Private accessability because of the Singleton pattern.
        /// </summary>
        /// <param name="databasePath">The path to the database file.</param>
        private SqLiteDataAccess (string databasePath)
        {
            connection=new SQLiteConnection (databasePath);
        }

        /// <summary>
        /// The concrete SQLiteConnection
        /// </summary>
        private SQLiteConnection connection;

        /// <summary>
        /// Object used for mutual exclusion.
        /// </summary>
        private static object locker;

        public T GetItem<T> (int key) where T : BusinessEntityBase, new ()
        {
            lock (locker)
            {
                return connection.Get<T> (key);  
            }
        }

        public IEnumerable<T> GetItems<T> () where T : BusinessEntityBase, new ()
        {
            lock (locker)
            {
                return connection.Table<T>().ToList ();
            }
        }

        public int SaveItem (BusinessEntityBase item)
        {
            lock (locker)
            {
                if (item.Id != 0)
                {
                    // Item already exist, update its values
                    connection.Update(item);
                    return item.Id;
                }
                else
                {
                    // Item doesn't exist, create a new entry
                    return connection.Insert(item);
                }
            }
        }

        public bool DeleteItem (BusinessEntityBase item)
        {
            lock (locker)
            {
                return connection.Delete (item) > 0;
            }
        }

    }
}
