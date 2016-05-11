using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.upb.hip.mobile.pcl.BusinessLayer.Models
{
    public class GeoCoordinate : BusinessEntityBase
    {

            public double Latitude { get; set; }
            public double Longitude { get; set; }

            public GeoCoordinate(double latitude, double longitude)
            {
                Latitude = latitude;
                Longitude = longitude;
            }

            public override string ToString()
            {
                return $"{Latitude},{Longitude}";
            }

        
    }
}
