using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActiveated = false;

    //필요한 컴포넌트 
    [SerializeField]
    private GameObject go_inventotyBase;
    [SerializeField]
    private GameObject go_Base;
    [SerializeField]
    private GameObject go_SlotsParent;
    [SerializeField]
    private Slider slider; 

    public  float invenmaxweight = 20f;


    public Text text_inventoryweight;


    private Slot[] slots;
    [SerializeField]
    private Drop drop;

    public Slot[] GetSlots() { return slots; }

    [SerializeField] private Item[] items;
    public void LoadToDrop(int _arrayNum, string _itemName,float _itemweight, int _itemNum)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemName == _itemName)
            {
                slots[_arrayNum].AddItem(items[i], _itemName,_itemweight, _itemNum);
                UpdateTotalWeight2();
            }
        }
    }
    private void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        drop = FindObjectOfType<Drop>();
    }
    private void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            inventoryActiveated = !inventoryActiveated;
            if (inventoryActiveated)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }
    }
    public void OpenInventory()
    {
       
        go_inventotyBase.SetActive(true);
    }
    public void CloseInventory()
    {

        go_inventotyBase.SetActive(false);
        go_Base.SetActive(false); // 기존 CloseInventory에 추가하기
    }
    public void ToggleinventoryBase()
    {
        inventoryActiveated = !inventoryActiveated;
         go_inventotyBase.SetActive(inventoryActiveated);
        //go_inventotyBase.SetActive(!go_inventotyBase.activeSelf);
    }
    public void AcquireItem2(Item _item, float _weight, int _count = 1)
    {
        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        
                        return;
                    }
                }

            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _item.itemName, _weight, _count);
               
                return;
            }
        }
        UpdateTotalWeight2();
    }
   
    public void UpdateTotalWeight2()
    {
        float totalWeight2 = 0f;

        // 모든 슬롯을 확인하며 아이템의 무게를 합산
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                totalWeight2 += slots[i].itemweight * slots[i].itemCount;
            }
        }

        // 텍스트 업데이트
        text_inventoryweight.text = $"{totalWeight2.ToString()}/{invenmaxweight}";

        if (totalWeight2 >= invenmaxweight)
        {
            //넘으면 플레이어 무브 느리게 한다던지
        }
    }
    public void increaseBag(int _count)
    {

        invenmaxweight += _count;
        StartCoroutine(UseObjectWithSlider(3f));
        
    }
    private IEnumerator UseObjectWithSlider(float duration)
    {
        float timer = 0f;
        slider.gameObject.SetActive(true); //슬라이더 활성화 


        while (timer < duration)
        {
            timer += Time.deltaTime; //시간 흐를수록 타이머 제어

            // 시간에 따라 슬라이더 갱신 
            slider.value = timer / duration;

            yield return null; // 다음 프레임까지 기다리고 
        }

        // 슬라이더 비활성화
        slider.gameObject.SetActive(false);
        UpdateTotalWeight2(); // 가방 무게 업데이트
    }

}

