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
    public partial class SingleRecord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Get_Session();
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

        protected void log_out_Click(object sender, EventArgs e)
        {
            Session.Remove("admin");
            Response.Redirect("LogIn.aspx");
        }

        protected void search_now_Click(object sender, EventArgs e)
        {
            try
            {
                Single_Record();
                Get_CustomerID();
                Travel_Count();
                Tour_Count();
                Transfert_Count();
                Business_Count();
            }
            catch(Exception ex) { }
        }

        public void Single_Record()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                string doc_id = search_by_id.Value;
                SqlCommand command = new SqlCommand("Select Passengers.DocumentId as 'Document ID', Passengers.FirstName as 'First Name', Passengers.LastName as 'Last Name', Passengers.Nationality, Services.ServiceName as 'Service Name', PassengerAir.UseDate as 'Travel Date' from PassengerAir inner join Passengers on PassengerAir.DocumentId = Passengers.DocumentId inner join Services on PassengerAir.ServiceId = Services.ServiceId where PassengerAir.DocumentId = '" + doc_id + "' order by PassengerAir.UseDate asc", connection);
                connection.Open();
                single_record_table.DataSource = command.ExecuteReader();
                single_record_table.DataBind();
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        public void Get_CustomerID()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                string doc_id = search_by_id.Value;
                SqlCommand command = new SqlCommand("select * from Passengers where DocumentId = '" + doc_id + "'", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cust_fail.Visible = false;
                    cust_ok.Visible = true;
                    cust_ok.InnerText = "Doc. ID : " + reader["DocumentId"].ToString();
                    first_name.InnerText = reader["FirstName"].ToString();
                    last_name.InnerText = reader["LastName"].ToString();
                    nick_name.InnerText = reader["NickName"].ToString();
                    nationality.InnerText = reader["Nationality"].ToString();
                    registration_date.InnerText = DateTime.Parse(reader["RegistrationDate"].ToString()).ToShortDateString();
                    byte[] avatar = (byte[])reader["CustomerAvatar"];
                    string conv_image = Convert.ToBase64String(avatar);
                    customer_avatar.ImageUrl = "data:Image/png;base64," + conv_image;
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
                string doc_id = search_by_id.Value;
                SqlCommand trav_command = new SqlCommand("Select * from TravelService where DocumentId = '" + doc_id + "' and Confirmation = 'yes'", connection);
                connection.Open();
                SqlDataReader reader = trav_command.ExecuteReader();
                int trav_service = 0;
                while (reader.Read())
                {
                    trav_service++;
                }
                if (trav_service == 0)
                {
                    trav_service = 0;
                }
                travel_service.InnerText = trav_service.ToString();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
        }

        public void Tour_Count()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                //int service = 1;
                string doc_id = search_by_id.Value;
                SqlCommand tour_command = new SqlCommand("Select * from TourService where DocumentId = '" + doc_id + "' and Confirmation = 'yes'", connection);
                connection.Open();
                SqlDataReader reader = tour_command.ExecuteReader();
                int tou_service = 0;
                while (reader.Read())
                {
                    tou_service++;
                }
                if (tou_service == 0)
                {
                    tou_service = 0;
                }
                tour_service.InnerText = tou_service.ToString();
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
                string doc_id = search_by_id.Value;
                SqlCommand trans_command = new SqlCommand("select * from TransfertService where DocumentId = '" + doc_id + "'", connection);
                connection.Open();
                SqlDataReader reader = trans_command.ExecuteReader();
                int trans_service = 0;
                while (reader.Read())
                {
                    trans_service++;
                }
                if (trans_service == 0)
                {
                    trans_service = 0;
                }
                transfert_service.InnerText = trans_service.ToString();
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
                string doc_id = search_by_id.Value;
                SqlCommand bus_command = new SqlCommand("Select * from BusinessService where DocumentId = '" + doc_id + "' and Confirmation = 'yes'", connection);
                connection.Open();
                SqlDataReader reader = bus_command.ExecuteReader();
                int busi_service = 0;
                while (reader.Read())
                {
                    busi_service++;
                }
                if (busi_service == 0)
                {
                    busi_service = 0;
                }
                business_service.InnerText = busi_service.ToString();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
        }
    }
}