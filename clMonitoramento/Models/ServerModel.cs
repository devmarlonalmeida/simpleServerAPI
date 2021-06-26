using clMonitoramento.DAL;
using clMonitoramento.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace clMonitoramento.Models
{
    public class ServerModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }

        public ServerModel()
        {

        }

        public ServerModel(Guid id)
        {
            Id = id;
        }

        public ServerModel(Guid id, string name, string ip, int port)
        {
            Id = id;
            Name = name;
            Ip = ip;
            Port = port;
        }

        public ServerModel(string name, string ip, int port)
        {
            Name = name;
            Ip = ip;
            Port = port;
        }

        public Guid CreateServer()
        {

            string sql =
                @"DECLARE @NewGuid TABLE(NewValue UNIQUEIDENTIFIER);
                INSERT INTO SERVER (NAME, IP, PORT)
                OUTPUT inserted.Id INTO @NewGuid(NewValue)
                VALUES (@Name, @Ip, @Port); 
                SELECT NewValue FROM @NewGuid;";

            return SqlDataAccess.SaveData(sql, this, true);
        }

        public List<ServerModel> LoadServers()
        {
            string sql =
                @"SELECT Id, Name, Ip, Port FROM SERVER";

            return SqlDataAccess.LoadData<ServerModel>(sql);
        }

        public List<ServerModel> LoadServer()
        {
            string sql =
                @"SELECT Id, Name, Ip, Port FROM SERVER where Id = @Id";

            return SqlDataAccess.LoadData<ServerModel>(sql, this);
        }

        public List<ServerModel> GetDisponibility()
        {
            string sql =
                @"SELECT Ip, Port FROM SERVER where Id = @Id";

            return SqlDataAccess.LoadData<ServerModel>(sql, this);
        }

        public bool UpdateServer()
        {

            string sql =
                @"UPDATE SERVER SET
                    NAME = @Name,
                    IP = @Ip,
                    PORT = @Port
                WHERE Id = @Id";

            return SqlDataAccess.SaveData(sql, this) > 0;
        }

        public bool DeleteServer()
        {
            if (FileUtil.DeleteDirectory(Id.ToString()))
            {
                string[] commands = new string[] { "DELETE FROM VIDEO WHERE SERVERID = @Id", "DELETE FROM SERVER WHERE Id = @Id" };

                return SqlDataAccess.OpenTransactionSameParameter(commands, new { Id });
            }
            else
                return false;
        }

        public List<VideoModel> LoadVideosFromServer()
        {
            string sql =
                @"SELECT
                    Id,
                    Description
                FROM Video where ServerId = @Id";

            return SqlDataAccess.LoadData<VideoModel>(sql, this);
        }
    }
}
