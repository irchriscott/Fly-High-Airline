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
    public partial class BusinessService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Get_Session();
            Business_Service();
            if (!IsPostBack)
            {
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

        public void Business_Service()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                SqlCommand command = new SqlCommand("select Passengers.DocumentId as 'Document ID', Passengers.FirstName as 'First Name', Passengers.LastName as 'Last Name', Passengers.Nationality, BusinessService.Confirmation, BusinessService.UseDate as 'Travel Date' from BusinessService inner join Passengers on BusinessService.DocumentId = Passengers.DocumentId where BusinessService.Confirmation = 'yes' or BusinessService.Confirmation = 'YES' order by BusinessService.UseDate desc", connection);
                connection.Open();
                business_service_customers.DataSource = command.ExecuteReader();
                business_service_customers.DataBind();
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        protected void business_service_customers_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                GridViewRow row = business_service_customers.SelectedRow;
                string doc_id = row.Cells[1].Text;
                SqlCommand command = new SqlCommand("select BusinessService.*, Passengers.FirstName, Passengers.LastName, Passengers.Nationality, Passengers.CustomerAvatar from BusinessService inner join Passengers on BusinessService.DocumentId = Passengers.DocumentId where BusinessService.DocumentId = '"+doc_id+"'", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cust_ok.Visible = true;
                    cust_ok.InnerText = "Doc. ID : " + reader["DocumentId"].ToString();
                    first_name.InnerText = reader["FirstName"].ToString();
                    last_name.InnerText = reader["LastName"].ToString();
                    nationality.InnerText = reader["Nationality"].ToString();
                    start_date.InnerText = DateTime.Parse(reader["StartDate"].ToString()).ToShortDateString();
                    end_date.InnerText = DateTime.Parse(reader["EndDate"].ToString()).ToShortDateString();
                    product.InnerText = reader["Products"].ToString();
                    quantity.InnerText = reader["Quantity"].ToString();
                    travel_date.InnerText = DateTime.Parse(reader["UseDate"].ToString()).ToShortDateString();
                    Get_Schedule(reader["UseDate"].ToString());
                    byte[] image = (byte[])reader["CustomerAvatar"];
                    string convertImage = Convert.ToBase64String(image);
                    select_customer_avatar.ImageUrl = "data:Image/png;base64," + convertImage;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
        }

        public void Get_Schedule(string date)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                DateTime trav_date = DateTime.Parse(date);
                int dayOfWeek = (int)trav_date.DayOfWeek;
                SqlCommand command = new SqlCommand("select * from AircraftsSchedule where DayNum = '" + dayOfWeek + "'", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    from.InnerText = reader["PlaceFrom"].ToString();
                    to.InnerText = reader["PlaceTo"].ToString();
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

        protected void search_now_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                string date = search_by_date.Value;
                cust_ok.Visible = true;
                cust_ok.InnerText = "Date : " + date;
                SqlCommand command = new SqlCommand("select Passengers.DocumentId as 'Document ID', Passengers.FirstName as 'First Name', Passengers.LastName as 'Last Name', Passengers.Nationality, BusinessService.Confirmation, BusinessService.UseDate as 'Travel Date' from BusinessService inner join Passengers on BusinessService.DocumentId = Passengers.DocumentId where BusinessService.UseDate = '" + date + "' order by Passengers.FirstName asc", connection);
                connection.Open();
                business_service_customers.DataSource = command.ExecuteReader();
                business_service_customers.DataBind();
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }
    }
}