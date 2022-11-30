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
    using System.Web;

    public partial class JobApplication
    {
        public int ApplicationId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> JobId { get; set; }
        public Nullable<System.DateTime> ApplicationDate { get; set; }
        public string Status { get; set; }
        public string AbleToReallocation { get; set; }
        public string PrevCompanyName { get; set; }
        [Required(ErrorMessage = "Experience is required")]
        [Range(typeof(int), "0", "100", ErrorMessage = "Experience can only be between 0 and 100")]
        public Nullable<int> WorkExperience { get; set; }
        [Required(ErrorMessage = "Notice period is required")]
        [Range(typeof(int), "0", "12", ErrorMessage = "Notice Period can only be between 0 and 12")]
        public Nullable<int> NoticePeriod { get; set; }
        public HttpPostedFileBase File { get; set; }

        public string Resume { get; set; }

        public virtual JobPost JobPost { get; set; }
        public virtual User User { get; set; }
    }
}