using clMonitoramento.DAL;
using clMonitoramento.Models;
using System;
using System.Collections.Generic;

namespace clMonitoramento.Business
{
    public class VideoBusiness
    {

        public static Guid AddVideoToServer(VideoModel video)
        {
            return video.AddVideoToServer();
        }

        public static bool DeleteVideo(Guid serverId, Guid videoId)
        {
            return new VideoModel(videoId, serverId).DeleteVideo();
        }

        public static List<VideoModel> GetDataFromVideo(Guid serverId, Guid videoId)
        {
            return new VideoModel(videoId, serverId).GetDataFromVideo();
        }

        public static byte[] GetBinaryContentFromVideo(Guid serverId, Guid videoId)
        {
            return new VideoModel(videoId, serverId).GetBinaryContentFromVideo();
        }
    }
}
