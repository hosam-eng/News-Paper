namespace NewsPaper.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data;
    using System.Web.Mvc;

    [Table("User")]
    public partial class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int userID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="*")]
        public string name { get; set; }

        [StringLength(50)]
        [Remote("check","User",ErrorMessage ="exist email",AdditionalFields ="id")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$",ErrorMessage ="Email not valid")]
        public string email { get; set; }

        [Required(ErrorMessage ="*")]
        [StringLength(50)]
        public string password { get; set; }
        [NotMapped]
        [System.ComponentModel.DataAnnotations.Compare("password",ErrorMessage = "Not matching")]
        [DisplayName("Confirm")]
        public string Confirm_password { get; set; }

        
    }
}
