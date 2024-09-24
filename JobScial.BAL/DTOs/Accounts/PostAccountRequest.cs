using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.DTOs.Accounts
{
    public class PostAccountRequest
    {
        [Key]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
