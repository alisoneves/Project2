using Project2Final.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project2Final.DAL
{
    public class MissionsContext : DbContext
    {
        public MissionsContext() : base("MissionsContext")
        {

        }

        public DbSet<Mission> Missions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MissionQuestions> MissionQuestions { get; set; }
    }
}