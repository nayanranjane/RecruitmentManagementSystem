using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecruitmentAdministrationSystemProject.Models
{
    public class JobPostMetadata
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public Nullable<int> CompanyID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "CTC is required")]
        public string CTC { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "No of Opening is required")]
        public Nullable<int> NoOfOpening { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Location is required")]
        public string Location { get; set; }
        public Nullable<System.DateTime> PostDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Date is required")]
        public Nullable<System.DateTime> LastDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Skill 1 is required")]
        public string Skill_1 { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Skill 2 is required")]
        public string Skill_2 { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Skill 3 is required")]
        public string Skill_3 { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Required Batch")]
        public Nullable<int> RequiredBatch { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "SSC Marks is required")]
        [Range(typeof(int), "35", "100", ErrorMessage = "Enter Valid Marks")]
        public Nullable<int> SSCMarks { get; set; }
        [Required(ErrorMessage = "HSC Marks is required")]
        [Range(typeof(int), "35", "100", ErrorMessage = "Enter Valid Marks")]
        public Nullable<int> HSCMarks { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "UG marks is required")]
        public Nullable<int> UGMarks { get; set; }
        [Required(ErrorMessage = "Experience is required")]
        [Range(typeof(int), "0", "100", ErrorMessage = "Enter Valid Experience")]
        public Nullable<int> RequiredExperience { get; set; }
    }
}