namespace AiXiu.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TBLogins
    {
        public int Id { get; set; }

        [Required]
        [StringLength(32)]
        public string UserName { get; set; }

        [Required]
        [StringLength(11)]
        public string MobileNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public virtual TBUsers TBUsers { get; set; }
    }
}
