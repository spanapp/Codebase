using ConnectionTestApplication.BusinessEntities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectionTestApplication
{
    public partial class Form1 : Form
    {
        private string mongoConStr = "mongodb://127.0.0.1:27017/gssapiServiceName=mongodb?readPreference=primary&ssl=false";
        private string dbName = "shop";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string password = txtPassword.Text;

            UserDetails userData = new UserDetails();
            userData.Password = password;
            userData.UserName = userName;

            try {
                var client = new MongoClient(mongoConStr);
                var database = client.GetDatabase(dbName);
                var collection = database.GetCollection<UserDetails>("UserDetails");
                collection.InsertOneAsync(userData);
                lblStatus.Text = "User Created.";
            }
            catch (Exception ex) {
                lblStatus.Text = "exception occurred.::" + ex.Message;
            }

           
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var client = new MongoClient(mongoConStr);
                var database = client.GetDatabase(dbName);
                List<UserDetails> users = new List<UserDetails>();
                users= database.GetCollection<UserDetails>("UserDetails").Find(new BsonDocument()).ToList();
                if (users.Count > 0) {
                    UserDetails userData = new UserDetails();
                    userData = users.Where(x => x.UserName == txtUserName.Text && x.Password == txtPassword.Text).FirstOrDefault();
                    if (userData != null)
                    {
                        lblStatus.Text = "User found.";
                    }
                    else {
                        lblStatus.Text = "User not found.";
                    }
                    
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "exception occurred.::" + ex.Message;
            }
        }
    }
}
