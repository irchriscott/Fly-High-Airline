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
    public partial class TodaySchedule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Today_Schedule();
            Travel_Service();
            Tour_Service();
            Transfert_Service();
            Business_Service();
            Passenger_Air();
            Get_Session();
            if (!IsPostBack)
            {
                cust_ok.Visible = false;
                confirm_reserv.Visible = false;
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

        public void Today_Schedule()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                string today = DateTime.Now.ToShortDateString();
                today_date.InnerText = today;
                rep_today.InnerText = today;
                DateTime date = DateTime.Parse(today);
                int dayOfWeek = (int)date.DayOfWeek;

                SqlCommand command = new SqlCommand("Select * from AircraftsSchedule WHERE DayNum = '" + dayOfWeek + "'", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string from = reader["PlaceFrom"].ToString();
                    string to = reader["PlaceTo"].ToString();
                    string aircraft = reader["AircraftName"].ToString();
                    today_airplaine.InnerText = aircraft;
                    rep_airplaine.InnerText = aircraft;
                    today_from.InnerText = from;
                    rep_from.InnerText = from;
                    today_to.InnerText = to;
                    rep_to.InnerText = to;
                }
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        public void Passenger_Air()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                string today = DateTime.Now.ToShortDateString();
                printed_time.InnerText = DateTime.Now.ToLongDateString();
                SqlCommand command = new SqlCommand("SELECT PassengerAir.DocumentId as 'Document ID', Passengers.FirstName as 'First Name', Passengers.LastName as 'Last Name', Passengers.Nationality, Services.ServiceName as 'Service', PassengerAir.SitNumber as 'Sit Number' FROM PassengerAir INNER JOIN Passengers ON PassengerAir.DocumentId = Passengers.DocumentId INNER JOIN Services ON PassengerAir.ServiceId = Services.ServiceId WHERE PassengerAir.UseDate = '" + today + "' AND PassengerAir.Confirmation = 'yes'  ORDER BY PassengerAir.SitNumber ASC", connection);
                connection.Open();
                airplaine_pass.DataSource = command.ExecuteReader();
                airplaine_pass.DataBind();
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }
        
        public void Travel_Service()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                string today = DateTime.Now.ToShortDateString();
                SqlCommand command = new SqlCommand("SELECT TravelService.DocumentId as 'Document ID', Passengers.FirstName as 'First Name', Passengers.LastName as 'Last Name', TravelService.Kid1 as 'First Kid', TravelService.Kid2 as 'Second Kid', TravelService.SitNumber as 'Sit Number', TravelService.Confirmation FROM TravelService INNER JOIN Passengers ON TravelService.DocumentId = Passengers.DocumentId WHERE TravelService.UseDate = '" + today + "'", connection);
                connection.Open();
                travel_service_customers.DataSource = command.ExecuteReader();
                travel_service_customers.DataBind();
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        public void Tour_Service()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                string today = DateTime.Now.ToShortDateString();
                SqlCommand command = new SqlCommand("SELECT TourService.DocumentId as 'Document ID', Passengers.FirstName as 'First Name', Passengers.LastName as 'Last Name', TourService.StartDate as 'Start Date', TourService.EndDate as 'End Date', TourService.SitNumber as 'Sit Number', TourService.Confirmation FROM TourService INNER JOIN Passengers ON TourService.DocumentId = Passengers.DocumentId WHERE TourService.UseDate = '" + today + "'", connection);
                connection.Open();
                tour_service_customers.DataSource = command.ExecuteReader();
                tour_service_customers.DataBind();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
        }

        public void Transfert_Service()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                string today = DateTime.Now.ToShortDateString();
                SqlCommand command = new SqlCommand("SELECT TransfertService.DocumentId as 'Document ID', Passengers.FirstName as 'First Name', Passengers.LastName as 'Last Name', Passengers.Nationality, TransfertService.SendFirstName as 'Receiver First Name', TransfertService.SendLastName as 'Receiver Last Name', TransfertService.SendAddress as 'Receiver Address', TransfertService.Confirmation FROM TransfertService INNER JOIN Passengers ON TransfertService.DocumentId = Passengers.DocumentId WHERE TransfertService.UseDate = '" + today + "'", connection);
                connection.Open();
                transfert_service_customers.DataSource = command.ExecuteReader();
                transfert_service_customers.DataBind();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
        }

        public void Business_Service()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                string today = DateTime.Now.ToShortDateString();
                SqlCommand command = new SqlCommand("SELECT BusinessService.DocumentId as 'Document ID', Passengers.FirstName as 'First Name', Passengers.LastName as 'Last Name', Passengers.Nationality, BusinessService.StartDate as 'Start Date', BusinessService.EndDate as 'End Date', BusinessService.SitNumber as 'Sit Number', BusinessService.Confirmation FROM BusinessService INNER JOIN Passengers ON BusinessService.DocumentId = Passengers.DocumentId WHERE BusinessService.UseDate = '" + today + "'", connection);
                connection.Open();
                business_service_customers.DataSource = command.ExecuteReader();
                business_service_customers.DataBind();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
        }

        protected void travel_service_customers_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                GridViewRow row = travel_service_customers.SelectedRow;
                string document_id = row.Cells[1].Text;
                string confirm = row.Cells[7].Text;
                SqlCommand command = new SqlCommand("SELECT Passengers.FirstName, Passengers.LastName, Passengers.NickName, Passengers.DocumentId, Passengers.Gender, Passengers.Nationality, Passengers.DateOdBirth, Passengers.PlaceOfBirth, Services.ServiceName, Passengers.EmailAddress, Passengers.CustomerAvatar FROM Passengers INNER JOIN Services ON Passengers.ServiceId = Services.ServiceId WHERE Passengers.DocumentId = '" + document_id + "'", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string db_first_name = reader["FirstName"].ToString();
                    string db_last_name = reader["LastName"].ToString();
                    string db_nick_name = reader["NickName"].ToString();
                    string db_document_id = reader["DocumentId"].ToString();
                    string db_gender = reader["Gender"].ToString();
                    string db_nationality = reader["Nationality"].ToString();
                    string db_service = reader["ServiceName"].ToString();
                    string db_dob = reader["DateOdBirth"].ToString();
                    string db_pod = reader["PlaceOfBirth"].ToString();
                    string db_email_address = reader["EmailAddress"].ToString();
                    byte[] image = (byte[])reader["CustomerAvatar"];

                    first_name.InnerText = db_first_name;
                    last_name.InnerText = db_last_name;
                    nick_name.InnerText = db_nick_name;
                    update_docid.InnerText = db_document_id;
                    update_fname.InnerText = db_first_name;
                    update_lname.InnerText = db_last_name;
                    update_nationality.InnerText = db_nationality;
                    cust_ok.Visible = true;
                    cust_ok.InnerText = "Doc. ID : " + db_document_id;
                    gender.InnerText = db_gender;
                    nationality.InnerText = db_nationality;
                    service.InnerText = db_service;
                    dob.InnerText = db_dob;
                    pob.InnerText = db_pod;
                    email_address.InnerText = db_email_address;
                    string imageConvert = Convert.ToBase64String(image);
                    select_customer_avatar.ImageUrl = "data:Image/png;base64," + imageConvert;
                    update_image.ImageUrl = "data:Image/png;base64," + imageConvert;
                }
                if (confirm == "NO")
                {
                    confirm_reserv.Visible = true;
                }
                else
                {
                    confirm_reserv.Visible = false;
                }
            }
            catch (Exception ex) { Response.Write(ex); }
            finally { connection.Close(); }
        }

        protected void tour_service_customers_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                GridViewRow row = tour_service_customers.SelectedRow;
                string document_id = row.Cells[1].Text;
                string confirm = row.Cells[7].Text;
                SqlCommand command = new SqlCommand("SELECT Passengers.FirstName, Passengers.LastName, Passengers.NickName, Passengers.DocumentId, Passengers.Gender, Passengers.Nationality, Passengers.DateOdBirth, Passengers.PlaceOfBirth, Services.ServiceName, Passengers.EmailAddress, Passengers.CustomerAvatar FROM Passengers INNER JOIN Services ON Passengers.ServiceId = Services.ServiceId WHERE Passengers.DocumentId = '" + document_id + "'", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string db_first_name = reader["FirstName"].ToString();
                    string db_last_name = reader["LastName"].ToString();
                    string db_nick_name = reader["NickName"].ToString();
                    string db_document_id = reader["DocumentId"].ToString();
                    string db_gender = reader["Gender"].ToString();
                    string db_nationality = reader["Nationality"].ToString();
                    string db_service = reader["ServiceName"].ToString();
                    string db_dob = reader["DateOdBirth"].ToString();
                    string db_pod = reader["PlaceOfBirth"].ToString();
                    string db_email_address = reader["EmailAddress"].ToString();
                    byte[] image = (byte[])reader["CustomerAvatar"];

                    first_name.InnerText = db_first_name;
                    last_name.InnerText = db_last_name;
                    nick_name.InnerText = db_nick_name;
                    update_docid.InnerText = db_document_id;
                    update_fname.InnerText = db_first_name;
                    update_lname.InnerText = db_last_name;
                    update_nationality.InnerText = db_nationality;
                    cust_ok.Visible = true;
                    cust_ok.InnerText = "Doc. ID : " + db_document_id;
                    gender.InnerText = db_gender;
                    nationality.InnerText = db_nationality;
                    service.InnerText = db_service;
                    dob.InnerText = db_dob;
                    pob.InnerText = db_pod;
                    email_address.InnerText = db_email_address;
                    string imageConvert = Convert.ToBase64String(image);
                    select_customer_avatar.ImageUrl = "data:Image/png;base64," + imageConvert;
                    update_image.ImageUrl = "data:Image/png;base64," + imageConvert;
                }
                if (confirm == "NO")
                {
                    confirm_reserv.Visible = true;
                }
                else
                {
                    confirm_reserv.Visible = false;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
        }

        protected void transfert_service_customers_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                GridViewRow row = transfert_service_customers.SelectedRow;
                string document_id = row.Cells[1].Text;
                string confirm = row.Cells[7].Text;
                SqlCommand command = new SqlCommand("SELECT Passengers.FirstName, Passengers.LastName, Passengers.NickName, Passengers.DocumentId, Passengers.Gender, Passengers.Nationality, Passengers.DateOdBirth, Passengers.PlaceOfBirth, Services.ServiceName, Passengers.EmailAddress, Passengers.CustomerAvatar FROM Passengers INNER JOIN Services ON Passengers.ServiceId = Services.ServiceId WHERE Passengers.DocumentId = '" + document_id + "'", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string db_first_name = reader["FirstName"].ToString();
                    string db_last_name = reader["LastName"].ToString();
                    string db_nick_name = reader["NickName"].ToString();
                    string db_document_id = reader["DocumentId"].ToString();
                    string db_gender = reader["Gender"].ToString();
                    string db_nationality = reader["Nationality"].ToString();
                    string db_service = reader["ServiceName"].ToString();
                    string db_dob = reader["DateOdBirth"].ToString();
                    string db_pod = reader["PlaceOfBirth"].ToString();
                    string db_email_address = reader["EmailAddress"].ToString();
                    byte[] image = (byte[])reader["CustomerAvatar"];

                    first_name.InnerText = db_first_name;
                    last_name.InnerText = db_last_name;
                    nick_name.InnerText = db_nick_name;
                    update_docid.InnerText = db_document_id;
                    update_fname.InnerText = db_first_name;
                    update_lname.InnerText = db_last_name;
                    update_nationality.InnerText = db_nationality;
                    cust_ok.Visible = true;
                    cust_ok.InnerText = "Doc. ID : " + db_document_id;
                    gender.InnerText = db_gender;
                    nationality.InnerText = db_nationality;
                    service.InnerText = db_service;
                    dob.InnerText = db_dob;
                    pob.InnerText = db_pod;
                    email_address.InnerText = db_email_address;
                    string imageConvert = Convert.ToBase64String(image);
                    select_customer_avatar.ImageUrl = "data:Image/png;base64," + imageConvert;
                    update_image.ImageUrl = "data:Image/png;base64," + imageConvert;
                }
                if (confirm == "NO")
                {
                    confirm_reserv.Visible = true;
                }
                else
                {
                    confirm_reserv.Visible = false;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
        }

        protected void business_service_customers_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                GridViewRow row = business_service_customers.SelectedRow;
                string document_id = row.Cells[1].Text;
                string confirm = row.Cells[7].Text;
                SqlCommand command = new SqlCommand("SELECT Passengers.FirstName, Passengers.LastName, Passengers.NickName, Passengers.DocumentId, Passengers.Gender, Passengers.Nationality, Passengers.DateOdBirth, Passengers.PlaceOfBirth, Services.ServiceName, Passengers.EmailAddress, Passengers.CustomerAvatar FROM Passengers INNER JOIN Services ON Passengers.ServiceId = Services.ServiceId WHERE Passengers.DocumentId = '" + document_id + "'", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string db_first_name = reader["FirstName"].ToString();
                    string db_last_name = reader["LastName"].ToString();
                    string db_nick_name = reader["NickName"].ToString();
                    string db_document_id = reader["DocumentId"].ToString();
                    string db_gender = reader["Gender"].ToString();
                    string db_nationality = reader["Nationality"].ToString();
                    string db_service = reader["ServiceName"].ToString();
                    string db_dob = reader["DateOdBirth"].ToString();
                    string db_pod = reader["PlaceOfBirth"].ToString();
                    string db_email_address = reader["EmailAddress"].ToString();
                    byte[] image = (byte[])reader["CustomerAvatar"];

                    first_name.InnerText = db_first_name;
                    last_name.InnerText = db_last_name;
                    nick_name.InnerText = db_nick_name;
                    update_docid.InnerText = db_document_id;
                    update_fname.InnerText = db_first_name;
                    update_lname.InnerText = db_last_name;
                    update_nationality.InnerText = db_nationality;
                    cust_ok.Visible = true;
                    cust_ok.InnerText = "Doc. ID : " + db_document_id;
                    gender.InnerText = db_gender;
                    nationality.InnerText = db_nationality;
                    service.InnerText = db_service;
                    dob.InnerText = db_dob;
                    pob.InnerText = db_pod;
                    email_address.InnerText = db_email_address;
                    string imageConvert = Convert.ToBase64String(image);
                    select_customer_avatar.ImageUrl = "data:Image/png;base64," + imageConvert;
                    update_image.ImageUrl = "data:Image/png;base64," + imageConvert;
                }
                if (confirm == "NO")
                {
                    confirm_reserv.Visible = true;
                }
                else
                {
                    confirm_reserv.Visible = false;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
        }

        protected void log_out_Click(object sender, EventArgs e)
        {
            Session.Remove("admin");
            Response.Redirect("LogIn.aspx");
        }

        protected void confirm_reservation_Click(object sender, EventArgs e)
        {

        }
    }
}