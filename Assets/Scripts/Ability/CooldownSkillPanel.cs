using TMPro;
using UnityEngine;

public class CooldownSkillPanel : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI CastText;
    public TextMeshProUGUI Time;

    public AbilityState Ability;

    private void Start()
    {
        Name.text = Ability.Ability.Name;
        CastText.text = Ability.Ability.CastString;
    }

    private void Update()
    {
        var cooldown = Mathf.Ceil(Mathf.Max(Ability.RemainingCooldown, 0));
        Time.text = $"{cooldown}s";
    }
}
