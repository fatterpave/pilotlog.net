using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PilotLog.NET.Models
{
    public class LogContext : DbContext
    {
        public DbSet<Log> Log { get; set; }
    }
}