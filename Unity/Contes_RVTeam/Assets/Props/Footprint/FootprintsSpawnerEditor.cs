using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FootprintsSpawner))]
public class FootprintsSpawnerEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        FootprintsSpawner spawner = (FootprintsSpawner)target;

        if (GUILayout.Button("Spawn"))
            spawner.Spawn();

    }
}
