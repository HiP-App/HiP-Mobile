// Copyright (C) 2016 History in Paderborn App - Universität Paderborn
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

using de.upb.hip.mobile.pcl.BusinessLayer.Managers;
using de.upb.hip.mobile.pcl.Common;
using de.upb.hip.mobile.pcl.Common.Contracts;
using Realms;

namespace de.upb.hip.mobile.pcl.BusinessLayer.Models {
    public partial class Image {

        private readonly IImageDimension imgDimension = IoCManager.Resolve<IImageDimension>();

<<<<<<< HEAD:HiPMobile/HiPMobile/BusinessLayer/Models/NonGenerated/Image.cs
        private int _width { get; set; }
       [Ignored]
=======
        private int ImageWidth { get; set; }
        [Ignored]
>>>>>>> 557187479ed9b76e94da697aff291be3637623db:Sources/HiPMobile/BusinessLayer/Models/NonGenerated/Image.cs
        public int Width {
            get {
                if (ImageWidth != 0)
                {
                    return ImageWidth;
                }
                var w = imgDimension.GetDimensions (this) [0];
                using (DbManager.StartTransaction ())
                {
                    ImageWidth = w;
                }
                return w;
            }
        }

<<<<<<< HEAD:HiPMobile/HiPMobile/BusinessLayer/Models/NonGenerated/Image.cs
        private int _height { get; set; }
       [Ignored]
=======
        private int ImageHeight { get; set; }
        [Ignored]
>>>>>>> 557187479ed9b76e94da697aff291be3637623db:Sources/HiPMobile/BusinessLayer/Models/NonGenerated/Image.cs
        public int Height
        {
            get
            {
                if (ImageHeight != 0)
                {
                    return ImageHeight;
                }
                var h = imgDimension.GetDimensions(this)[1];
                using (DbManager.StartTransaction ())
                {
                    ImageHeight = h;
                }
                return h;
            }
        }

    }
}