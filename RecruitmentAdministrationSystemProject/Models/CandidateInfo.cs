//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RecruitmentAdministrationSystemProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class CandidateInfo
    {
        public int CandidateId { get; set; }
        public Nullable<int> UserId { get; set; }
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
        public Nullable<int> HSC_PassingYear { get; set; }
        [Required(ErrorMessage = "UG Marks is required")]
        [Range(typeof(int), "35", "100", ErrorMessage = "Enter Valid Marks")]
        public Nullable<int> UG_Marks { get; set; }
        [Required(ErrorMessage = "UG College Year is required")]
        public string UG_College { get; set; }
        [Required(ErrorMessage = "Passing Year is required")]
        [Range(typeof(int), "1900", "2100", ErrorMessage = "Enter Valid Year")]
        public Nullable<int> UG_PassingYear { get; set; }
        public string Skill_1 { get; set; }
        public string Skill_2 { get; set; }
        public string Skill_3 { get; set; }
        public string Gender { get; set; }
    
        public virtual User User { get; set; }
    }
}
