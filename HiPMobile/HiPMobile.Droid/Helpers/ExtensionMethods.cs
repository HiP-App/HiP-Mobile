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

using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using de.upb.hip.mobile.pcl.BusinessLayer.Models;

namespace de.upb.hip.mobile.droid.Helpers {
    public static class ExtensionMethods {

        public static Drawable GetDrawable (this Image img, Context context)
        {
            return new BitmapDrawable (context.Resources, BitmapFactory.DecodeByteArray (img.Data, 0, img.Data.Length));
        }

        public static int[] GetDimensions (this Image img)
        {
            var bmp = BitmapFactory.DecodeByteArray (img.Data, 0, img.Data.Length);
            return new[] {bmp.Width, bmp.Height};
        }

    }
}