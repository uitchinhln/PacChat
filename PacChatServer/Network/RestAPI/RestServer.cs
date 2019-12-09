using Microsoft.Owin.Hosting;
using PacChatServer.Utils.ThreadUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.RestAPI
{
    public class RestServer
    {
        private IDisposable _webapp;

        public void Start(String ip, int port, CountdownLatch latch)
        {
            try
            {
                _webapp = WebApp.Start<Startup>(String.Format("http://{0}:{1}", ip, port));
                PacChatServer.GetServer().Logger.Info("Bind success. REST Server is listening on " + ip + ":" + port);
                latch.Signal();
            } catch (Exception e)
            {
                PacChatServer.GetServer().Logger.Error(e);
            }
        }

        public void Stop()
        {
            _webapp?.Dispose();
        }
    }
}
