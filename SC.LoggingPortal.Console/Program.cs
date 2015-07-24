using Microsoft.AspNet.SignalR.Client;
using SC.LoggingPortal.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC.LoggingPortal.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] data = new string[] {};
            foreach(var arg in args)
            {
                if(arg.StartsWith("--server=", StringComparison.OrdinalIgnoreCase))
                {
                    data = arg.Split('=');
                }
            }

            if(data.Count() < 2)
            {
                return;
            }


            var connection = new HubConnection(data[1]);
            var hub = connection.CreateHubProxy("LoggingHub");
            connection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    System.Console.WriteLine("There was an error opening the connection: {0}", task.Exception.GetBaseException());
                }
                else
                {
                    System.Console.WriteLine("Connected");
                }
            }).Wait();

            hub.On<LogMessage>("pull", param => 
            {
                SetConsoleColor(param.LogLevel);
                System.Console.WriteLine(param.LoggerMessage);
                System.Console.ForegroundColor = ConsoleColor.White;
            });


            System.Console.Read();
            connection.Stop();
        }

        private static void SetConsoleColor(string param)
        {
            var level = param.Split('-').FirstOrDefault();
            if(string.IsNullOrWhiteSpace(level))
            {
                return;
            }

            ConsoleColor color = ConsoleColor.White;

            switch (level.Trim().ToUpper())
            {
                case "ERROR":
                case "CRITICAL":
                case "FATAL":
                    color = ConsoleColor.Red;
                    break;
                case "WARN":
                    color = ConsoleColor.Yellow;
                    break;
            }

            System.Console.ForegroundColor = color;
        }
    }
}
