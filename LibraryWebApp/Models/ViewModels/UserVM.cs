using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Models.ViewModels
{
    public class UserVM
    {
        [DisplayName("List Of Customers")]
        public List<IdentityUser> ListOfCustomers { get; set; }
        [DisplayName("List Of Admins")]
        public List<IdentityUser> ListOfAdmins{ get; set; }
        [DisplayName("List Of Workers")]
        public List<IdentityUser> ListOfWorkers { get; set; }
    }
}
