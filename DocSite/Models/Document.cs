using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DocSite.Models
{
    public class Document
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int DocumentID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int? ProductId { get; set; }

        [Required]
        public string FilePath { get; set; }

        public string LastModified { get; set; }


    }

    public class Organization
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int DocumentID { get; set; }
        public string Name { get; set; }
        [Required]
        public string FilePath { get; set; }

    }
}