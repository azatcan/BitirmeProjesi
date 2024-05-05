using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitirme.Domain.Entities
{
    public class Connections
    {
        public Guid Id { get; set; }
        public Guid? UserID { get; set; }
        public Guid? GroupID { get; set; }
        public string ConnectionType { get; set; }

        public Users User { get; set; }
        public Groups Group { get; set; }
    }
}
