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
    public partial class DailyRecord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Daily_Record();
            Schedule_Detail();
            Travel_Count();
            Tour_Count();
            Transfert_Count();
            Business_Count();
            Get_Session();
            if (!IsPostBack)
            {
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

        public void Daily_Record()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                //search_by_date.Value = DateTime.Now.ToShortDateString();
                cust_ok.InnerText = "Date : " + search_by_date.Value;
                DateTime date = DateTime.Parse(search_by_date.Value);
                int dayOfWeek = (int)date.DayOfWeek;

                SqlCommand command = new SqlCommand("select Passengers.DocumentId as 'Document ID', Passengers.FirstName as 'First Name', Passengers.LastName as 'Last Name', Passengers.Nationality, Services.ServiceName as 'Service Name', Passengers.RegistrationDate as 'Registration Date' from PassengerAir inner join Passengers on PassengerAir.DocumentId = Passengers.DocumentId inner join Services on PassengerAir.ServiceId = Services.ServiceId where PassengerAir.UseDate = '" + search_by_date.Value + "' and PassengerAir.Confirmation = 'yes' order by Passengers.FirstName asc", connection);
                connection.Open();
                daily_record_table.DataSource = command.ExecuteReader();
                daily_record_table.DataBind();
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        public void Schedule_Detail()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                //search_by_date.Value = DateTime.Now.ToShortDateString();
                DateTime date = DateTime.Parse(search_by_date.Value);
                int dayOfWeek = (int)date.DayOfWeek;

                SqlCommand command = new SqlCommand("select * from AircraftsSchedule where DayNum = '" + dayOfWeek + "'", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    air_name.InnerText = reader["AircraftName"].ToString();
                    air_from.InnerText = reader["PlaceFrom"].ToString();
                    air_to.InnerText = reader["PlaceTo"].ToString();
                    air_time.InnerText = reader["MoveTime"].ToString();
                }
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        public void Travel_Count()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                //int service = 0;
                string use_date = search_by_date.Value;
                SqlCommand trav_command = new SqlCommand("Select * from TravelService where UseDate = '" + use_date + "' and Confirmation = 'yes'", connection);
                connection.Open();
                SqlDataReader reader = trav_command.ExecuteReader();
                int travel_service = 0;
                while (reader.Read())
                {
                    travel_service++;
                }
                if(travel_service == 0)
                {
                    travel_service = 0;
                }
                air_travel.InnerText = travel_service.ToString();
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        public void Tour_Count()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                //int service = 1;
                string use_date = search_by_date.Value;
                SqlCommand tour_command = new SqlCommand("Select * from TourService where UseDate = '" + use_date + "' and Confirmation = 'yes'", connection);
                connection.Open();
                SqlDataReader reader = tour_command.ExecuteReader();
                int tour_service = 0;
                while (reader.Read())
                {
                    tour_service++;
                }
                if (tour_service == 0)
                {
                    tour_service = 0;
                }
                air_tour.InnerText = tour_service.ToString();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
        }

        public void Transfert_Count()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                //int service = 2;
                string use_date = search_by_date.Value;
                SqlCommand trans_command = new SqlCommand("select * from TransfertService where UseDate = '" + use_date + "'", connection);
                connection.Open();
                SqlDataReader reader = trans_command.ExecuteReader();
                int transfert_service = 0;
                while (reader.Read())
                {
                    transfert_service++;
                }
                if (transfert_service == 0)
                {
                    transfert_service = 0;
                }
                air_transfert.InnerText = transfert_service.ToString();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
        }

        public void Business_Count()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                //int service = 3;
                string use_date = search_by_date.Value;
                SqlCommand bus_command = new SqlCommand("Select * from BusinessService where UseDate = '" + use_date + "' and Confirmation = 'yes'", connection);
                connection.Open();
                SqlDataReader reader = bus_command.ExecuteReader();
                int business_service = 0;
                while (reader.Read())
                {
                    business_service++;
                }
                if (business_service == 0)
                {
                    business_service = 0;
                }
                air_business.InnerText = business_service.ToString();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
        }

        protected void log_out_Click(object sender, EventArgs e)
        {
            Session.Remove("admin");
            Response.Redirect("LogIn.aspx");
        }

        protected void search_now_Click(object sender, EventArgs e)
        {
            try
            {
                Daily_Record();
                Travel_Count();
                Tour_Count();
                Transfert_Count();
                Business_Count();
            }
            catch(Exception ex) { }
        }
    }
}