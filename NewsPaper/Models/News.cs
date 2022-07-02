namespace NewsPaper.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage ="*")]
        [Key]
        public int newsID { get; set; }

        [StringLength(50)]
        public string title { get; set; }

        [Column("ref")]
        [StringLength(50)]
        public string _ref { get; set; }

        [StringLength(50)]
        public string description { get; set; }

        public string photo { get; set; }

        public DateTime? datehire { get; set; }

        public int userID { get; set; }
        public int catID { get; set; }
    }
}
