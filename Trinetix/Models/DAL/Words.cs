namespace Trinetix
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Words
    {
        public Words()
        {
            WordID = Guid.NewGuid();
        }
        [Key]
        public Guid WordID { get; set; }

        [Required]
        [StringLength(128)]
        public string WordName { get; set; }

        public int WordPositionCol { get; set; }

        public int WordPositionRow { get; set; }

        public Guid FileId { get; set; }

        public virtual Files Files { get; set; }
    }
}
