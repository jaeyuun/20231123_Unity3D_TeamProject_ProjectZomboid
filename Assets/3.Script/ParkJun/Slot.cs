using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour ,IPointerClickHandler,IBeginDragHandler,IDragHandler,IEndDragHandler,IDropHandler
{
    public Item item;
    public string itemName;
    public int itemCount;
    public Image itemImage;

    [SerializeField]
    private Text text_Name;
    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject go_NameImage;
    [SerializeField]
    private GameObject go_CountImage;

    //이미지의 투명도 조절 
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    //아이템 획득
    public void AddItem(Item _item,string _name,int _count=1)
    {
        item = _item;
        itemCount = _count;
        itemName = _name;
        itemImage.sprite = item.itemImage;

        if (item.itemType !=Item.ItemType.Equipment)
        {
           // go_NameImage.SetActive(true);
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
            text_Name.text = itemName;

        }
        else
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }
       

        SetColor(1);
    }
    //아이템 갯수 조정 
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();
        if (itemCount<=0)
        {
            ClearSlot();
        }
    }
    //슬롯 초기화
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);


        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button==PointerEventData.InputButton.Right)
        {
            if (item !=null)
            {
                if (item.itemType==Item.ItemType.Equipment)
                {
                    //장착
                }
                else
                {
                    Debug.Log(item.itemName + " 을 사용했습니다.");
                    //소모 
                    SetSlotCount(-1);
                   
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item!=null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
          
           
        }
      
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag 호출됨");
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot!=null)
        {
            ChangeSlot();
        }
        Debug.Log("OnDrop 호출됨");
      
    }
    private void ChangeSlot()
    {
        Item _tempItem = item;
        int _tempItemCount = itemCount;
        string _tempItemName = itemName;

        //넣어주기 
        AddItem(DragSlot.instance.dragSlot.item,DragSlot.instance.dragSlot.itemName, DragSlot.instance.dragSlot.itemCount);

        if (_tempItem !=null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemName, _tempItemCount);
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }

    }
}
