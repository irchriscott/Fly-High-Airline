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
    public partial class AirctaftsList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Aircraft_List();
            Print_Aircraft_List();
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

        public void Aircraft_List()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                SqlCommand command = new SqlCommand("SELECT Aircrafts.AircraftId as 'Aircraft ID', Aircrafts.AircraftName as 'Name', Aircrafts.TotalSits as 'Total Sits', Aircrafts.VIP, Aircrafts.BusinessClass as 'Business Class', Aircrafts.EconomyClass as 'Economy Class' FROM Aircrafts", connection);
                connection.Open();
                aircr_list.DataSource = command.ExecuteReader();
                aircr_list.DataBind();
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        public void Print_Aircraft_List()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                printed_time.InnerText = DateTime.Now.ToLongDateString();
                SqlCommand command = new SqlCommand("SELECT Aircrafts.AircraftName as 'Name', Aircrafts.TotalSits as 'Total Sits', Aircrafts.VIP, Aircrafts.BusinessClass as 'Business Class', Aircrafts.EconomyClass as 'Economy Class', Aircrafts.StartedDate as 'Started Date' FROM Aircrafts", connection);
                connection.Open();
                aircraft_list.DataSource = command.ExecuteReader();
                aircraft_list.DataBind();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
        }

        protected void save_airplaine_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                string id = id_craft.Value;
                string name = name_craft.Value;
                int sits = Convert.ToInt32(sits_craft.Value);
                int vip = Convert.ToInt32(vip_craft.Value);
                int business = Convert.ToInt32(business_craft.Value);
                int economy = Convert.ToInt32(economy_craft.Value);
                string date = start_date_craft.Value;

                int ch_sits = (economy - business) + (business - vip) + vip;

                if(ch_sits == sits)
                {
                    SqlCommand command = new SqlCommand("INSERT INTO Aircrafts (AircraftId, AircraftName, TotalSits, VIP, BusinessClass, EconomyClass, StartedDate) VALUES ('" + id + "','" + name + "','" + sits + "','" + vip + "','" + business + "','" + economy + "','" + date + "')", connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    cust_ok.Visible = true;
                    cust_fail.Visible = false;
                    cust_ok.InnerText = "Aircraft Added Successfully";
                }
                else
                {
                    cust_fail.Visible = true;
                    cust_ok.Visible = false;
                    cust_fail.InnerText = "Sits Dont Match";
                }
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        protected void delete_airplaine_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                int air_id = Convert.ToInt32(aircra_id.InnerText);
                SqlCommand command = new SqlCommand("Delete from Aircrafts where AircraftId = '" + air_id + "'", connection);
                connection.Open();
                command.ExecuteNonQuery();
                cust_fail.Visible = false;
                cust_ok.Visible = true;
                cust_ok.InnerText = "Aircraft Deleted Successfuly";
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        protected void aircr_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                GridViewRow row = aircr_list.SelectedRow;
                string craft_id = row.Cells[1].Text;
                SqlCommand command = new SqlCommand("Select * from Aircrafts where AircraftId = '" + craft_id + "'", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cust_ok.Visible = true;
                    cust_fail.Visible = false;
                    cust_ok.InnerText = reader["AircraftName"].ToString();
                    air_name.InnerText = reader["AircraftName"].ToString();
                    aircra_id.InnerText = reader["AircraftId"].ToString();
                    air_sits.InnerText = reader["TotalSits"].ToString();
                    air_date.InnerText = reader["StartedDate"].ToString();

                    int t_vip = (int)reader["VIP"];
                    int t_business = (int)reader["BusinessClass"];
                    int t_economy = (int)reader["EconomyClass"];

                    int economy = t_economy - t_business;
                    int business = t_business - t_vip;
                    int vip = t_economy - economy - business;
                    air_vip.InnerText = vip.ToString();
                    air_busi.InnerText = business.ToString();
                    air_eco.InnerText = economy.ToString();
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