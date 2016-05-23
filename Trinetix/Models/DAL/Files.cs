namespace Trinetix
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Files
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Files()
        {
            FileId = Guid.NewGuid();
            Words = new HashSet<Words>();
        }

        [Key]
        public Guid FileId { get; set; }

        [Required]
        [StringLength(256)]
        public string FileName { get; set; }

        public Guid FileTypeID { get; set; }

        public DateTime DateCreated { get; set; }

        public Guid DirrectoryID { get; set; }

        public virtual Dirrectories Dirrectories { get; set; }

        public virtual FileData FileData { get; set; }

        public virtual FileTypes FileTypes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Words> Words { get; set; }
    }
}
