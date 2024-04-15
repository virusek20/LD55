using System;
using System.Drawing.Printing;
using UnityEditor;
using UnityEngine;

public class AlignSelection
{
    [MenuItem("Procedures/Selection/Align Position")]
    public static void AlignPosition() => Align(AlignPosition);

    private static void AlignPosition(GameObject gameobject)
    {
        Vector3 p;
        p = gameobject.transform.position;
        p = new Vector3(p.x * 2, p.y * 2, p.z * 2);
        p = new Vector3(Mathf.Round(p.x), Mathf.Round(p.y), Mathf.Round(p.z));
        p = new Vector3(p.x / 2, p.y / 2, p.z / 2);
        gameobject.transform.position = p;
        EditorUtility.SetDirty(gameobject.transform);
    }

    public static void Align(Action<GameObject> action)
    {
        foreach (var m in Selection.gameObjects)
            action.Invoke(m);
    }
}
