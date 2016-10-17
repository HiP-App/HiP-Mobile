using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Foundation;
using de.upb.hip.mobile.pcl.BusinessLayer.Models;

namespace HiPMobile.iOS
{
    /// <summary>
    /// Class that should provide information from the model for the list with exhibit
    /// </summary>
    public class ExhibitCellViewModel
    {
        public UIImage Image;
        public string Name;
        public string exhibitID;

        public ExhibitCellViewModel(string id, Image image, string exhibitName)
        {
            this.exhibitID = id;
            NSData imageData = NSData.FromArray(image.Data);
            this.Image = new UIImage(imageData);
            this.Name = exhibitName;
        }
    }
}