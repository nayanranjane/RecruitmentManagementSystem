using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecruitmentAdministrationSystemProject.Models
{
    public partial class JobApplication
    {
        public HttpPostedFileBase File { get; set; }

    }
    public class ApplicationMetaData
    {
        public string Status { get; set; }
        public string AbleToReallocation { get; set; }
        public string PrevCompanyName { get; set; }
        [Required(ErrorMessage = "Experience is required")]
        [Range(typeof(int), "0", "100", ErrorMessage = "Experience can only be between 0 and 100")]
        public Nullable<int> WorkExperience { get; set; }
        [Required(ErrorMessage = "Notice period is required")]
        [Range(typeof(int), "0", "12", ErrorMessage = "Notice Period can only be between 0 and 12")]
        public Nullable<int> NoticePeriod { get; set; }
        [Required(ErrorMessage = "REsume is required")]
        public HttpPostedFileBase File { get; set; }
    }
}