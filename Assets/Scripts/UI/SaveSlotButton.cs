using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSlotButton : MonoBehaviour
{
    public int slotID; // Gán trong Inspector

    public void OnClickSlot()
    {
        if (SaveSystem.HasSaveFile(slotID))
        {
            SaveSystem.LoadGame(slotID);
        }
        else
        {
            SaveSystem.CreateNewGame(slotID);
        }

        GameManager.LoadScene("Level_1"); // Hoặc pendingLoadData.sceneName nếu em load theo file
    }
}
