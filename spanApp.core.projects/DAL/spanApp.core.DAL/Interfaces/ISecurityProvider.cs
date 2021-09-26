using spanApp.core.foundation.BusinessObjects.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spanApp.core.DAL.Interfaces
{
    public interface ISecurityProvider
    {
        UserAccount CheckIfUserExists(string emailAddress, string phoneNumber);
        UserAccount Register(UserAccount userAccount);
        UserAccount Login(string loginName, string password);
    }
}
