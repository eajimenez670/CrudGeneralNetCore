using System.ComponentModel.DataAnnotations;

namespace CrudUser.Entities
{
    public class User
    {
        [Key]
        public string Identification { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string IdTypeIdentification { get; set; }
        public TypeIdentification TypeIdentification { get; set; }
    }
}
