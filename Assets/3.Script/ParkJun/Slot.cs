using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Item item;
    public string itemName;
    public int itemCount;
    public float itemweight;
    public Image itemImage;

    public bool isFirstClick = true;

    [SerializeField]
    private Text text_Name;
    [SerializeField]
    private Text text_Count;

    [SerializeField]
    private GameObject go_NameImage;
    [SerializeField]
    private GameObject go_CountImage;

    public GameObject Bat_B;
    public GameObject Gun_B;
    public Player_Attack player_Attack;


    [SerializeField]
    private Slider slider;

    [SerializeField] private RectTransform baseRect;  // Inventory_Base 의 영역
    [SerializeField] private RectTransform dropRect;
    // private Rect baseRect;  // Inventory_Base 이미지의 Rect 정보 받아 옴.
    [SerializeField] RectTransform quickSlotBaseRect;


    private ItemEffectDataBase theitemEffectDataBase;

    private Drop drop;

    private Inventory inventory;

    private ActionController thePlayer;

    private InputNumber theInputNumber;
    private Player_Move playerMove;
    private void Start()
    {
        /* Transform parentTransform = transform.parent.parent.parent;
         baseRect = parentTransform.GetComponent<RectTransform>().rect;*/


        theitemEffectDataBase = FindObjectOfType<ItemEffectDataBase>();
        drop = FindObjectOfType<Drop>();
        inventory = FindObjectOfType<Inventory>();
        theInputNumber = FindObjectOfType<InputNumber>();
        thePlayer = FindObjectOfType<ActionController>();
        playerMove = FindObjectOfType<Player_Move>();
    }

    //이미지의 투명도 조절 
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    //아이템 획득
    public void AddItem(Item _item, string _name, float _itemWeight, int _count = 1)
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



        if (item.itemType != Item.ItemType.Equipment)
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

        if (itemCount <= 0)
        {
            ClearSlot();
        }
    }
    //슬롯 초기화
    public void ClearSlot()
    {
        // itemweight -= item.itemweight * itemCount; // 들어온 무게 빼기


        item = null;
        itemImage.sprite = null;
        itemName = null;
        SetColor(0);

        go_NameImage.SetActive(false);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                if (item.itemType == Item.ItemType.Equipment)
                {
                    //장비 장착
                    if (item.itemName == "Bat")
                    {
                        Bat_B.SetActive(true);
                        Gun_B.SetActive(false);
                        player_Attack.Bat_Get = true;
                        player_Attack.Gun_Get = false;
                    }
                    else if (item.itemName == "Gun")
                    {
                        Gun_B.SetActive(true);
                        Bat_B.SetActive(false);
                        player_Attack.Gun_Get = true;
                        player_Attack.Bat_Get = false;
                    }

                }
                else if (item.itemType == Item.ItemType.Used)
                {
                    RectTransform slotRectTransform = GetComponent<RectTransform>();
                    if (!IsInsideBaseRect(slotRectTransform))
                    {
                        StartCoroutine(UseItemWithSlider(item, 2f));
                        isFirstClick = false;
                    }
                    else
                    {
                        isFirstClick = true;
                    }

                }
                else if (item.itemType == Item.ItemType.objectUsed)
                {
                    StartCoroutine(UseObjectWithSlider(item, 8f));


                }
                else if (item.itemType == Item.ItemType.Ingredient)
                {
                    // 첫 번째 클릭 시
                    if (isFirstClick)
                    {
                        inventory.OnBag(20);
                        isFirstClick = false;
                    }
                    // 두 번째 클릭 시
                    else
                    {
                        inventory.OffBag(20);
                        isFirstClick = true;
                    }
                }
                else
                {

                }
            }
        }
    }
    private bool IsInsideBaseRect(RectTransform slotRectTransform)
    {
        // 기준 Rect 내부에 있는지 여부 확인
        return RectTransformUtility.RectangleContainsScreenPoint(dropRect, slotRectTransform.position);
    }
    private IEnumerator UseItemWithSlider(Item _item, float duration)
    {
        float timer = 0f;
        slider.gameObject.SetActive(true); //슬라이더 활성화 
        player_Attack.anim.SetBool("isDrinking", true);
        SetSlotCount(-1);

        while (timer < duration)
        {
            timer += Time.deltaTime; //시간 흐를수록 타이머 제어

            // 시간에 따라 슬라이더 갱신 
            slider.value = timer / duration;

            yield return null; // 다음 프레임까지 기다리고 
        }




        // 아이템 효과 적용 
        theitemEffectDataBase.UseItem(_item);


        // 슬라이더 비활성화
        slider.gameObject.SetActive(false);
        player_Attack.anim.SetBool("isDrinking", false);
        //currentWeight -= _item.itemweight;
        drop.UpdateTotalWeight();
        inventory.UpdateTotalWeight2();

    }
    private IEnumerator UseObjectWithSlider(Item _item, float duration)
    {
        float timer = 0f;
        slider.gameObject.SetActive(true); //슬라이더 활성화 
        playerMove.speed = Mathf.Max(0.1f, Mathf.Min(2f, playerMove.speed - 1.5f));

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


        // 슬라이더 비활성화
        slider.gameObject.SetActive(false);
        playerMove.speed = Mathf.Max(0.1f, Mathf.Min(2f, playerMove.speed + 1.5f));
        //currentWeight -= _item.itemweight;
        drop.UpdateTotalWeight();
        inventory.UpdateTotalWeight2();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
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


        if (!((DragSlot.instance.transform.localPosition.x > baseRect.rect.xMin
           && DragSlot.instance.transform.localPosition.x < baseRect.rect.xMax
           && DragSlot.instance.transform.localPosition.y > baseRect.rect.yMin
           && DragSlot.instance.transform.localPosition.y < baseRect.rect.yMax)
           ||
           (DragSlot.instance.transform.localPosition.x > quickSlotBaseRect.rect.xMin
           && DragSlot.instance.transform.localPosition.x < quickSlotBaseRect.rect.xMax
           && DragSlot.instance.transform.localPosition.y + baseRect.transform.localPosition.y > quickSlotBaseRect.rect.yMin + quickSlotBaseRect.transform.localPosition.y
           && DragSlot.instance.transform.localPosition.y + baseRect.transform.localPosition.y < quickSlotBaseRect.rect.yMax + quickSlotBaseRect.transform.localPosition.y)))
        {

            if (DragSlot.instance.dragSlot!=null)
            {
                for (int i = 0; i < DragSlot.instance.dragSlot.itemCount; i++)
                {
                    Instantiate(DragSlot.instance.dragSlot.item.itemPrefab,
                                thePlayer.transform.position + thePlayer.transform.forward * 1.5f,
                                Quaternion.identity);
                   

                }
                DragSlot.instance.dragSlot.ClearSlot();
            }

            //아이템 버릴 때 사운드  Todo...      Exit_item
          

        }
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
        drop.UpdateTotalWeight();
        inventory.UpdateTotalWeight2();





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

            //ItemDisObject();

        }
        else

            ItemDisObject();




        drop.UpdateTotalWeight();
        inventory.UpdateTotalWeight2();



    }
    private void ChangeSlot()
    {


        Item _tempItem = item;
        int _tempItemCount = itemCount;
        string _tempItemName = itemName;

        float _tempItemWeight = itemweight;

        //넣어주기 
        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemName, DragSlot.instance.dragSlot.itemweight, DragSlot.instance.dragSlot.itemCount);

        if (_tempItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemName, _tempItemWeight, _tempItemCount);

            itemweight -= _tempItemWeight * _tempItemCount;



        }
        else
        {
            ItemDisObject();

            DragSlot.instance.dragSlot.ClearSlot();


        }

    }
    private void ItemDisObject()
    {
        // OverlapBox에 사용할 박스 영역 정의
        Vector3 halfExtents = new Vector3(0.8f, 4f, 0.8f);
        // 플레이어 주변 지정된 박스 영역 내의 모든 콜라이더 가져오기
        Collider[] hitColliders = Physics.OverlapBox(thePlayer.transform.position, halfExtents);

        foreach (Collider coll in hitColliders)
        {
            // 콜라이더가 지정된 태그를 가지고 있는지 확인
            if (coll.CompareTag("Item"))
            {
                Item draggedItem = DragSlot.instance.dragSlot.item;

                // 슬롯에 옮겨진 아이템과 플레이어 주변 아이템을 비교
                if (draggedItem != null && draggedItem == coll.GetComponent<ItemPickup>().item)
                {
                    int itemCountToDestroy = DragSlot.instance.dragSlot.itemCount; // 드래그 된 아이템의 갯수 가져오기

                    // 드래그 된 갯수만큼 아이템 파괴
                    for (int i = 0; i < itemCountToDestroy; i++)
                    {
                        Destroy(coll.gameObject);
                    }

                }
            }
        }
    }
    /*    private void OnDrawGizmos()
        {
            // OverlapBox에 사용할 박스 영역 정의
            Vector3 halfExtents = new Vector3(0.5f, 4f, 1f);

            // 기즈모의 색상 설정
            Gizmos.color = Color.yellow;

            // 기즈모로 박스의 윤곽선 그리기
            Gizmos.DrawWireCube(thePlayer.transform.position,  halfExtents);
        }
    */




    //마우스가 슬롯에 들어갈 때 
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null) //예외처리 
        {
            theitemEffectDataBase.ShowToolTip(item, transform.position);
        }

    }
    //마우스가 슬롯에서 빠져나갈 때
    public void OnPointerExit(PointerEventData eventData)
    {
        theitemEffectDataBase.HideToolTip();
    }


}
