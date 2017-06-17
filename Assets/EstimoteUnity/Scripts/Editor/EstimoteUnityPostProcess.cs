#if UNITY_IOS
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

namespace OMobile.EstimoteUnity
{
	public static class EstimoteUnityPostProcess
	{

		[PostProcessBuild]
		private static void OnPostProcessBuild (BuildTarget target, string path)
		{
			#if UNITY_IOS
			string plistPath = path + "/Info.plist";
			PlistDocument plist = new PlistDocument ();
			plist.ReadFromFile (plistPath);
			PlistElementDict rootDict = plist.root;
			string usageDescription = "For beacon detection";
			rootDict.SetString ("NSLocationUsageDescription", usageDescription);
			rootDict.SetString ("NSLocationAlwaysUsageDescription", usageDescription);

			// Write to file
			plist.WriteToFile (plistPath);
			#endif
		}
	}
}

#endif