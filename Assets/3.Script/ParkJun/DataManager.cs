using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;

[System.Serializable]
public class Item2
{
    public Item2(string _Type, string _Name, string _Explain, string _Number, bool _isUsing, string _Index)
    {
        Type = _Type;
        Name = _Name;
        Explain = _Explain;
        Number = _Number;
        isUsing = _isUsing;
        Index = _Index;
    }
    public string Type, Name, Explain, Number,Index;
    public bool isUsing;

}
public class PlayerData
{
    public string name = "미스터 박";
    public int health=90;
    public int maxhealth;
    public int attack;
    public int ZombieCount = 1000000;
    public PlayerData()
    {
        // 생성자에서 초기값을 설정합니다.
        attack = 10; // 초기 공격력 값
    }
}

public class DataManager : MonoBehaviour
{
    [Header("아이템")]
    #region
    //아이템 
    public TextAsset ItemDatabase;
    public List<Item2> AllItemList, MyItemList, CurItemList;
    public string currentType = "Item";
    public GameObject[] Slot, usingImage;
    public Image[] TabImage, ItemImage;
    public Sprite TapIdleSprite, TabSelectSprite;
    public Sprite[] ItemSprite;
    public GameObject ExplainPanel;
    public GameObject OpenItemPanel;
    bool _OpenInventory = false;
    public RectTransform[] slotRect;
    public RectTransform CanvasRect;
    public InputField ItemNameInput, ItemNumberInput;
    RectTransform ExplainRect;
    IEnumerator PointerCoroutine;
    #endregion


    [Header("플레이어 데이터베이스")]
    public string path;
    public int nowSlot;

    [Header("플레이어")]
    //플레이어 

    bool _OpenStat = false;
    public GameObject OpenStatPanel;


    public PlayerData nowPlayer = new PlayerData();


    public static DataManager instance;

 
    private void Awake()
    {
        #region 싱글톤 
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion

        path = Application.persistentDataPath + "/save";
        print(path);
    }

    private void Start()
    {
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');
        print(line.Length);

        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            AllItemList.Add(new Item2(row[0], row[1], row[2], row[3], row[4] == "TRUE", row[5]));
        }
        Load();
        PlayerLoadData();
        
        ExplainRect = ExplainPanel.GetComponent<RectTransform>();

        OpenItemPanel.SetActive(_OpenInventory);
        OpenStatPanel.SetActive(_OpenStat);


    }


    private void FixedUpdate()
    {

        //인벤토리 
        RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasRect, Input.mousePosition, Camera.main, out Vector2 anchoredPos);
        ExplainRect.anchoredPosition = anchoredPos + new Vector2(-220, -200);
    }
    public void GetItemClik()
    {
        Item2 curItem = MyItemList.Find(x => x.Name == ItemNameInput.text);
        if (curItem != null)
        {
            curItem.Number = (int.Parse(curItem.Number) + int.Parse(ItemNumberInput.text)).ToString();
        }
        else
        {
            Item2 curAllItem = AllItemList.Find(x => x.Name == ItemNameInput.text);
            if (curAllItem !=null)
            {
                curAllItem.Number = ItemNumberInput.text;
                MyItemList.Add(curAllItem);
            }
        }
        MyItemList.Sort((p1, p2) => p1.Index.CompareTo(p2.Index));
        Save();
    }
    public void GetBeef()
    {
        Item2 curltem = MyItemList.Find(x => x.Name == "Beef");

        if (curltem != null)
        {
            int currentNumber = int.Parse(curltem.Number);
            currentNumber++; // 아이템 획득 시 Number를 1 증가시킴
            curltem.Number = currentNumber.ToString();

         
        }
        else
        {
            Item2 newItem = AllItemList.Find(x => x.Name == "Beef");

            MyItemList.Add(newItem); // 아이템을 MyItemList에 추가
            newItem.Number = "1";

        }
      
        //MyItemList.Sort((p1, p2) => p1.Index.CompareTo(p2.Index));
        Save(); // 변경된 내용을 저장
    }
    public void GetPizza()
    {

    }
    public void GetLemon()
    {

    }
    public void RemoveItemClik()
    {
        Item2 curItem = MyItemList.Find(x => x.Name == ItemNameInput.text);
        if (curItem !=null)
        {
            int curNumber = int.Parse(curItem.Number) - int.Parse(ItemNumberInput.text);

            if (curNumber<=0)
            {
                MyItemList.Remove(curItem);
            }
            else
            {
                curItem.Number = curNumber.ToString();
            }
        }
        MyItemList.Sort((p1, p2) => p1.Index.CompareTo(p2.Index));
        Save();
    }
    public void RemoveItem()
    {
        Item2 BasicItem = AllItemList.Find(x => x.Name == "Ammo");
        MyItemList = new List<Item2>() { BasicItem };
        Save();
    }
    public void GetBat()
    {
        Item2 curltem = MyItemList.Find(x => x.Name == "Bat");

        if (curltem != null)
        {
            int currentNumber = int.Parse(curltem.Number);
            currentNumber++; // 아이템 획득 시 Number를 1 증가시킴
            curltem.Number = currentNumber.ToString();


        }
        else
        {
            Item2 newItem = AllItemList.Find(x => x.Name == "Bat");

            MyItemList.Add(newItem); // 아이템을 MyItemList에 추가
            newItem.Number = "1";

        }

        //MyItemList.Sort((p1, p2) => p1.Index.CompareTo(p2.Index));
        Save(); // 변경된 내용을 저장
    }
    public void ToggleInventory()
    { 
            _OpenInventory = ! _OpenInventory;
            OpenItemPanel.SetActive(_OpenInventory);
    }
    public void ToggleStat()
    {
        _OpenStat = ! _OpenStat;
        OpenStatPanel.SetActive(_OpenStat);
    }
    public void SlotClick(int slotNum)
    {
        Item2 CurItem = CurItemList[slotNum];
        Item2 UsingItem = CurItemList.Find(x => x.isUsing == true);
        #region 
        /*   if (currentType == "Item")
           {
               if (UsingItem != null) UsingItem.isUsing = false;
               CurItem.isUsing = true;
           }
           else if(currentType == "Weapon")
           {
               CurItem.isUsing = !CurItem.isUsing;
               if (UsingItem != null) UsingItem.isUsing = false;
           }
           else
           {
              // CurItem.isUsing = !CurItem.isUsing;
               if (UsingItem != null) UsingItem.isUsing = false;

               int curNumber = int.Parse(CurItem.Number);

               if (curNumber > 0)
               {
                   curNumber--;

                   CurItem.Number = curNumber.ToString();
               }
               if (curNumber == 0)
               {
                   MyItemList.Remove(CurItem);

               }

           }*/
        #endregion

        if (currentType=="Food")
        {
            // CurItem.isUsing = !CurItem.isUsing;
            if (UsingItem != null) UsingItem.isUsing = false;

            int curNumber = int.Parse(CurItem.Number);
            if (CurItem.Name=="Beef")
            {         
                if (curNumber > 0)
                {
                    curNumber--;

                    CurItem.Number = curNumber.ToString();

                  
                    Player_UI player_ui = FindObjectOfType<Player_UI>();
                    player_ui.UsingBeef();
                    PlayerSaveData();

                    Debug.Log(nowPlayer.health);
                      if (nowPlayer.health > nowPlayer.maxhealth)
                       {
                           nowPlayer.health = nowPlayer.maxhealth; // 최대 체력을 넘지 않도록 조정
                       }
                }

                if (curNumber == 0)
                {
                    MyItemList.Remove(CurItem);
                }
            } 
            if (CurItem.Name == "Lemon")
            {
                if (curNumber > 0)
                {
                    curNumber--;

                    CurItem.Number = curNumber.ToString();

                    // 플레이어 체력을 증가시킴
                    nowPlayer.health += 20; // 10만큼 체력을 증가시킴
                    Debug.Log(nowPlayer.health);
                      if (nowPlayer.health > nowPlayer.maxhealth)
                       {
                           nowPlayer.health = nowPlayer.maxhealth; // 최대 체력을 넘지 않도록 조정
                       }
                }

                if (curNumber == 0)
                {
                    MyItemList.Remove(CurItem);
                }
            }
            if (CurItem.Name == "Pizza")
            {
                if (curNumber > 0)
                {
                    curNumber--;

                    CurItem.Number = curNumber.ToString();

                    // 플레이어 체력을 증가시킴
                    nowPlayer.health += 40; // 10만큼 체력을 증가시킴
                    Debug.Log(nowPlayer.health);
                       if (nowPlayer.health > nowPlayer.maxhealth)
                       {
                           nowPlayer.health = nowPlayer.maxhealth; // 최대 체력을 넘지 않도록 조정
                       }
                }

                if (curNumber == 0)
                {
                    MyItemList.Remove(CurItem);
                }
            }

        }
        else if (currentType =="Weapon")
        {
          

            if (CurItem.Name == "Bat")
            {
                Player_UI player_ui = FindObjectOfType<Player_UI>();
                if (CurItem.isUsing) // "Bat" 아이템이 사용 중인 경우에만 공격력을 올립니다.
                {                
                    player_ui.OffBat();
                 
                }
                else
                {             
                    player_ui.UsingBat();
                            
                }
                PlayerSaveData();
                CurItem.isUsing = !CurItem.isUsing;

                // 이미 다른 아이템이 사용 중이면 비사용으로 설정
                if (UsingItem != null)
                {
                    UsingItem.isUsing = false;
                }
            }       
            if (CurItem.Name == "Sowrd")
           {

                Player_UI player_ui = FindObjectOfType<Player_UI>();

                if (CurItem.isUsing) // "Bat" 아이템이 사용 중인 경우에만 공격력을 올립니다.
                {                 
                    player_ui.OffSword();   
                }
                else
                {
                   player_ui.UsingSword();
                    
                }
                PlayerSaveData();
                CurItem.isUsing = !CurItem.isUsing;

                // 이미 다른 아이템이 사용 중이면 비사용으로 설정
                if (UsingItem != null)
                {
                    UsingItem.isUsing = false;
                }
            }
          




        }
        Save();
    }
    public void TapItemClik(string tapName)
    {
        // 현재 아이템 리스트에 클릭한 타입만 추가
        currentType = tapName;
        CurItemList = MyItemList.FindAll(x => x.Type == tapName);


        for (int i = 0; i < Slot.Length; i++)
        {
            // 슬롯과 텍스트 보이기
            bool isExist = i < CurItemList.Count;
            Slot[i].SetActive(isExist);

            Slot[i].GetComponentInChildren<Text>().text = isExist ? CurItemList[i].Name + "/" + CurItemList[i].Number : "";
         

            // 아이템 이미지와 사용중인지 보이기
            if (isExist)
            {
                usingImage[i].SetActive(CurItemList[i].isUsing);
                ItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.Name == CurItemList[i].Name)];
 
            }
        }

        //탭 이미지
        int tapNum = 0;
        switch (tapName)
        {
            case "Item":
                tapNum = 0;
                break;
            case "Weapon":
                tapNum = 1;
                break;
            case "Food":  
                tapNum = 2;
                    break;  
        }
        for (int i = 0; i < TabImage.Length; i++)
        {
            TabImage[i].sprite = i == tapNum ? TabSelectSprite : TapIdleSprite;
        }

    }
    public void PointerEnter(int slotNum)
    {
        PointerCoroutine = PointerEnterDelay(slotNum);
        StartCoroutine(PointerCoroutine);


        ExplainPanel.GetComponentInChildren<Text>().text = CurItemList[slotNum].Name;
        ExplainPanel.transform.GetChild(2).GetComponent<Image>().sprite = Slot[slotNum].transform.GetChild(1).GetComponent<Image>().sprite;
        ExplainPanel.transform.GetChild(3).GetComponent<Text>().text = CurItemList[slotNum].Number + "개";
        ExplainPanel.transform.GetChild(4).GetComponent<Text>().text = CurItemList[slotNum].Explain;


    }
    IEnumerator PointerEnterDelay(int slotNum)
    {
        yield return new WaitForSeconds(0.2f);
       
        ExplainPanel.SetActive(true);
    }
    public void PointerExit(int slotNum)

    {
       
        StopCoroutine(PointerCoroutine);
        ExplainPanel.SetActive(false);
    }
    public void Save()
    {
        string jdata = JsonConvert.SerializeObject(MyItemList);
        print(jdata);
        File.WriteAllText(Application.dataPath + "/Resources/MyItemText.txt", jdata);

        TapItemClik(currentType);
    }
    void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/Resources/MyItemText.txt");
        MyItemList = JsonConvert.DeserializeObject<List<Item2>>(jdata);

        TapItemClik(currentType);
    }
    
    public void PlayerSaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);

        File.WriteAllText(path + nowSlot.ToString(), data);

        
    }
    public void PlayerLoadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    
    }
   /* public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }*/
}
