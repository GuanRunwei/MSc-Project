﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathological_section_diagnosis_assistant_software.Helper
{
    class PictureInterceptor
    {
        #region 图片数据类型限制
        public static string[] LimitPictureType = { "png", "jpg", "gif", "jpeg", "bmp", "ico" };
        #endregion

        //public bool JudgePictures(IFormFileCollection files)
        //{
        //    foreach (var file in files)
        //    {
        //        Console.WriteLine(Path.GetExtension(file.FileName).ToLower());
        //        if (!LimitPictureType.Contains(Path.GetExtension(file.FileName).ToLower().Substring(1)))
        //            return false;
        //    }
        //    return true;
        //}
    }
}
