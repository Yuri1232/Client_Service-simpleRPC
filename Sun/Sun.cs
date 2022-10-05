using System;
using System.Threading;

using NLog;
using NLog.Config;
using NLog.Targets;

using SimpleRpc;
using SimpleRpc.Transports;
using SimpleRpc.Serialization.Hyperion;
using SimpleRpc.Transports.Http.Client;

using Microsoft.Extensions.DependencyInjection;

using Contract;

namespace client_sun
{
    class Suns
    {
        // initating Nlog
        Logger log = LogManager.GetCurrentClassLogger();

        // Configuring NLog on C#
        private void LogConfiguration()
        {
            var config = new LoggingConfiguration();
            var ForConsole =
                new ConsoleTarget("console")
                {
                    Layout = @"${date:format=HH\:mm\:ss}| ${level}| ${message}| ${exception}"
                };

            config.AddTarget(ForConsole);
            config.AddRuleForAllLevels(ForConsole);

            LogManager.Configuration = config;

        }

        // Implementing SimpleRpc
        private void Run()
        {
            LogConfiguration();

            // keep calling until the request recover from connection failture
            while(true)
            {
                try
                {
                    var SC = new ServiceCollection();
                    SC.AddSimpleRpcClient(
                        "service",
                        new HttpClientTransportOptions
                        {
                            Url = "http://127.0.0.1:6000/Younus",
                            Serializer = "HyperionMessageSerializer"
                        }

                        ).AddSimpleRpcHyperionSerializer();
                    SC.AddSimpleRpcProxy<IService>("service");
                    var SP = SC.BuildServiceProvider();
                    var service = SP.GetService<IService>();
                    SunMethod(service);
                }
                catch(Exception e)
                {
                    // log out this message whenever an error occurs. 
                    log.Warn(e, "Unhandled exception caught. Will restart main loop.");

                    // giving the console a bit of time so it wont overlap over other console Write Lines
                    Thread.Sleep(2000);
                }

            }
            

        }

        // Random genorate Ture => shine Flase => no shine 

        private void SunMethod(IService S){

            var random = new Random();

            while(true){
        
                bool shine = random.Next(0,2) > 0;
                log.Info("Sun state: " + shine);
                if(shine){
                    S.SunCount();
                    S.SunAdd();
                
                    
                   
                }
                else{
                     S.SunCount();
                }
               
                
                 Thread.Sleep(2000);

            }

        }
        
        static void Main(string[] args)
        {
            var self = new Suns();
            self.Run();
            
        }
    }
}
