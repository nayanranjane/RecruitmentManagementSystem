using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentAdministrationSystemProject.Models
{
    public class CompanyDetails
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Img { get; set; }
        public string MobileNo { get; set; }
        public string Location { get; set; }
        public int CompanyId { get; set; }
        public string CompanyUrl { get; set; }
        public Nullable<long> EmployeeCount { get; set; }
        public string AboutCompany { get; set; }
        public Nullable<System.DateTime> FoundDate { get; set; }
    }
}