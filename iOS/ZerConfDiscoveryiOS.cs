using System;
using System.Diagnostics;
using Foundation;
using Xamarin.Forms;
using ZeroConfTest.iOS;

[assembly: Dependency(typeof(ZerConfDiscoveryiOS))]
namespace ZeroConfTest.iOS
{
	public class ZerConfDiscoveryiOS : IZeroConfDiscovery
	{
		public ZerConfDiscoveryiOS()
		{
		}


		public string DiscoverStuff()
		{
			Debug.WriteLine("**** Starting DiscoverStuff");

			string someResult = string.Empty;

			InitBonjourBrowser();

			Debug.WriteLine("**** Exiting DiscoverStuff");
			return someResult;
		}


		static NSNetServiceBrowser _bonjourBrowserService;


		static void InitBonjourBrowser()
		{
			_bonjourBrowserService = new NSNetServiceBrowser();

			_bonjourBrowserService.FoundService += delegate (object sender, NSNetServiceEventArgs e)
			{
				Console.WriteLine("Bonjour found service '{0}'", e.Service.Name);
			};
			_bonjourBrowserService.ServiceRemoved += delegate (object sender, NSNetServiceEventArgs e)
			{
				Console.WriteLine("Bonjour removed service '{0}'", e.Service.Name);
			};
			_bonjourBrowserService.SearchStarted += delegate (object sender, EventArgs e)
			{
				Console.WriteLine("Bonjour starting search");
			};
			_bonjourBrowserService.SearchStopped += delegate (object sender, EventArgs e)
			{
				Console.WriteLine("Bonjour stopped searching");
			};
			_bonjourBrowserService.NotSearched += delegate (object sender, NSNetServiceErrorEventArgs e)
			{
				Console.WriteLine("Bonjour not searched");
			};

			_bonjourBrowserService.SearchForServices("_http._tcp", "");
		}
	}
}

