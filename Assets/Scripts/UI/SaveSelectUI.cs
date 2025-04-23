
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSelectUI : MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

