using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3.Utility
{
    [Serializable]
    public class Node
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Node Next { get; set; }
        public Node(User data)
        {
            this.Id = data.Id;
            this.Name = data.Name;
            this.Email = data.Email;
            this.Password = data.Password;
        }
        
    }
}
