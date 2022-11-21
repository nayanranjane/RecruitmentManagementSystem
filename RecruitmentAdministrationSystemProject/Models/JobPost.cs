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
    
    public partial class JobPost
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JobPost()
        {
            this.JobApplications = new HashSet<JobApplication>();
        }
    
        public int JobId { get; set; }
        public string Title { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public string CTC { get; set; }
        public Nullable<int> NoOfOpening { get; set; }
        public string Location { get; set; }
        public Nullable<System.DateTime> PostDate { get; set; }
        public Nullable<System.DateTime> LastDate { get; set; }
        public Nullable<int> Skill_1 { get; set; }
        public Nullable<int> Skill_2 { get; set; }
        public Nullable<int> Skill_3 { get; set; }
        public Nullable<int> RequiredBatch { get; set; }
        public string Description { get; set; }
        public Nullable<int> SSCMarks { get; set; }
        public Nullable<int> HSCMarks { get; set; }
        public Nullable<int> UGMarks { get; set; }
        public Nullable<int> RequiredExperience { get; set; }
    
        public virtual Company Company { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobApplication> JobApplications { get; set; }
    }
}
