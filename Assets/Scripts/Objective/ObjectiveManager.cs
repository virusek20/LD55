using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ObjectiveState
{
    public int Counter;
    public bool IsCompleted => !IsFailed && Counter >= Objective.RequiredCounters;
    public bool IsFailed;
    public ObjectiveScriptableObject Objective;
}

public class ObjectiveManager : MonoBehaviour
{
    public List<ObjectiveScriptableObject> Objectives = new();
    public GameObject ObjectivePanelPrefab;

    // TODO: Could be dictionary
    public List<ObjectiveState> States = new();

    public UnityEvent OnMissionCompleted;
    public UnityEvent OnMissionFail;

    public UnityEvent<ObjectiveScriptableObject> OnObjectiveFailed;
    public UnityEvent<ObjectiveScriptableObject> OnObjectiveCompleted;

    private void Awake()
    {
        foreach (var objective in Objectives)
        {
            var state = new ObjectiveState
            {
                Counter = 0,
                Objective = objective
            };

            States.Add(state);
            var panel = Instantiate(ObjectivePanelPrefab, transform);
            panel.GetComponent<ObjectivePanel>().Objective = state;
        }
    }

    public void FailObjective(string id)
    {
        var objective = FindState(id);
        objective.IsFailed = true;

        OnObjectiveFailed.Invoke(objective.Objective);
        if (objective.Objective.IsRequired) OnMissionFail.Invoke();
    }

    public void AddCounter(string id)
    {
        var objective = FindState(id);
        if (objective.IsCompleted) // No need to re-notify everyone
        {
            objective.Counter++;
            return;
        }

        objective.Counter++;
        if (objective.IsCompleted) OnObjectiveCompleted.Invoke(objective.Objective);
        CheckMissionCompletion();
    }

    private void CheckMissionCompletion()
    {
        var isMissing = States.Any(s => !s.IsCompleted);
        if (!isMissing) OnMissionCompleted.Invoke();
    }

    private ObjectiveState FindState(string id)
    {
        return States.FirstOrDefault(s => s.Objective.Id == id) ?? throw new Exception($"Failed to locate objective with id '{id}'");
    }
}
