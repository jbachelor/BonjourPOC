using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Zeroconf;

namespace ZeroConfTest
{
	public partial class ZeroConfTestPage : ContentPage
	{
		public ZeroConfTestPage()
		{
			InitializeComponent();
		}

		void OnDiscoverStuff(object sender, System.EventArgs e)
		{
			Debug.WriteLine("**** Starting OnDiscoverStuff");

			var discovery = DependencyService.Get<IZeroConfDiscovery>().DiscoverStuff();

			Debug.WriteLine("**** Exiting OnDiscoverStuff");
		}

		void OnFindPrintersProtocolOnly(object sender, System.EventArgs e)
		{
			ProbeForNetworkPrinters();
		}

		void OnFindPrinters(object sender, System.EventArgs e)
		{
			ProbeForNetworkPrintersWithStuff();
		}

		void OnBrowseAll(object sender, System.EventArgs e)
		{
			EnumerateAllServicesFromAllHosts();
		}

		void OnDummyServiceCheck(object sender, System.EventArgs e)
		{
			DummyServiceCheck();
		}

		public async Task DummyServiceCheck()
		{
			Debug.WriteLine($"Starting DummyServiceCheck");
			var results = await ZeroconfResolver.ResolveAsync("_test._tcp.local.");
			Debug.WriteLine($"Exiting DummyServiceCheck");
		}

		public async Task EnumerateAllServicesFromAllHosts()
		{
			Debug.WriteLine("Entering EnumerateAllServicesFromAllHosts");
			ILookup<string, string> domains = await ZeroconfResolver.BrowseDomainsAsync();
			var responses = await ZeroconfResolver.ResolveAsync(domains.Select(g => g.Key));
			foreach (var resp in responses)
			{
				Debug.WriteLine(resp);
			}
			Debug.WriteLine("Done with EnumerateAllServicesFromAllHosts");
		}

		public async Task ProbeForNetworkPrinters()
		{
			Debug.WriteLine("**** Starting ProbeForNetworkPrinters");

			IReadOnlyList<IZeroconfHost> results = await
				ZeroconfResolver.ResolveAsync("_printer._tcp.local.");

			Debug.WriteLine("**** Finished ProbeForNetworkPrinters");
		}

		public async Task ProbeForNetworkPrintersWithStuff()
		{
			Debug.WriteLine("**** Starting ProbeForNetworkPrintersWithStuff");

			IReadOnlyList<IZeroconfHost> results = await
				ZeroconfResolver.ResolveAsync("_printer._tcp.local.", TimeSpan.FromSeconds(2), 2, 2000, (obj) =>
				{
					Debug.WriteLine($"Got a thing:  {obj}");
				}, default(System.Threading.CancellationToken));

			Debug.WriteLine("**** Finished ProbeForNetworkPrintersWithStuff");
		}
	}
}

