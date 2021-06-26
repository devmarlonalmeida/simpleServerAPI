using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using apiMonitoramento.Models;
using clMonitoramento.Business;
using clMonitoramento.Util;

namespace apiMonitoramento.Controllers
{
    public class ServersController : ApiController
    {

        #region Servers

        #region Get

        //GET: api/servers
        public IHttpActionResult Get()
        {

            List<ServerDTO> lista = new List<ServerDTO>();
            lista = (from s in ServerBusiness.LoadServers()
                     select new ServerDTO
                     {
                         Id = s.Id,
                         Name = s.Name,
                         Ip = s.Ip,
                         Port = s.Port
                     }).ToList();

            if (lista.Count == 0)
                return NotFound();

            return Ok(lista);
        }

        [Route("api/servers/{serverId}")]
        [HttpGet]
        public IHttpActionResult Get(Guid serverId)
        {
            ServerDTO obj = (from s in ServerBusiness.LoadServer(serverId)
                     select new ServerDTO
                     {
                         Id = s.Id,
                         Name = s.Name,
                         Ip = s.Ip,
                         Port = s.Port
                     }).FirstOrDefault();
            if (obj == null)
                return NotFound();

            return Ok(obj);
        }

        [Route("api/servers/available/{serverId}")]
        [HttpGet]
        public IHttpActionResult GetDisponibility(Guid serverId)
        {
            ServerDTO obj = (from s in ServerBusiness.GetDisponibility(serverId)
                    select new ServerDTO
                    {
                        Ip = s.Ip,
                        Port = s.Port
                    }).FirstOrDefault();

            if (obj == null)
                return NotFound();

            return Ok(obj);
        }

        #endregion

        #region Post

        //POST: api/server
        [Route("api/server")]
        [HttpPost]
        public IHttpActionResult Post(ServerDTO server)
        {
            if (server == null ||
                string.IsNullOrWhiteSpace(server.Name) ||
                string.IsNullOrWhiteSpace(server.Ip) ||
                server.Port <= 0)
                return BadRequest();

            return Ok(ServerBusiness.CreateServer(new clMonitoramento.Models.ServerModel(server.Name, server.Ip, server.Port)));
        }

        #endregion

        #region Put

        //POST: api/server
        [Route("api/server")]
        [HttpPut]
        public IHttpActionResult Put(ServerDTO server)
        {
            if (server == null ||
                server.Id == Guid.Empty ||
                string.IsNullOrWhiteSpace(server.Name) ||
                string.IsNullOrWhiteSpace(server.Ip) ||
                server.Port <= 0)
                return BadRequest();

            return Ok(ServerBusiness.UpdateServer(new clMonitoramento.Models.ServerModel(server.Id, server.Name, server.Ip, server.Port)));
        }

        #endregion

        #region Delete

        [Route("api/servers/{serverId}")]
        [HttpDelete]
        public IHttpActionResult Delete(Guid serverId)
        {
            if (serverId == Guid.Empty)
                return BadRequest();

            return Ok(ServerBusiness.DeleteServer(serverId));
        }

        #endregion

        #endregion

        #region Videos

        #region Get

        [Route("api/servers/{serverId}/videos/{videoId}")]
        [HttpGet]
        public IHttpActionResult GetDataFromVideo(Guid serverId, Guid videoId)
        {
            List<VideoDTO> list = (from v in VideoBusiness.GetDataFromVideo(serverId, videoId)
                     select new VideoDTO
                     {
                         Id = v.Id,
                         Description = v.Description,
                         SizeInBytes = FileUtil.GetFileSize(serverId.ToString(), v.Id.ToString())
                    }).ToList();

            if (list.Count == 0)
                return NotFound();

            return Ok(list);
        }

        [Route("api/servers/{serverId}/videos")]
        [HttpGet]
        public IHttpActionResult GetVideosFromServer(Guid serverId)
        {
            List<VideoDTO> list = (from v in ServerBusiness.LoadVideosFromServer(serverId)
                     select new VideoDTO
                     {
                         Id = v.Id,
                         Description = v.Description,
                         SizeInBytes = FileUtil.GetFileSize(serverId.ToString(), v.Id.ToString())
                     }).ToList();

            if (list.Count == 0)
                return NotFound();

            return Ok(list);
        }

        [Route("api/servers/{serverId}/videos/{videoId}/binary")]
        [HttpGet]
        public IHttpActionResult GetBinaryContentFromVideo(Guid serverId, Guid videoId)
        {
            if (serverId == Guid.Empty || videoId == Guid.Empty)
                return BadRequest();

            byte[] bytes = VideoBusiness.GetBinaryContentFromVideo(serverId, videoId);
            if (bytes == null)
                return NotFound();

            return Ok(Convert.ToBase64String(bytes));
        }
        #endregion

        #region Post

        //POST: api/server
        [Route("api/servers/{serverId}/videos")]
        [HttpPost]
        public IHttpActionResult Post(Guid serverId, [FromBody]VideoDTO video)
        {
            if (video == null ||
               string.IsNullOrWhiteSpace(video.Description) ||
               string.IsNullOrWhiteSpace(video.Base64) ||
               serverId == Guid.Empty)
                return BadRequest();

            return Ok(VideoBusiness.AddVideoToServer(new clMonitoramento.Models.VideoModel(video.Description, Convert.FromBase64String(video.Base64), serverId)));
        }

        #endregion

        #region Delete

        //DELETE: api/servers/{serverId}/videos/{videoId}
        [Route("api/servers/{serverId}/videos/{videoId}")]
        [HttpDelete]
        public IHttpActionResult Delete(Guid serverId, Guid videoId)
        {
            if (serverId == Guid.Empty || videoId == Guid.Empty)
                return BadRequest();

            return Ok(VideoBusiness.DeleteVideo(serverId, videoId));
        }

        #endregion

        #endregion

    }
}
