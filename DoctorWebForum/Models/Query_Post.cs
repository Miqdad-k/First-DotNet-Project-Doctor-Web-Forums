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
    
    public partial class Query_Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Query_Post()
        {
            this.Query_Reply = new HashSet<Query_Reply>();
        }
    
        public int q_id { get; set; }
        [Display( Name = "Question")]
        [Required]
        public string question { get; set; }
        [Required]
        public string Summary { get; set; }
        public System.DateTime CreateOn { get; set; }
        public string U_id { get; set; }
        [Required]
        public string tags { get; set; }
    
        public virtual UserDetails UserDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Query_Reply> Query_Reply { get; set; }
    }
}
