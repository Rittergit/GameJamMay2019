using System;
using UnityEngine;
using UnityEngine.Networking;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject window;
    [SerializeField] private GameObject slaveWin;
    [SerializeField] private GameObject landlordWin;

    private AudioSource audioSource;
    private float restoreTimeScale;

    void Start()
    {
        this.window.SetActive(false);
        this.restoreTimeScale = Time.timeScale;
        EventSystem.Subscribe(GameManager.GameOverEvent, this.OnGameOver);

        audioSource = GetComponent<AudioSource>();
    }

    void OnDestroy()
    {
        Time.timeScale = this.restoreTimeScale;
        EventSystem.Unsubscribe(GameManager.GameOverEvent, this.OnGameOver);
    }

    public void Exit()
    {
        Time.timeScale = this.restoreTimeScale;

        // TODO: Exit to lobby.
        NetworkManager.singleton.StopServer();
    }

    private void OnGameOver(object sender, EventArgs e)
    {
        //Game Over Effect
        audioSource.Play();
        var gm = GameManager.Singleton;

        this.window.SetActive(true);
        this.slaveWin.SetActive(
            gm.CurrentWinner == GameManager.WinnerType.Slave);
        this.landlordWin.SetActive(
            gm.CurrentWinner == GameManager.WinnerType.Landlord);

        this.restoreTimeScale = Time.timeScale;
        Time.timeScale = 0f;
    }
}
