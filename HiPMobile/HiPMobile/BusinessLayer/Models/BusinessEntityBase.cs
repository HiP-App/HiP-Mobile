using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace de.upb.hip.mobile.pcl.BusinessLayer.Models
{
    public abstract class BusinessEntityBase
    {
        public BusinessEntityBase()
        {
        }
        // we need ID, so be able to genericly access DB from data layer
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }


    }
}
