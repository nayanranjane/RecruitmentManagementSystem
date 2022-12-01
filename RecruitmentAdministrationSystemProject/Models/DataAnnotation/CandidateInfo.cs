using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecruitmentAdministrationSystemProject.Models
{

    public class CandidateInfoMetaData
    {
        [Required(ErrorMessage = "SSC Marks is required")]
        [Range(typeof(int), "35", "100", ErrorMessage = "Enter Valid Marks")]
        public Nullable<int> SSC_Marks { get; set; }
        [Required(ErrorMessage = "SSC College Year is required")]
        public string SSC_College { get; set; }
        [Required(ErrorMessage = "Passing Year is required")]
        [Range(typeof(int), "1900", "2100", ErrorMessage = "Enter Valid Year")]
        public Nullable<int> SSC_PassingYear { get; set; }
        [Required(ErrorMessage = "HSC Marks is required")]
        [Range(typeof(int), "35", "100", ErrorMessage = "Enter Valid Marks")]
        public Nullable<int> HSC_Marks { get; set; }
        [Required(ErrorMessage = "HSC College Year is required")]
        public string HSC_College { get; set; }
        [Range(typeof(int), "1900", "2100", ErrorMessage = "Enter Valid Year")]
        [Required(ErrorMessage = "Passing Year is required")]
        public Nullable<int> HSC_PassingYear { get; set; }
        [Required(ErrorMessage = "UG Marks is required")]
        [Range(typeof(int), "35", "100", ErrorMessage = "Enter Valid Marks")]
        public Nullable<int> UG_Marks { get; set; }
        [Required(ErrorMessage = "UG College Year is required")]
        public string UG_College { get; set; }
        [Required(ErrorMessage = "Passing Year is required")]
        [Range(typeof(int), "1900", "2100", ErrorMessage = "Enter Valid Year")]
        public Nullable<int> UG_PassingYear { get; set; }
    }
}