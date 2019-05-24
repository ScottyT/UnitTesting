using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestingControllers.Models
{
    public class SessionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
