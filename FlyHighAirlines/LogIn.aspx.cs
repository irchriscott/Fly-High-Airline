using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;

namespace FlyHighAirlines
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                registration_failed.Visible = false;
                registration_success.Visible = false;
                login_error.Visible = false;
            }
        }
        protected void Login_User(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Administrators WHERE UserName = '"+ Username.Text +"' AND PassCode = '"+ Password.Text +"'", connection);
                //command.Parameters.AddWithValue("@Username", User);
                //command.Parameters.AddWithValue("@Password", Pass);
                connection.Open();
                SqlDataReader myReader = command.ExecuteReader();
                int result = 0;
                while (myReader.Read())
                {
                    result = result + 1;
                }
                if (result >= 1)
                {
                    Session["admin"] = Username.Text;
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    registration_failed.Visible = false;
                    registration_success.Visible = false;
                    login_error.Visible = true;
                    login_error.InnerText = "Password or Username Incorrect";
                }
            }
            catch (Exception ex) { Response.Write(ex); }
            finally { connection.Close(); }
        }

        protected void add_new_admin_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                int id = Int32.Parse(admin_id.Value);
                string fname = admin_fname.Value;
                string lname = admin_lname.Value;
                string username = admin_username.Value;
                string address = admin_address.Value;
                string start_date = admin_startdate.Value;
                string end_date = admin_enddate.Value;
                string password = admin_password.Value;
                string function = admin_function.Value;
                string number = admin_number.Value;

                HttpPostedFile postedFile = admin_avatar.PostedFile;
                string fileName = Path.GetFileName(postedFile.FileName);
                string fileExtention = Path.GetExtension(fileName);
                int filesize = postedFile.ContentLength;

                if(string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(id.ToString()) || string.IsNullOrEmpty(start_date) || string.IsNullOrEmpty(end_date))
                {
                    registration_failed.Visible = true;
                    registration_failed.InnerText = "Fill Up All Fields Please";
                    registration_success.Visible = false;
                    login_error.Visible = false;
                }
                else
                {
                    if (fileExtention.ToLower() == ".jpg" || fileExtention.ToLower() == ".png" || fileExtention.ToLower() == ".gif" || fileExtention.ToLower() == ".jpeg")
                    {
                        Stream stream = postedFile.InputStream;
                        BinaryReader binaryReader = new BinaryReader(stream);
                        byte[] image = binaryReader.ReadBytes((int)stream.Length);

                        SqlCommand command = new SqlCommand("InsertAdmin", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        SqlParameter paramAdminId = new SqlParameter()
                        {
                            ParameterName = "@AdminId",
                            Value = admin_id.Value
                        };
                        command.Parameters.Add(paramAdminId);
                        SqlParameter paramFirstName = new SqlParameter()
                        {
                            ParameterName = "@FirstName",
                            Value = admin_fname.Value
                        };
                        command.Parameters.Add(paramFirstName);
                        SqlParameter paramLastName = new SqlParameter()
                        {
                            ParameterName = "@LastName",
                            Value = admin_lname.Value
                        };
                        command.Parameters.Add(paramLastName);
                        SqlParameter paramUsername= new SqlParameter()
                        {
                            ParameterName = "@Username",
                            Value = admin_username.Value
                        };
                        command.Parameters.Add(paramUsername);
                        SqlParameter paramPassword = new SqlParameter()
                        {
                            ParameterName = "@Password",
                            Value = admin_password.Value
                        };
                        command.Parameters.Add(paramPassword);
                        SqlParameter paramPhoneNumber = new SqlParameter()
                        {
                            ParameterName = "@PhoneNumber",
                            Value = admin_number.Value
                        };
                        command.Parameters.Add(paramPhoneNumber);
                        SqlParameter paramAddress = new SqlParameter()
                        {
                            ParameterName = "@Address",
                            Value = admin_address.Value
                        };
                        command.Parameters.Add(paramAddress);
                        SqlParameter paramFunction = new SqlParameter()
                        {
                            ParameterName = "@Function",
                            Value = admin_function.Value
                        };
                        command.Parameters.Add(paramFunction);
                        SqlParameter paramAdminAvatar = new SqlParameter()
                        {
                            ParameterName = "@AdminAvatar",
                            Value = image
                        };
                        command.Parameters.Add(paramAdminAvatar);
                        SqlParameter paramStartDate = new SqlParameter()
                        {
                            ParameterName = "@StartDate",
                            Value = admin_startdate.Value
                        };
                        command.Parameters.Add(paramStartDate);
                        SqlParameter paramEndDate = new SqlParameter()
                        {
                            ParameterName = "@EndDate",
                            Value = admin_enddate.Value
                        };
                        command.Parameters.Add(paramEndDate);
                        connection.Open();
                        
                        command.ExecuteNonQuery();
                        registration_failed.Visible = false;
                        login_error.Visible = false;
                        registration_success.Visible = true;
                        registration_success.InnerText = "Admin Added Successfuly";
                        //while (reader.Read()) { }
                    }
                    else
                    {
                        registration_failed.Visible = true;
                        registration_failed.InnerText = "Only Images are Allowed";
                        registration_success.Visible = false;
                        login_error.Visible = false;
                    }
                }
            }
            catch(Exception ex) { Response.Write(ex); }
            finally { connection.Close(); }
        }
    }
}