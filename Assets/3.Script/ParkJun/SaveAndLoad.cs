using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class SaveData
{
    public Vector3 playerPos;
    public Vector3 playerRot;

    public float savedHp;
    public int savedSp;
    public int savedDp;
    public int saveAtt;
    public int savedHungry;
    public int savedThirsty;



    // 슬롯은 직렬화가 불가능. 직렬화가 불가능한 애들이 있다.

    //아이템 불러오기 
    public List<int> invenArrayNumber = new List<int>();
    public List<string> invenItemName = new List<string>();
    public List<int> invenItemNumber = new List<int>();
    public List<float> invenItemweight = new List<float>();
    //퀵슬롯 
   
    public List<int> quickSlotArrayNumber = new List<int>();
    public List<string> quickSlotItemName = new List<string>();
    public List<int> quickSlotItemNumber = new List<int>();
    public List<float> quickSlotItemweigh = new List<float>();

    //좀비 
   
}


public class SaveAndLoad : MonoBehaviour
{
    public  SaveData saveData = new SaveData();
    

    private string save_data_directory; // 경로 
    private string save_filename = "/SaveFile.txt";

    private Player_Move thePlayer;

    private ZombieSpawner theZombie;

    private Inventory theInventory;
    private StatusController theStatus;

    // Load 일 때 SetUp 예외처리
    public bool isLoad = true; // 저장된 파일이 있을 때 true

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
        theStatus = FindObjectOfType<StatusController>();
        theZombie = FindObjectOfType<ZombieSpawner>();


        //플레이어 위치 
        saveData.playerPos = thePlayer.transform.position; //위치 저장 
        saveData.playerRot = thePlayer.transform.eulerAngles; //회전값 저장 



        // 좀비
     


        //플레이어 스탯 
        saveData.savedHp = theStatus.GetcurrentHP();
        saveData.savedDp = theStatus.GetcurrentDP();
        saveData.savedSp = theStatus.GetcurrentSP();
        saveData.savedSp = theStatus.GetcurrentAtt();
        saveData.savedHungry = theStatus.GetcurrentHungry();
        saveData.savedThirsty = theStatus.GetcurrentThirsty();

        //플레이어 아이템 (인벤토리)
        Slot[] slots = theInventory.GetSlots();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item !=null)
            {
                saveData.invenArrayNumber.Add(i);
                saveData.invenItemName.Add(slots[i].item.itemName);
                saveData.invenItemweight.Add(slots[i].itemweight);
                saveData.invenItemNumber.Add(slots[i].itemCount);
            }
        }

        Slot[] quickSlots = theInventory.GetQuickSlots();
        for (int i = 0; i < quickSlots.Length; i++)
        {
            if (quickSlots[i].item != null)
            {
                saveData.quickSlotArrayNumber.Add(i);
                saveData.quickSlotItemName.Add(quickSlots[i].item.itemName);
                saveData.quickSlotItemweigh.Add(slots[i].itemweight);
                saveData.quickSlotItemNumber.Add(quickSlots[i].itemCount);
            }
        }
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(save_data_directory + save_filename, json);
    }
    public void LoadData()
    {
        if (File.Exists(save_data_directory + save_filename)) //파일이 있을때만 로드
        {
            isLoad = true;
            string loadJson = File.ReadAllText(save_data_directory + save_filename);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            thePlayer = FindObjectOfType<Player_Move>();
            theInventory = FindObjectOfType<Inventory>();
            theStatus = FindObjectOfType<StatusController>();
            theZombie = FindObjectOfType<ZombieSpawner>();

            //플레이어 위치 
            thePlayer.transform.position = saveData.playerPos; //위치 불러오기 
            thePlayer.transform.eulerAngles = saveData.playerRot; //회전값 불러오기 



            //플레이어 스탯 
            theStatus.SetcurrentHP(saveData.savedHp);
            theStatus.SetcurrentDP(saveData.savedDp);
            theStatus.SetcurrentSP(saveData.savedSp);
            theStatus.SetcurrentAtt(saveData.saveAtt);
            theStatus.SetcurrentHungry(saveData.savedHungry);
            theStatus.SetcurrentThirsty(saveData.savedThirsty);

            //플레이어 아이템 (인벤토리)
            for (int i = 0; i < saveData.invenItemName.Count; i++)
            {
                theInventory.LoadToDrop(saveData.invenArrayNumber[i], saveData.invenItemName[i], saveData.invenItemweight[i], saveData.invenItemNumber[i]);
            }
            // 퀵슬롯 로드
            for (int i = 0; i < saveData.quickSlotItemName.Count; i++)
            {
                theInventory.LoadToQuick(saveData.quickSlotArrayNumber[i], saveData.quickSlotItemName[i], saveData.quickSlotItemweigh[i], saveData.quickSlotItemNumber[i]);

            }
        }
    }
 



}
