﻿// Copyright (C) 2016 History in Paderborn App - Universität Paderborn
//  
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//  
//       http://www.apache.org/licenses/LICENSE-2.0
//  
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using de.upb.hip.mobile.pcl.DataAccessLayer;
using Realms;

namespace de.upb.hip.mobile.pcl.DataLayer {
    public class RealmTransaction : BaseTransaction {

        public Transaction Transaction { get; set; }

        private Realm instance;

        public RealmTransaction (Transaction transaction, Realm instance)
        {
            this.Transaction = transaction;
            this.instance = instance;
        }

        public override void Commit ()
        {
            Transaction.Commit ();
        }

        public override void Rollback ()
        {
            Transaction.Rollback ();
        }

        public override void Dispose ()
        {
            base.Dispose ();

            Transaction.Dispose ();
        }

    }
}