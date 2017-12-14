using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project2Final.Models
{
    [Table("MissionQuestions")]
    public class MissionQuestions
    {
        [Key]
        public int missionQuestionID { get; set; }
        public int? userID { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public string userEmail { get; set; }
        public int missionID { get; set; }
    }
}