namespace Trinetix
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FileData")]
    public partial class FileData
    {
        public FileData()
        {
            FileId = Guid.NewGuid();
        }
        [Key]
        public Guid FileId { get; set; }

        [Column("FileData", TypeName = "image")]
        [Required]
        public byte[] FileData1 { get; set; }

        public virtual Files Files { get; set; }
    }
}
