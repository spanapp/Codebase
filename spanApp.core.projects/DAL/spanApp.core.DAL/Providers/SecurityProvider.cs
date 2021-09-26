using MySql.Data.MySqlClient;
using spanApp.core.DAL.Interfaces;
using spanApp.core.foundation.BusinessObjects.Account;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spanApp.core.DAL.Providers
{
    public class SecurityProvider : ISecurityProvider
    {
        string constr = ConfigurationManager.ConnectionStrings["spanAppMySQL"].ConnectionString;
        MySqlConnection connection = null;
        MySqlCommand command = null;
        MySqlDataAdapter adapter = null;
        DataTable dt = null;
        public UserAccount CheckIfUserExists(string emailAddress, string phoneNumber) {
            UserAccount userAccount = null;
            try
            {
                
                InitalizeConection();

                //string commandStr = "select * from user_account U where ((U.LoginName='" + emailAddress + "' OR U.EmailAddress='" + emailAddress + "') OR " +
                //    "(U.LoginName='" + phoneNumber + "' OR U.PhoneNumber='" + phoneNumber + "'))";

                using (command = new MySqlCommand("USP_USER_LOGIN", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", 0);
                    command.Parameters.AddWithValue("@OpMode", "CHECKUSER");
                    command.Parameters.AddWithValue("@EmailAddress", emailAddress);
                    command.Parameters.AddWithValue("@LoginName", string.Empty);
                    command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    command.Parameters.AddWithValue("@Password", string.Empty);
                    using (adapter = new MySqlDataAdapter(command))
                    {
                        dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            userAccount = new UserAccount();
                            userAccount.ID = Int32.Parse(dt.Rows[0]["ID"].ToString());
                            userAccount.FirstName = dt.Rows[0]["FirstName"].ToString();
                            userAccount.LastName = dt.Rows[0]["LastName"].ToString();
                            userAccount.EmailAddress = dt.Rows[0]["EmailAddress"].ToString();
                            userAccount.LoginName = dt.Rows[0]["LoginName"].ToString();
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

        public UserAccount Login(string loginName, string password)
        {
            UserAccount userAccount = null;
            try
            {

                InitalizeConection();

                //string commandStr = "select * from user_account U where ((U.LoginName='" + emailAddress + "' OR U.EmailAddress='" + emailAddress + "') OR " +
                //    "(U.LoginName='" + phoneNumber + "' OR U.PhoneNumber='" + phoneNumber + "'))";

                using (command = new MySqlCommand("USP_USER_LOGIN", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", 0);
                    command.Parameters.AddWithValue("@OpMode", "LOGIN");
                    command.Parameters.AddWithValue("@EmailAddress", string.Empty);
                    command.Parameters.AddWithValue("@LoginName", loginName);
                    command.Parameters.AddWithValue("@PhoneNumber", string.Empty);
                    command.Parameters.AddWithValue("@Password", password);
                    using (adapter = new MySqlDataAdapter(command))
                    {
                        dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            userAccount = new UserAccount();
                            userAccount.ID = Int32.Parse(dt.Rows[0]["ID"].ToString());
                            userAccount.LoginName = dt.Rows[0]["LoginName"].ToString();
                            userAccount.FirstName = dt.Rows[0]["FirstName"].ToString();
                            userAccount.Name = dt.Rows[0]["Name"].ToString();
                            userAccount.LastName = dt.Rows[0]["LastName"].ToString();
                            userAccount.EmailAddress = dt.Rows[0]["EmailAddress"].ToString();
                            userAccount.PhoneNumber = dt.Rows[0]["PhoneNumber"].ToString();
                            userAccount.AccountStatus = dt.Rows[0]["AccountStatus"].ToString();
                            userAccount.AccountType = dt.Rows[0]["AccountType"].ToString();
                            userAccount.AccountRole = dt.Rows[0]["AccountRole"].ToString();
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



        public UserAccount Register(UserAccount userAccount) {
            try
            {
                InitalizeConection();
                using (command = new MySqlCommand("USP_ACCOUNT_MANAGE", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", 0);
                    command.Parameters.AddWithValue("@OpMode", "REGISTER");
                    command.Parameters.AddWithValue("@EmailAddress", userAccount.EmailAddress);
                    command.Parameters.AddWithValue("@LoginName", userAccount.EmailAddress);
                    command.Parameters.AddWithValue("@PhoneNumber", userAccount.PhoneNumber);
                    command.Parameters.AddWithValue("@AccountRole", userAccount.AccountRole);
                    command.Parameters.AddWithValue("@FirstName", userAccount.FirstName);
                    command.Parameters.AddWithValue("@LastName", userAccount.LastName);
                    command.Parameters.AddWithValue("@Name", userAccount.Name);
                    command.Parameters.AddWithValue("@Password", userAccount.Password);
                    command.Parameters.AddWithValue("@UserSource", userAccount.Source);
                    command.Parameters.AddWithValue("@CreatedBy", 1);
                    command.Parameters.AddWithValue("@CreatedDate",DateTime.Now);
                    command.Parameters.AddWithValue("@ModifiedBy", 1);
                    command.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                    using (adapter = new MySqlDataAdapter(command))
                    {
                        dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            userAccount.ID = Int32.Parse(dt.Rows[0]["ID"].ToString());
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
        


        protected void InitalizeConection()
        {
            if (constr != string.Empty)
            {
                try
                {
                    connection = new MySqlConnection(constr);
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
        }
    }
}
