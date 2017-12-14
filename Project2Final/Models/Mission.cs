using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Project2Final.Models
{
    [Table("Mission")]
    public class Mission
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string President { get; set; }
        public string Address { get; set; }
        public string Language { get; set; }
        public string Climate { get; set; }
        public string DominateReligion { get; set; }
        public string FlagURL { get; set; }

    }
}