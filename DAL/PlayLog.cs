namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PlayLog")]
    public partial class PlayLog
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string SessionID { get; set; }

        public int? Cash { get; set; }

        public bool? Win { get; set; }

        public int? PutCache { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
