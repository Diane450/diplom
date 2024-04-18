using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.ModelsDTo
{
    public class UserDTO
    {
        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int UserTypeId { get; set; }
    }
}
