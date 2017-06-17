using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

namespace OMobile.EstimoteUnity
{
	public class EstimoteUnityDemo : MonoBehaviour
	{

		#region Public Variables

		public EstimoteUnity _EstimoteUnity;
		public Text _DebugText;

		#endregion

		#region Unity Methods

		void Start ()
		{
			_EstimoteUnity.OnDidRangeBeacons += HandleDidRangeBeacons;
		}

		#endregion

		#region Public Methods

		public void StartScanning ()
		{
			_EstimoteUnity.StartScanning ();
		}

		public void StopScanning ()
		{
			_EstimoteUnity.StopScanning ();
			_DebugText.text = "Press \"Start Scanning\" to scan for beacons";
		}

		#endregion

		#region Private Methods

		private void HandleDidRangeBeacons (List<EstimoteUnityBeacon> beacons)
		{
			string debugBeacons = "";
			if (beacons != null && beacons.Count > 0) {
				foreach (EstimoteUnityBeacon beacon in beacons) {
					debugBeacons += beacon.ToString () + "\n";
				}
			}
			_DebugText.text = debugBeacons;
		}

		#endregion

	}
}