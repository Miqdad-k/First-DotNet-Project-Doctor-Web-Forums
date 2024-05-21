using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWebForum.Models
{
    public class DoctorDetails
    {
        public string U_id { get; set; }
       
        public string U_name { get; set; }
        
        public string F_name { get; set; }
        
        public System.DateTime DOB { get; set; }
        
        public string Gender { get; set; }
        
        public string Phone_No { get; set; }
        
        public string Status { get; set; }
        
        public string Specailization { get; set; }

        public string u_Image { get; set; }
        public System.DateTime JoinedDate { get; set; }
        
        public string Nationality { get; set; }
        
        public string Email { get; set; }
        
        public string City { get; set; }
        
        public string country { get; set; }
        public int p_id { get; set; }
        
        public string School { get; set; }
        
        public string College { get; set; }
        
        public string University { get; set; }
        
        public string Experience { get; set; }
    }
}