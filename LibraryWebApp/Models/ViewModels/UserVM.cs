using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Models.ViewModels
{
    public class UserVM
    {
       public IEnumerable<IdentityUser> ListOfUsers { get; set; }
    }
}
