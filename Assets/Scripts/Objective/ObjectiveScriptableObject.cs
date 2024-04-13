using UnityEngine;

[CreateAssetMenu(menuName = "LD55/Objective")]
public class ObjectiveScriptableObject : ScriptableObject
{
    public string Id;

    public string Name;
    public string Description;
    public Sprite Icon;

    public bool CanFail;
    public bool IsRequired;

    [Range(1, 100)]
    public int RequiredCounters = 1;
}
