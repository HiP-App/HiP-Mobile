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
        /// <summary>
        /// Id for identifying BusinessObjects. The Id must be unique amon other objects of the same type.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public BusinessEntityBase() { }
    }
}
