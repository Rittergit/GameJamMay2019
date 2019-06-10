using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayModeSelect : MonoBehaviour
{
    public string nextScene;

    [SerializeField] GameObject playModeSelectWindow;
    [SerializeField] NetworkLobbyHUD networkLobby;
    [SerializeField] PlayerSelectionManager playerSelectionManager;

    [SerializeField] GameObject splitscreenPrefab;

    public void Start()
    {
        this.networkLobby.enabled = false;
        this.playerSelectionManager.enabled = false;
    }

    public void StartSplitscreen()
    {
        var splitscreen = GameObject.Instantiate(this.splitscreenPrefab);
        GameObject.DontDestroyOnLoad(splitscreen);
        SceneManager.LoadScene(this.nextScene);
    }

    public void StartNetwork()
    {
        this.playModeSelectWindow.SetActive(false);
        this.playerSelectionManager.enabled = true;
    }
}
