//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoctorWebForum.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Query_Reply
    {
        public int R_id { get; set; }
        [Required]
        public string Reply { get; set; }
        public System.DateTime CreateOn { get; set; }
        public Nullable<int> q_id { get; set; }
        public string U_id { get; set; }
    
        public virtual Query_Post Query_Post { get; set; }
        public virtual UserDetails UserDetails { get; set; }
    }
}