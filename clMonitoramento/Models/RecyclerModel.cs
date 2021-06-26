using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using clMonitoramento.DAL;

namespace clMonitoramento.Models
{
    public sealed class RecyclerModel
    {
        private static RecyclerModel instance = null;
        private static readonly object locker = new object();
        private bool IsRunning = false;

        RecyclerModel()
        {

        }

        public static RecyclerModel Instance
        {
            get
            {
                lock (locker)
                {
                    if(instance == null)
                    {
                        instance = new RecyclerModel();
                    }
                    return instance;
                }
            }
        }

        private void UpdateStatus(bool status)
        {
            IsRunning = status;
        }

        public bool GetStatus()
        {
            return IsRunning;
        }

        public bool RunRecycler(int days)
        {
            UpdateStatus(true);

            //abaixo um teste para simular uma tarefa demorada
            //Thread.Sleep(10000);

            DeleteOldVideos(days);
            UpdateStatus(false);
            return false;
        }

        private bool DeleteOldVideos(int days)
        {
            bool ok = false;

            string sql =
                @"SELECT ID, ServerId FROM VIDEO WHERE VIDEO.REGISTERDATE >= DATEADD(DAY, (@days * -1), GETDATE())";

            List<VideoModel> list = SqlDataAccess.LoadData<VideoModel>(sql, new { days });

            if (list.Count > 0)
                for(int i = 0; i < list.Count && ok; i++)
                    ok = list[i].DeleteVideo();

            return ok;
        }


    }
}
