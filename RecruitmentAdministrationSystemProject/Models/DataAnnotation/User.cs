using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecruitmentAdministrationSystemProject.Models { 
    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
        public string ConfirmPassword { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

    }
    public class UserMetaData
    {
        public int UserId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = " 6 character Required")]
        public string Password { get; set; }
        //  [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm Password is required")]
        //   [DataType(DataType.Password)]
        // [MinLength(6, ErrorMessage = "6 character Required")]
        //  [Compare("Password", ErrorMessage = "Password are not matching")]
        public string ConfirmPassword { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Image is required")]
        public string Img { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile No is required")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string MobileNo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Location is required")]
        public string Location { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Role is required")]
        public Nullable<int> RoleId { get; set; }
    }
}