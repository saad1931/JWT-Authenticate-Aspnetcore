using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT_Authorization
{
    public interface IJWTAuthenticationManager
    {
        string AUthenticate(string username, string password);
    }
}
