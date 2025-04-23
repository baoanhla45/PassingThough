using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveSlotUI : MonoBehaviour
{
    public int slotIndex;

    public Text textStatus;      
    public Text textAreaName;
    public Text textMoney;

    void Start()
    {
        LoadSaveData();
    }

    void LoadSaveData()
    {
        string path = SaveSystem.GetSavePath(slotIndex);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveManager.SaveData data = JsonUtility.FromJson<SaveManager.SaveData>(json);

            textStatus.text = $"HP: {data.currentHealth}/{data.playerHealth} - Mana: {data.currentMana}/{data.playerMana}";
            textAreaName.text = "Area: " + data.areaName;
            textMoney.text = "Money: " + data.money;
        }
        else
        {
            textStatus.text = "New Game";
            textAreaName.text = "";
            textMoney.text = "";
        }
    }
}
