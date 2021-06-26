using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apiMonitoramento.Models
{
    public class ServerDTO
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }

        public ServerDTO()
        {

        }

    }
}