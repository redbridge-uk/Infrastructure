using System.Reactive.Subjects;

namespace Redbridge.Connectivity
{
	public interface INetworkConnectivityMonitor
	{
		void Start();
		void Stop();
		BehaviorSubject<NetworkState> NetworkStatus { get; }
	}
}
