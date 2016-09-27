using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EyeXFramework;
using Tobii.EyeX.Framework;

using SuperSocket.SocketBase.Config;
using SuperWebSocket;

namespace GazeServer
{
    public class Program
    {
        static WebSocketServer appServer;
        static StringBuilder gazeStringBuilder = new StringBuilder();

        public static void Main(string[] args)
        {
            EyeXHost _eyeXHost = new EyeXHost();
            _eyeXHost.Start();
            var stream = _eyeXHost.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered);
            stream.Next += (s, e) => GazePointDataStream(s, e);

            appServer = new WebSocketServer();
            var config = new ServerConfig();
            config.Name = "eyegaze";
            config.Port = 2012;
            config.MaxRequestLength = 10000;

            // Setup the appServer 
            if (!appServer.Setup(config)) //Setup with listening port 
            {
                Console.WriteLine("Failed to setup!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine();

            // Try to start the appServer 
            if (!appServer.Start())
            {
                Console.WriteLine("Failed to start!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("The server started successfully, press key 'q' to stop it!");

            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }

            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }

            //Stop the appServer 
            appServer.Stop();

            Console.WriteLine();
            Console.WriteLine("The server was stopped!");
            Console.ReadKey();

        }

        private static void GazePointDataStream(object sender, GazePointEventArgs e)
        {
            var sessions = appServer.GetAllSessions();
            if (sessions.Count() < 1)
                return;
            
            foreach (var session in sessions)
            {
                try
                {
                    session.Send(gazeJSON(e.X, e.Y, "" + e.Timestamp));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex);
                }

            }
        }
        private static string gazeJSON(double x, double y, string t)
        {
            gazeStringBuilder = new StringBuilder();
            gazeStringBuilder.Append("{");
            gazeStringBuilder.Append("\"gaze\":{");
            gazeStringBuilder.Append("\"x\":" + "\"" + x + "\",");
            gazeStringBuilder.Append("\"y\":" + "\"" + y + "\",");
            gazeStringBuilder.Append("\"timestamp\":" + "\"" + t + "\"");
            gazeStringBuilder.Append("}");
            gazeStringBuilder.Append("}");

            return gazeStringBuilder.ToString();
        }
    }
}
