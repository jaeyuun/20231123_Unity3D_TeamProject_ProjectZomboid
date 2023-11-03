using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public Item(string _Type, string _Name, string _Explain, string _Number, bool _isUsing, string _Index)
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
public class Player_Data
{
    public string name;
    public int health;
    public int maxhealth;
    public int attack;
}
public class DataManager : MonoBehaviour
{
    [Header("아이템")]
    //아이템 
    public TextAsset ItemDatabase;
    public List<Item> AllItemList, MyItemList, CurItemList;
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

    [Header("플레이어")]
    //플레이어 
    public Text name;
    public Text attack;
    public Text health;
    public Text maxhealth;


    bool _OpenStat = false;
    public GameObject OpenStatPanel;
   


    private void Start()
    {
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');
        print(line.Length);

        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            AllItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4] == "TRUE", row[5]));
        }
        Load();
        ExplainRect = ExplainPanel.GetComponent<RectTransform>();

        OpenItemPanel.SetActive(_OpenInventory);
        OpenStatPanel.SetActive(_OpenStat);


    }
   
  
    private void Update()
    {


        //인벤토리 
        RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasRect, Input.mousePosition, Camera.main, out Vector2 anchoredPos);
       ExplainRect.anchoredPosition = anchoredPos +new Vector2(-220, -200);


    }
    public void GetItemClik()
    {
        Item curItem = MyItemList.Find(x => x.Name == ItemNameInput.text);
        if (curItem != null)
        {
            curItem.Number = (int.Parse(curItem.Number) + int.Parse(ItemNumberInput.text)).ToString();
        }
        else
        {
            Item curAllItem = AllItemList.Find(x => x.Name == ItemNameInput.text);
            if (curAllItem !=null)
            {
                curAllItem.Number = ItemNumberInput.text;
                MyItemList.Add(curAllItem);
            }
        }
        MyItemList.Sort((p1, p2) => p1.Index.CompareTo(p2.Index));
        Save();
    }
    public void RemoveItemClik()
    {
        Item curItem = MyItemList.Find(x => x.Name == ItemNameInput.text);
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
        Item BasicItem = AllItemList.Find(x => x.Name == "Ammo");
        MyItemList = new List<Item>() { BasicItem };
        Save();
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
        Item CurItem = CurItemList[slotNum];
        Item UsingItem = CurItemList.Find(x => x.isUsing == true);

        if (currentType == "Item")
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

            Slot[i].GetComponentInChildren<Text>().text = isExist ? CurItemList[i].Name : "";

            // 아이템 이미지와 사용중인지 보이기
            if (isExist)
            {
                ItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.Name == CurItemList[i].Name)];
                usingImage[i].SetActive(CurItemList[i].isUsing);
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
        yield return new WaitForSeconds(0.5f);
        print(slotNum + "슬롯 들어옴 ");
        ExplainPanel.SetActive(true);
    }
    public void PointerExit(int slotNum)

    {
        print(slotNum + "슬롯 나감 ");
        StopCoroutine(PointerCoroutine);
        ExplainPanel.SetActive(false);
    }
    void Save()
    {
        string jdata = JsonConvert.SerializeObject(MyItemList);
        print(jdata);
        File.WriteAllText(Application.dataPath + "/Resources/MyItemText.txt", jdata);

        TapItemClik(currentType);
    }
    void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/Resources/MyItemText.txt");
        MyItemList = JsonConvert.DeserializeObject<List<Item>>(jdata);

        TapItemClik(currentType);
    }
    public void UpdateHealth()
    {
        health.text = $"체력 : {health} / {maxhealth}";
    }



}
