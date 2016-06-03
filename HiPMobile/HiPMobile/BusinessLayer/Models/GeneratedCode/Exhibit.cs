﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
/*Copyright (C) 2016 History in Paderborn App - Universit�t Paderborn
 
  Licensed under the Apache License, Version 2.0 (the "License");
  you may not use this file except in compliance with the License.
  You may obtain a copy of the License at
 
       http://www.apache.org/licenses/LICENSE-2.0
 
  Unless required by applicable law or agreed to in writing, software
  distributed under the License is distributed on an "AS IS" BASIS,
  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
  See the License for the specific language governing permissions and
  limitations under the License.*/
namespace de.upb.hip.mobile.pcl.BusinessLayer.Models
{
	using Realms;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public partial class Exhibit : RealmObject, IIdentifiable
	{
		//Attributes
		[ObjectId]
		private string _id{ get; set; }
		public string Id{
			get{ return _id; }
			set{ Realm.GetInstance ().Write (() => _id = value); }
		}

		private string _name{ get; set; }
		public string Name{
			get{ return _name; }
			set{ Realm.GetInstance ().Write (() => _name = value); }
		}

		private string _description{ get; set; }
		public string Description{
			get{ return _description; }
			set{ Realm.GetInstance ().Write (() => _description = value); }
		}

		private CustomGeoPoint _location{ get; set; }
		public CustomGeoPoint Location{
			get{ return _location; }
			set{ Realm.GetInstance ().Write (() => _location = value); }
		}

		private RealmList<StringElement> _categories{ get; }
		public IList<StringElement> Categories{
			get{ return _categories; }
		}

		private RealmList<StringElement> _tags{ get; }
		public IList<StringElement> Tags{
			get{ return _tags; }
		}

		//Associations
		private RealmList<Page> _pages{ get; }
		public IList<Page> Pages{
			 get{ return _pages; }
		}

		public virtual Image Image{ get; set; }

		// Contructor
		public Exhibit(){
		}
	}
}

