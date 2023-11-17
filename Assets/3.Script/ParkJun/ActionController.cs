using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range; // 습득 가능한 최대 거리

    private bool pickupActivated = false; // 습득 가능할 시 true
    

    private Collider hitCollider; // 충돌한 콜라이더 정보 저장

    [SerializeField]
    private string itemTag = "Item"; // 아이템에 할당된 태그

   /* [SerializeField]
    private Text actionText;*/
    [SerializeField]
    private GameObject go_DropBase;
    [SerializeField]
    private Drop theDrop;
    [SerializeField]
    private Inventory theInventory;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    Player_Attack player_Attack;


 

    private void Update()
    {
       // CheckItem();
        //TryAction();
    }
  
    public void ToggleDropBase()
    {
        go_DropBase.SetActive(true);

        // 드롭베이스가 닫힐 때 아이템 슬롯 초기화 가능하도록 설정
        //opClear.SetCanClearSlots(go_DropBase.activeSelf);
    }

    private void TryAction()
    {
        if (Input.GetMouseButtonDown(1)) // 마우스 오른쪽 버튼 (1) 입력 확인
        {
            if (pickupActivated)
            {
                //PickupItem();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(itemTag))
        {
            pickupActivated = true;
            go_DropBase.gameObject.SetActive(true);
            theInventory.OpenInventory();

            ItemPickup itemPickup = other.GetComponent<ItemPickup>();
            if (itemPickup != null && !itemPickup.hasBeenPickedUp)
            {
                theDrop.AcquireItem(itemPickup.item, itemPickup.item.itemweight);
                //PickupItem();
                itemPickup.hasBeenPickedUp = true;

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(itemTag))
        {
            ItemPickup itemPickup = other.GetComponent<ItemPickup>();
            itemPickup.hasBeenPickedUp = false;

            slider.gameObject.SetActive(false);
            player_Attack.anim.SetBool("isDrinking", false);
            infoDisAppear();

        }
    }
    



    private void infoDisAppear()
    {
        
        // actionText.gameObject.SetActive(false);
        pickupActivated = false;
       // theInventory.CloseInventory();
        go_DropBase.gameObject.SetActive(false);
        theInventory.CloseInventory();
        
        
        for (int i = 0; i < 20; i++)
        {
            theDrop.RemoveItem(i);
        }
      
    }
    private void infoAppear()
    {

        //actionText.gameObject.SetActive(true);
        go_DropBase.gameObject.SetActive(true);
        theInventory.OpenInventory();
    }
    public void OffDropBase()
    {
        go_DropBase.gameObject.SetActive(false);
    }
   
}

