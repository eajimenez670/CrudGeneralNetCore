using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudUser.Entities
{
    public class TypeIdentification
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}
