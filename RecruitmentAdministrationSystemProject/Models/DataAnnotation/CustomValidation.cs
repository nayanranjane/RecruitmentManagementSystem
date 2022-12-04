using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace RecruitmentAdministrationSystemProject.Models.DataAnnotation
{
    public class ValidationFileAttribute:RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            //if (file == null)
            //{
            //    return false;
            //}

            //if (file.ContentLength > 1 * 1024 * 1024)
            //{
            //    return false;
            //}
            if (file != null)
            {
                if (Path.GetExtension(file.FileName).ToLower() == ".png" || Path.GetExtension(file.FileName).ToLower() == ".jpg" || Path.GetExtension(file.FileName).ToLower() == ".jpeg")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

    }
    public class DatetimeValidationAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var date = (DateTime)value;

            if (date < DateTime.Now)
            {
                return false;
            }
            else{
                return true;
            }

        }

    }
}