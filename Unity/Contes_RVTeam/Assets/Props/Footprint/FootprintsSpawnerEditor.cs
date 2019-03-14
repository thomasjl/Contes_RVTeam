using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
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
#endif
