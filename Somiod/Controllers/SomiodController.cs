using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Somiod.Models;

namespace Somiod.Controllers
{
    public class SomiodController : ApiController
    {
        [HttpGet]
        [Route("api/somiod")]
        public IHttpActionResult GetAllApplications() //antonio
        {

            return Ok();
        }

        [HttpGet]
        [Route("api/somiod/{app_name}")]
        public IHttpActionResult GetApplication() //ricardo
        {

            return Ok();
        }

        [HttpGet]
        [Route("api/somiod/{app_name}/{cont_name}")]
        public IHttpActionResult GetContainer() //antonio
        {

            return Ok();
        }

        [HttpGet]
        [Route("api/somiod/{app_name}/{cont_name}/record/{data_name}")]
        public IHttpActionResult GetRecord() //ricardo
        {

            return Ok();
        }

        [HttpGet]
        [Route("api/somiod/{app_name}/{cont_name}/notification/{data_name}")]
        public IHttpActionResult GetNotification() //antonio
        {

            return Ok();
        }

        [HttpGet]
        [Route("api/somiod")]
        public IHttpActionResult GetAllsApplicationsUsingLocate() //ricardo
        {

            return Ok();
        }

        [HttpGet]
        [Route("api/somiod/{app_name}")]
        public IHttpActionResult GetAllsInformationUsingLocate() //ricardo
        {

            return Ok();
        }

        [HttpPost]
        [Route("api/somiod")]
        //o container é como exemplo mudar depois
        public IHttpActionResult PostApplcation([FromBody]Container value) //ricardo
        {

            return Ok();
        }

        [HttpPost]
        [Route("api/somiod/{app_name}")]
        //o container é como exemplo mudar depois
        public IHttpActionResult PostContainer(string app_name, [FromBody] Container value) //antonio
        {

            return Ok();
        }

        [HttpPost]
        [Route("api/somiod/{app_name}/{cont_name}/notification")]
        //o container é como exemplo mudar depois
        public IHttpActionResult PostNotification(string app_name, [FromBody] Container value) //ricardo
        {

            return Ok();
        }

        [HttpPost]
        [Route("api/somiod/{app_name}/{cont_name}/record")]
        //o container é como exemplo mudar depois
        public IHttpActionResult PostRecord(string app_name, [FromBody] Container value) //antonio
        {

            return Ok();
        }

        [HttpPut]
        [Route("api/somiod/{app_name}")]
        public IHttpActionResult PutApplication(string app_name, [FromBody] Container value) //ricardo
        {

            return Ok();
        }

        [HttpPut]
        [Route("api/somiod/{app_name}/{cont_name}")]
        public IHttpActionResult PutContainer(string app_name, [FromBody] Container value) //ricardo
        {

            return Ok();
        }

        [HttpDelete]
        [Route("api/somiod/{app_name}")]
        public IHttpActionResult DeleteApplication(string app_name, [FromBody] Container value) //ricardo
        {

            return Ok();
        }

        [HttpDelete]
        [Route("api/somiod/{app_name}/{cont_name}")]
        public IHttpActionResult DeleteContainer(string app_name, [FromBody] Container value) //antonio
        {

            return Ok();
        }
        [HttpDelete]
        [Route("api/somiod/{app_name}/{cont_name}/notification/{data_name}")]
        public IHttpActionResult DeleteNotification(string app_name, [FromBody] Container value)//ricardo
        {

            return Ok();
        }
        [HttpDelete]
        [Route("api/somiod/{app_name}/{cont_name}/record/{data_name}")]
        public IHttpActionResult DeleteRecord(string app_name, [FromBody] Container value) //antonio
        {

            return Ok();
        }

    }
}
