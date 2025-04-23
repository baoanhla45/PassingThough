using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveSystem
{
    private static SaveManager.SaveData pendingLoadData;

    public static string GetSavePath(int slotIndex)
    {
        return Application.persistentDataPath + "/save_slot_" + slotIndex + ".json";
    }

    public static bool HasSaveFile(int slotIndex)
    {
        return File.Exists(GetSavePath(slotIndex));
    }

    public static void CreateNewGame(int slotIndex)
    {
        SaveManager.SaveData data = new SaveManager.SaveData();

        // Dữ liệu mặc định khi tạo game mới
        data.sceneName = "Level_1"; // Đổi tên scene theo đúng tên em dùng
        data.playerPosX = 0f;
        data.playerPosY = 0f;
        data.playerHealth = 100f;
        data.playerMana = 50f;
        data.currentHealth = 100;
        data.currentMana = 50;
        data.money = 0;
        data.areaName = "Starting Area";
        data.isBossDefeated = false;

        Save(slotIndex, data);
        LoadGame(slotIndex); // Tạo xong là load vào game luôn
    }

    public static void LoadGame(int slotIndex)
    {
        string path = GetSavePath(slotIndex);
        if (!File.Exists(path)) return;

        string json = File.ReadAllText(path);
        pendingLoadData = JsonUtility.FromJson<SaveManager.SaveData>(json);

        SceneManager.sceneLoaded += OnSceneLoaded;
        GameManager.LoadScene(pendingLoadData.sceneName); // Load theo tên scene
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && pendingLoadData != null)
        {
            player.transform.position = new Vector2(pendingLoadData.playerPosX, pendingLoadData.playerPosY);

            PlayerController pc = player.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.currentHealth = pendingLoadData.currentHealth;
                pc.currentMana = pendingLoadData.currentMana;
                pc.money = pendingLoadData.money;
                // Nếu có thêm dữ liệu thì set tiếp ở đây
            }
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
        pendingLoadData = null;
    }

    public static void Save(int slotIndex, SaveManager.SaveData data)
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            PlayerController player = playerObj.GetComponent<PlayerController>();
            if (player != null)
            {
                data.playerPosX = player.transform.position.x;
                data.playerPosY = player.transform.position.y;
                data.currentHealth = player.currentHealth;
                data.currentMana = player.currentMana;
                data.money = player.money;
            }
        }

        data.sceneName = SceneManager.GetActiveScene().name;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetSavePath(slotIndex), json);
        Debug.Log("[SaveSystem] Saved game at slot " + slotIndex);
    }

    public static void SaveCurrentGame(int slotIndex)
    {
        SaveManager.SaveData data = new SaveManager.SaveData();
        Save(slotIndex, data);
    }

    public static void DeleteSaveFile(int slotIndex)
    {
        string path = GetSavePath(slotIndex);
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("[SaveSystem] Deleted save file at slot " + slotIndex);
        }
    }
}
