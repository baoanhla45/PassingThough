using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    // Hàm này sẽ được gọi khi nhấn nút "Start Game"
    public void StartGame()
    {
        SceneManager.LoadScene("SaveSelect");
    }
    public void ExitGame()
    {
        Debug.Log("Thoát game!"); // Giúp test trong Editor
        Application.Quit();
    }
}
