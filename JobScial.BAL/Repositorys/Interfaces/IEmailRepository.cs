using JobScial.BAL.Models;
using JobScial.BAL.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.Repositorys.Interfaces
{
    public interface IEmailRepository
    {
        void SendEmail(Message message);
    }
}
