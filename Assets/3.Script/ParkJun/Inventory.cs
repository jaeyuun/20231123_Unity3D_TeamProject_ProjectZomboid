using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActiveated = false;

    //필요한 컴포넌트 
    [SerializeField]
    private GameObject go_inventotyBase; //인벤토리 영역 
    [SerializeField]
    private GameObject go_ToolTipBase;
    [SerializeField]
    private GameObject go_QuickSlotParent;  // 퀵슬롯 영역
    [SerializeField]
    private GameObject go_SlotsParent;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Player_Move player_move;

    public  float invenmaxweight = 20f;


    public Text text_inventoryweight;

    public GameObject Bag;

    private Slot[] slots;
    private Slot[] quickSlots; // 퀵슬롯의 슬롯들
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
        quickSlots = go_QuickSlotParent.GetComponentsInChildren<Slot>();
        //drop = FindObjectOfType<Drop>();
    }
    private void Update()
    {
        TryOpenInventory();
        TryDoubleClick();
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
        go_ToolTipBase.SetActive(false); // 기존 CloseInventory에 추가하기
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

    private void TryDoubleClick()
    {
        // 마우스 왼쪽 더블클릭 감지
        if (Input.GetMouseButtonDown(0) && Input.GetMouseButtonDown(0))
        {
            // 더블클릭한 슬롯을 찾아서 해당 아이템을 퀵슬롯에 추가
            TryDoubleClickToAddToQuickSlot();
        }
    }
    private void TryDoubleClickToAddToQuickSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null && slots[i].isFirstClick)
            {
                // 더블클릭한 슬롯의 아이템을 퀵슬롯에 추가
                AddToQuickSlot(slots[i].item, slots[i].item.itemName, slots[i].itemweight, slots[i].itemCount);
                break; // 더블클릭 처리가 끝났으므로 반복문 종료
            }
        }
    }
    private void AddToQuickSlot(Item _item, string _name, float _itemWeight, int _count = 1)
    {
        for (int i = 0; i < quickSlots.Length; i++)
        {
            if (quickSlots[i].item == null)
            {
                // 퀵슬롯 배열에서 비어있는 첫 번째 슬롯에 아이템 추가
                quickSlots[i].AddItem(_item, _name, _itemWeight, _count);
                // 해당 슬롯의 isFirstClick 상태를 변경 (더블클릭 방지를 위해)
                quickSlots[i].isFirstClick = false;
                break; // 아이템 추가가 완료되었으므로 반복문 종료
            }
        }
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
            player_move.speed = Mathf.Max(1.5f, Mathf.Min(3f, player_move.speed - 1.5f));
            //무겁다는 아이콘 띄우기 
        }
        else if (totalWeight2 <=invenmaxweight)
        {
            //같거나 작아진다면 
            //속도 정상화 
            player_move.speed = Mathf.Max(1.5f, Mathf.Min(3f, player_move.speed + 1.5f));
            //무겁다는 아이콘 오프 
        }
    }
    public void OnBag(int _count)
    {

        invenmaxweight += _count;
        StartCoroutine(UseObjectWithSlider(3f));
        
    }
    public void OffBag(int _count)
    {
        invenmaxweight -= _count;
        // Bag.SetActive(false);
        UpdateTotalWeight2(); // 가방 무게 업데이트
    }
    private IEnumerator UseObjectWithSlider(float duration)
    {
        float timer = 0f;
        slider.gameObject.SetActive(true); //슬라이더 활성화 
       // Bag.SetActive(true);


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

