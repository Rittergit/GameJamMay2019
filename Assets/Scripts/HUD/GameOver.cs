using System;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject window;

    private float restoreTimeScale;

    void Start()
    {
        this.window.SetActive(false);
        this.restoreTimeScale = Time.timeScale;
        EventSystem.Subscribe(GameManager.GameOverEvent, this.OnGameOver);
    }

    void OnDestroy()
    {
        Time.timeScale = this.restoreTimeScale;
        EventSystem.Unsubscribe(GameManager.GameOverEvent, this.OnGameOver);
    }

    public void Exit()
    {
        // TODO: Exit to lobby.
    }

    private void OnGameOver(object sender, EventArgs e)
    {
        this.window.SetActive(true);
        this.restoreTimeScale = Time.timeScale;
        Time.timeScale = 0f;
    }
}
