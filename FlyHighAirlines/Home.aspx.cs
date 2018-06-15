using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlyHighAirlines
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Customers_List();
            Customers_Print();
            Get_Session();
            //sess_username.InnerText = Session["admin"].ToString();
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
            catch(Exception ex) { Response.Write(ex); }
            finally { connection.Close(); }
        }

        public void Customers_List()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                SqlCommand command = new SqlCommand("select Passengers.DocumentId as 'Document ID', Passengers.FirstName as 'First Name', Passengers.LastName as 'Last Name', Services.ServiceName as 'Service', Passengers.Nationality as 'Nationality', Passengers.RegistrationDate as 'Registration Date' from Passengers inner join Services on Passengers.ServiceId = Services.ServiceId order by Passengers.RegistrationDate desc ", connection);
                connection.Open();
                customers_list.DataSource = command.ExecuteReader();
                customers_list.DataBind();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
        }

        public void Customers_Print()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                printed_time.InnerText = DateTime.Now.ToLongDateString();
                SqlCommand command = new SqlCommand("Select Passengers.DocumentId as 'Document ID', Passengers.FirstName as 'First Name', Passengers.LastName as 'Last Name', Passengers.NickName as 'Nick Name', Passengers.Gender, Passengers.Nationality, Services.ServiceName as 'Service', Passengers.RegistrationDate as 'Registration Date' from Passengers inner join Services on Passengers.ServiceId = Services.ServiceId order by Passengers.FirstName asc", connection);
                connection.Open();
                cust_report.DataSource = command.ExecuteReader();
                cust_report.DataBind();
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        protected void save_customer_Click(object sender, EventArgs e)
        {
            SqlConnection newConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                string document_cust = cust_document.Value;
                string fname_cust = cust_fname.Value;
                string lname_cust = cust_lname.Value;
                string nname_cust = cust_nname.Value;
                string pob_cust = cust_pod.Value;
                string nationality_cust = cust_nationality.Value;
                string email_cust = cust_email.Value;
                string gender_cust = cust_gender.Value;
                //string registration_date_cust = DateTime.Now.ToString("yyyy-mm-dd");
                string image_blob = image_captured.Src;

                HttpPostedFile postedFile = image_upload.PostedFile;
                string fileName = Path.GetFileName(postedFile.FileName);
                string fileExtention = Path.GetExtension(fileName);
                int filesize = postedFile.ContentLength;

                if (string.IsNullOrEmpty(document_cust.ToString()) || string.IsNullOrEmpty(fname_cust.ToString()) || string.IsNullOrEmpty(lname_cust.ToString()) || string.IsNullOrEmpty(cust_nationality.ToString()))
                {
                    cust_fail.Visible = true;
                    cust_fail.InnerText = "Fill All Fields Please";
                    cust_ok.Visible = false;
                }
                else
                {
                    if (fileExtention.ToLower() == ".jpg" || fileExtention.ToLower() == ".png" || fileExtention.ToLower() == ".gif" || fileExtention.ToLower() == ".jpeg")
                    {
                        byte[] image = null;
                        Stream stream = postedFile.InputStream;
                        BinaryReader binaryReader = new BinaryReader(stream);
                        image = binaryReader.ReadBytes((int)stream.Length);

                        string registration_date_cust = DateTime.Parse(cust_registration_date.Value).ToShortDateString();
                        string dob_cust = DateTime.Parse(cust_dob.Value).ToShortDateString();
                        int service_cust = Convert.ToInt32(cust_service.Value);

                        SqlCommand command = new SqlCommand("InsertNewPassenger", newConnection);
                        command.CommandType = CommandType.StoredProcedure;
                        SqlParameter parameterFirstname = new SqlParameter()
                        {
                            ParameterName = "@firstname",
                            Value  = cust_fname.Value
                        };
                        command.Parameters.Add(parameterFirstname);
                        SqlParameter parameterLastname = new SqlParameter()
                        {
                            ParameterName = "@lastname",
                            Value = cust_lname.Value
                        };
                        command.Parameters.Add(parameterLastname);
                        SqlParameter parameterNickname = new SqlParameter()
                        {
                            ParameterName = "@nickname",
                            Value = cust_nname.Value
                        };
                        command.Parameters.Add(parameterNickname);
                        SqlParameter parameterDocument = new SqlParameter()
                        {
                            ParameterName = "@documentid",
                            Value = cust_document.Value
                        };
                        command.Parameters.Add(parameterDocument);
                        SqlParameter parameterDob = new SqlParameter()
                        {
                            ParameterName = "@dateofbirth",
                            Value = cust_dob.Value
                        };
                        command.Parameters.Add(parameterDob);
                        SqlParameter parameterPob = new SqlParameter()
                        {
                            ParameterName = "@placeofbirth",
                            Value = cust_pod.Value
                        };
                        command.Parameters.Add(parameterPob);
                        SqlParameter parameterEmail = new SqlParameter()
                        {
                            ParameterName = "@email",
                            Value = cust_email.Value
                        };
                        command.Parameters.Add(parameterEmail);
                        SqlParameter parameterNat = new SqlParameter()
                        {
                            ParameterName = "@nationality",
                            Value = cust_nationality.Value
                        };
                        command.Parameters.Add(parameterNat);
                        SqlParameter parameterGender = new SqlParameter()
                        {
                            ParameterName = "@gender",
                            Value = cust_gender.Value
                        };
                        command.Parameters.Add(parameterGender);
                        SqlParameter parameterService = new SqlParameter()
                        {
                            ParameterName = "@service",
                            Value = cust_service.Value
                        };
                        command.Parameters.Add(parameterService);
                        SqlParameter parameterRegdate = new SqlParameter()
                        {
                            ParameterName = "@registration",
                            Value = cust_registration_date.Value
                        };
                        command.Parameters.Add(parameterRegdate);
                        SqlParameter parameterImage = new SqlParameter()
                        {
                            ParameterName = "@customeravatar",
                            Value = image
                        };
                        command.Parameters.Add(parameterImage);
                        newConnection.Open();
                        command.ExecuteNonQuery();

                        if (send_email.Checked)
                        {
                            MailMessage mailMessage = new MailMessage();
                            mailMessage.From = new MailAddress("irchristianscott@gmail.com");
                            mailMessage.To.Add(email_cust);
                            mailMessage.Subject = "Fly High Airlines Thanks !!!";
                            mailMessage.Body = "Hey " + fname_cust + " " + lname_cust + " " + nname_cust + ",<br/><br/> We want to thank you for having choosen our company Fly High Airlines for your your journey, tours, transferts and  business and we will make our best to give you the best of us.<br/><br/> Faithfuly Yours.<br/><br/> Admin Fly High Airlines";
                            mailMessage.IsBodyHtml = true;

                            SmtpClient smtpClient = new SmtpClient();
                            smtpClient.Host = "smtp.gmail.com";
                            System.Net.NetworkCredential networkCred = new System.Net.NetworkCredential();
                            networkCred.UserName = "irchristianscott@gmail.com";
                            networkCred.Password = "323639371998";
                            smtpClient.UseDefaultCredentials = true;
                            smtpClient.Credentials = networkCred;
                            smtpClient.Port = 587;
                            smtpClient.EnableSsl = true;
                            smtpClient.Send(mailMessage);

                            cust_fail.Visible = false;
                            cust_ok.Visible = true;
                            cust_ok.InnerText = "Customer Added Successfuly And Email Sent";
                        }
                        else
                        {
                            cust_fail.Visible = false;
                            cust_ok.Visible = true;
                            cust_ok.InnerText = "Customer Added Successfuly";
                        }
                        Customers_List();
                    }
                    else
                    {
                        cust_fail.Visible = true;
                        cust_fail.InnerText = "Only Images are Allowed";
                        cust_ok.Visible = false;
                    }
                }
            }
            catch(Exception ex) {
                //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + ex + "');", true);
                Response.Write(ex); 
            }
            finally { newConnection.Close(); }
        }

        protected void customers_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = customers_list.SelectedRow;
            string document_id = row.Cells[1].Text;
            cust_ok.Visible = true;
            cust_ok.InnerText = "Doc. ID : " + document_id;
            cust_fail.Visible = false;
            customer_document.InnerText = document_id;

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                SqlCommand sqlCommand = new SqlCommand("Select FirstName, LastName, Nationality, EmailAddress, ServiceId, CustomerAvatar from Passengers where DocumentId = '" + document_id + "'", connection);
                connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    string first_name = reader["FirstName"].ToString();
                    string last_name = reader["LastName"].ToString();
                    string service = reader["ServiceId"].ToString();
                    string nationality = reader["Nationality"].ToString();
                    string email = reader["EmailAddress"].ToString();
                    byte[] avatar = (byte[])reader["CustomerAvatar"];

                    select_fname.Text = first_name;
                    select_lname.Text = last_name;
                    service_id.InnerText = service;
                    update_docid.InnerText = document_id;
                    update_fname.InnerText = first_name;
                    update_lname.InnerText = last_name;
                    update_nationality.InnerText = nationality;
                    update_email.Value = email;

                    string imageConvert = Convert.ToBase64String(avatar);
                    update_image.ImageUrl = "data:Image/png;base64," + imageConvert;
                    select_customer_avatar.ImageUrl = "data:Image/png;base64," + imageConvert;

                    if(service == "0")
                    {
                        first_label.InnerText = "Reason";
                        second_label.InnerText = "Visa Possession";
                        third_label.InnerText = "Kid 1";
                        fourth_label.InnerText = "Kid 2";
                        fith_label.InnerText = "Sit Number";
                    }
                    else if(service == "1")
                    {
                        first_label.InnerText = "Start Date";
                        second_label.InnerText = "End Date";
                        third_label.InnerText = "Place to visit";
                        fourth_label.InnerText = "Residence";
                        fith_label.InnerText = "Sit Number";
                    }
                    else if(service == "2")
                    {
                        first_label.InnerText = "Rec First Name";
                        second_label.InnerText = "Rec Last Name";
                        third_label.InnerText = "Object Transfered";
                        fourth_label.InnerText = "Receiver Email";
                        fith_label.InnerText = "Receiver Address";
                    }
                    else if(service == "3")
                    {
                        first_label.InnerText = "Start Date";
                        second_label.InnerText = "End Date";
                        third_label.InnerText = "Product";
                        fourth_label.InnerText = "Quanity";
                        fith_label.InnerText = "Sit Number";
                    }
                }
            }
            catch(Exception ex) { }
            finally { connection.Close();  }
        }

        protected void Passenger_Air()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                string var_date_trav = traveldate.Value;
                string document_id = customer_document.InnerText;
                string var_service = service_id.InnerText;
                string sit_number = fith_var.Value;
                string confirm = "yes";
                SqlCommand command = new SqlCommand("INSERT INTO PassengerAir (DocumentId, SitNumber, UseDate, ServiceId, Confirmation) VALUES ('" + document_id + "', '" + sit_number + "', '" + var_date_trav + "', '" + var_service + "', '" + confirm + "')", connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        protected void Account_Save()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                string entry_date = traveldate.Value;
                string entry_service = service_id.InnerText;
                string entry_amount = amount_paid.Value;
                string entry_currency = currency.Value;

                SqlCommand insCommand = new SqlCommand("INSERT INTO Entry (EntryDate, ServiceId, Amount, Currency) VALUES ('" + entry_date + "', '" + entry_service + "', '" + entry_amount + "', '" + entry_currency + "')", connection);
                connection.Open();
                insCommand.ExecuteNonQuery();
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
        }

        protected void Save_Receipt(string document_id, string service, int service_id)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                string date = traveldate.Value;
                rec_date.InnerText = DateTime.Now.ToString();
                //DateTime trav_date = DateTime.ParseExact(date, "yy-mm-dd hh:mm:ss tt", new CultureInfo("en-Us", true), DateTimeStyles.NoCurrentDateDefault);
                DateTime trav_date = DateTime.Parse(date);
                int dayOfWeek = (int)trav_date.DayOfWeek;
                //Console.WriteLine(dayOfWeek)

                //string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
                 
                SqlCommand command = new SqlCommand("SELECT * FROM AircraftsSchedule WHERE DayNum = '" + dayOfWeek + "'", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string aircraft = reader["AircraftName"].ToString();
                    string from = reader["PlaceFrom"].ToString();
                    string to = reader["PlaceTo"].ToString();
                    string time = reader["MoveTime"].ToString();
                    
                    rec_fname.InnerText = select_fname.Text;
                    rec_lname.InnerText = select_lname.Text;
                    rec_documentid.InnerText = document_id;
                    rec_service.InnerText = service;
                    rec_airplaine.InnerText = aircraft;
                    rec_from.InnerText = from;
                    rec_to.InnerText = to;
                    rec_amountpaid.InnerText = amount_paid.Value + " " + currency.Value;
                    rec_travel_date.InnerText = traveldate.Value;

                    if(service_id == 2)
                    {
                        rec_class_text.InnerText = "Sent To";
                        rec_sit_text.InnerText = "Object Sent";
                        rec_class.InnerText = first_var.Value + " " + second_var.Value;
                        rec_sitnumber.InnerText = third_var.Value;
                    }
                    else
                    {
                        int sitnum = Convert.ToInt32(fith_var.Value);
                        External external = new External();
                        string sit_class = external.Airplaine_Class(aircraft, sitnum);
                        rec_class.InnerText = sit_class;
                        rec_sitnumber.InnerText = fith_var.Value;
                        rec_class_text.InnerText = "Class";
                        rec_sit_text.InnerText = "Sit Number";
                    }
                }
            }
            catch(Exception ex) { Response.Write(ex); }
            finally { connection.Close(); }
        }

        protected void save_service_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                string var_first = first_var.Value;
                string var_second = second_var.Value;
                string var_third = third_var.Value;
                string var_fourth = fourth_var.Value;
                string var_fith = fith_var.Value;
                string var_service = service_id.InnerText;
                string var_amount = amount_paid.Value;
                string var_currency = currency.Value;
                string var_date_trav = traveldate.Value;
                string document_id = customer_document.InnerText;
                string confirmation = "yes";
                int serv_id = Convert.ToInt32(var_service);

                if (var_service == "0")
                {
                    SqlCommand cmdCheck = new SqlCommand("SELECT * FROM PassengerAir WHERE SitNumber = '" + var_fith + "' AND UseDate = '" + var_date_trav + "'", connection);
                    connection.Open();
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    int result = 0;
                    while (reader.Read())
                    {
                        result = result + 1;
                    }
                    if (result >= 1)
                    {
                        cust_fail.Visible = true;
                        cust_fail.InnerText = "The sit is already taken";
                        cust_ok.Visible = false;
                    }
                    else
                    {
                        reader.Close();
                        SqlCommand cmdInsert = new SqlCommand("INSERT INTO TravelService (DocumentId, Reason, VisaPossesstion, Kid1, Kid2, SitNumber, Confirmation, AmountPaid, Currency, UseDate) VALUES ('" + document_id + "', '" + var_first + "', '" + var_second + "', '" + var_third + "', '" + var_fourth + "', '" + var_fith + "', '" + confirmation + "', '" + var_amount + "', '" + var_currency + "', '" + var_date_trav + "')", connection);
                        //connection.Open();
                        cmdInsert.ExecuteNonQuery();
                        Passenger_Air();
                        Account_Save();
                        Save_Receipt(document_id, "Travel Service", serv_id);
                        cust_fail.Visible = false;
                        cust_ok.InnerText = "Service Saved Successfuly";
                        cust_ok.Visible = true;
                    }
                }
                else if(var_service == "1")
                {
                    SqlCommand cmdCheck = new SqlCommand("SELECT * FROM PassengerAir WHERE SitNumber = '" + var_fith + "' AND UseDate = '" + var_date_trav + "'", connection);
                    connection.Open();
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    int result = 0;
                    while (reader.Read())
                    {
                        result = result + 1;
                    }
                    if (result >= 1)
                    {
                        cust_fail.Visible = true;
                        cust_fail.InnerText = "The sit is already taken";
                        cust_ok.Visible = false;
                    }
                    else
                    {
                        reader.Close();
                        SqlCommand cmdInsert = new SqlCommand("INSERT INTO TourService (DocumentId, StartDate, EndDate, PlaceVisite, Residence, SitNumber, Confirmation, AmountPaid, Currency, UseDate) VALUES ('" + document_id + "', '" + var_first + "', '" + var_second + "', '" + var_third + "', '" + var_fourth + "', '" + var_fith + "', '" + confirmation + "', '" + var_amount + "', '" + var_currency + "', '" + var_date_trav + "')", connection);
                        //connection.Open();
                        cmdInsert.ExecuteNonQuery();
                        Passenger_Air();
                        Account_Save();
                        Save_Receipt(document_id, "Tour Service", serv_id);
                        cust_fail.Visible = false;
                        cust_ok.InnerText = "Service Saved Successfuly";
                        cust_ok.Visible = true;
                    }
                }
                else if(var_service == "2")
                {
                    SqlCommand cmdInsert = new SqlCommand("INSERT INTO TransfertService (DocumentId, SendFirstName, SendLastName, ObjectSent, SendEmail, SendAddress, Confirmation, AmountPaid, Currency, UseDate) VALUES ('" + document_id + "', '" + var_first + "', '" + var_second + "', '" + var_third + "', '" + var_fourth + "', '" + var_fith + "', '" + confirmation + "', '" + var_amount + "', '" + var_currency + "', '" + var_date_trav + "')", connection);
                    connection.Open();
                    cmdInsert.ExecuteNonQuery();
                    Account_Save();
                    Save_Receipt(document_id, "Transfert Service", serv_id);
                    cust_fail.Visible = false;
                    cust_ok.InnerText = "Service Saved Successfuly";
                    cust_ok.Visible = true;

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("irchristianscott@gmail.com");
                    mailMessage.To.Add(var_fourth);
                    mailMessage.Subject = "Fly High Airlines : Reception from " + select_fname.Text + " " + select_lname.Text;
                    mailMessage.Body = "Hey Mr(Ms) " + var_first + " " + var_second + ",<br/><br/> Fly High Airlines has received object from " + select_fname.Text + " " + select_lname.Text + " which is (are) " + var_third + ". Please come and pick them to our office or tell us to come and deliver you them.<br/><br/> Faithfuly yours. <br/><br/>Fly High Airlines Admin";
                    mailMessage.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "smtp.gmail.com";
                    System.Net.NetworkCredential networkCred = new System.Net.NetworkCredential();
                    networkCred.UserName = "irchristianscott@gmail.com";
                    networkCred.Password = "323639371998";
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = networkCred;
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMessage);
                }
                else if(var_service == "3")
                {
                    SqlCommand cmdCheck = new SqlCommand("SELECT * FROM PassengerAir WHERE SitNumber = '" + var_fith + "' AND UseDate = '" + var_date_trav + "'", connection);
                    connection.Open();
                    SqlDataReader reader = cmdCheck.ExecuteReader();
                    int result = 0;
                    while (reader.Read())
                    {
                        result = result + 1;
                    }
                    if (result >= 1)
                    {
                        cust_fail.Visible = true;
                        cust_fail.InnerText = "The sit is already taken";
                        cust_ok.Visible = false;
                    }
                    else
                    {
                        reader.Close();
                        SqlCommand cmdInsert = new SqlCommand("INSERT INTO BusinessService (DocumentId, StartDate, EndDate, Products, Quantity, SitNumber, Confirmation, AmountPaid, Currency, UseDate) VALUES ('" + document_id + "', '" + var_first + "', '" + var_second + "', '" + var_third + "', '" + var_fourth + "', '" + var_fith + "', '" + confirmation + "', '" + var_amount + "', '" + var_currency + "', '" + var_date_trav + "')", connection);
                        //connection.Open();
                        cmdInsert.ExecuteNonQuery();
                        Passenger_Air();
                        Account_Save();
                        Save_Receipt(document_id, "Business Service", serv_id);
                        cust_fail.Visible = false;
                        cust_ok.InnerText = "Service Saved Successfuly";
                        cust_ok.Visible = true;
                    }
                }
            }
            catch(Exception ex) { Response.Write(ex); }
            finally { connection.Close(); }
        }

        protected void update_customer_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                string document_id = update_docid.InnerText;
                string email_address = update_email.Value;
                string service_id = update_service.Value;

                if (update_avatar.HasFile)
                {
                    HttpPostedFile postedFile = update_avatar.PostedFile;
                    string fileName = Path.GetFileName(postedFile.FileName);
                    string fileExtention = Path.GetExtension(fileName);
                    int filesize = postedFile.ContentLength;

                    if(fileExtention.ToLower() == ".jpg" || fileExtention.ToLower() == ".png" || fileExtention.ToLower() == ".jpeg" || fileExtention.ToLower() == ".gif")
                    {
                        byte[] image = null;
                        Stream stream = postedFile.InputStream;
                        BinaryReader binaryReader = new BinaryReader(stream);
                        image = binaryReader.ReadBytes((int)stream.Length);

                        SqlCommand updateCommand = new SqlCommand("UPDATE Passengers SET EmailAddress = '" + email_address + "', ServiceId = '" + service_id + "', CustomerAvatar = '" + image + "' WHERE DocumentId = '" + document_id + "'", connection);
                        connection.Open();
                        updateCommand.ExecuteNonQuery();
                        cust_fail.Visible = false;
                        cust_ok.Visible = true;
                        cust_ok.InnerText = "Passenger Updated !!!";
                        Customers_List();
                    }
                    else
                    {
                        cust_fail.Visible = true;
                        cust_ok.Visible = false;
                        cust_fail.InnerText = "Only Image are Allowed";
                    }
                }
                else
                {
                    SqlCommand updateCommand = new SqlCommand("UPDATE Passengers SET EmailAddress = '" + email_address + "', ServiceId = '" + service_id + "' WHERE DocumentId = '" + document_id + "'", connection);
                    connection.Open();
                    updateCommand.ExecuteNonQuery();
                    cust_fail.Visible = false;
                    cust_ok.Visible = true;
                    cust_ok.InnerText = "Passenger Updated !!!";
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