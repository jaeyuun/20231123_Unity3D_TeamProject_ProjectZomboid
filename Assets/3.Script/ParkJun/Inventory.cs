using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActiveated = false;

    //필요한 컴포넌트 
    [SerializeField]
    private GameObject go_inventoryBase;
    [SerializeField]
    private GameObject go_Base;
    [SerializeField]
    private GameObject go_SlotsParent;


    private Slot[] slots;

    private void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }
    void Update()
    {
        TryOpenInventory();
    }
    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I)) // 마우스 왼쪽 버튼 (1) 입력 확인
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
        go_inventoryBase.SetActive(true);
    }
    private void CloseInventory()
    {

        go_inventoryBase.SetActive(false);
        go_Base.SetActive(false); // 기존 CloseInventory에 추가하기
    }
    public void ToggleinventoryBase()
    {
        inventoryActiveated = !inventoryActiveated;
        go_inventoryBase.SetActive(inventoryActiveated);
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
