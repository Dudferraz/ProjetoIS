using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using RestSharp;
using DashboardForm.Models;

namespace DashboardForm
{
    public partial class Form1 : Form
    {
        public String contentAppName;
        string baseURI = Properties.Settings.Default.RestApi;
        RestClient client = null;

        public Form1()
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(baseURI))
            {
                MessageBox.Show("O Base URI não está configurado. Verifique as configurações.", "Erro");
                return;
            }

            client = new RestClient(baseURI);
        }

        //App Text box
        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
    
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            // Obter os nomes da aplicação, container, record e notificação das RichTextBoxes
            string appName = richTextBox2.Text.Trim();        // Nome da aplicação
            string contName = rtbContainer.Text.Trim();       // Nome do container (opcional)
            string recordName = rtbRecord.Text.Trim();        // Nome do record (opcional)
            string notifName = rtbNotification.Text.Trim();   // Nome da notificação (opcional)

            // Determinar o header somiod-locate com base na seleção da comboBox
            string somiodLocate = comboBoxHeaders.SelectedItem?.ToString()?.ToLower();

            // Validar o nome da aplicação
            if (string.IsNullOrEmpty(appName) && somiodLocate != "application")
            {
                MessageBox.Show("Por favor, insira o nome da aplicação.");
                return;
            }

            // Validar o Base URI
            if (string.IsNullOrEmpty(baseURI))
            {
                MessageBox.Show("O Base URI não está configurado. Verifique as configurações.", "Erro");
                return;
            }

     

            // Validar se somiod-locate não é app ou há campos adicionais preenchidos
            if (!string.IsNullOrEmpty(somiodLocate) && somiodLocate != "application" &&
                (!string.IsNullOrEmpty(contName) || !string.IsNullOrEmpty(recordName) || !string.IsNullOrEmpty(notifName)))
            {
                MessageBox.Show("Para usar este somiod-locate, apenas o nome da aplicação deve ser fornecido.", "Erro");
                return;
            }
            //validar se somiod-locate : app , não tem mais nada preenchido
            if (!string.IsNullOrEmpty(somiodLocate) && somiodLocate == "application" &&
                (!string.IsNullOrEmpty(contName) || !string.IsNullOrEmpty(recordName) || !string.IsNullOrEmpty(notifName) || !string.IsNullOrEmpty(appName)))
            {
                MessageBox.Show("Para usar o somiod-locate da Application não pode fornecer mais nada nas richTextBoxes", "Erro");
                return;
            }


            // Definir o endpoint com base nos inputs fornecidos
            string endpoint;
            if (!string.IsNullOrEmpty(notifName) && !string.IsNullOrEmpty(contName))
            {
                endpoint = $"api/somiod/{appName}/{contName}/notif/{notifName}"; // Endpoint para a notificação
            }
            else if (!string.IsNullOrEmpty(recordName) && !string.IsNullOrEmpty(contName))
            {
                endpoint = $"api/somiod/{appName}/{contName}/record/{recordName}"; // Endpoint para o record
            }
            else if (!string.IsNullOrEmpty(contName))
            {
                endpoint = $"api/somiod/{appName}/{contName}"; // Endpoint para o container
            }else if (!string.IsNullOrEmpty(appName))
            {
                endpoint = $"api/somiod/{appName}"; // Endpoint para a aplicação
            }
            else
            {
                endpoint = $"api/somiod"; // Endpoint default
            }

          

            // Criar a requisição GET
            RestRequest request = new RestRequest(endpoint, Method.Get);
            request.AddHeader("Accept", "application/xml");

            if (!string.IsNullOrEmpty(somiodLocate))
            {
                request.AddHeader("somiod-locate", somiodLocate); // Adicionar o header opcional
            } else
            {
                somiodLocate = "";
            }

            try
            {
                // Enviar a requisição
                var response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    try
                    {
                        // Carregar o XML retornado
                        string xmlContent = response.Content;
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(xmlContent);


                        XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);

                        // se houver opcoes do simiodLocate
                        if (somiodLocate != "")
                        {
                            namespaceManager.AddNamespace("default", "http://schemas.microsoft.com/2003/10/Serialization/Arrays");
                        }
                        else
                        {
                            // se não houver
                            namespaceManager.AddNamespace("default", "http://schemas.datacontract.org/2004/07/Somiod.Models");
                        }


                        // se tem headers opcionais: -------
                        if (somiodLocate.Equals("notification") || somiodLocate.Equals("record") || somiodLocate.Equals("container") || somiodLocate.Equals("application"))
                        {

                            XmlNodeList stringNodes = xmlDoc.SelectNodes("//default:string", namespaceManager);
                            MostrarLista(stringNodes);

                        }
                        // ---------------------------------


                        if (!string.IsNullOrEmpty(notifName))
                        {
                            // Extrair propriedades da notificação
                            XmlNode idNode = xmlDoc.SelectSingleNode("/default:Notification/default:id", namespaceManager);
                            XmlNode nameNode = xmlDoc.SelectSingleNode("/default:Notification/default:name", namespaceManager);
                            XmlNode dateNode = xmlDoc.SelectSingleNode("/default:Notification/default:creation_datetime", namespaceManager);
                            XmlNode eventNode = xmlDoc.SelectSingleNode("/default:Notification/default:event", namespaceManager);
                            XmlNode endpointNode = xmlDoc.SelectSingleNode("/default:Notification/default:endpoint", namespaceManager);
                            XmlNode enabledNode = xmlDoc.SelectSingleNode("/default:Notification/default:enabled", namespaceManager);
                            XmlNode parentNode = xmlDoc.SelectSingleNode("/default:Notification/default:parent", namespaceManager);

                            // Construir o texto das propriedades da notificação
                            string notifDetails = $"id: {idNode?.InnerText}\n" +
                                                  $"name: {nameNode?.InnerText}\n" +
                                                  $"creation date: {dateNode?.InnerText}\n" +
                                                  $"parent: {parentNode?.InnerText}\n" +
                                                  $"event: {eventNode?.InnerText}\n" +
                                                  $"endpoint: {endpointNode?.InnerText}\n" +
                                                  $"enabled: {enabledNode?.InnerText}";

                            rtbShow.Text = notifDetails;
                        }
                        else if (!string.IsNullOrEmpty(recordName))
                        {
                            // Extrair propriedades do record
                            XmlNode idNode = xmlDoc.SelectSingleNode("/default:Record/default:id", namespaceManager);
                            XmlNode nameNode = xmlDoc.SelectSingleNode("/default:Record/default:name", namespaceManager);
                            XmlNode dateNode = xmlDoc.SelectSingleNode("/default:Record/default:creation_datetime", namespaceManager);
                            XmlNode contentNode = xmlDoc.SelectSingleNode("/default:Record/default:content", namespaceManager);
                            XmlNode parentNode = xmlDoc.SelectSingleNode("/default:Record/default:parent", namespaceManager);

                            // Construir o texto das propriedades do record
                            string recordDetails = $"id: {idNode?.InnerText}\n" +
                                                   $"name: {nameNode?.InnerText}\n" +
                                                   $"creation date: {dateNode?.InnerText}\n" +
                                                   $"content: {contentNode?.InnerText}\n" +
                                                   $"parent: {parentNode?.InnerText}\n";

                            rtbShow.Text = recordDetails;
                        }
                        else if (!string.IsNullOrEmpty(contName))
                        {
                            // Extrair propriedades do container
                            XmlNode idNode = xmlDoc.SelectSingleNode("/default:Container/default:id", namespaceManager);
                            XmlNode nameNode = xmlDoc.SelectSingleNode("/default:Container/default:name", namespaceManager);
                            XmlNode dateNode = xmlDoc.SelectSingleNode("/default:Container/default:creation_datetime", namespaceManager);
                            XmlNode parentNode = xmlDoc.SelectSingleNode("/default:Container/default:parent", namespaceManager);

                            // Construir o texto das propriedades do container
                            string containerDetails = $"id: {idNode?.InnerText}\n" +
                                                      $"name: {nameNode?.InnerText}\n" +
                                                      $"creation date: {dateNode?.InnerText}\n" +
                                                      $"parent: {parentNode?.InnerText}";

                            rtbShow.Text = containerDetails;
                        }
                        else if(somiodLocate == "")
                        {
                            // Extrair propriedades da aplicação
                            XmlNode idNode = xmlDoc.SelectSingleNode("/default:Application/default:id", namespaceManager);
                            XmlNode nameNode = xmlDoc.SelectSingleNode("/default:Application/default:name", namespaceManager);
                            XmlNode dateNode = xmlDoc.SelectSingleNode("/default:Application/default:creation_datetime", namespaceManager);

                            // Construir o texto das propriedades da aplicação
                            string appDetails = $"id: {idNode?.InnerText}\n" +
                                                $"name: {nameNode?.InnerText}\n" +
                                                $"creation date: {dateNode?.InnerText}";

                            rtbShow.Text = appDetails;
                        }

                    }
                    catch (Exception ex)
                    {
                        rtbShow.Text = $"Erro ao processar a resposta: {ex.Message}";
                    }
                }
                else
                {
                    rtbShow.Text = $"Erro ao acessar o recurso:\n" +
                                   $"Código HTTP: {response.StatusCode}\n" +
                                   $"Descrição: {response.StatusDescription}";
                }
            }
            catch (Exception ex)
            {
                rtbShow.Text = $"Erro na conexão: {ex.Message}";
            }
        }


        // Método auxiliar para exibir a lista de itens
        private void MostrarLista(XmlNodeList stringNodes)
        {
            if (stringNodes != null && stringNodes.Count > 0)
            {
                StringBuilder result = new StringBuilder();

                foreach (XmlNode node in stringNodes)
                {
                    result.AppendLine(node.InnerText);
                }

                rtbShow.Text = result.ToString();
            }
            else
            {
                rtbShow.Text = "Nenhuma tag <string> encontrada.";
            }
        }




        private void btnGetAllApps_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(baseURI))
            {
                MessageBox.Show("O Base URI não está configurado. Verifique as configurações.", "Erro");
                return;
            }

            RestRequest request = new RestRequest("api/somiod", Method.Get);
            request.AddHeader("Accept", "application/xml");

            try
            {
                // Enviar a requisição
                var response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    try
                    {
                        // Carregar o XML retornado
                        string xmlContent = response.Content;
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(xmlContent);


                        Console.WriteLine(xmlContent);

                        // Configurar o namespace para acessar os nós do XML
                        XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
                        namespaceManager.AddNamespace("default", "http://schemas.datacontract.org/2004/07/Somiod.Models");

                        // Selecionar todos os nós <Application>
                        XmlNodeList appNodes = xmlDoc.SelectNodes("/default:ArrayOfApplication/default:Application", namespaceManager);

                        if (appNodes == null || appNodes.Count == 0)
                        {
                            rtbShow.Text = "Nenhuma aplicação encontrada.";
                            return;
                        }

                        // Construir a lista de aplicações
                        StringBuilder appList = new StringBuilder();
                        foreach (XmlNode appNode in appNodes)
                        {
                            string id = appNode.SelectSingleNode("default:id", namespaceManager)?.InnerText ?? "Sem ID";
                            string name = appNode.SelectSingleNode("default:name", namespaceManager)?.InnerText ?? "Sem Nome";

                            appList.AppendLine($"ID: {id}, Name: {name}");
                        }

                        // Exibir a lista na RichTextBox
                        rtbShow.Text = appList.ToString();
                    }
                    catch (Exception ex)
                    {
                        rtbShow.Text = $"Erro ao processar a resposta: {ex.Message}";
                    }
                }
                else
                {
                    // Mostrar mensagem de erro na RichTextBox
                    rtbShow.Text = $"Erro ao acessar as aplicações:\n" +
                                   $"Código HTTP: {response.StatusCode}\n" +
                                   $"Descrição: {response.StatusDescription}";
                }
            }
            catch (Exception ex)
            {
                rtbShow.Text = $"Erro na conexão: {ex.Message}";
            }

        }



        private void btnPost_Click(object sender, EventArgs e)
        {
            // Obter os valores necessários da interface
            string appName = richTextBox2.Text.Trim(); // Nome da aplicação
            string containerName = rtbContainer.Text.Trim(); // Nome do container
            string recordName = rtbRecord.Text.Trim(); // Nome do record
            string notifName = rtbNotification.Text.Trim(); // Nome da notificação
            string endpointBase = rtbContent.Text.Trim(); // Parte base do endpoint
            string notifProtocol = comboBoxNotf.SelectedItem?.ToString(); // Protocolo escolhido (mqtt/http)
            string notifEvent = comboBoxEvent.SelectedItem?.ToString(); // Evento selecionado (1/2)
            string baseUri = baseURI;

            // Validar os dados obrigatórios
            if (string.IsNullOrEmpty(baseUri))
            {
                MessageBox.Show("O Base URI não está configurado. Verifique as configurações.", "Erro");
                return;
            }

            if (string.IsNullOrEmpty(appName))
            {
                MessageBox.Show("Por favor, insira o nome da aplicação.", "Erro");
                return;
            }

            // Configurar requisição
            RestRequest request;
            string endpoint;

            if (!string.IsNullOrEmpty(notifName) && !string.IsNullOrEmpty(containerName))
            {
                // Criar requisição para criação de uma notificação
                endpoint = $"api/somiod/{appName}/{containerName}/notif";
                request = new RestRequest(endpoint, Method.Post);

                if (string.IsNullOrEmpty(notifProtocol) || string.IsNullOrEmpty(endpointBase) || string.IsNullOrEmpty(notifEvent))
                {
                    MessageBox.Show("Por favor, preencha todos os campos obrigatórios para criar uma notificação.", "Erro");
                    return;
                }

                if(notifEvent.Equals("creation"))
                {
                    notifEvent = "1";
                }
                else
                {
                    notifEvent = "2";
                }

                var notification = new
                {
                    res_type = "notification",
                    name = notifName,
                    endpoint = $"{notifProtocol}://{endpointBase}",
                    @event = notifEvent
                };
                request.AddObject(notification);
            }
            else if (!string.IsNullOrEmpty(recordName) && !string.IsNullOrEmpty(containerName))
            {
                // Criar requisição para criação de um record dentro do container
                endpoint = $"api/somiod/{appName}/{containerName}/record";
                request = new RestRequest(endpoint, Method.Post);

                var record = new
                {
                    res_type = "record",
                    name = recordName,
                    content = endpointBase
                };
                request.AddObject(record);
            }
            else if (!string.IsNullOrEmpty(containerName))
            {
                // Criar requisição para criação de um container dentro da aplicação
                endpoint = $"api/somiod/{appName}";
                request = new RestRequest(endpoint, Method.Post);

                var container = new
                {
                    res_type = "container",
                    name = containerName
                };
                request.AddObject(container);
            }
            else
            {
                // Criar requisição para criação da aplicação
                endpoint = "api/somiod";
                request = new RestRequest(endpoint, Method.Post);

                var app = new Models.Application
                {
                    res_type = "application",
                    name = appName
                };
                request.AddObject(app);
            }

            request.RequestFormat = DataFormat.Xml;
            request.AddHeader("Accept", "application/xml");

            try
            {
                // Enviar a requisição
                var response = client.Execute(request);
                string xmlContent = response.Content;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlContent);

                XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
                namespaceManager.AddNamespace("default", "http://schemas.datacontract.org/2004/07/Somiod.Models");

                XmlNode nameNode;
                XmlNode idNode;

                if (!string.IsNullOrEmpty(notifName) && !string.IsNullOrEmpty(containerName))
                {
                    // Processar resposta para criação da notificação
                    nameNode = xmlDoc.SelectSingleNode("/default:Notification/default:name", namespaceManager);
                    idNode = xmlDoc.SelectSingleNode("/default:Notification/default:id", namespaceManager);

                    string notif_name = nameNode?.InnerText;
                    string notif_id = idNode?.InnerText;

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        rtbShow.Text = $"Notification '{notif_name}' created within container '{containerName}' in app '{appName}' with id: {notif_id}";
                    }
                    else
                    {
                        rtbShow.Text = $"Erro ao criar notificação:\n" +
                                       $"Código HTTP: {response.StatusCode}\n" +
                                       $"Descrição: {response.StatusDescription}\n" +
                                       $"Detalhes: {response.Content}";
                    }
                }
                else if (!string.IsNullOrEmpty(recordName) && !string.IsNullOrEmpty(containerName))
                {
                    // Processar resposta para criação do record
                    nameNode = xmlDoc.SelectSingleNode("/default:Record/default:name", namespaceManager);
                    idNode = xmlDoc.SelectSingleNode("/default:Record/default:id", namespaceManager);

                    string rec_name = nameNode?.InnerText;
                    string rec_id = idNode?.InnerText;

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        rtbShow.Text = $"Record '{rec_name}' created within container '{containerName}' in app '{appName}' with id: {rec_id}";
                    }
                    else
                    {
                        rtbShow.Text = $"Erro ao criar record:\n" +
                                       $"Código HTTP: {response.StatusCode}\n" +
                                       $"Descrição: {response.StatusDescription}\n" +
                                       $"Detalhes: {response.Content}";
                    }
                }
                else if (!string.IsNullOrEmpty(containerName))
                {
                    // Processar resposta para criação do container
                    nameNode = xmlDoc.SelectSingleNode("/default:Container/default:name", namespaceManager);
                    idNode = xmlDoc.SelectSingleNode("/default:Container/default:id", namespaceManager);

                    string container_name = nameNode?.InnerText;
                    string container_id = idNode?.InnerText;

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        rtbShow.Text = $"Container '{container_name}' created within app '{appName}' with id: {container_id}";
                    }
                    else
                    {
                        rtbShow.Text = $"Erro ao criar container:\n" +
                                       $"Código HTTP: {response.StatusCode}\n" +
                                       $"Descrição: {response.StatusDescription}\n" +
                                       $"Detalhes: {response.Content}";
                    }
                }
                else
                {
                    // Processar resposta para criação da aplicação
                    nameNode = xmlDoc.SelectSingleNode("/default:Application/default:name", namespaceManager);
                    idNode = xmlDoc.SelectSingleNode("/default:Application/default:id", namespaceManager);

                    string app_name = nameNode?.InnerText;
                    string app_id = idNode?.InnerText;

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        rtbShow.Text = $"App created with name: {app_name} and id: {app_id}";
                    }
                    else
                    {
                        rtbShow.Text = $"Erro ao criar aplicação:\n" +
                                       $"Código HTTP: {response.StatusCode}\n" +
                                       $"Descrição: {response.StatusDescription}\n" +
                                       $"Detalhes: {response.Content}";
                    }
                }
            }
            catch (Exception ex)
            {
                rtbShow.Text = $"Erro na conexão: {ex.Message}, provavelmente falta alguma parametro";
            }
        }


        private void btnPut_Click(object sender, EventArgs e)
        {
            // Obter os valores necessários da interface
            string appName = richTextBox2.Text.Trim(); // Nome atual da aplicação
            string newName = rtbContent.Text.Trim(); // Novo nome da aplicação
            string containerName = rtbContainer.Text.Trim();
            string parentID = rtbParentID.Text.Trim();
            string baseUri = baseURI;

            // Validar os dados obrigatórios
            if (string.IsNullOrEmpty(baseUri))
            {
                MessageBox.Show("O Base URI não está configurado. Verifique as configurações.", "Erro");
                return;
            }

            if (string.IsNullOrEmpty(appName))
            {
                MessageBox.Show("Por favor, preencha o nome da aplicação.", "Erro");
                return;
            }

            try
            {



                if (!string.IsNullOrEmpty(appName) && !string.IsNullOrEmpty(containerName) && (!string.IsNullOrEmpty(parentID) || !string.IsNullOrEmpty(newName)))
                {

                    // Configurar o endpoint
                    string endpoint = $"api/somiod/{appName}/{containerName}";

                    // Criar requisição PUT
                    RestRequest request = new RestRequest(endpoint, Method.Put);

                    // Montar o corpo da requisição
                    var updatedCont = new
                    {
                        name = newName,
                        parent = parentID
                    };
                    request.AddObject(updatedCont);

                    request.RequestFormat = DataFormat.Xml;

                    request.AddHeader("Accept", "application/xml");



                    try
                    {
                        // Enviar a requisição
                        var response = client.Execute(request);
                        string responseContent = response.Content;

                        // Processar a resposta
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(responseContent);

                        XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
                        namespaceManager.AddNamespace("default", "http://schemas.datacontract.org/2004/07/Somiod.Models");

                        XmlNode nameNode = xmlDoc.SelectSingleNode("/default:Container/default:name", namespaceManager);
                        XmlNode idNode = xmlDoc.SelectSingleNode("/default:Container/default:id", namespaceManager);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            string updatedContainerName = nameNode?.InnerText;
                            string containerId = idNode?.InnerText;

                            rtbShow.Text = $"Container updated successfully:\nName: {updatedContainerName}\nID: {containerId}";
                        }
                        else
                        {
                            rtbShow.Text = $"Erro ao atualizar container:\n" +
                                           $"Código HTTP: {response.StatusCode}\n" +
                                           $"Descrição: {response.StatusDescription}\n" +
                                           $"Detalhes: {responseContent}";
                        }
                    }
                    catch (Exception ex)
                    {
                        rtbShow.Text = $"Erro na conexão: {ex.Message}";
                    }

                }
                else if (!string.IsNullOrEmpty(appName))
                {

                    // Configurar o endpoint
                    string endpoint = $"api/somiod/{appName}";

                    // Criar requisição PUT
                    RestRequest request = new RestRequest(endpoint, Method.Put);

                    // Montar o corpo da requisição
                    var updatedApp = new
                    {
                        name = newName
                    };
                    request.AddObject(updatedApp);

                    request.RequestFormat = DataFormat.Xml;

                    request.AddHeader("Accept", "application/xml");



                    try
                    {
                        // Enviar a requisição
                        var response = client.Execute(request);
                        string responseContent = response.Content;

                        // Processar a resposta
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(responseContent);

                        XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
                        namespaceManager.AddNamespace("default", "http://schemas.datacontract.org/2004/07/Somiod.Models");

                        XmlNode nameNode = xmlDoc.SelectSingleNode("/default:Application/default:name", namespaceManager);
                        XmlNode idNode = xmlDoc.SelectSingleNode("/default:Application/default:id", namespaceManager);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            string updatedAppName = nameNode?.InnerText;
                            string appId = idNode?.InnerText;

                            rtbShow.Text = $"Application updated successfully:\nName: {updatedAppName}\nID: {appId}";
                        }
                        else
                        {
                            rtbShow.Text = $"Erro ao atualizar aplicação:\n" +
                                           $"Código HTTP: {response.StatusCode}\n" +
                                           $"Descrição: {response.StatusDescription}\n" +
                                           $"Detalhes: {responseContent}";
                        }
                    }
                    catch (Exception ex)
                    {
                        rtbShow.Text = $"Erro na conexão: {ex.Message}";
                    }


                }
            }
            catch (Exception ex)
            {
                rtbShow.Text = $"Erro na conexão: {ex.Message}";
            }



        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Obter os valores necessários da interface
            string appName = richTextBox2.Text.Trim(); // Nome atual da aplicação
            string containerName = rtbContainer.Text.Trim(); //Nome atual do container
            string recordName = rtbRecord.Text.Trim(); // Nome do record
            string notifName = rtbNotification.Text.Trim(); // Nome da notificação
            string baseUri = baseURI;

            // Validar os dados obrigatórios
            if (string.IsNullOrEmpty(baseUri))
            {
                MessageBox.Show("O Base URI não está configurado. Verifique as configurações.", "Erro");
                return;
            }

            if (string.IsNullOrEmpty(appName))
            {
                MessageBox.Show("Por favor, preencha o nome da aplicação.", "Erro");
                return;
            }

            try
            {
                if(!string.IsNullOrEmpty(appName) && !string.IsNullOrEmpty(containerName) && !string.IsNullOrEmpty(notifName))
                {
                    // Configurar o endpoint
                    string endpoint = $"api/somiod/{appName}/{containerName}/notif/{notifName}";

                    // Criar requisição DELETE
                    RestRequest request = new RestRequest(endpoint, Method.Delete);

                    // Montar o corpo da requisição
                    var updatedCont = new
                    {

                    };
                    request.AddObject(updatedCont);

                    request.RequestFormat = DataFormat.Xml;

                    request.AddHeader("Accept", "application/xml");



                    try
                    {
                        // Enviar a requisição
                        var response = client.Execute(request);


                        if (response.StatusCode == HttpStatusCode.OK)
                        {



                            rtbShow.Text = $"Notification {notifName} deleted successfully!";
                        }
                        else
                        {
                            rtbShow.Text = $"Erro ao atualizar container:\n" +
                                           $"Código HTTP: {response.StatusCode}\n" +
                                           $"Descrição: {response.StatusDescription}\n";
                        }
                    }
                    catch (Exception ex)
                    {
                        rtbShow.Text = $"Erro na conexão: {ex.Message}";
                    }
                }
                else if (!string.IsNullOrEmpty(appName) && !string.IsNullOrEmpty(containerName) && !string.IsNullOrEmpty(recordName))
                {
                    // Configurar o endpoint
                    string endpoint = $"api/somiod/{appName}/{containerName}/record/{recordName}";

                    // Criar requisição DELETE
                    RestRequest request = new RestRequest(endpoint, Method.Delete);

                    // Montar o corpo da requisição
                    var updatedCont = new
                    {

                    };
                    request.AddObject(updatedCont);

                    request.RequestFormat = DataFormat.Xml;

                    request.AddHeader("Accept", "application/xml");



                    try
                    {
                        // Enviar a requisição
                        var response = client.Execute(request);


                        if (response.StatusCode == HttpStatusCode.OK)
                        {



                            rtbShow.Text = $"Record {recordName} deleted successfully!";
                        }
                        else
                        {
                            rtbShow.Text = $"Erro ao atualizar container:\n" +
                                           $"Código HTTP: {response.StatusCode}\n" +
                                           $"Descrição: {response.StatusDescription}\n";
                        }
                    }
                    catch (Exception ex)
                    {
                        rtbShow.Text = $"Erro na conexão: {ex.Message}";
                    }
                }
                else if (!string.IsNullOrEmpty(appName) && !string.IsNullOrEmpty(containerName))
                {

                    // Configurar o endpoint
                    string endpoint = $"api/somiod/{appName}/{containerName}";

                    // Criar requisição DELETE
                    RestRequest request = new RestRequest(endpoint, Method.Delete);

                    // Montar o corpo da requisição
                    var updatedCont = new
                    {

                    };
                    request.AddObject(updatedCont);

                    request.RequestFormat = DataFormat.Xml;

                    request.AddHeader("Accept", "application/xml");



                    try
                    {
                        // Enviar a requisição
                        var response = client.Execute(request);
                        

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
              
                      

                            rtbShow.Text = $"Container {containerName} deleted successfully!";
                        }
                        else
                        {
                            rtbShow.Text = $"Erro ao atualizar container:\n" +
                                           $"Código HTTP: {response.StatusCode}\n" +
                                           $"Descrição: {response.StatusDescription}\n";
                        }
                    }
                    catch (Exception ex)
                    {
                        rtbShow.Text = $"Erro na conexão: {ex.Message}";
                    }

                }
                else if (!string.IsNullOrEmpty(appName))
                {

                    // Configurar o endpoint
                    string endpoint = $"api/somiod/{appName}";

                    // Criar requisição PUT
                    RestRequest request = new RestRequest(endpoint, Method.Delete);

                    // Montar o corpo da requisição
                    var updatedApp = new
                    {
                       
                    };
                    request.AddObject(updatedApp);

                    request.RequestFormat = DataFormat.Xml;

                    request.AddHeader("Accept", "application/xml");



                    try
                    {
                        // Enviar a requisição
                        var response = client.Execute(request);
                      
                        

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                        
                         

                            rtbShow.Text = $"Application {appName} Deleted successfully!";
                        }
                        else
                        {
                            rtbShow.Text = $"Erro ao atualizar aplicação:\n" +
                                           $"Código HTTP: {response.StatusCode}\n" +
                                           $"Descrição: {response.StatusDescription}\n";
                                        
                        }
                    }
                    catch (Exception ex)
                    {
                        rtbShow.Text = $"Erro na conexão: {ex.Message}";
                    }


                }
            }
            catch (Exception ex)
            {
                rtbShow.Text = $"Erro na conexão: {ex.Message}";
            }


        }




        //methods that have to be here or else it causes error If I remove them 


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }


        //content to send
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void rtbShow_TextChanged(object sender, EventArgs e)
        {

        }

        private void rtbContainer_TextChanged(object sender, EventArgs e)
        {

        }

        private void rtbRecord_TextChanged(object sender, EventArgs e)
        {

        }

        private void rtbNotification_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

       
    }
}
