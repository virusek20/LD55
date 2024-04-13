using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivePanel : MonoBehaviour
{
    public ObjectiveState Objective;

    public Image Background;
    public Image Icon;

    public TextMeshProUGUI Title;
    public TextMeshProUGUI Description;

    void Start()
    {
        Icon.sprite = Objective.Objective.Icon;
        Title.text = Objective.Objective.Name;
    }

    void Update()
    {
        // TODO: Big string generation
        Description.text = $"{Objective.Objective.Description} [{Objective.Counter}/{Objective.Objective.RequiredCounters}]";

        Color color;
        if (Objective.IsCompleted) color = Color.green;
        else if (Objective.IsFailed) color = Color.red;
        else color = Color.yellow;

        color.a = 0.1f;
        Background.color = color;
    }
}
