using UnityEngine;
using System.Collections;

namespace OMobile.EstimoteUnity
{
	public class EstimoteUnityAndroid
	{
		#if UNITY_ANDROID

		#region Private Static Variables

		private static AndroidJavaObject mActivityContext;
		private static AndroidJavaObject mEstimoteUnityAndroidPlugin;
		private static bool mInitialized = false;

		#endregion

		#region Public Static Methods

		public static void StartScanning (string beaconUUID)
		{
			if (!mInitialized) {
				Initialize (beaconUUID);
			}
			mEstimoteUnityAndroidPlugin.Call ("StartScanning");
		}

		public static void StopScanning ()
		{
			if (!mInitialized) {
				return;
			}
			mEstimoteUnityAndroidPlugin.Call ("StopScanning");
		}

		#endregion

		#region Private Static Methods

		private static void Initialize (string beaconUUID)
		{
            Debug.Log("Initialize Estimote SDK");
			using (AndroidJavaClass unityPlayerClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer")) {
				mActivityContext = unityPlayerClass.GetStatic<AndroidJavaObject> ("currentActivity");
			}
			using (AndroidJavaClass estimoteUnityAndroidPluginClass = new AndroidJavaClass ("uk.co.omobile.estimoteunityandroidplugin.EstimoteUnityAndroidPlugin")) {
				mEstimoteUnityAndroidPlugin = estimoteUnityAndroidPluginClass.CallStatic<AndroidJavaObject> ("Instance");
				mEstimoteUnityAndroidPlugin.Call ("InitEstimote", mActivityContext, beaconUUID);
			}
			mInitialized = true;
		}

		#endregion

		#endif
	}
}