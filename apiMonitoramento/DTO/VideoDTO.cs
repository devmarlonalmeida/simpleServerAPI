using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apiMonitoramento.Models
{
    public class VideoDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public byte[] Content { get; set; }
        public int SizeInBytes { get; set; }
        public string Base64 { get; set; }
        public Guid ServerId { get; set; }

        public VideoDTO()
        {

        }

    }
}