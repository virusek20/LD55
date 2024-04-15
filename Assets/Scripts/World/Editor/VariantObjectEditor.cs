using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(VariantObject))]
public class VariantObjectEditor : Editor
{
    private VariantObject _target;

    private void OnEnable() => _target = target as VariantObject;

    private void OnDisable() => _target = null;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        int selectedVariant = _target.SelectedVariantIndex;

        GUILayout.BeginHorizontal();
        GUILayout.Label($"Selected variant: {_target.SelectedVariantName}");
        if(GUILayout.Button("Previous"))
        {
            selectedVariant--;
            _target.SelectVariant(selectedVariant);
            EditorUtility.SetDirty(_target);
        }
        if (GUILayout.Button("Next"))
        {
            selectedVariant++;
            _target.SelectVariant(selectedVariant);
            EditorUtility.SetDirty(_target);
        }
        GUILayout.EndHorizontal();
    }
}
