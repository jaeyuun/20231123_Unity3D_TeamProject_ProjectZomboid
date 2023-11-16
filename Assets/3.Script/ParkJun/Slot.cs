using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour ,IPointerEnterHandler ,IPointerExitHandler,IPointerClickHandler,IBeginDragHandler,IDragHandler,IEndDragHandler,IDropHandler
{
    public Item item;
    public string itemName;
    public int itemCount;
    public float itemweight;
    public Image itemImage;

    [SerializeField]
    private Text text_Name;
    [SerializeField]
    private Text text_Count;

    [SerializeField]
    private GameObject go_NameImage;
    [SerializeField]
    private GameObject go_CountImage;

   

    [SerializeField]
    private Slider slider;

    private bool isUsingItem = false;

    private Rect baseRect;  // Inventory_Base 이미지의 Rect 정보 받아 옴.

    [SerializeField]
    private ItemEffectDataBase theitemEffectDataBase;
    [SerializeField]
    private  Drop drop;
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private ActionController thePlayer;

    private InputNumber theInputNumber;

    private void Start()
    {
        baseRect = transform.parent.parent.GetComponent<RectTransform>().rect;
       // theitemEffectDataBase = get<ItemEffectDataBase>();
       
       // drop = FindObjectOfType<Drop>();
       // inventory = FindObjectOfType<Inventory>();
       // theInputNumber = FindObjectOfType<InputNumber>();
      //  thePlayer = FindObjectOfType<ActionController>();
    }

    //이미지의 투명도 조절 
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    //아이템 획득
    public void AddItem(Item _item,string _name,float _itemWeight, int _count=1 )
    {
        item = _item;
        itemCount = _count;
        itemName = _name;
        itemImage.sprite = item.itemImage;

        itemweight = _itemWeight;
        /*  weight = _item.itemweight;
          weight2 = weight;*/
        /*   totalWeight += _item.itemweight * _count;
          Debug.Log("무게 증가: " + totalWeight);*/



        if (item.itemType !=Item.ItemType.Equipment)
        {
           
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
            go_NameImage.SetActive(true);
            text_Name.text = itemName;
           

        }
        else
        {

            text_Count.text = "0";
            go_CountImage.SetActive(false);
            go_NameImage.SetActive(true);
            text_Name.text = itemName;
            
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
    public void ClearSlot()
    {
            itemweight -= item.itemweight * itemCount; // 들어온 무게 빼기
            Debug.Log("무게 감소: " + itemweight);
        
     

        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        itemName = null;
        SetColor(0);

        go_NameImage.SetActive(false);
        

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
                    //장비 장착
                }
                else if (item.itemType == Item.ItemType.Used)
                {
                    StartCoroutine(UseItemWithSlider(item, 2f));
                }
                else if (item.itemType==Item.ItemType.objectUsed)
                {
                    StartCoroutine(UseObjectWithSlider(item, 8f));
                }
                else
                {
                    inventory.increaseBag(20);
                    SetSlotCount(-1);
                }
            }
        }
    }
    private IEnumerator UseItemWithSlider(Item _item, float duration)
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

        // 소모 시킨 후 
        SetSlotCount(-1);

        // 아이템 효과 적용 
        theitemEffectDataBase.UseItem(_item);
        Debug.Log(_item.itemName + " 을 사용했습니다.");

        // 슬라이더 비활성화
        slider.gameObject.SetActive(false);

        //currentWeight -= _item.itemweight;
        drop.UpdateTotalWeight();
        inventory.UpdateTotalWeight2();

    }
    private IEnumerator UseObjectWithSlider(Item _item,float duration)
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

        // 소모 시킨 후 
        SetSlotCount(-1);

        // 아이템 효과 적용 
        theitemEffectDataBase.UseItem(_item);
        Debug.Log(_item.itemName + " 을 사용했습니다.");

        // 슬라이더 비활성화
        slider.gameObject.SetActive(false);

        //currentWeight -= _item.itemweight;
        drop.UpdateTotalWeight();
        inventory.UpdateTotalWeight2();
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

      
        if (DragSlot.instance.transform.localPosition.x < baseRect.xMin
            || DragSlot.instance.transform.localPosition.x > baseRect.xMax
            || DragSlot.instance.transform.localPosition.y < baseRect.yMin
            || DragSlot.instance.transform.localPosition.y > baseRect.yMax)
        {


            // theInputNumber.Call();

          
            DragSlot.instance.dragSlot.ClearSlot();
             drop.UpdateTotalWeight();
            inventory.UpdateTotalWeight2();

        }
        else
        {
            //drop.UpdateTotalWeight();
            inventory.UpdateTotalWeight2();
            Debug.Log("OnEndDrag 호출됨");
            DragSlot.instance.SetColor(0);
            DragSlot.instance.dragSlot = null;
        }
    }


    public void OnDrop(PointerEventData eventData)
    {
        //드래그 중인 슬롯이 존재하는지 확인
           if (DragSlot.instance.dragSlot != null)
           {
            ItemDisObject();
            //현재 슬롯과 드래그 중인 슬롯에 모두 아이템이 존재하는지 확인
            if (item != null && DragSlot.instance.dragSlot.item != null &&
                   item.itemName == DragSlot.instance.dragSlot.item.itemName) //현재 슬롯과 드래그 중인 슬롯의 아이템 이름이 같은지 확인합니다.
               {
                   // 같은 아이템이면 수량을 합친다
                   SetSlotCount(DragSlot.instance.dragSlot.itemCount);
               
                    DragSlot.instance.dragSlot.ClearSlot(); // 드래그한 슬롯 비우기
               
               }
               else
                ChangeSlot();
                drop.UpdateTotalWeight();
                inventory.UpdateTotalWeight2();
                ItemDisObject();

           }
          else
            drop.UpdateTotalWeight();
            inventory.UpdateTotalWeight2();
            ItemDisObject();






        Debug.Log("OnDrop 호출됨");

    }
    private void ChangeSlot()
    {
       

        Item _tempItem = item;
        int _tempItemCount = itemCount;
        string _tempItemName = itemName;

       float _tempItemWeight = itemweight;

        //넣어주기 
        AddItem(DragSlot.instance.dragSlot.item,DragSlot.instance.dragSlot.itemName, DragSlot.instance.dragSlot.itemweight,DragSlot.instance.dragSlot.itemCount);

        if (_tempItem !=null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemName, _tempItemWeight, _tempItemCount);

            itemweight -= _tempItemWeight * _tempItemCount;

            Debug.Log("무게 감소: " + itemweight);

        }
        else
        {
            ItemDisObject();

            DragSlot.instance.dragSlot.ClearSlot();
            Debug.Log("ChangeSlot - Cleared Slot");

        }

    }
    private void ItemDisObject()
    {
        // OverlapSphere에 사용할 반경 정의
        float range = 1f;
        // 플레이어 주변 지정된 반경 내의 모든 콜라이더 가져오기
        Collider[] hitcoll = Physics.OverlapSphere(thePlayer.transform.position, range);

        foreach (Collider coll in hitcoll)
        {
            // 콜라이더가 지정된 태그를 가지고 있는지 확인
            if (coll.CompareTag("Item"))
            {
                Item draggedItem = DragSlot.instance.dragSlot.item;

                // 슬롯에 옮겨진 아이템과 플레이어 주변 아이템을 비교
                if (draggedItem != null && draggedItem == coll.GetComponent<ItemPickup>().item)
                {
                    Destroy(coll.gameObject);

                    Debug.Log(draggedItem.itemName + " 아이템이 파괴되었습니다.");
                }
            }
        }
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(thePlayer.transform.position, 1.5f);
    }

    //마우스가 슬롯에 들어갈 때 
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item!=null) //예외처리 
        {
            theitemEffectDataBase.ShowToolTip(item,transform.position);
        }
        
    }
    //마우스가 슬롯에서 빠져나갈 때
    public void OnPointerExit(PointerEventData eventData)
    {
        theitemEffectDataBase.HideToolTip();
    }

   
}
