using RecruitmentAdministrationSystemProject.Models.DataAnnotation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecruitmentAdministrationSystemProject.Models
{
    [MetadataType(typeof(ApplicationMetaData))]
    public partial class JobApplication
    {
        //[Required (ErrorMessage ="Resume is Required")]
        //[Display(Name = "Your Resulme")]
        //[RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.doc|.docx|.pdf)$", ErrorMessage = "Only Img Files Allowed")]

       [ValidationFile(ErrorMessage ="File is required")]
      //  [Required(ErrorMessage ="File is required")]
        public HttpPostedFileBase File { get; set; }

    }
    public class ApplicationMetaData
    {
        public string Status { get; set; }
        public string AbleToReallocation { get; set; }
        [Required(ErrorMessage = "Enter NA if not applicable")]
        public string PrevCompanyName { get; set; }
        [Required(ErrorMessage = "Experience is required")]
        [Range(typeof(int), "0", "100", ErrorMessage = "Experience can only be between 0 and 100")]
        public Nullable<int> WorkExperience { get; set; }
        [Required(ErrorMessage = "Notice period is required")]
        [Range(typeof(int), "0", "12", ErrorMessage = "Notice Period can only be between 0 and 12")]
        public Nullable<int> NoticePeriod { get; set; }

    }
}