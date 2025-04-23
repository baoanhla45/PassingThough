using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public GameObject overwritePanel;
    private int pendingSlotToOverwrite = -1;

    [System.Serializable]
    public class SaveData
    {
        public string sceneName;
        public float playerPosX;
        public float playerPosY;

        public int currentHealth;
        public int currentMana;
        public int money;

        public float playerHealth;
        public float playerMana;

        public string areaName;
        public bool isBossDefeated;
    }


    public void OnSaveSlotSelected(int slotIndex)
    {
        if (SaveSystem.HasSaveFile(slotIndex))
        {
            pendingSlotToOverwrite = slotIndex;
            overwritePanel.SetActive(true);
        }
        else
        {
            SaveSystem.CreateNewGame(slotIndex);
        }
    }

    public void ConfirmOverwrite()
    {
        if (pendingSlotToOverwrite != -1)
        {
            SaveSystem.CreateNewGame(pendingSlotToOverwrite);
            pendingSlotToOverwrite = -1;
            overwritePanel.SetActive(false);
        }
    }

    public void CancelOverwrite()
    {
        pendingSlotToOverwrite = -1;
        overwritePanel.SetActive(false);
    }
}
