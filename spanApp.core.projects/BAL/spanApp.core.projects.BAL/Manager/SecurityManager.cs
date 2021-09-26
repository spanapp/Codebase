using Newtonsoft.Json.Linq;
using spanApp.core.common.Encryption;
using spanApp.core.DAL.Interfaces;
using spanApp.core.DAL.Providers;
using spanApp.core.foundation.BusinessObjects.Account;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spanApp.core.projects.BAL.Manager
{
    public class SecurityManager
    {
        ISecurityProvider provider;
        
        public bool CheckIfUserExists(string emailAddress, string phoneNumber) {
            bool isUserExists = false;
            try
            {
                InitializeSecurityProvider();
                UserAccount userAccount = new UserAccount();
                userAccount = provider.CheckIfUserExists(emailAddress, phoneNumber);
                if (userAccount != null)
                {
                    if (userAccount.ID > 0)
                    {
                        isUserExists = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return isUserExists;
        }

        public UserAccount Register(UserAccount userAccount)
        {
            List<string> errors = new List<string>();
            try
            {
                if (userAccount != null) {
                    if (userAccount.FirstName == string.Empty
                        || userAccount.EmailAddress == string.Empty
                        || userAccount.PhoneNumber == string.Empty
                        || userAccount.Password == string.Empty)
                    {
                        errors.Add("Validation failed in user account creation.");
                    }
                    else {
                        InitializeSecurityProvider();
                        UserAccount existingUser = new UserAccount();
                        existingUser = provider.CheckIfUserExists(userAccount.EmailAddress, userAccount.PhoneNumber);
                        if (existingUser != null)
                        {
                            if (existingUser.ID > 0)
                            {
                                userAccount = existingUser;
                            }
                        }
                        else
                        {
                            userAccount.Name = userAccount.FirstName + " " + userAccount.LastName;
                            if (userAccount.EmailAddress != "" && userAccount.EmailAddress != null)
                            {
                                userAccount.LoginName = userAccount.EmailAddress;
                            }
                            else if (userAccount.PhoneNumber != "" && userAccount.PhoneNumber != null)
                            {
                                userAccount.LoginName = userAccount.PhoneNumber;
                            }
                            userAccount.Password = EncryptionService.Encrypt(ConfigurationManager.AppSettings["EncKey"].ToString(), userAccount.Password);
                            userAccount = provider.Register(userAccount);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                userAccount = null;
            }
            return userAccount;
        }

        public JObject Login(string loginName, string password) {
            JObject loginData = null;

            try
            {
                InitializeSecurityProvider();
                if (loginName != string.Empty && password != string.Empty) {
                    string encryptedPwd = EncryptionService.Encrypt(password);
                    UserAccount userAccount = new UserAccount();

                    userAccount = provider.Login(loginName, encryptedPwd);

                    if (userAccount != null) {
                        if (userAccount.ID > 0) {
                            loginData = new JObject();
                            loginData.Add("AccountID", userAccount.ID);
                            loginData.Add("AccountRole", userAccount.AccountRole);
                            loginData.Add("AccountStatus", userAccount.AccountStatus);
                            loginData.Add("AccountType", userAccount.AccountType);
                            loginData.Add("Name", userAccount.Name);
                            loginData.Add("FirstName", userAccount.FirstName);
                            loginData.Add("LastName", userAccount.LastName);
                            loginData.Add("EmailAddress", userAccount.EmailAddress);
                            loginData.Add("PhoneNumber", userAccount.PhoneNumber);
                        }
                        if (loginData != null) {
                            loginData.Add("SessionData", EncryptionService.Encrypt(loginData.ToString()));
                        }
                    }

                    
                }
            }
            catch (Exception ex)
            {
                loginData = null;
                throw;
            }
            return loginData;
        }

        private void InitializeSecurityProvider() {
            provider = new SecurityProvider();
        }

    }
}
