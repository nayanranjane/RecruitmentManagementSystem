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
    
    public partial class JobApplication
    {
        public int ApplicationId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> JobId { get; set; }
        public Nullable<System.DateTime> ApplicationDate { get; set; }
        public string Status { get; set; }
        public string AbleToReallocation { get; set; }
        public string PrevCompanyName { get; set; }
        public Nullable<int> WorkExperience { get; set; }
        public Nullable<int> NoticePeriod { get; set; }
        public string Resume { get; set; }
    
        public virtual JobPost JobPost { get; set; }
        public virtual User User { get; set; }
    }
}
