using UnityEngine;

public class CooldownPanel : MonoBehaviour
{
    public AbilityInput Ability;
    public GameObject AbilitySlotPrefab;

    private void Start()
    {
        foreach (var ability in Ability.States)
        {
            var slot = Instantiate(AbilitySlotPrefab, transform);
            slot.GetComponent<CooldownSkillPanel>().Ability = ability;
        }
    }
}
