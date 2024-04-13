using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbilityState
{
    public float RemainingCooldown = 0f;
    public AbilityScriptableObject Ability;
}

public class AbilityInput : MonoBehaviour
{
    public bool CastMode { get; private set; }
    public string CurrentCast { get; private set; }

    public List<AbilityScriptableObject> Abilities = new();
    public List<AbilityState> States = new();

    public UnityEvent<AbilityScriptableObject> OnSuccessfulCast;
    public UnityEvent OnFailedCast;

    private void Awake()
    {
        foreach (var ability in Abilities)
        {
            States.Add(new AbilityState
            {
                RemainingCooldown = 0f,
                Ability = ability
            });
        }
    }

    private void Update()
    {
        // Tick cooldowns (underflow is fine)
        foreach (var state in States) state.RemainingCooldown -= Time.deltaTime;

        // Don't start processing before next frame or we get the space
        if (CastMode) ProcessCast();
        if (Input.GetKeyDown(KeyCode.Space)) ToggleCast(true);
    }

    private void ToggleCast(bool canFail)
    {
        if (canFail && CurrentCast != "" && CastMode) OnFailedCast.Invoke();

        CastMode = !CastMode;
        CurrentCast = "";
    }

    private void ProcessCast()
    {
        // Filter out backspace
        if (Input.inputString.Contains('\b')) return;

        CurrentCast += Input.inputString;

        foreach (var state in States)
        {
            if (state.Ability.CastString == CurrentCast)
            {
                if (state.RemainingCooldown > 0)
                {
                    OnFailedCast.Invoke();
                    ToggleCast(true);
                    return;
                }

                Instantiate(state.Ability.SpawnPrefab, transform.position, transform.rotation);
                state.RemainingCooldown = state.Ability.Cooldown;

                OnSuccessfulCast.Invoke(state.Ability);
                ToggleCast(false);
            }
        }
    }
}
