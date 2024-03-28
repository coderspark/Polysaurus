using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (DinoManager))]
public class DinoManagerEditor : Editor {

	public override void OnInspectorGUI() {
		DinoManager dinoManager = (DinoManager)target;
        DrawDefaultInspector();
		if (GUILayout.Button("Spawn Dinos")) {
			dinoManager.SpawnDinos();
		}
	}
}
