using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data;

namespace FlyHighAirlines
{
    public partial class PilotsList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Pilots_List();
            Pilot_Print_List();
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

        public void Pilots_List()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                SqlCommand command = new SqlCommand("SELECT Pilots.PilotId as 'Pilot ID', Pilots.FirstName as 'First Name', Pilots.LastName as 'Last Name', Aircrafts.AircraftName as 'Aircraft', Pilots.DateStart as 'Started Date', Pilots.DateEnd as 'End Date' FROM Pilots INNER JOIN Aircrafts ON Pilots.Aircraft = Aircrafts.AircraftId", connection);
                connection.Open();
                pilot_list.DataSource = command.ExecuteReader();
                pilot_list.DataBind();
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        public void Pilot_Print_List()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                printed_time.InnerText = DateTime.Now.ToLongDateString();
                SqlCommand command = new SqlCommand("SELECT Pilots.FirstName as 'First Name', Pilots.LastName as 'Last Name', Aircrafts.AircraftName as 'Aircraft', Pilots.PhoneNumber as 'Phone Number', Pilots.Addresse as 'Address', Pilots.DateStart as 'Started Date', Pilots.DateEnd as 'End Date' FROM Pilots INNER JOIN Aircrafts ON Pilots.Aircraft = Aircrafts.AircraftId", connection);
                connection.Open();
                pilots_print_list.DataSource = command.ExecuteReader();
                pilots_print_list.DataBind();
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        protected void pilot_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                GridViewRow row = pilot_list.SelectedRow;
                int id = Convert.ToInt32(row.Cells[1].Text);
                string sql = "Select Pilots.*, Aircrafts.AircraftName from Pilots inner join Aircrafts on Pilots.Aircraft = Aircrafts.AircraftId where Pilots.PilotId = '" + id + "'";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cust_fail.Visible = false;
                    cust_ok.Visible = true;
                    cust_ok.InnerText = reader["FirstName"].ToString() + " "+ reader["LastName"].ToString();
                    update_fname.InnerText = reader["FirstName"].ToString();
                    update_lname.InnerText = reader["LastName"].ToString();
                    pilot_id.InnerText = reader["PilotId"].ToString();
                    p_fname.InnerText = reader["FirstName"].ToString();
                    p_lastname.InnerText = reader["LastName"].ToString();
                    p_aircraft.InnerText = reader["AircraftName"].ToString();
                    update_arcraft.Value = reader["Aircraft"].ToString();
                    p_address.InnerText = reader["Addresse"].ToString();
                    update_address.Value = reader["Addresse"].ToString();
                    p_number.InnerText = reader["PhoneNumber"].ToString();
                    update_number.Value = reader["PhoneNumber"].ToString();
                    p_start.InnerText = reader["DateStart"].ToString();
                    p_end.InnerText = reader["DateEnd"].ToString();
                    byte[] image = (byte[])reader["PilotAvatar"];
                    string imageConvert = Convert.ToBase64String(image);
                    pilot_avatar.ImageUrl = "data:Image/png;base64," + imageConvert;
                    update_image.ImageUrl = "data:Image/png;base64," + imageConvert;
                }
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        protected void delete_pilot_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                int pil_id = Convert.ToInt32(pilot_id.InnerText);
                SqlCommand command = new SqlCommand("Delete from Pilots where PilotId = '" + pil_id + "'", connection);
                connection.Open();
                command.ExecuteNonQuery();
                Pilots_List();
                Pilot_Print_List();
                cust_fail.Visible = false;
                cust_ok.Visible = true;
                cust_ok.InnerText = "Pilot Deleted Successfuly";
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        protected void save_pilot_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                string id = pi_id.Value;
                string fname = pi_fname.Value;
                string lname = pi_lname.Value;
                string number = pi_number.Value;
                string address = pi_address.Value;
                string aircraft = pi_aicraft.Value;
                string start_date = pi_startdate.Value;
                string end_date = pi_enddate.Value;
                HttpPostedFile postedFile = pilot_avatar_upload.PostedFile;
                string file_name = Path.GetFileName(postedFile.FileName);
                string extention = Path.GetExtension(file_name);

                if(string.IsNullOrEmpty(id.ToString()) || string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname) || string.IsNullOrEmpty(number) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(aircraft.ToString()) || string.IsNullOrEmpty(start_date) || string.IsNullOrEmpty(end_date))
                {
                    cust_fail.Visible = true;
                    cust_fail.InnerText = "Fill Up All Fields";
                    cust_ok.Visible = false;
                }
                else
                {
                    if (extention.ToLower() == ".jpg" || extention.ToLower() == ".png" || extention.ToLower() == ".jpeg" || extention.ToLower() == ".gif")
                    {
                        SqlCommand ch_plaine = new SqlCommand("Select * from Aircrafts where AircraftId = '"+aircraft+"'", connection);
                        connection.Open();
                        SqlDataReader reader = ch_plaine.ExecuteReader();
                        int result = 0;
                        while (reader.Read())
                        {
                            result++;
                        }

                        if (result >= 1)
                        {
                            reader.Close();
                            connection.Close();
                            byte[] image = null;
                            Stream stream = postedFile.InputStream;
                            BinaryReader binaryReader = new BinaryReader(stream);
                            image = binaryReader.ReadBytes((int)stream.Length);

                            //SqlCommand command = new SqlCommand("INSERT INTO Pilots (PilotId, FirstName, LastName, PhoneNumber, Addresse, Aircraft, DateStart, DateEnd, PilotAvatar) VALUES('" + id + "','" + fname + "','" + lname + "','" + number + "','" + address + "','" + aircraft + "','" + start_date + "','" + end_date + "','" + image + "')", connection);
                            SqlCommand command = new SqlCommand("InsertPilot", connection);
                            command.CommandType = CommandType.StoredProcedure;
                            SqlParameter paramPilotId = new SqlParameter()
                            {
                                ParameterName = "@AdminId",
                                Value = pi_id.Value
                            };
                            command.Parameters.Add(paramPilotId);
                            SqlParameter paramFirstName = new SqlParameter()
                            {
                                ParameterName = "@FirstName",
                                Value = pi_fname.Value
                            };
                            command.Parameters.Add(paramFirstName);
                            SqlParameter paramLastName = new SqlParameter()
                            {
                                ParameterName = "@LastName",
                                Value = pi_lname.Value
                            };
                            command.Parameters.Add(paramLastName);
                            SqlParameter paramPhoneNumber = new SqlParameter()
                            {
                                ParameterName = "@PhoneNumber",
                                Value = pi_number.Value
                            };
                            command.Parameters.Add(paramPhoneNumber);
                            SqlParameter paramAddress = new SqlParameter()
                            {
                                ParameterName = "@PhoneNumber",
                                Value = pi_address.Value
                            };
                            command.Parameters.Add(paramAddress);
                            SqlParameter paramAircraft = new SqlParameter()
                            {
                                ParameterName = "@Aircraft",
                                Value = pi_aicraft.Value
                            };
                            command.Parameters.Add(paramAircraft);
                            SqlParameter paramStartDate = new SqlParameter()
                            {
                                ParameterName = "@StartDate",
                                Value = pi_startdate.Value
                            };
                            command.Parameters.Add(paramStartDate);
                            SqlParameter paramEndDate = new SqlParameter()
                            {
                                ParameterName = "@EndDate",
                                Value = pi_enddate.Value
                            };
                            command.Parameters.Add(paramEndDate);
                            SqlParameter paramPilotAvatar = new SqlParameter()
                            {
                                ParameterName = "@PilotAvatar",
                                Value = image
                            };
                            command.Parameters.Add(paramPilotAvatar);
                            connection.Open();
                            command.ExecuteNonQuery();
                            Pilots_List();
                            Pilot_Print_List();
                            cust_fail.Visible = false;
                            cust_ok.InnerText = "Pilot Added Successfuly";
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
            catch(Exception ex) { Response.Write(ex); }
            finally { connection.Close(); }
        }

        protected void update_pilot_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                string pil_id = pilot_id.InnerText;
                string aircraft = update_arcraft.Value;
                string address = update_address.Value;
                string number = update_number.Value;

                SqlCommand ch_command = new SqlCommand("Select * from Aircrafts where AircraftId = '" + aircraft + "'", connection);
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
                    if (update_avatar.HasFile)
                    {
                        HttpPostedFile posted_file = update_avatar.PostedFile;
                        string file_name = Path.GetFileName(posted_file.FileName);
                        string extention = Path.GetExtension(file_name);

                        if (extention.ToLower() == ".jpg" || extention.ToLower() == ".png" || extention.ToLower() == ".jpeg" || extention.ToLower() == ".gif")
                        {
                            byte[] image = null;
                            Stream stream = posted_file.InputStream;
                            BinaryReader binaryReader = new BinaryReader(stream);
                            image = binaryReader.ReadBytes((int)stream.Length);

                            SqlCommand command = new SqlCommand("Update Pilots Set PhoneNumber = '" + number + "', Addresse = '" + address + "', Aircraft = '" + aircraft + "' where PilotId = '" + pil_id + "', PilotAvatar = '" + image + "'", connection);
                            connection.Open();
                            command.ExecuteNonQuery();
                            Pilots_List();
                            Pilot_Print_List();
                            cust_ok.Visible = true;
                            cust_ok.InnerText = "Pilot Updated Successfuly";
                            cust_fail.Visible = false;
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
                        SqlCommand command = new SqlCommand("Update Pilots Set PhoneNumber = '" + number + "', Addresse = '" + address + "', Aircraft = '" + aircraft + "' where PilotId = '" + pil_id + "'", connection);
                        connection.Open();
                        command.ExecuteNonQuery();
                        Pilots_List();
                        Pilot_Print_List();
                        cust_ok.Visible = true;
                        cust_ok.InnerText = "Pilot Updated Successfuly";
                        cust_fail.Visible = false;
                    }
                }
                else
                {
                    cust_fail.Visible = true;
                    cust_ok.Visible = false;
                    cust_fail.InnerText = "Unknown Aircraft ID";
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