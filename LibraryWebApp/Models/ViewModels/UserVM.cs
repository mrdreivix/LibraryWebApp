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
        public List<ApplicationUser> ListOfCustomers { get; set; }
        [DisplayName("List Of Admins")]
        public List<ApplicationUser> ListOfAdmins{ get; set; }
        [DisplayName("List Of Workers")]
        public List<Worker> ListOfWorkers { get; set; }
    }
}
