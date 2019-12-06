using DotNetty.Codecs.Http;
using PacChatServer.Network.RestfulServer.Alias.AliasHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.RestfulServer.Alias
{
    public class AliasManager
    {
        SortedDictionary<String, IAliasExecutor> registeredAlias = null;

        PacChatServer server = null;

        PageNotFound pageNotFound = new PageNotFound();

        private AliasManager()
        {
            this.server = PacChatServer.GetServer();
            registeredAlias = new SortedDictionary<string, IAliasExecutor>();
            new AliasInitialize().Initialize(this);
        }

        public void RegisterAlias(string uri, IAliasExecutor executor)
        {
            try
            {
                if (uri == null) return;
                uri = uri.Trim().ToUpper();

                if (uri.Length == 0)
                {
                    Exception e = new Exception(String.Format("The alias must have at least 1 character (excluding spaces)!"));
                    throw e;
                }
                if (executor == null)
                {
                    Exception e = new Exception(String.Format("Executor can't be null!!!"));
                    throw e;
                }
                if (registeredAlias.ContainsKey(uri))
                {
                    Exception e = new Exception(String.Format("The URI \"{0}\" already exists!!!", uri.ToLower()));
                    throw e;
                }
                registeredAlias.Add(uri, executor);
                server.Logger.Info(String.Format("  - URI {0} registered successfully.", uri));
            }
            catch (Exception e)
            {
                server.Logger.Error(e);
            }
        }

        public void ExecuteAlias(String query, IFullHttpRequest request, IFullHttpResponse response)
        {
            try
            {

                QueryStringDecoder decoder = new QueryStringDecoder(query);

                string uri = decoder.Path;

                if (uri.EndsWith("/"))
                {
                    uri = uri.Remove(uri.Length - 1);
                }

                if (uri == null)
                {
                    pageNotFound.Execute(decoder.Parameters, request, response);
                    return;
                }
                uri = uri.ToUpper().Trim();
                if (uri.Length < 1)
                {
                    pageNotFound.Execute(decoder.Parameters, request, response);
                    return;
                }

                if (!registeredAlias.ContainsKey(uri))
                {
                    server.Logger.Error(String.Format("URI \"{0}\" is not exists!!!", uri.ToLower()));
                    pageNotFound.Execute(decoder.Parameters, request, response);
                    return;
                }

                if (registeredAlias.ContainsKey(uri))
                {
                    registeredAlias[uri].Execute(decoder.Parameters, request, response);
                }
            }
            catch (Exception e)
            {
                server.Logger.Error(e);
                throw;
            }
        }

        public void Unregister(string uri)
        {
            uri = uri.Trim().ToUpper();
            if (registeredAlias.ContainsKey(uri))
            {
                registeredAlias.Remove(uri);
            }
        }

        public void UnregisterAll()
        {
            registeredAlias.Clear();
        }

        public static void StartService(bool forceRestart = false)
        {
            try
            {
                if (Instance == null || forceRestart)
                {
                    Instance = new AliasManager();
                    PacChatServer.GetServer().Logger.Info("Alias Service started successfully");
                }
            }
            catch (Exception e)
            {
                PacChatServer.GetServer().Logger.Error(e);
            }
        }

        public static AliasManager Instance { get; private set; }
    }
}
