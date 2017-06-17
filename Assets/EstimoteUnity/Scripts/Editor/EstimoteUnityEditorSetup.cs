using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace OMobile.EstimoteUnity
{
    public class EstimoteUnityEditorSetup : EditorWindow
    {

        #region Private Variables

        //		private string mIOSFramworkURL = EstimoteUnityEditorUtils.IOS_ESTIMOTE_FRAMEWORK_URL;
        private bool mIsDownloading = false;
        private bool mHasDownloadCompleted = false;
        private byte[] mDownloadedBytes;
        private System.Action mOnDownloadCompleteAction;

        // GUI
        private GUIStyle mTitleStyle = null;
        private GUIStyle mSubTitleStyle = null;

        #endregion

        #region Menu Items

        [MenuItem("Window/O-Mobile/Estimote Unity/Setup")]
        public static void OpenWindow()
        {
            EstimoteUnityEditorSetup window = (EstimoteUnityEditorSetup)EditorWindow.GetWindow(typeof(EstimoteUnityEditorSetup));
            window.titleContent = new GUIContent("Estimote Unity Setup");
            window.Show();
        }

        #endregion

        #region Unity Methods

        void OnEnable()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;
        }

        void OnGUI()
        {
            DrawHeader();

            EditorGUILayout.Space();

            DrawInfo();

            EditorGUILayout.Space();

            DrawAndroidSetup();

            EditorGUILayout.Space();

            DrawIOSSetup();
        }

        void Update()
        {
            if (mIsDownloading)
            {
                if (mHasDownloadCompleted)
                {
                    mIsDownloading = false;
                    mHasDownloadCompleted = false;
                    if (mOnDownloadCompleteAction != null)
                    {
                        mOnDownloadCompleteAction.Invoke();
                        mOnDownloadCompleteAction = null;
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        private void DrawHeader()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Estimote Unity Setup", GetTitleStyle());
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("by Oakley Mobile Ltd.", GetSubTitleStyle());
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void DrawInfo()
        {
            EditorGUILayout.HelpBox("Before you can build using the Estimote Unity plugin you" +
            " need to ensure you have downloaded and placed the correct framework/aar files" +
            " into your project.\n\nOn Android you can do this automatically by using the button below." +
            " For iOS you will need to do this manually until we find a solution", MessageType.Info);
        }

        private void DrawAndroidSetup()
        {
            GUILayout.Label("Android Setup", EditorStyles.boldLabel);
            bool androidStatus = EstimoteUnityEditorUtils.CheckAndroidStatus();
            GUI.color = (androidStatus == true ? Color.green : Color.red);
            GUILayout.Label("Status: " + (androidStatus == true ? "Complete" : "Incomplete"));
            GUI.color = Color.white;

            if (GUILayout.Button(androidStatus == true ? "Redownload Estimote AAR" : "Download Estimote AAR"))
            {
                DownloadEstimoteAAR();
            }
        }

        private void DrawIOSSetup()
        {
            GUILayout.Label("iOS Setup", EditorStyles.boldLabel);
            //			mIOSFramworkURL = EditorGUILayout.TextField ("Estimote Framework URL:", mIOSFramworkURL);
            bool iOSStatus = EstimoteUnityEditorUtils.CheckIOSStatus();
            GUI.color = (iOSStatus == true ? Color.green : Color.red);
            GUILayout.Label("Status: " + (iOSStatus == true ? "Complete" : "Incomplete"));
            GUI.color = Color.white;

            EditorGUILayout.HelpBox("Once you have downloaded the archive, extract it and place the" +
            " EstimoteSDK.framework inside your Unity project at EstimoteUnity/Plugins/iOS/. The" +
            " status message above will turn green once completed correctly.", MessageType.Info);

            if (GUILayout.Button("Open Estimote Github"))
            {
                OpenIOSEstimoteGithub();
            }
        }

        private void OpenIOSEstimoteGithub()
        {
            Application.OpenURL(EstimoteUnityEditorUtils.IOS_ESTIMOTE_FRAMEWORK_URL);

            // NOTE: The following does not work. Once we have a workaround for the extraction issues we will implement an automatic solution.
            //			PerformDownload (mIOSFramworkURL, SaveEstimoteFrameworkIntoProject, "Downloading Estimote.framework");
        }

        private void DownloadEstimoteAAR()
        {
            PerformDownload(EstimoteUnityEditorUtils.ANDROID_ESTIMOTE_AAR_URL, SaveAARIntoProject, "Downloading AAR");
        }

        private void PerformDownload(string url, System.Action callback, string downloadMessage)
        {
            Debug.Log("Downloading from URL: " + url);
            mIsDownloading = true;
            mHasDownloadCompleted = false;
            mOnDownloadCompleteAction = callback;
            EditorUtility.DisplayProgressBar("Downloading...", downloadMessage, 0.5f);
            WebClient client = new WebClient();
            client.DownloadDataCompleted += delegate (object sender, DownloadDataCompletedEventArgs response)
            {
                mHasDownloadCompleted = true;
                if (response.Error != null)
                {
                    Debug.LogError(response.Error.Message);
                    return;
                }
                mDownloadedBytes = response.Result;
            };
            client.DownloadDataAsync(new System.Uri(url));
        }

        /// <summary>
        /// This currently does not work. There are issues with extraction of the zip, once we have a solution we will implement this again.
        /// </summary>
        private void SaveEstimoteFrameworkIntoProject()
        {
            //			EditorUtility.ClearProgressBar ();
            //
            //			string filePath = Application.dataPath + "/EstimoteUnity/Plugins/iOS/iOS-SDK.zip";
            //			File.WriteAllBytes (filePath, mDownloadedBytes);
            //
            //			System.Diagnostics.Process proc = new System.Diagnostics.Process ();
            //			proc.StartInfo.FileName = "sh";
            //			proc.StartInfo.Arguments = "osx.sh";
            //			proc.StartInfo.RedirectStandardError = true;
            //			proc.StartInfo.RedirectStandardOutput = true;
            //			proc.StartInfo.CreateNoWindow = true;
            //			proc.StartInfo.WorkingDirectory = Application.dataPath + "/EstimoteUnity/Plugins/iOS/";
            //			proc.StartInfo.UseShellExecute = false;
            //			proc.Start ();
            //
            //			// Check to see if we have the SDK already
            //			if (Directory.Exists (EstimoteUnityEditorUtils.IOS_ESTIMOTE_FRAMEWORK_PATH)) {
            //				FileUtil.DeleteFileOrDirectory (EstimoteUnityEditorUtils.IOS_ESTIMOTE_FRAMEWORK_PATH);
            //			}
            //
            //			string commitVersion = mIOSFramworkURL.Substring (mIOSFramworkURL.LastIndexOf ("/") + 1);
            //			commitVersion = commitVersion.Replace (".zip", "");
            //
            //			FileUtil.MoveFileOrDirectory (Application.dataPath + "/EstimoteUnity/Plugins/iOS/iOS-SDK-" + commitVersion + "/EstimoteSDK/EstimoteSDK.framework", Application.dataPath + "/EstimoteUnity/Plugins/iOS/EstimoteSDK.framework");
            //
            //			FileUtil.DeleteFileOrDirectory (Application.dataPath + "/EstimoteUnity/Plugins/iOS/iOS-SDK-" + commitVersion + "/");
            //			FileUtil.DeleteFileOrDirectory (Application.dataPath + "/EstimoteUnity/Plugins/iOS/iOS-SDK.zip");
            //
            //			AssetDatabase.Refresh ();
            //
            //			try {
            //				PluginImporter[] pluginImporters = PluginImporter.GetAllImporters ();
            //				foreach (PluginImporter pluginImporter in pluginImporters) {
            //					bool iosPluginImporter = pluginImporter.assetPath.Contains ("EstimoteSDK");
            //					if (iosPluginImporter) {
            //						pluginImporter.SetCompatibleWithAnyPlatform (false);
            //						pluginImporter.SetCompatibleWithEditor (false);
            //						pluginImporter.SetCompatibleWithPlatform (BuildTarget.iOS, true);
            //						pluginImporter.SaveAndReimport ();
            //					}
            //				}
            //			} catch (Exception e) {
            //				Debug.Log ("Failed to update plugin import settings");
            //			}
            //
            //			EditorUtility.DisplayDialog ("Done!", "The Estimote.framework has been added into your project.", "OK");
        }

        private void SaveAARIntoProject()
        {
            EditorUtility.ClearProgressBar();

            string filePath = EstimoteUnityEditorUtils.GetAndroidEstimoteFrameworkPath();
            File.WriteAllBytes(filePath, mDownloadedBytes);
            AssetDatabase.Refresh();

            EditorUtility.DisplayDialog("Done!", "The Estimote SDK AAR file has been added into your project.", "OK");
        }

        private GUIStyle GetTitleStyle()
        {
            if (mTitleStyle == null)
            {
                mTitleStyle = new GUIStyle(EditorStyles.largeLabel);
                mTitleStyle.fontStyle = FontStyle.Bold;
                mTitleStyle.fontSize = 20;
            }
            return mTitleStyle;
        }

        private GUIStyle GetSubTitleStyle()
        {
            if (mSubTitleStyle == null)
            {
                mSubTitleStyle = new GUIStyle(EditorStyles.largeLabel);
            }
            return mSubTitleStyle;
        }

        private bool MyRemoteCertificateValidationCallback(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            bool isOk = true;
            // If there are errors in the certificate chain, look at each error to determine the cause.
            if (sslPolicyErrors != SslPolicyErrors.None)
            {
                for (int i = 0; i < chain.ChainStatus.Length; i++)
                {
                    if (chain.ChainStatus[i].Status != X509ChainStatusFlags.RevocationStatusUnknown)
                    {
                        chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                        chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                        chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                        chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                        bool chainIsValid = chain.Build((X509Certificate2)certificate);
                        if (!chainIsValid)
                        {
                            isOk = false;
                        }
                    }
                }
            }
            return isOk;
        }

        #endregion

    }
}