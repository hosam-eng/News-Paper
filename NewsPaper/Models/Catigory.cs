namespace NewsPaper.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel;

    [Table("Catigory")]
    public partial class Catigory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Catigory ID")]
        public int catID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "*")]
        public string name { get; set; }
    }
}
