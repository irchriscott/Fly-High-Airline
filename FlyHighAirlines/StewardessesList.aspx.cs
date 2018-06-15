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
    public partial class StewardessesList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Stewardesses_List();
            Stewardesses_Print_List();
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

        public void Stewardesses_List()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                SqlCommand command = new SqlCommand("SELECT Stewardess.StewardessId as 'ID', Stewardess.FirstName as 'First Name', Stewardess.LastName as 'Last Name', Aircrafts.AircraftName as 'Aircraft', Stewardess.DateStart as 'Started Date', Stewardess.DateEnd as 'End Date' FROM Stewardess INNER JOIN Aircrafts ON Stewardess.Aircraft = Aircrafts.AircraftId", connection);
                connection.Open();
                stewardess_list.DataSource = command.ExecuteReader();
                stewardess_list.DataBind();
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        public void Stewardesses_Print_List()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                printed_time.InnerText = DateTime.Now.ToLongDateString();
                SqlCommand command = new SqlCommand("SELECT Stewardess.StewardessId as 'Stewardess ID', Stewardess.FirstName as 'First Name', Stewardess.LastName as 'Last Name', Aircrafts.AircraftName as 'Aircraft', Stewardess.Addresse as 'Address', Stewardess.PhoneNumber as'Phone Number', Stewardess.DateStart as 'Started Date', Stewardess.DateEnd as 'End Date' FROM Stewardess INNER JOIN Aircrafts ON Stewardess.Aircraft = Aircrafts.AircraftId", connection);
                connection.Open();
                stewardess_print_list.DataSource = command.ExecuteReader();
                stewardess_print_list.DataBind();
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        protected void stewardess_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                GridViewRow row = stewardess_list.SelectedRow;
                string stew_id = row.Cells[1].Text;
                SqlCommand command = new SqlCommand("Select Stewardess.*, Aircrafts.AircraftName from Stewardess inner join Aircrafts on Stewardess.Aircraft = Aircrafts.AircraftId where Stewardess.StewardessId = '"+stew_id+"'", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    steward_id.InnerText = reader["StewardessId"].ToString();
                    cust_fail.Visible = false;
                    cust_ok.Visible = true;
                    cust_ok.InnerText = reader["FirstName"].ToString() + " " + reader["LastName"].ToString();
                    s_fname.InnerText = reader["FirstName"].ToString();
                    s_lname.InnerText = reader["LastName"].ToString();
                    update_fname.InnerText = reader["FirstName"].ToString();
                    update_lname.InnerText = reader["LastName"].ToString();
                    s_number.InnerText = reader["PhoneNumber"].ToString();
                    update_number.Value = reader["PhoneNumber"].ToString();
                    s_address.InnerText = reader["Addresse"].ToString();
                    update_address.Value = reader["Addresse"].ToString();
                    s_aircraft.InnerText = reader["AircraftName"].ToString();
                    update_arcraft.Value = reader["Aircraft"].ToString();
                    s_start.InnerText = reader["DateStart"].ToString();
                    s_end.InnerText = reader["DateEnd"].ToString();
                    byte[] image = (byte[])reader["StewardessAvatar"];
                    string convImage = Convert.ToBase64String(image);
                    stewardess_avatar.ImageUrl = "data:Image/png;base64," + convImage;
                    update_image.ImageUrl = "data:Image/png;base64," + convImage;
                }
            }
            catch(Exception ex) { Response.Write(ex); }
            finally { connection.Close(); }
        }

        protected void delete_stewardess_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                string id = steward_id.InnerText;
                SqlCommand command = new SqlCommand("Delete from Stewardess where StewardessId = '" + id + "'", connection);
                connection.Open();
                command.ExecuteNonQuery();
                Stewardesses_List();
                Stewardesses_Print_List();
                cust_ok.Visible = true;
                cust_ok.InnerText = "Stewardess Deleted";
                cust_fail.Visible = false;
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        protected void save_stewardess_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                string id = st_id.Value;
                string fname = st_fname.Value;
                string lname = st_lname.Value;
                string address = st_address.Value;
                string aircraft = st_aicraft.Value;
                string number = st_number.Value;
                string start_date = st_startdate.Value;
                string end_date = st_enddate.Value;
                HttpPostedFile posted_file = stewardess_avatar_upload.PostedFile;
                string file_name = Path.GetFileName(posted_file.FileName);
                string extension = Path.GetExtension(file_name);

                if(string.IsNullOrEmpty(id) || string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(number) || string.IsNullOrEmpty(aircraft) || string.IsNullOrEmpty(start_date) || string.IsNullOrEmpty(end_date))
                {
                    cust_fail.Visible = true;
                    cust_fail.InnerText = "Fill All Fields";
                    cust_ok.Visible = false;
                }
                else
                {
                    if(extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".gif")
                    {
                        SqlCommand ch_command = new SqlCommand("select * from Aircrafts where AircraftId = '" + aircraft + "'", connection);
                        connection.Open();
                        SqlDataReader reader = ch_command.ExecuteReader();
                        int result = 0;
                        while (reader.Read())
                        {
                            result++;
                        }

                        if (result >= 1)
                        {
                            connection.Close();
                            reader.Close();
                            byte[] image = null;
                            Stream stream = posted_file.InputStream;
                            BinaryReader binaryReader = new BinaryReader(stream);
                            image = binaryReader.ReadBytes((int)stream.Length);

                            //SqlCommand insert_command = new SqlCommand("INSERT INTO Stewardess (StewardessId, FirstName, LastName, PhoneNumber, Addresse, Aircraft, DateStart, DateEnd, StewardessAvatar) VALUES('"+id+"','"+fname+"','"+lname+"','"+number+"','"+address+"','"+aircraft+"','"+start_date+"','"+end_date+"','"+image+"')", connection);
                            SqlCommand command = new SqlCommand("InsertPilot", connection);
                            command.CommandType = CommandType.StoredProcedure;
                            SqlParameter paramPilotId = new SqlParameter()
                            {
                                ParameterName = "@StewardessId",
                                Value = st_id.Value
                            };
                            command.Parameters.Add(paramPilotId);
                            SqlParameter paramFirstName = new SqlParameter()
                            {
                                ParameterName = "@FirstName",
                                Value = st_fname.Value
                            };
                            command.Parameters.Add(paramFirstName);
                            SqlParameter paramLastName = new SqlParameter()
                            {
                                ParameterName = "@LastName",
                                Value = st_lname.Value
                            };
                            command.Parameters.Add(paramLastName);
                            SqlParameter paramPhoneNumber = new SqlParameter()
                            {
                                ParameterName = "@PhoneNumber",
                                Value = st_number.Value
                            };
                            command.Parameters.Add(paramPhoneNumber);
                            SqlParameter paramAddress = new SqlParameter()
                            {
                                ParameterName = "@PhoneNumber",
                                Value = st_address.Value
                            };
                            command.Parameters.Add(paramAddress);
                            SqlParameter paramAircraft = new SqlParameter()
                            {
                                ParameterName = "@Aircraft",
                                Value = st_aicraft.Value
                            };
                            command.Parameters.Add(paramAircraft);
                            SqlParameter paramStartDate = new SqlParameter()
                            {
                                ParameterName = "@StartDate",
                                Value = st_startdate.Value
                            };
                            command.Parameters.Add(paramStartDate);
                            SqlParameter paramEndDate = new SqlParameter()
                            {
                                ParameterName = "@EndDate",
                                Value = st_enddate.Value
                            };
                            command.Parameters.Add(paramEndDate);
                            SqlParameter paramPilotAvatar = new SqlParameter()
                            {
                                ParameterName = "@StewardessAvatar",
                                Value = image
                            };
                            command.Parameters.Add(paramPilotAvatar);
                            connection.Open();
                            command.ExecuteNonQuery();
                            Stewardesses_List();
                            Stewardesses_Print_List();
                            cust_fail.Visible = false;
                            cust_ok.InnerText = "Stewardess Addedd Successfuly";
                            cust_ok.Visible = true;
                        }
                        else
                        {
                            cust_fail.Visible = true;
                            cust_fail.InnerText = "Unknown Aircraft ID";
                            cust_ok.Visible = false;
                        }
                    }
                    else
                    {
                        cust_fail.Visible = true;
                        cust_fail.InnerText = "Only Images are Allowed";
                        cust_ok.Visible = false;
                    }
                }
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        protected void update_stewardess_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                string stew_id = steward_id.InnerText;
                string address = update_address.Value;
                string number = update_number.Value;
                string aircraft = update_arcraft.Value;

                SqlCommand ch_command = new SqlCommand("select * from Aircrafts where AircrfatId = '" + aircraft + "'", connection);
                connection.Open();
                SqlDataReader reader = ch_command.ExecuteReader();
                int result = 0;
                while (reader.Read())
                {
                    result++;
                }
                if (result >= 1)
                {
                    connection.Close();
                    reader.Close();
                    if (update_avatar.HasFile)
                    {
                        HttpPostedFile posted_file = update_avatar.PostedFile;
                        string file_name = Path.GetFileName(posted_file.FileName);
                        string extension = Path.GetExtension(file_name);

                        if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".gif" || extension.ToLower() == ".jpeg")
                        {
                            byte[] image = null;
                            Stream stream = posted_file.InputStream;
                            BinaryReader binary_reader = new BinaryReader(stream);
                            image = binary_reader.ReadBytes((int)stream.Length);

                            SqlCommand ins_command = new SqlCommand("UPDATE Stewardess SET PhoneNumber = '" + number + "', Aircraft = '" + aircraft + "', Addresse = '" + address + "', StewardessAvatar = '" + image + "' where StewardessId = '" + stew_id + "'", connection);
                            connection.Open();
                            ins_command.ExecuteNonQuery();
                            cust_fail.Visible = false;
                            cust_ok.Visible = true;
                            cust_ok.InnerText = "Stewardess Updated";
                        }
                        else
                        {
                            cust_fail.Visible = true;
                            cust_fail.InnerText = "Only Images are Allowed";
                            cust_ok.Visible = false;
                        }
                    }
                    else
                    {
                        SqlCommand ins_command = new SqlCommand("UPDATE Stewardess SET PhoneNumber = '" + number + "', Aircraft = '" + aircraft + "', Addresse = '" + address + "' where StewardessId = '" + stew_id + "'", connection);
                        connection.Open();
                        ins_command.ExecuteNonQuery();
                        cust_fail.Visible = false;
                        cust_ok.Visible = true;
                        cust_ok.InnerText = "Stewardess Updated";
                    }
                }
                else
                {
                    cust_fail.Visible = true;
                    cust_fail.InnerText = "Unknown Aircraft ID";
                    cust_ok.Visible = false;
                }
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        protected void log_out_Click(object sender, EventArgs e)
        {
            Session.Remove("admin");
            Response.Redirect("LogIn.aspx");
        }
    }
}