namespace Trinetix
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WordsGrouped")]
    public partial class WordsGrouped
    {
        [StringLength(128)]
        public string WordName { get; set; }

        [Key]
        [StringLength(256)]
        public string FileName { get; set; }

        public int? CountWords { get; set; }
    }
}
