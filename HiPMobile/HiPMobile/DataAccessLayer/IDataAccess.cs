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
using de.upb.hip.mobile.pcl.BusinessLayer.Models;

namespace de.upb.hip.mobile.pcl.DataAccessLayer {
    public interface IDataAccess {

        /*
        T GetItem<T> (int key) where T : BusinessEntityBase; //had to change string to int
        IEnumerable<T> GetItems<T> () where T : BusinessEntityBase;

        Maybe to change this signature into:
        */

        BusinessEntityBase GetItem (int key);
        BusinessEntityBase GetItems ();
        int SaveItem (BusinessEntityBase item);
        void DeleteItem (int key); //had to change item to int

    }
}