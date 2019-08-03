using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{
    [SerializeField] private string nextScene;

    public void GoToNextScene()
    {
        SceneManager.LoadScene(this.nextScene);
    }
}
