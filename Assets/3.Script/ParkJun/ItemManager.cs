using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public Item(string _Type , string _Name,string _Explain,string _Number,bool _isUsing)
    {
        Type = _Type;
        Name = _Name;
        Explain = _Explain;
        Number = _Number;
        isUsing = _isUsing;
    }
    public string Type, Name, Explain, Number;
    public bool isUsing;
    
}
public class ItemManager : MonoBehaviour
{
    public TextAsset ItemDatabase;
    public List<Item> AllItemList, MyItemList, CurItemList;
    public string currentType = "Item";
    public GameObject[] Slot, usingImage;
    public Image[] TabImage, ItemImage;
    public Sprite TapIdleSprite, TabSelectSprite;
    public Sprite[] ItemSprite;
    public GameObject ExplainPanel;
    public GameObject OpenPanel;
    bool OpenInventory = false;
    public RectTransform[] slotRect;
    public RectTransform CanvasRect;
    RectTransform ExplainRect;
    IEnumerator PointerCoroutine;
   
    
    void Start()
    {
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');
        print(line.Length);

        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            AllItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4] == "TRUE"));
        }
        Load();
        ExplainRect = ExplainPanel.GetComponent<RectTransform>();

        OpenPanel.SetActive(OpenInventory);




    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenInventory = !OpenInventory;
            OpenPanel.SetActive(OpenInventory);
        }

    }
    public void SlotClick(int slotNum)
    {
        Item CurItem = CurItemList[slotNum];
        Item UsingItem = CurItemList.Find(x => x.isUsing == true);

        if (currentType=="Item")
        {
           
                if (UsingItem != null) UsingItem.isUsing = false;
                CurItem.isUsing = true;
            
        }
        else
        {
            CurItem.isUsing = !CurItem.isUsing;
            if (UsingItem != null) UsingItem.isUsing = false;
        }
        Save();
       

    }
    public void TapClik(string tapName)
    {
        // 현재 아이템 리스트에 클릭한 타입만 추가
        currentType = tapName ;
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
                usingImage [i].SetActive(CurItemList[i].isUsing);
            }
        }

        //탭 이미지
        int tabNum = 0;
        switch (tapName)
        {
            case "Item":tabNum = 0;
                break;
            case "Weapon":tabNum = 1;
                break;
            /*case "ETC":  tabNum = 2;
                break;  */
        }
        for (int i = 0; i < TabImage.Length; i++)
        {
            TabImage[i].sprite = i == tabNum ? TabSelectSprite : TapIdleSprite;
        }

    }
    public void PointerEnter(int slotNum)
    {
        PointerCoroutine = PointerEnterDelay(slotNum);
        StartCoroutine(PointerCoroutine);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasRect, Input.mousePosition, Camera.main, out Vector2 anchoredPos);
        ExplainRect.anchoredPosition = anchoredPos;

        ExplainPanel.GetComponentInChildren<Text>().text = CurItemList[slotNum].Name;


    }
    IEnumerator  PointerEnterDelay(int slotNum)
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
        string jdata = JsonConvert.SerializeObject(MyItemList );
        print(jdata);
        File.WriteAllText(Application.dataPath + "/Resources/MyItemText.txt", jdata);

        TapClik(currentType);
    }
   void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/Resources/MyItemText.txt");
        MyItemList = JsonConvert.DeserializeObject<List<Item>>(jdata);

        TapClik(currentType);
    }


}
