using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public static bool dropActiveated = false;

    //필요한 컴포넌트 
    [SerializeField]
    private GameObject go_dropBase;
    [SerializeField]
    private GameObject go_Base;
    [SerializeField]
    private GameObject go_SlotsParent;

  

    private Slot[] slots;
    
    //public Slot[] GetSlots() { return slots; }

    [SerializeField] private Item[] items;
 /*   public void LoadToInven(int _arrayNum,string _itemName, int _itemNum)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemName==_itemName)
            {
                slots[_arrayNum].AddItem(items[i],_itemName,_itemNum);
            }
        }
    }  */

    private void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
      
    }
    void Update()
    {
        //TryOpenInventory();
    }
   private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I)) // 마우스 왼쪽 버튼 (1) 입력 확인
        {
            dropActiveated = !dropActiveated;
            if (dropActiveated)
            {
                OpenDrop();
            }
            else
            {
                CloseDrop();
            }
        }
    }
    public void OpenDrop()
    {
        go_dropBase.SetActive(true);
    }
    private void CloseDrop()
    {

        go_dropBase.SetActive(false);
        go_Base.SetActive(false); // 기존 CloseInventory에 추가하기
     
    }
    public void ToggleinventoryBase()
    {
        dropActiveated = !dropActiveated;
        go_dropBase.SetActive(dropActiveated);
    }

   
    public void AcquireItem(Item _item, int _count = 1)
    {
        if (Item.ItemType.Equipment!=_item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item !=null)
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
            if (slots[i].item ==null)
            {
                slots[i].AddItem(_item, _item.itemName,_count);
                return;
            }
        }
    }
}
