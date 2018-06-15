using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlyHighAirlines
{
    public partial class AircraftSchedules : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Aircraft_Schedules();
            Print_Schedules();
            Get_Session();
            if (!IsPostBack)
            {
                cust_ok.Visible = false;
                cust_fail.Visible = false;
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

        public void Aircraft_Schedules()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                SqlCommand command = new SqlCommand("SELECT AircraftsSchedule.AircraftId as 'Aircraft ID', AircraftsSchedule.AircraftName as 'Name', AircraftsSchedule.TotalSits as 'Total Sits', AircraftsSchedule.TravelDay as 'Travel Day', AircraftsSchedule.PlaceFrom as 'From', AircraftsSchedule.PlaceTo as 'To' FROM AircraftsSchedule", connection);
                connection.Open();
                aircraft_schedules.DataSource = command.ExecuteReader();
                aircraft_schedules.DataBind(); 
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        public void Print_Schedules()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                printed_time.InnerText = DateTime.Now.ToLongDateString();
                SqlCommand command = new SqlCommand("SELECT AircraftsSchedule.AircraftName as 'Name', AircraftsSchedule.TotalSits as 'Total Sits', AircraftsSchedule.TravelDay as 'Travel Day', AircraftsSchedule.PlaceFrom as 'From', AircraftsSchedule.PlaceTo as 'To', AircraftsSchedule.MoveTime as 'Time' FROM AircraftsSchedule", connection);
                connection.Open();
                aircraft_sch.DataSource = command.ExecuteReader();
                aircraft_sch.DataBind();
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        protected void aircraft_schedules_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                GridViewRow row = aircraft_schedules.SelectedRow;
                string air_id = row.Cells[1].Text;
                SqlCommand command = new SqlCommand("Select * from AircraftsSchedule where AircraftId = '" + air_id + "'", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    shedule_id.InnerText = reader["AircraftId"].ToString();
                    air_name.InnerText = reader["AircraftName"].ToString();
                    cust_ok.Visible = true;
                    cust_ok.InnerText = reader["AircraftName"].ToString();
                    air_sits.InnerText = reader["TotalSits"].ToString();
                    air_from.InnerText = reader["PlaceFrom"].ToString();
                    air_to.InnerText = reader["PlaceTo"].ToString();
                    air_trav_day.InnerText = reader["TravelDay"].ToString();
                    air_start_day.InnerText = reader["StartedDate"].ToString();
                    air_time.InnerText = reader["MoveTime"].ToString();
                }    
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        protected void save_schedule_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                string air_name = sch_aircraft_name.Value;
                string air_from = sch_place_from.Value;
                string air_to = sch_plate_to.Value;
                string air_travday = sch_trav_day.Value;
                string air_time = sch_time.Value;
                string air_id = sch_id.Value;
                int air_day_num;

                switch (air_travday)
                {
                    case "Mondays":
                        air_day_num = 1;
                        break;
                    case "Tuesdays":
                        air_day_num = 2;
                        break;
                    case "Wednesdays":
                        air_day_num = 3;
                        break;
                    case "Thursdays":
                        air_day_num = 4;
                        break;
                    case "Fridays":
                        air_day_num = 5;
                        break;
                    case "Saturdays":
                        air_day_num = 6;
                        break;
                    case "Sundays":
                        air_day_num = 7;
                        break;
                    default:
                        air_day_num = 0;
                        break;
                }
                string air_total_sits = null;
                string air_vip = null;
                string air_business = null;
                string air_economic = null;
                string air_start_date = null;
                SqlCommand checkCommand = new SqlCommand("SELECT * FROM Aircrafts WHERE AircraftName = '" + air_name + "'", connection);
                connection.Open();
                SqlDataReader reader = checkCommand.ExecuteReader();
                int result = 0;
                while (reader.Read())
                {
                    result++;
                    air_total_sits = reader["TotalSits"].ToString();
                    air_vip = reader["VIP"].ToString();
                    air_business = reader["BusinessClass"].ToString();
                    air_economic = reader["EconomyClass"].ToString();
                    air_start_date = reader["StartedDate"].ToString();
                    
                }
                if (result >= 1)
                {
                    //reader.Close();
                    connection.Close();
                    SqlCommand insertCommand = new SqlCommand("INSERT INTO AircraftsSchedule (AircraftId, AircraftName, TotalSits, VIP, BusinessClass, EconomyClass, StartedDate, TravelDay, PlaceFrom, PlaceTo, MoveTime, DayNum) VALUES ('" + air_id + "','" + air_name + "','" + air_total_sits + "','" + air_vip + "','" + air_business + "','" + air_economic + "','" + air_start_date + "','" + air_travday + "','" + air_from + "','" + air_to + "','" + air_time + "','" + air_day_num + "')", connection);
                    connection.Open();
                    insertCommand.ExecuteNonQuery();
                    Aircraft_Schedules();
                    Print_Schedules();
                    cust_fail.Visible = false;
                    cust_ok.Visible = true;
                    cust_ok.InnerText = "Aircraft Schedule Saved";
                }
                else
                {
                    cust_fail.Visible = true;
                    cust_ok.Visible = false;
                    cust_fail.InnerText = "Unknown Aircraft Name";
                }
            }
            catch(Exception ex) { Response.Write(ex); }
            finally { connection.Close(); }
        }

        protected void delete_schedule_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                int sched_id = Convert.ToInt32(shedule_id.InnerText);
                SqlCommand command = new SqlCommand("Delete from AircraftsSchedule where AircraftId = '" + sched_id + "'", connection);
                connection.Open();
                command.ExecuteNonQuery();
                Aircraft_Schedules();
                Print_Schedules();
                cust_fail.Visible = false;
                cust_ok.Visible = true;
                cust_ok.InnerText = "Aircraft Schedule Deleted";
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