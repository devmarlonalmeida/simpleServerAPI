using clMonitoramento.DAL;
using clMonitoramento.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace clMonitoramento.Business
{
    public static class RecyclerBusiness
    {
        public static async Task RunRecycler(int days)
        {
            //teste assincrono
            RecyclerModel recycler = RecyclerModel.Instance;
            Task.Run(() => recycler.RunRecycler(days));
        }

        public static string GetRecyclerStatus()
        {
            if (RecyclerModel.Instance.GetStatus())
            {
                return "running";
            }
            else
            {
                return "not running";
            }
        }
    }
}
