using clMonitoramento.DAL;
using clMonitoramento.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clMonitoramento.Models
{
    public class VideoModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public byte[] Content { get; set; }
        public int SizeInBytes { get; set; }
        public string Base64 { get; set; }
        public Guid ServerId { get; set; }

        public VideoModel()
        {

        }
        public VideoModel(string description, byte[] content, Guid idServer)
        {
            Description = description;
            Content = content;
            ServerId = idServer;
        }
        public VideoModel(string description, string base64, Guid idServer)
        {
            Description = description;
            Base64 = base64;
            ServerId = idServer;
        }
        public VideoModel(Guid videoId, Guid idServer)
        {
            Id = videoId;
            ServerId = idServer;
        }
        public VideoModel(byte[] content)
        {
            Content = content;
        }

        public Guid AddVideoToServer()
        {
            Guid id;
            bool ok = false;
            string sql =
                @"DECLARE @NewGuid TABLE(NewValue UNIQUEIDENTIFIER);
                INSERT INTO Video (Description, ServerId)
                OUTPUT inserted.Id INTO @NewGuid(NewValue)
                VALUES (@Description, @ServerId); 
                SELECT NewValue FROM @NewGuid;";

            id = SqlDataAccess.SaveData(sql, this, true);

            if (id != null && id != Guid.Empty)
                ok = FileUtil.SaveFile(ServerId.ToString(), id.ToString(), Content);

            if (!ok)
                return Guid.Empty;
            else
                return id;
        }

        public bool DeleteVideo()
        {
            string sql =
                @"DELETE FROM VIDEO WHERE ServerId = @ServerId AND Id = @Id";

            if (SqlDataAccess.SaveData(sql, this) > 0 && FileUtil.DeleteFile(ServerId.ToString(), Id.ToString()))
                return true;
            else
                return false;
        }

        public List<VideoModel> GetDataFromVideo()
        {

            string sql =
                @"SELECT
                    Id,
                    Description
                FROM Video where ServerId = @ServerId
                    and Id = @Id";

            return SqlDataAccess.LoadData<VideoModel>(sql, this);
        }

        public byte[] GetBinaryContentFromVideo()
        {
            return FileUtil.GetFileContent(ServerId.ToString(), Id.ToString());
        }
    }
}
