using clMonitoramento.DAL;
using clMonitoramento.Models;
using System;
using System.Collections.Generic;

namespace clMonitoramento.Business
{
    public static class ServerBusiness
    {
        public static Guid CreateServer(ServerModel server)
        {
            return server.CreateServer();
        }

        public static List<ServerModel> LoadServers()
        {
            return new ServerModel().LoadServers();
        }

        public static List<ServerModel> LoadServer(Guid serverId)
        {
            return new ServerModel(serverId).LoadServer();
        }

        public static List<ServerModel> GetDisponibility(Guid serverId)
        {
            return new ServerModel(serverId).GetDisponibility();
        }

        public static bool UpdateServer(ServerModel server)
        {
            return server.UpdateServer();
        }

        public static bool DeleteServer(Guid serverId)
        {
            return new ServerModel(serverId).DeleteServer();
        }

        public static List<VideoModel> LoadVideosFromServer(Guid serverId)
        {
            return new ServerModel(serverId).LoadVideosFromServer();
        }
    }
}
