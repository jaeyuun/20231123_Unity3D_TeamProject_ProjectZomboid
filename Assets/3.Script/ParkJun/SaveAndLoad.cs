using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class SaveData
{
    public Vector3 playerPos;
    public Vector3 playerRot;

    public Vector3 zombiePos;
    public Vector3 zombieRot;

    //아이템 불러오기 
    public List<int> invenArrayNumber = new List<int>();
    public List<string> invenItemName = new List<string>();
    public List<int> invenItemNumber = new List<int>();

    //좀비 
    /*
        1. ZombieSpawner의 ZombieList: 좀비 수가 저장되어 있음
        2. Zombie
     */
}

public class SaveAndLoad : MonoBehaviour
{
    private SaveData saveData = new SaveData();

    private string save_data_directory; // 경로 
    private string save_filename = "/SaveFile.txt";

    private Player_Move thePlayer;
    private ZombieController theZombie;

    private Inventory theInventory;

    private void Start()
    {
        save_data_directory = Application.dataPath + "/Saves/";
        if (!Directory.Exists(save_data_directory)) //디렉토리가 없으면 하나를 만들어라 
        {
            Directory.CreateDirectory(save_data_directory);
        }
    }
    public void SaveData()
    {
        thePlayer = FindObjectOfType<Player_Move>();
        theInventory = FindObjectOfType<Inventory>();
        theZombie = FindObjectOfType<ZombieController>();

        saveData.playerPos = thePlayer.transform.position; //위치 저장 
        saveData.playerRot = thePlayer.transform.eulerAngles; //회전값 저장 


        saveData.zombiePos = theZombie.transform.position;
        saveData.zombieRot = theZombie.transform.eulerAngles;

        Slot[] slots = theInventory.GetSlots();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item !=null)
            {
                saveData.invenArrayNumber.Add(i);
                saveData.invenItemName.Add(slots[i].item.itemName);
                saveData.invenItemNumber.Add(slots[i].itemCount);
            }
        }
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(save_data_directory + save_filename, json);

        Debug.Log("저장 완료");
        Debug.Log(json);
    }
    public void LoadData()
    {
        if (File.Exists(save_data_directory + save_filename)) //파일이 있을때만 로드
        {
            string loadJson = File.ReadAllText(save_data_directory + save_filename);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            thePlayer = FindObjectOfType<Player_Move>();
            theInventory = FindObjectOfType<Inventory>();
            theZombie = FindObjectOfType<ZombieController>();

            thePlayer.transform.position = saveData.playerPos; //위치 불러오기 
            thePlayer.transform.eulerAngles = saveData.playerRot; //회전값 불러오기 

            theZombie.transform.position = saveData.zombiePos;
            theZombie.transform.eulerAngles = saveData.zombieRot;

            for (int i = 0; i < saveData.invenItemName.Count; i++)
            {
                theInventory.LoadToDrop(saveData.invenArrayNumber[i], saveData.invenItemName[i], saveData.invenItemNumber[i]);
            }

            Debug.Log("로드 완료");
            Debug.Log(loadJson);
        }
        else
            Debug.Log("세이브 파일이 없습니다.");
        
    }
}
