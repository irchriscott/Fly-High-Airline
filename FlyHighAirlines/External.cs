using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FlyHighAirlines
{
    public class External
    {
        string airplaine_class = null;
        public string Airplaine_Class(string airplaine, int sit_number)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
            try
            {
                SqlCommand command = new SqlCommand("SELECT VIP, BusinessClass, EconomyClass FROM Aircrafts WHERE AircraftName = '" + airplaine + "'", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int vip = (int)reader["VIP"];
                    int bus = (int)reader["BusinessClass"];
                    int eco = (int)reader["EconomyClass"];

                    if (sit_number <= vip)
                    {
                        airplaine_class = "VIP Class";
                    }
                    else if (vip > sit_number || sit_number <= bus)
                    {
                        airplaine_class = "Business Class";
                    }
                    else if (bus > sit_number || sit_number <= eco)
                    {
                        airplaine_class = "Economy Class";
                    }
                    else
                    {
                        
                    }
                }
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
            return airplaine_class;
        }
    }
}