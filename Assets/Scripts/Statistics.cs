using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Statistics : MonoBehaviour
{
    public TextMeshProUGUI StatisticsText;
    public ObjectiveManager Objective;

    void Start()
    {
        ShowStats();
    }

    public void ShowStats()
    {
        StatisticsText.text = $"Time: {Mathf.Floor(Time.timeSinceLevelLoad / 60)}m {Mathf.Ceil(Time.timeSinceLevelLoad) % 60}s\n" +
            $"Objectives:\n" +
            $"\tCompleted: {Objective.States.Count(s => s.IsCompleted)}/{Objective.States.Count(s => !s.Objective.CanFail)}\n" +
            $"\tFailed: {Objective.States.Count(s => s.IsFailed)}/{Objective.States.Count(s => s.Objective.CanFail)}\n";
    }

    public void Reload()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
