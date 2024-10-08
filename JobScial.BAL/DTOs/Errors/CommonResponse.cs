using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenZStyleAPP.BAL.Errors
{
    public class CommonResponse
    {
        public int Status { get; set; }

        public object? Data { get; set; }

        //public Pagination? Pagination { get; set; }

        public string? Message { get; set; }
    }
}
