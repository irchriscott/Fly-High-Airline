using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlyHighAirlines
{
    public partial class AdministratorsList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Administratoer_Print_List();
            Administrator_List();
            Get_Session();
            if (!IsPostBack)
            {
                cust_fail.Visible = false;
                cust_ok.Visible = false;
            }
        }

        public void Get_Session()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                if (Session["admin"] != null)
                {
                    string adin_user = Session["admin"].ToString();
                    SqlCommand command = new SqlCommand("Select * from Administrators where UserName = '" + adin_user + "'", connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        sess_names.InnerText = reader["FirstName"].ToString() + " " + reader["LastName"].ToString();
                        //sess_username.InnerText = reader["UserName"].ToString();
                        byte[] avatar = (byte[])reader["AdminAvatar"];
                        string convImage = Convert.ToBase64String(avatar);
                        sess_avatar.ImageUrl = "data:Image/png;base64," + convImage;
                    }
                }
                else
                {
                    Response.Redirect("LogIn.aspx");
                }
            }
            catch (Exception ex) { Response.Write(ex); }
            finally { connection.Close(); }
        }

        public void Administrator_List()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                SqlCommand command = new SqlCommand("SELECT AdminId as 'Admin ID', FirstName as 'First Name', LastName as 'Last Name', RoleName as 'Function', StartDate as 'Start Date', EndDate as 'End Date' FROM Administrators", connection);
                connection.Open();
                admin_list.DataSource = command.ExecuteReader();
                admin_list.DataBind();
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        public void Administratoer_Print_List()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                printed_time.InnerText = DateTime.Now.ToLongDateString();
                SqlCommand command = new SqlCommand("SELECT AdminId as 'Admin ID', FirstName as 'First Name', LastName as 'Last Name', PhoneNumber as 'Phone Number', RoleName as 'Function', StartDate as 'Start Date', EndDate as 'End Date' FROM Administrators", connection);
                connection.Open();
                admin_print_list.DataSource = command.ExecuteReader();
                admin_print_list.DataBind();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
        }

        protected void admin_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                GridViewRow row = admin_list.SelectedRow;
                string ad_id = row.Cells[1].Text;
                SqlCommand command = new SqlCommand("Select * from Administrators where AdminId = '" + ad_id + "'", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    adm_id.InnerText = reader["AdminId"].ToString();
                    cust_fail.Visible = false;
                    cust_ok.Visible = true;
                    cust_ok.InnerText = reader["FirstName"].ToString() + " " + reader["LastName"].ToString();
                    adm_address.InnerText = reader["Addresse"].ToString();
                    adm_function.InnerText = reader["RoleName"].ToString();
                    adm_end.InnerText = reader["EndDate"].ToString();
                    adm_fname.InnerText = reader["FirstName"].ToString();
                    adm_lname.InnerText = reader["LastName"].ToString();
                    adm_number.InnerText = reader["PhoneNumber"].ToString();
                    adm_start.InnerText = reader["StartDate"].ToString();
                    byte[] image = (byte[])reader["AdminAvatar"];
                    string convImage = Convert.ToBase64String(image);
                    adm_avatar.ImageUrl = "data:Image/png;base64," + convImage;
                }
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        protected void delete_admin_Click(object sender, EventArgs e)
        {

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

                if (string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(id.ToString()) || string.IsNullOrEmpty(start_date) || string.IsNullOrEmpty(end_date))
                {
                    cust_fail.Visible = true;
                    cust_fail.InnerText = "Fill Up All Fields Please";
                    cust_ok.Visible = false;
                }
                else
                {
                    if (fileExtention.ToLower() == ".jpg" || fileExtention.ToLower() == ".png" || fileExtention.ToLower() == ".gif" || fileExtention.ToLower() == ".jpeg")
                    {
                        Stream stream = postedFile.InputStream;
                        BinaryReader binaryReader = new BinaryReader(stream);
                        byte[] image = binaryReader.ReadBytes((int)stream.Length);

                        //SqlCommand command = new SqlCommand("insert into Administrators (AdminID, FirstName, LastName, UserName, PassCode, PhoneNumber, Addresse, RoleName, AdminAvatar, StartDate, EndDate) values ('" + id + "', '" + fname + "', '" + lname + "', '" + username + "', '" + password + "', '" + number + "','" + address + "', '" + function + "', '" + image + "', '" + start_date + "', '" + end_date + "')", connection);
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
                        SqlParameter paramUsername = new SqlParameter()
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
                            ParameterName = "@PhoneNumber",
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
                        Administratoer_Print_List();
                        Administrator_List();
                        cust_fail.Visible = false;
                        cust_ok.Visible = true;
                        cust_fail.InnerText = "Admin Added Successfuly";
                    }
                    else
                    {
                        cust_fail.Visible = true;
                        cust_fail.InnerText = "Only Images are Allowed";
                        cust_ok.Visible = false;
                    }
                }
            }
            catch (Exception ex) { Response.Write(ex); }
            finally { connection.Close(); }
        }

        protected void log_out_Click(object sender, EventArgs e)
        {
            Session.Remove("admin");
            Response.Redirect("LogIn.aspx");
        }

    }
}