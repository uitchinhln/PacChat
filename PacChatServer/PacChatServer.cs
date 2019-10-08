using DotNetty.Transport.Channels;
using PacChatServer.Net;
using PacChatServer.Net.Protocol;
using PacChatServer.Utils.ThreadUtils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PacChatServer
{
    class PacChatServer
    {
        public PacChatServer()
        {
            //CountdownLatch latch = new CountdownLatch(1);

            //ChatServer server = new ChatServer(this, new ProtocolProvider(), latch);
            //Task<IChannel> channel = server.BindAsync(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1515));

            //latch.Wait();

            //if (channel != null)
            //{
            //    _ = server.ShutdownAsync();
            //}
            C c = new C();
            F f = new F();
            B d = new D();
            A e = new E();
            B e1 = new E();
            Console.WriteLine("type of {0} is {1}", "c", f is D);
            Console.WriteLine("type of {0} is {1}", "c", c.GetType());
            Console.WriteLine("type of {0} is {1}", "f", f.GetType());
            Console.WriteLine("type of {0} is {1}", "d", d.GetType());
            Console.WriteLine("type of {0} is {1}", "e", e.GetType());
            Console.WriteLine("type of {0} is {1}", "e1", e1.GetType());
            Console.WriteLine("{0} is assignable from {1} : {2}", "f", "c", f.GetType().IsAssignableFrom(c.GetType()));
            Console.WriteLine("{0} is assignable from {1} : {2}", "c", "f", c.GetType().IsAssignableFrom(f.GetType()));
            List<A> a = new List<A>();
            a.Add(c);
        }

        public interface A
        {

        }

        public abstract class B
        {

        }

        public class C : A
        {
            public int X()
            {
                return 0;
            }
        }

        public class D : B
        {
            public int X()
            {
                return 0;
            }

        }

        public class E : B, A
        {
            public int X()
            {
                return 0;
            }

        }

        public class F : C
        {
            public int P()
            {
                return 0;
            }

        }
    }
}
