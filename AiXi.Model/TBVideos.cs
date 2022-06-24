namespace AiXi.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TBVideos
    {
        [Key]
        [StringLength(100)]
        public string VideoId { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Headline { get; set; }

        [StringLength(20)]
        public string Location { get; set; }

        [StringLength(200)]
        public string CoverURL { get; set; }

        public int Status { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime UploadTime { get; set; }

        public virtual TBUsers TBUsers { get; set; }
    }
}
