using UnityEngine;
using System.IO;

namespace OMobile.EstimoteUnity
{
	public class EstimoteUnityEditorUtils
	{

		#if UNITY_EDITOR

		#region Public Static Variables

		public static string IOS_ESTIMOTE_FRAMEWORK_URL = "https://github.com/Estimote/iOS-SDK/archive/master.zip";
//		public static string ANDROID_ESTIMOTE_AAR_URL = "http://github.com/Estimote/Android-SDK/blob/master/EstimoteSDK/estimote-sdk.aar?raw=true";
		public static string ANDROID_ESTIMOTE_AAR_URL = "http://github.com/Estimote/Android-SDK/blob/076f4ec884281cac5cc1d29d6a726561c090955f/EstimoteSDK/estimote-sdk.aar?raw=true";

		#endregion

		#region Private Static Variables

		private static string IOS_ESTIMOTE_FRAMEWORK_PATH = "/EstimoteUnity/Plugins/iOS/EstimoteSDK.framework/";
		private static string ANDROID_ESTIMOTE_AAR_PATH = "/EstimoteUnity/Plugins/Android/estimote-sdk.aar";

		#endregion

		#region Public Static Methods

		public static string GetIOSEstimoteFrameworkPath ()
		{
			return Application.dataPath + IOS_ESTIMOTE_FRAMEWORK_PATH;
		}

		public static string GetAndroidEstimoteFrameworkPath ()
		{
			return Application.dataPath + ANDROID_ESTIMOTE_AAR_PATH;
		}

		public static bool CheckIOSStatus ()
		{
			bool status = false;
			if (Directory.Exists (GetIOSEstimoteFrameworkPath ())) {
				status = true;
			}
			return status;
		}

		public static bool CheckAndroidStatus ()
		{
			bool status = false;
			if (File.Exists (GetAndroidEstimoteFrameworkPath ())) {
				status = true;
			}
			return status;
		}

		#endregion

		#endif

	}
}