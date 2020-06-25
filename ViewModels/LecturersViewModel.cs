using BigSchool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BigSchool.ViewModels
{
    public class LecturersViewModel
    {

        public IEnumerable<ApplicationUser> LecturersFollow { get; set; }
        public bool ShowAction { get; set; }
    }
}