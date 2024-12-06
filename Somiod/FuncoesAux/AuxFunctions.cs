using Somiod.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Somiod.FuncoesAux
{
    public class AuxFunctions : ApiController
    {
        string connectionString = Properties.Settings.Default.ConnString;
        public Application SelectBDApplication(string app_name, SqlConnection conn)
        {
            Application app = null;

            //ir procurar a aplication
            string querySelect = "SELECT * FROM application where name = @name_app";
            SqlCommand commandSelect = new SqlCommand(querySelect, conn);
            commandSelect.Parameters.AddWithValue("@name_app", app_name);
            SqlDataReader reader = commandSelect.ExecuteReader();
            while (reader.Read())
            {
                //Criar produto
                app = new Application
                {
                    id = (int)reader["Id"],
                    name = (string)reader["Name"],
                    creation_datetime = reader.GetDateTime(reader.GetOrdinal("Creation_datetime")),
                    res_type = "application",
                };
            }
            reader.Close();

            return app;
        }

        public Container SelectBDContainer(string cont_name, SqlConnection conn)
        {
            Container container = null;

            //ir procurar a aplication
            string querySelect = "SELECT * FROM container where name = @cont_name";
            SqlCommand commandSelect = new SqlCommand(querySelect, conn);
            commandSelect.Parameters.AddWithValue("@cont_name", cont_name);
            SqlDataReader reader = commandSelect.ExecuteReader();
            while (reader.Read())
            {
                //Criar produto
                container = new Container
                {
                    id = (int)reader["Id"],
                    name = (string)reader["Name"],
                    parent = (int)reader["Parent"],
                    creation_datetime = reader.GetDateTime(reader.GetOrdinal("Creation_datetime")),
                    res_type = "container",
                };
            }
            reader.Close();

            return container;
        }

        public Record SelectBDRecord(string record_name, SqlConnection conn)
        {
            Record record = null;

            //ir procurar a aplication
            string querySelect = "SELECT * FROM record where name = @record_name";
            SqlCommand commandSelect = new SqlCommand(querySelect, conn);
            commandSelect.Parameters.AddWithValue("@record_name", record_name);
            SqlDataReader reader = commandSelect.ExecuteReader();
            while (reader.Read())
            {
                //Criar produto
                record = new Record
                {
                    id = (int)reader["Id"],
                    name = (string)reader["Name"],
                    parent = (int)reader["Parent"],
                    creation_datetime = reader.GetDateTime(reader.GetOrdinal("Creation_datetime")),
                    content = (string)reader["Content"],
                    res_type = "record",
                };
            }
            reader.Close();

            return record;
        }

        public Notification SelectBDNotification(string notification_name, SqlConnection conn)
        {
            Notification notification = null;

            //ir procurar a aplication
            string querySelect = "SELECT * FROM notification where name = @notification_name";
            SqlCommand commandSelect = new SqlCommand(querySelect, conn);
            commandSelect.Parameters.AddWithValue("@notification_name", notification_name);
            SqlDataReader reader = commandSelect.ExecuteReader();
            while (reader.Read())
            {
                //Criar produto
                notification = new Notification
                {
                    id = (int)reader["Id"],
                    name = (string)reader["Name"],
                    parent = (int)reader["Parent"],
                    creation_datetime = reader.GetDateTime(reader.GetOrdinal("Creation_datetime")),
                    @event = (byte)reader["Event"],
                    endpoint = (string)reader["Endpoint"],
                    enabled = (bool)reader["Enabled"] == true ? 1 : 0,
                    res_type = "notification"
                };
            }
            reader.Close();

            return notification;
        }

        public Boolean CheckNameExist(string name, SqlConnection conn)
        {
            if (SelectBDApplication(name, conn) != null || SelectBDContainer(name, conn) != null
                || SelectBDRecord(name, conn) != null || SelectBDNotification(name, conn) != null)
            {
                return true;
            }

            return false;
        }

        public IHttpActionResult GetApplicationName()
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string queryApplication = "SELECT Name FROM application;";
                    SqlCommand sqlCommandContainer = new SqlCommand(queryApplication, connection);

                    using (SqlDataReader reader = sqlCommandContainer.ExecuteReader())
                    {
                        List<String> applicationNames = new List<String>();
                        while (reader.Read())
                        {
                            String name = reader["Name"].ToString();
                            applicationNames.Add(name);
                        }
                        if (applicationNames.Count > 0)
                        {
                            return Ok(applicationNames);


                        }


                        return NotFound();

                    }

                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }

        }

        public IHttpActionResult GetContainerName(String app_name)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Application app = SelectBDApplication(app_name, connection);
                    string queryContainer = "SELECT Name FROM container WHERE Parent= @parentID;";
                    SqlCommand sqlCommandContainer = new SqlCommand(queryContainer, connection);
                    sqlCommandContainer.Parameters.AddWithValue("@parentID", app.id);

                    using (SqlDataReader reader = sqlCommandContainer.ExecuteReader())
                    {
                        List<String> containerNames = new List<String>();
                        while (reader.Read())
                        {
                            String name = reader["Name"].ToString();
                            containerNames.Add(name);
                        }
                        return Ok(containerNames);
                    }

                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }

        }

        public IHttpActionResult GetRecordName(String app_name)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Application app = SelectBDApplication(app_name, connection);
                    string queryContainer = "SELECT Id FROM container WHERE Parent= @parentID;";
                    SqlCommand sqlCommandContainer = new SqlCommand(queryContainer, connection);
                    sqlCommandContainer.Parameters.AddWithValue("@parentID", app.id);


                    List<int> containerIds = new List<int>();
                    using (SqlDataReader containerReader = sqlCommandContainer.ExecuteReader())
                    {
                        while (containerReader.Read())
                        {
                            containerIds.Add((int)containerReader["Id"]);
                        }
                    }

                    if (!containerIds.Any())
                    {
                        return NotFound(); // No containers found for the application
                    }


                    List<string> recordNames = new List<string>();
                    string recordQuery = "SELECT Name FROM record WHERE Parent = @container_id";

                    foreach (int containerId in containerIds)
                    {
                        SqlCommand recordCommand = new SqlCommand(recordQuery, connection);
                        recordCommand.Parameters.AddWithValue("@container_id", containerId);

                        using (SqlDataReader recordReader = recordCommand.ExecuteReader())
                        {
                            while (recordReader.Read())
                            {
                                recordNames.Add((string)recordReader["Name"]);
                            }
                        }
                    }

                    if (!recordNames.Any())
                    {
                        return NotFound(); // No records found for the application's containers
                    }

                    return Ok(recordNames);

                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult GetNotificationName(String app_name)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Application app = SelectBDApplication(app_name, connection);
                    string queryContainer = "SELECT Id FROM container WHERE Parent= @parentID;";
                    SqlCommand sqlCommandContainer = new SqlCommand(queryContainer, connection);
                    sqlCommandContainer.Parameters.AddWithValue("@parentID", app.id);


                    List<int> containerIds = new List<int>();
                    using (SqlDataReader containerReader = sqlCommandContainer.ExecuteReader())
                    {
                        while (containerReader.Read())
                        {
                            containerIds.Add((int)containerReader["Id"]);
                        }
                    }

                    if (!containerIds.Any())
                    {
                        return NotFound();

                    }


                    List<string> notificationNames = new List<string>();
                    string recordQuery = "SELECT Name FROM notification WHERE Parent = @container_id";

                    foreach (int containerId in containerIds)
                    {
                        SqlCommand recordCommand = new SqlCommand(recordQuery, connection);
                        recordCommand.Parameters.AddWithValue("@container_id", containerId);

                        using (SqlDataReader recordReader = recordCommand.ExecuteReader())
                        {
                            while (recordReader.Read())
                            {
                                notificationNames.Add((string)recordReader["Name"]);
                            }
                        }
                    }

                    if (!notificationNames.Any())
                    {
                        return NotFound(); // No records found for the application's containers
                    }

                    return Ok(notificationNames);

                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
