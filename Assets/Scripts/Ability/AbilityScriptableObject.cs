using UnityEngine;

[CreateAssetMenu(menuName = "LD55/Ability")]
public class AbilityScriptableObject : ScriptableObject
{
    public string Name;
    public string CastString;

    public float Cooldown;
    public GameObject SpawnPrefab;
}
