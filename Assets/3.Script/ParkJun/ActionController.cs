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

    [SerializeField]
    private Text actionText;
    [SerializeField]
    private GameObject go_DropBase;
    [SerializeField]
    private Drop theDrop;
    [SerializeField]
    private Inventory theInventory;
 

  

    private void Update()
    {
        CheckItem();
        TryAction();
    }
  
    public void ToggleDropBase()
    {
        go_DropBase.SetActive(!go_DropBase.activeSelf);

        // 드롭베이스가 닫힐 때 아이템 슬롯 초기화 가능하도록 설정
        //opClear.SetCanClearSlots(go_DropBase.activeSelf);
    }

    private void TryAction()
    {
        if (Input.GetMouseButtonDown(1)) // 마우스 오른쪽 버튼 (1) 입력 확인
        {
            if (pickupActivated)
            {
                PickupItem();
            }
        }
    }

    private void PickupItem()
    {
        if (hitCollider != null)
        {
            ItemPickup itemPickup = hitCollider.GetComponent<ItemPickup>();
            if (itemPickup != null)
            {
                Debug.Log(itemPickup.item.itemName + "획득");
                theDrop.AcquireItem(itemPickup.item);
                Destroy(hitCollider.gameObject);
                infoDisAppear();
            }
         
           
        }
        
    }

    private void CheckItem()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        
        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag(itemTag))
            {
                pickupActivated = true;
                actionText.gameObject.SetActive(true);
                go_DropBase.gameObject.SetActive(true);

                theInventory.OpenInventory();
                if (pickupActivated)
                {
                    PickupItem();
                }
                hitCollider = col;
                // actionText.text = "<color=red>"+col.transform.name+ "</color>"+ "획득" + "<color=yellow>" + "(마우스 오른쪽 버튼)" + "</color>";
                actionText.text = "<color=red>" + col.transform.name + "</color>" + "획득";
                return; // 첫 번째 아이템만 처리하고 나머지는 무시
            }
        }

        infoDisAppear();
        
        //OffDropBase();
    }
   

    private void infoDisAppear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
       
    }
    public void OffDropBase()
    {
        go_DropBase.gameObject.SetActive(false);
    }
   
}

