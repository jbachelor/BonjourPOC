using System;
using Xamarin.Forms;
using ZeroConfTest.Droid;

[assembly: Dependency(typeof(ZeroConfDiscoveryDroid))]
namespace ZeroConfTest.Droid
{
	public class ZeroConfDiscoveryDroid : IZeroConfDiscovery
	{
		public ZeroConfDiscoveryDroid()
		{
		}

		public string DiscoverStuff()
		{
			throw new NotImplementedException();
		}
	}
}

