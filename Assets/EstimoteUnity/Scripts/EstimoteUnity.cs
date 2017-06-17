using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MiniJSON;
using System.Linq;

namespace OMobile.EstimoteUnity
{
	[Serializable]
	public class EstimoteUnity : MonoBehaviour
	{

		#region Public Variables

		/// <summary>
		/// The UUID for the beacons you wish to detect. Defaults to Estimote beacons UUID.
		/// </summary>
		public string BeaconsUUID = "B9407F30-F5F8-466E-AFF9-25556B57FE6D";

		/// <summary>
		/// An event called when beacons have been detected. Passes through the list of beacons detected.
		/// </summary>
		public System.Action<List<EstimoteUnityBeacon>> OnDidRangeBeacons;

		#endregion

		#region Public Methods

		/// <summary>
		/// Starts scanning for beacons.
		/// Also initialises the system if required.
		/// </summary>
		public void StartScanning ()
		{
			#if !UNITY_EDITOR
			#if UNITY_IOS
			EstimoteUnityIOS.StartScanning (BeaconsUUID);
			#elif UNITY_ANDROID
			EstimoteUnityAndroid.StartScanning (BeaconsUUID);
			#endif
			#endif
		}

		/// <summary>
		/// Stops scanning for beacons.
		/// No more updates will come through once this has been called until StartScanning is called again.
		/// </summary>
		public void StopScanning ()
		{
			#if !UNITY_EDITOR
			#if UNITY_IOS
			EstimoteUnityIOS.StopScanning ();
			#elif UNITY_ANDROID
			EstimoteUnityAndroid.StopScanning ();
			#endif
			#endif
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Called by the native plugin when beacons are detected.
		/// </summary>
		/// <param name="beacons">A string representation of the detected beacons</param>
		private void DidRangeBeacons (string beacons)
		{
			if (!string.IsNullOrEmpty (beacons)) {
				Debug.Log (beacons);
				List<EstimoteUnityBeacon> estimoteBeacons = new List<EstimoteUnityBeacon> ();

				var beaconsJsonList = MiniJSON.Json.Deserialize (beacons) as List<object>;
				foreach (object o in beaconsJsonList) {
					Dictionary<string, object> beaconJson = o as Dictionary<string, object>;

					string uuid = (string)beaconJson ["UUID"];
					int major = (int)((long)beaconJson ["Major"]);
					int minor = (int)((long)beaconJson ["Minor"]);
					int range = (int)((long)beaconJson ["BeaconRange"]);
					int strength = (int)((long)beaconJson ["RSSI"]);
					double accuracy = GetDouble (beaconJson ["Accuracy"]);

					EstimoteUnityBeacon estimoteUnityBeacon = new EstimoteUnityBeacon (uuid, major, minor, range, strength, accuracy);
					estimoteBeacons.Add (estimoteUnityBeacon);
				}

				if (OnDidRangeBeacons != null) {
					OnDidRangeBeacons.Invoke (estimoteBeacons);
				}
			} else {
				if (OnDidRangeBeacons != null) {
					OnDidRangeBeacons.Invoke (null);
				}
			}
		}

		private double GetDouble (object obj)
		{
			double val;
			if (obj.GetType () == typeof(Int64)) {
				val = System.Convert.ToDouble (obj);
			}
			val = (double)obj;
			return val;
		}

		#endregion

	}
}