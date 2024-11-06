using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.DTOs.FireBase
{
    public class FireBaseImage
    {
        public string ApiKey { get; set; }
        public string Bucket { get; set; }
        public string AuthEmail { get; set; }
        public string AuthPassword { get; set; }
    }
}
