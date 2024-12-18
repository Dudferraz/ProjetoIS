using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Somiod.Models;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using Newtonsoft.Json.Linq;
using System.Runtime.Remoting.Messaging;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
//using MQTTnet;
//using MQTTnet.Client;
using Somiod.FuncoesAux;
using System.Threading.Tasks;
using RestSharp;
using System.Text;

namespace Somiod.Controllers
{


    public class SomiodController : ApiController
    {
        string connectionString = Properties.Settings.Default.ConnString;
        AuxFunctions funAuxiliares = new AuxFunctions();

        [HttpGet]
        [Route("api/somiod")]
        public IHttpActionResult GetAllApplications() //antonio
        {
            if (Request.Headers.Contains("somiod-locate"))
            {
                if (Request.Headers.GetValues("somiod-locate").Contains("application"))
                {

                    //return funAuxiliares.GetApplicationName();
                    return GetApplicationName();
                }

                if (Request.Headers.GetValues("somiod-locate").Count() > 0)
                {

                    return BadRequest("Invalid Paramenter in somiod-locate");
                }
            }



            try
            {
                List<Application> apps = new List<Application>();
                string query = "SELECT * FROM application;";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand commandSelect = new SqlCommand(query, conn);
                    SqlDataReader reader = commandSelect.ExecuteReader();
                    while (reader.Read())
                    {
                        //Criar produto
                        Application app = new Application
                        {
                            id = (int)reader["Id"],
                            name = (string)reader["Name"],
                            creation_datetime = reader.GetDateTime(reader.GetOrdinal("Creation_datetime")),
                            res_type = "application",
                        };

                        apps.Add(app);
                    }
                    reader.Close();

                    if (apps.Count == 0)
                    {
                        return NotFound();
                    }

                    return Ok(apps);

                }
            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("api/somiod/{app_name}")]
        public IHttpActionResult GetApplication(string app_name) //ricardo
        {

            if (Request.Headers.Contains("somiod-locate"))
            {
                if (Request.Headers.GetValues("somiod-locate").Contains("container"))
                {

                    //return funAuxiliares.GetContainerName(app_name);
                    return GetContainerName(app_name);
                }

                if (Request.Headers.GetValues("somiod-locate").Contains("record"))
                {

                    //return funAuxiliares.GetRecordName(app_name);
                    return GetRecordName(app_name);
                }

                if (Request.Headers.GetValues("somiod-locate").Contains("notification"))
                {

                    //return funAuxiliares.GetNotificationName(app_name);
                    return GetNotificationName(app_name);
                }

                if (Request.Headers.GetValues("somiod-locate").Count() > 0)
                {

                    return BadRequest("Invalid Paramenter in somiod-locate");
                }
            }

            try
            {


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    //Application app = funAuxiliares.SelectBDApplication(app_name, connection);
                    Application app = SelectBDApplication(app_name, connection);

                    if (app == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Ok(app);
                    }
                }
            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("api/somiod/{app_name}/{cont_name}")]
        public IHttpActionResult GetContainer(string app_name, string cont_name) //antonio
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //Application app = funAuxiliares.SelectBDApplication(app_name, connection); //procura application
                    Application app = SelectBDApplication(app_name, connection);
                    Console.Write(app);

                    if (app == null)
                    {
                        return NotFound();
                    }

                    Container cont = null;
                    //cont = funAuxiliares.SelectBDContainer(cont_name, connection);
                    cont = SelectBDContainer(cont_name, connection);
                    if (cont == null)
                    {
                        return NotFound();
                    }

                    if (cont.parent != app.id)
                    {
                        return NotFound();
                    }



                    return Ok(cont);
                }
            }
            catch (Exception)
            {

                return InternalServerError();
            }

        }

        [HttpGet]
        [Route("api/somiod/{app_name}/{cont_name}/record/{data_name}")]
        public IHttpActionResult GetRecord(String app_name, String cont_name, String data_name) //ricardo
        {

            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //Application app = funAuxiliares.SelectBDApplication(app_name, connection);
                    Application app = SelectBDApplication(app_name, connection);//procura application
                    Console.Write(app);
                    if (app == null)
                    {
                        return NotFound();
                    }

                    Container cont = null;
                    //cont = funAuxiliares.SelectBDContainer(cont_name, connection);
                    cont = SelectBDContainer(cont_name, connection);
                    if (cont == null)
                    {
                        return NotFound();
                    }

                    if (cont.parent != app.id)
                    {
                        return NotFound();
                    }

                    Record record = null;
                    //record = funAuxiliares.SelectBDRecord(data_name, connection);
                    record = SelectBDRecord(data_name, connection);
                    if (record == null)
                    {
                        return NotFound();
                    }
                    if (record.parent != cont.id)
                    {
                        return NotFound();
                    }

                    return Ok(record);
                 

                }
            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("api/somiod/{app_name}/{cont_name}/notif/{data_name}")]
        public IHttpActionResult GetNotification(string app_name, string cont_name, string data_name) //antonio
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //Application app = funAuxiliares.SelectBDApplication(app_name, connection); //procura application
                    Application app = SelectBDApplication(app_name, connection);
                    Console.Write(app);

                    if (app == null)
                    {
                        return NotFound();
                    }

                    Container cont = null;
                    //cont = funAuxiliares.SelectBDContainer(cont_name, connection);
                    cont = SelectBDContainer(cont_name, connection);
                    if (cont == null)
                    {
                        return NotFound();
                    }

                    if (cont.parent != app.id)
                    {
                        return NotFound();
                    }

                    Notification notification = null;
                    //notification = funAuxiliares.SelectBDNotification(data_name, connection);
                    notification = SelectBDNotification(data_name, connection);
                    if (notification == null)
                    {
                        return NotFound();
                    }

                    if (notification.parent != cont.id)
                    {
                        return NotFound();
                    }

                    return Ok(notification);


                }
            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("api/somiod")]
        //o container é como exemplo mudar depois
        public IHttpActionResult PostApplication([FromBody] Application value) //ricardo
        {
            try
            {
                string query = "INSERT INTO application(name, creation_datetime ) VALUES (@name, @date);";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    bool nomeRepetido = false;
                    int maxId = 0;
                    string nome_app_mudado = null;
                    connection.Open();
                    //if (funAuxiliares.CheckNameExist(value.name, connection))
                    if (CheckNameExist(value.name, connection))
                    {
                        nomeRepetido = true;
                    }


                    SqlCommand sqlCommand = new SqlCommand(query, connection);

                    if (nomeRepetido)
                    {
                        string querySelect = "SELECT MAX(Id) AS ID FROM application";
                        SqlCommand commandSelect = new SqlCommand(querySelect, connection);
                        SqlDataReader reader = commandSelect.ExecuteReader();
                        while (reader.Read())
                        {
                            maxId = (int)reader["ID"];
                        }
                        reader.Close();
                        nome_app_mudado = value.name + "_application_" + maxId;
                        sqlCommand.Parameters.AddWithValue("@name", nome_app_mudado);
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@name", value.name);
                    }

                    
                    
                    sqlCommand.Parameters.AddWithValue("@date", DateTime.Now);


                    int rows = sqlCommand.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        /*string queryApp = "SELECT * FROM application WHERE Name= @name;";
                        SqlCommand commandSelectApp = new SqlCommand(queryApp, connection);
                        commandSelectApp.Parameters.AddWithValue("@name", nome_app_mudado);
                        SqlDataReader reader = commandSelectApp.ExecuteReader();
                        while (reader.Read())
                        {
                            maxId = (int)reader["ID"];
                        }
                        reader.Close();*/
                        Application app_nova = SelectBDApplication(nomeRepetido == true ? nome_app_mudado : value.name, connection);
                        return Ok(app_nova);
                    }
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/somiod/{app_name}")]
        //o container é como exemplo mudar depois
        public IHttpActionResult PostContainer(string app_name, [FromBody] Container value) //antonio
        {
            try
            {

                string query = "Insert Into container(name, creation_datetime, parent) Values (@name, @date, @parent);";
                Application app = null;
                Console.WriteLine(value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    bool nomeRepetido = false;
                    int maxId = 0;
                    string nome_real = null;

                    conn.Open();

                    //if (funAuxiliares.CheckNameExist(value.name, conn))
                    if (CheckNameExist(value.name, conn))
                    {
                        nomeRepetido = true;
                    }

                    SqlCommand command = new SqlCommand(query, conn);


                    if (nomeRepetido)
                    {
                        string querySelect = "SELECT MAX(Id) AS ID FROM container";
                        SqlCommand commandSelect = new SqlCommand(querySelect, conn);
                        SqlDataReader reader = commandSelect.ExecuteReader();
                        while (reader.Read())
                        {
                            maxId = (int)reader["ID"];
                        }
                        reader.Close();
                        nome_real = value.name + "_container_" + maxId;
                        command.Parameters.AddWithValue("@name", nome_real);
                    }
                    else
                    {
                        nome_real = value.name;
                        command.Parameters.AddWithValue("@name", nome_real);
                    }

                    command.Parameters.AddWithValue("@date", DateTime.Now);

                    //app = funAuxiliares.SelectBDApplication(app_name, conn);
                    app = SelectBDApplication(app_name, conn);

                    if (app == null)
                    {
                        //a aplicação nao foi encontrada
                        return NotFound();
                    }

                    command.Parameters.AddWithValue("@parent", app.id);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Container cont_novo_mudado = SelectBDContainer(nome_real, conn);
                        return Ok(cont_novo_mudado);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            catch (Exception ex)
            {


                //return NotFound();
                return InternalServerError();
            }

        }

        [HttpPost]
        [Route("api/somiod/{app_name}/{cont_name}/notif")]
        //o container é como exemplo mudar depois
        public IHttpActionResult PostNotification(String app_name, String cont_name, [FromBody] Notification value) //ricardo, este ta com problemas
        {

            try
            {


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    bool nomeRepetido = false;
                    int maxId = 0;
                    string nome_real = null;

                    //if (funAuxiliares.CheckNameExist(value.name, connection))
                    if (CheckNameExist(value.name, connection))
                    {
                        nomeRepetido = true;
                    }

                    //Application app = funAuxiliares.SelectBDApplication(app_name, connection); //procura application
                    Application app = SelectBDApplication(app_name, connection);
                    Console.Write(app);
                    if (app == null)
                    {
                        return NotFound();
                    }

                    Container cont = null;
                    //cont = funAuxiliares.SelectBDContainer(cont_name, connection);
                    cont = SelectBDContainer(cont_name, connection);
                    if (cont == null)
                    {
                        return NotFound();
                    }

                    if (cont.parent != app.id)
                    {
                        return NotFound();
                    }


                    string query = "INSERT INTO notification(name, creation_datetime, parent, [event], endpoint, enabled ) VALUES (@name, @creation_datetime, @parent, @newEvent, @endpoint, @enabled);";


                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    Console.WriteLine("Endpoint: " + value);

                    if (nomeRepetido)
                    {
                        string querySelect = "SELECT MAX(Id) AS ID FROM notification";
                        SqlCommand commandSelect = new SqlCommand(querySelect, connection);
                        SqlDataReader reader = commandSelect.ExecuteReader();
                        while (reader.Read())
                        {
                            maxId = (int)reader["ID"];
                        }
                        reader.Close();
                        nome_real = value.name + "_notification_" + maxId;
                        sqlCommand.Parameters.AddWithValue("@name", nome_real);
                    }
                    else
                    {
                        nome_real = value.name;
                        sqlCommand.Parameters.AddWithValue("@name", nome_real);
                    }

                    sqlCommand.Parameters.AddWithValue("@creation_datetime", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@parent", cont.id);
                    sqlCommand.Parameters.AddWithValue("@newEvent", value.@event);
                    if (!string.IsNullOrEmpty(value.endpoint))
                    {
                        sqlCommand.Parameters.AddWithValue("@endpoint", value.endpoint);
                    }
                    else
                    {
                        // If no endpoint is provided, you can either set a default value or skip this parameter entirely.
                        // Example: Set it to an empty string if it's optional
                        sqlCommand.Parameters.AddWithValue("@endpoint", DBNull.Value);  // or use an empty string if you prefer: ""
                    }
                    //sqlCommand.Parameters.AddWithValue("@endpoint", value.Endpoint);
                    sqlCommand.Parameters.AddWithValue("@enabled", value.enabled);
                    int rows = sqlCommand.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        Notification notification = SelectBDNotification(nome_real, connection);
                        return Ok(notification);
                    }


                    return BadRequest();

                }
            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("api/somiod/{app_name}/{cont_name}/record")]
        //o container é como exemplo mudar depois
        public IHttpActionResult PostRecord(string app_name, string cont_name, [FromBody] Record value) //antonio
        {

            Console.WriteLine(value.name);

            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    bool nomeRepetido = false;
                    int maxId = 0;
                    string nome_real = null;

                    //if (funAuxiliares.CheckNameExist(value.name, connection))
                    if (CheckNameExist(value.name, connection))
                    {
                        nomeRepetido = true;
                    }

                    //Application app = funAuxiliares.SelectBDApplication(app_name, connection); //procura application
                    Application app = SelectBDApplication(app_name, connection); //procura application
                    Console.Write(app);
                    if (app == null)
                    {
                        return NotFound();
                    }

                    Container cont = null;
                    //cont = funAuxiliares.SelectBDContainer(cont_name, connection);
                    cont = SelectBDContainer(cont_name, connection);
                    if (cont == null)
                    {
                        return NotFound();
                    }

                    if (cont.parent != app.id)
                    {
                        return NotFound();
                    }


                    string query = "INSERT INTO record(name, content, creation_datetime, parent ) VALUES (@name, @content, @creation_datetime, @parent);";


                    SqlCommand sqlCommand = new SqlCommand(query, connection);

                    if (nomeRepetido)
                    {
                        string querySelect = "SELECT MAX(Id) AS ID FROM record";
                        SqlCommand commandSelect = new SqlCommand(querySelect, connection);
                        SqlDataReader reader = commandSelect.ExecuteReader();
                        while (reader.Read())
                        {
                            maxId = (int)reader["ID"];
                        }
                        reader.Close();
                        nome_real = value.name + "_record_" + maxId;
                        sqlCommand.Parameters.AddWithValue("@name", nome_real);
                    }
                    else
                    {
                        nome_real = value.name;
                        sqlCommand.Parameters.AddWithValue("@name", value.name);
                    }

                    sqlCommand.Parameters.AddWithValue("@creation_datetime", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@parent", cont.id);
                    sqlCommand.Parameters.AddWithValue("@content", value.content);

                    int rows = sqlCommand.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        Record record_novo = SelectBDRecord(nome_real, connection);
                        PublishMessageAsync(value.content, cont_name, 1);
                        return Ok(record_novo);
                    }


                    return BadRequest();

                }
            }
            catch (Exception)
            {

                return InternalServerError();
            };
        }

        [HttpPut]
        [Route("api/somiod/{app_name}")]
        public IHttpActionResult PutApplication(string app_name, [FromBody] Application value) //ricardo
        {
            try
            {
                string query = "UPDATE application SET Name=@newName WHERE Name=@app_name;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //if (funAuxiliares.CheckNameExist(value.name, connection))
                    if (CheckNameExist(value.name, connection))
                    {
                        return BadRequest();
                    }

                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@newName", value.name);
                    sqlCommand.Parameters.AddWithValue("@creation_datetime", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@app_name", app_name);
                    //sqlCommand.Parameters.AddWithValue("@price", value.Price);
                    //sqlCommand.Parameters.AddWithValue("@idProd", id);

                    int rows = sqlCommand.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        return Ok();
                    }
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }

        }

        [HttpPut]
        [Route("api/somiod/{app_name}/{cont_name}")]
        public IHttpActionResult PutContainer(String app_name, String cont_name, [FromBody] Container value) //ricardo
        {

            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    if (value.name != null)
                    {
                        //if (funAuxiliares.CheckNameExist(value.name, connection))
                        if (CheckNameExist(value.name, connection))
                        {
                            return BadRequest();
                        }
                    }


                    //Application app = funAuxiliares.SelectBDApplication(app_name, connection); //procura application
                    Application app = SelectBDApplication(app_name, connection); //procura application
                    if (app == null)
                    {
                        return NotFound();
                    }

                    Container cont = null;
                    //cont = funAuxiliares.SelectBDContainer(cont_name, connection);
                    cont = SelectBDContainer(cont_name, connection);
                    if (cont == null)
                    {
                        return NotFound();
                    }

                    if (cont.parent != app.id)
                    {
                        return NotFound();
                    }



                    string query = "UPDATE container SET Name=@newName, Parent=@newParent WHERE Name=@cont_name;";
                    SqlCommand sqlCommand = new SqlCommand(query, connection);

                    //sqlCommand.Parameters.AddWithValue("@creation_datetime", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@cont_name", cont_name);
                    if (value.name != null)
                    {
                        sqlCommand.Parameters.AddWithValue("@newName", value.name);
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@newName", cont.name);
                    }


                    if (value.parent != 0)
                    {
                        sqlCommand.Parameters.AddWithValue("@newParent", value.parent);
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@newParent", cont.parent);
                    }



                    int rows = sqlCommand.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        return Ok();
                    }
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("api/somiod/{app_name}")]
        public IHttpActionResult DeleteApplication(String app_name) //ricardo
        {

            try
            {
                string query = "DELETE FROM application WHERE Name = @app_name";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@app_name", app_name);

                    int rows = sqlCommand.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        return Ok();
                    }
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("api/somiod/{app_name}/{cont_name}")]
        public IHttpActionResult DeleteContainer(string app_name, string cont_name) //antonio
        {

            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();


                    //Application app = funAuxiliares.SelectBDApplication(app_name, connection); //procura application
                    Application app = SelectBDApplication(app_name, connection); //procura application
                    if (app == null)
                    {
                        return NotFound();
                    }

                    Container cont = null;
                    //cont = funAuxiliares.SelectBDContainer(cont_name, connection);
                    cont = SelectBDContainer(cont_name, connection);
                    if (cont == null)
                    {
                        return NotFound();
                    }

                    if (cont.parent != app.id)
                    {
                        return NotFound();
                    }

                    string query = "DELETE FROM container WHERE Name = @cont_name";


                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@cont_name", cont_name);

                    int rows = sqlCommand.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        return Ok();
                    }
                    return InternalServerError();

                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        [HttpDelete]
        [Route("api/somiod/{app_name}/{cont_name}/notif/{data_name}")]
        public IHttpActionResult DeleteNotification(String app_name, String cont_name, String data_name)//ricardo
        {

            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();


                    //Application app = funAuxiliares.SelectBDApplication(app_name, connection); //procura application
                    Application app = SelectBDApplication(app_name, connection); //procura application
                    if (app == null)
                    {
                        return NotFound();
                    }

                    Container cont = null;
                    //cont = funAuxiliares.SelectBDContainer(cont_name, connection);
                    cont = SelectBDContainer(cont_name, connection);
                    if (cont == null)
                    {
                        return NotFound();
                    }

                    if (cont.parent != app.id)
                    {
                        return NotFound();
                    }

                    //Notification notification = funAuxiliares.SelectBDNotification(data_name, connection);
                    Notification notification = SelectBDNotification(data_name, connection);
                    if (notification == null)
                    {
                        return NotFound();
                    }

                    if (notification.parent != cont.id)
                    {
                        return NotFound();
                    }



                    string query = "DELETE FROM notification WHERE Name = @data_name";


                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@data_name", data_name);

                    int rows = sqlCommand.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        return Ok();
                    }
                    return NotFound();

                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("api/somiod/{app_name}/{cont_name}/record/{data_name}")]
        public IHttpActionResult DeleteRecord(string app_name, string cont_name, string data_name) //antonio
        {

            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();


                    //Application app = funAuxiliares.SelectBDApplication(app_name, connection); //procura application
                    Application app = SelectBDApplication(app_name, connection); //procura application
                    if (app == null)
                    {
                        return NotFound();
                    }

                    Container cont = null;
                    //cont = funAuxiliares.SelectBDContainer(cont_name, connection);
                    cont = SelectBDContainer(cont_name, connection);
                    if (cont == null)
                    {
                        return NotFound();
                    }

                    if (cont.parent != app.id)
                    {
                        return NotFound();
                    }

                    //Record record = funAuxiliares.SelectBDRecord(data_name, connection);
                    Record record = SelectBDRecord(data_name, connection);
                    if (record == null)
                    {
                        return NotFound();
                    }

                    if (record.parent != cont.id)
                    {
                        return NotFound();
                    }

                    string query = "DELETE FROM record WHERE Name = @data_name";


                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@data_name", data_name);

                    int rows = sqlCommand.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        PublishMessageAsync(record.content, cont_name, 2);
                        return Ok();
                    }
                    return InternalServerError();

                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

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

        private void PublishMessageAsync(String record, String contName, int eventType)
        {

            


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Container cont = SelectBDContainer(contName, connection);
                List<Notification> notifications = new List<Notification>();
                string recordQuery = "SELECT * FROM notification WHERE Parent = @container_id";


                SqlCommand recordCommand = new SqlCommand(recordQuery, connection);
                recordCommand.Parameters.AddWithValue("@container_id", cont.id);

                using (SqlDataReader recordReader = recordCommand.ExecuteReader())
                {
                    while (recordReader.Read())
                    {
                        Notification notification = new Notification
                        {
                            @event = (byte)recordReader["Event"],
                            endpoint = (string)recordReader["Endpoint"],
                        };
                        notifications.Add(notification);
                    }
                }



                if (!notifications.Any())
                {
                    return;
                }

                // Criação das listas separadas
                List<Notification> listMqtt = new List<Notification>();
                List<Notification> listHttp = new List<Notification>();

                // Separar os endpoints nas listas apropriadas
                foreach (Notification notification in notifications)
                {
                    if (eventType != notification.@event)
                    {
                        continue;
                    }

                    if (notification.endpoint.StartsWith("mqtt://"))
                    {
                        listMqtt.Add(notification); // Adiciona na lista MQTT
                    }
                    else if (notification.endpoint.StartsWith("http://"))
                    {
                        listHttp.Add(notification); // Adiciona na lista HTTP
                    }
                }


                // Processar mensagens para MQTT
                HashSet<string> processedEndpointsMqtt = new HashSet<string>();
                foreach (Notification notification in listMqtt)
                {

                    if (processedEndpointsMqtt.Contains(notification.endpoint))
                    {
                        continue; // Ignora endpoints duplicados
                    }

                    processedEndpointsMqtt.Add(notification.endpoint);

                    string cleanedEndpoint = notification.endpoint.Replace("mqtt://", "");

                    MqttClient mClient = new MqttClient(cleanedEndpoint);

                    mClient.Connect(Guid.NewGuid().ToString());

                    if (!mClient.IsConnected)
                    {
                        return;
                    }

                    var mensagem = Encoding.UTF8.GetBytes(record);
                    Console.WriteLine(mensagem);

                    if (mClient.IsConnected)
                    {
                        mClient.Publish(contName, mensagem);
                    }

          

                    mClient.Disconnect();


                }


                HashSet<string> processedEndpointsHttp = new HashSet<string>();
                foreach (Notification notification in listHttp)
                {
                    if (processedEndpointsHttp.Contains(notification.endpoint))
                    {
                        continue; // Ignora endpoints duplicados
                    }

                    processedEndpointsHttp.Add(notification.endpoint);


                    RestClient client = null;
                    client = new RestClient(notification.endpoint);

                    RestRequest request = new RestRequest("", Method.Post);
                    request.RequestFormat = DataFormat.Xml;

                    request.AddObject(record);

                    var response = client.Execute(request);

                    Console.WriteLine(response.Content);


                }
            }
        }
    }
}
