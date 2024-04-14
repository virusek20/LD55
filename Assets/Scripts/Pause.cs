using UnityEngine;
using UnityEngine.Events;

public class Pause : MonoBehaviour
{
    public UnityEvent OnPaused;
    public UnityEvent OnUnpaused;

    public bool Paused { get; private set; }
    public GameObject PauseScreen;

    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;

        Paused = !Paused;

        Time.timeScale = Paused ? 0f : 1f;
        PauseScreen.SetActive(Paused);

        if (Paused) OnPaused.Invoke();
        else OnUnpaused.Invoke();   
    }
}
