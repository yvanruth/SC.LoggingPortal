using Microsoft.AspNet.SignalR.Client;
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
            var connection = new HubConnection("http://scloggingportalservice");
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

            hub.On<string>("pull", param => { System.Console.WriteLine(param); });
            System.Console.Read();
            connection.Stop();
        }
    }
}
