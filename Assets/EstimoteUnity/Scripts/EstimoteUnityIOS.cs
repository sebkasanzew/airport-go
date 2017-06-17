using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

namespace OMobile.EstimoteUnity
{
	public class EstimoteUnityIOS
	{

		#if UNITY_IOS

		#region Native Methods
		[DllImport ("__Internal")]
		private static extern void StartEstimoteScanning (string beaconUUID);

		[DllImport ("__Internal")]
		private static extern void StopEstimoteScanning ();

		[DllImport ("__Internal")]
		private static extern int CheckDeviceSupportsBeacons ();

		#endregion

		#region Public Static Methods

		public static void StartScanning (string beaconUUID)
		{
			if (!CheckDeviceSupported ()) {
				return;
			}
			StartEstimoteScanning (beaconUUID);
		}

		public static void StopScanning ()
		{
			StopEstimoteScanning ();
		}

		public static bool CheckDeviceSupported ()
		{
			return CheckDeviceSupportsBeacons () == 1;
		}

		#endregion

		#endif

	}
}