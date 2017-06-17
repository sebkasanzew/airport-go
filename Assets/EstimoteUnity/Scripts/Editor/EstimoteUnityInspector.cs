using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace OMobile.EstimoteUnity
{
	[CustomEditor (typeof(EstimoteUnity))]
	public class EstimoteUnityInspector : Editor
	{

		#region Private Variables

		// GUI
		private GUIStyle mTitleStyle = null;
		private GUIStyle mSubTitleStyle = null;

		// Properties
		private SerializedProperty mBeaconsUUIDProperty;

		#endregion


		#region Unity Methods

		void OnEnable ()
		{
			mBeaconsUUIDProperty = this.serializedObject.FindProperty ("BeaconsUUID");
		}

		public override void OnInspectorGUI ()
		{
			serializedObject.Update ();

			DrawHeading ();

			EditorGUILayout.Space ();

			DrawProperties ();

			DrawWarnings ();

			EditorGUILayout.Space ();
			EditorGUILayout.Space ();

			DrawAbout ();

			EditorGUILayout.Space ();

			if (GUI.changed) {
				serializedObject.ApplyModifiedProperties ();
			}
		}

		#endregion

		#region Private Methods

		private void DrawHeading ()
		{
			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace ();
			GUILayout.Label ("Estimote Unity", GetTitleStyle ());
			GUILayout.FlexibleSpace ();
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace ();
			GUILayout.Label ("by Oakley Mobile Ltd.", GetSubTitleStyle ());
			GUILayout.FlexibleSpace ();
			GUILayout.EndHorizontal ();
		}

		private void DrawProperties ()
		{
			EditorGUILayout.HelpBox ("This is the Proximity UUID for the beacons you wish to detect." +
			"\nNOTE: Currently only a single Proximity UUID is supported.", MessageType.Info);
			EditorGUILayout.PropertyField (mBeaconsUUIDProperty);
		}

		private void DrawWarnings ()
		{
			// Check Android Status
			if (!EstimoteUnityEditorUtils.CheckAndroidStatus ()) {
				EditorGUILayout.Space ();
				GUI.color = Color.red;
				EditorGUILayout.HelpBox ("Android setup is not complete!" +
				" To complete the setup go to Window/O-Mobile/Estimote Unity/Setup and follow the instructions.", MessageType.Info);
				GUI.color = Color.white;
			}

			// Check iOS Status
			if (!EstimoteUnityEditorUtils.CheckIOSStatus ()) {
				EditorGUILayout.Space ();
				GUI.color = Color.red;
				EditorGUILayout.HelpBox ("iOS setup is not complete!" +
				" To complete the setup go to Window/O-Mobile/Estimote Unity/Setup and follow the instructions.", MessageType.Info);
				GUI.color = Color.white;
			}

			// Show Button
			if (!EstimoteUnityEditorUtils.CheckAndroidStatus () || !EstimoteUnityEditorUtils.CheckIOSStatus ()) {
				EditorGUILayout.Space ();
				if (GUILayout.Button ("Open Estimote Unity Setup")) {
					EstimoteUnityEditorSetup.OpenWindow ();
				}
			}
		}

		private void DrawAbout ()
		{
			// Estimotes Developer Portal
			if (GUILayout.Button ("Visit Estimote Developer Portal")) {
				Application.OpenURL ("http://developer.estimote.com/");
			}

			EditorGUILayout.Space ();

			// O-Mobile's website
			if (GUILayout.Button ("Visit Oakley Mobile's Website")) {
				Application.OpenURL ("http://www.o-mobile.co.uk/");
			}
		}

		private GUIStyle GetTitleStyle ()
		{
			if (mTitleStyle == null) {
				mTitleStyle = new GUIStyle (EditorStyles.largeLabel);
				mTitleStyle.fontStyle = FontStyle.Bold;
				mTitleStyle.fontSize = 20;
			}
			return mTitleStyle;
		}

		private GUIStyle GetSubTitleStyle ()
		{
			if (mSubTitleStyle == null) {
				mSubTitleStyle = new GUIStyle (EditorStyles.largeLabel);
			}
			return mSubTitleStyle;
		}

		#endregion

	}
}