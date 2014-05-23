using UnityEngine;
using System.Collections;
using UnityEditor;

public class audioEditor : EditorWindow {


	public GameObject audioObject;
	[MenuItem("Window/Audio Controller")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(audioEditor));
	}

	void OnGUI () {

		GUILayout.Space(20);
		GUILayout.Label("Welcome to the Audio Manager");
		GUILayout.Label("Please Go to the starting scene of your project,");
		GUILayout.Label("then click Setup.");
		if(GUILayout.Button("Setup AudioObject"))
		{
			if(GameObject.Find("AudioController") == true)
			{
				Debug.LogError("AudioController already exists in scene");
			}
			else
			{
				GameObject go = new GameObject("AudioController");
				go.transform.position = new Vector3(0, 0, 0);
				go.AddComponent<audioController>();
				Debug.Log("AudioController Created");
			}
		}
		GUILayout.Space(10);
		if(GUILayout.Button("Documentation"))
		{
			Debug.Log("Go To Documentation");
		}

	}

}
