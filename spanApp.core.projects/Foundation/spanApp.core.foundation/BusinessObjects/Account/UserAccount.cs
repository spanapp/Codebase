using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spanApp.core.foundation.BusinessObjects.Account
{
    public class UserAccount
    {
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string LoginName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountRoleID { get; set; }
        public string AccountRole { get; set; }
        public string AccountStatus { get; set; }
        public string AccountType { get; set; }
        public string Source { get; set; }
        public string AlternateEmailAddress { get; set; }
        public string AlternatePhoneNumber { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
