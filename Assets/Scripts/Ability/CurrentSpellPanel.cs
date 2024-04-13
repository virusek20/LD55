using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentSpellPanel : MonoBehaviour
{
    public AbilityInput Ability;
    public TextMeshProUGUI CurrentSpellText;
    public Image ActiveBar;

    void Update()
    {
        CurrentSpellText.text = Ability.CurrentCast;
        ActiveBar.enabled = Ability.CastMode;
    }
}
