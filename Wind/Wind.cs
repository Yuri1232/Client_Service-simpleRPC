namespace Wind;
using System;

using Microsoft.Extensions.DependencyInjection;

using SimpleRpc.Serialization.Hyperion;
using SimpleRpc.Transports;
using SimpleRpc.Transports.Http.Client;

using NLog;

using Contract;



/// <summary>
/// Client example.
/// </summary>
class Winds
{
	/// <summary>
	/// Logger for this class.
	/// </summary>
	Logger log = LogManager.GetCurrentClassLogger();

	/// <summary>
	/// Configures logging subsystem.
	/// </summary>
	private void ConfigureLogging()
	{
		var config = new NLog.Config.LoggingConfiguration();

		var console =
			new NLog.Targets.ConsoleTarget("console")
			{
				Layout = @"${date:format=HH\:mm\:ss}|${level}| ${message} ${exception}"
			};
		config.AddTarget(console);
		config.AddRuleForAllLevels(console);

		LogManager.Configuration = config;
	}

	/// <summary>
	/// Program body.
	/// </summary>
	private void Run() {
		//configure logging
		ConfigureLogging();

		// keep calling until the request recover from connection failture
		while( true )
		{
			try {
				
				var sc = new ServiceCollection();
				sc
					.AddSimpleRpcClient(
						"service", 
						new HttpClientTransportOptions
						{
							Url = "http://127.0.0.1:6000/Younus",
							Serializer = "HyperionMessageSerializer"
						}
					)
					.AddSimpleRpcHyperionSerializer();

				sc.AddSimpleRpcProxy<IService>("service");

				var sp = sc.BuildServiceProvider();

				var service = sp.GetService<IService>();

                WindMethod(service);

			
            }
            catch (Exception e)
				{
					//log whatever exception to console
					log.Warn(e, "Unhandled exception caught. Will restart main loop.");

					// giving the console a bit of time so it wont overlap over other console Write Lines
					Thread.Sleep(2000);
				}
        
        }
    }


			 private void WindMethod(IService service)
		{
			var random = new Random();

				var index_Wind=service.ResgisterWind();
			

			while (true)
			{
                //wind generates random values that show if it blows(true) or not(false) 
                bool isBlowing = random.Next(0, 2) > 0;

                log.Info("Wind value: " + isBlowing);
				service.SetWind(index_Wind,isBlowing);
              

                // giving the console a bit of time so it wont overlap over other console Write Lines
                Thread.Sleep(2000);
			}
		}
		/// <summary>
		/// Program entry point.
		/// </summary>
		/// <param name="args">Command line arguments.</param>
		static void Main(string[] args)
		{
			var self = new Winds();
			self.Run();
		}
}
