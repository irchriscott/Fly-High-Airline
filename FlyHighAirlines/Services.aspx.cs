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
    public partial class Services : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cust_ok.Visible = false;
            }

            Travel_Service_Customers();
            Tour_Service_Customers();
            Transfert_Service_Customers();
            Business_Service_Customers();
            Get_Session();
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

        protected void Travel_Service_Customers()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                int service_id = 0;
                SqlCommand command = new SqlCommand("SELECT DocumentId as 'Document ID', FirstName as 'First Name', LastName as 'Last Name', NickName as 'Nick Name', Gender, Nationality FROM Passengers WHERE ServiceId = '" + service_id + "'", connection);
                connection.Open();
                travel_service_customers.DataSource = command.ExecuteReader();
                travel_service_customers.DataBind();
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        protected void Tour_Service_Customers()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                int service_id = 1;
                SqlCommand command = new SqlCommand("SELECT DocumentId as 'Document ID', FirstName as 'First Name', LastName as 'Last Name', NickName as 'Nick Name', Gender, Nationality FROM Passengers WHERE ServiceId = '" + service_id + "'", connection);
                connection.Open();
                tour_service_customers.DataSource = command.ExecuteReader();
                tour_service_customers.DataBind();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
        }

        protected void Transfert_Service_Customers()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                int service_id = 2;
                SqlCommand command = new SqlCommand("SELECT DocumentId as 'Document ID', FirstName as 'First Name', LastName as 'Last Name', NickName as 'Nick Name', Gender, Nationality FROM Passengers WHERE ServiceId = '" + service_id + "'", connection);
                connection.Open();
                transfert_service_customers.DataSource = command.ExecuteReader();
                transfert_service_customers.DataBind();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
        }

        protected void Business_Service_Customers()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                int service_id = 3;
                SqlCommand command = new SqlCommand("SELECT DocumentId as 'Document ID', FirstName as 'First Name', LastName as 'Last Name', NickName as 'Nick Name', Gender, Nationality FROM Passengers WHERE ServiceId = '" + service_id + "'", connection);
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
                }
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        protected void tour_service_customers_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                GridViewRow row = tour_service_customers.SelectedRow;
                string document_id = row.Cells[1].Text;
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
    }
}