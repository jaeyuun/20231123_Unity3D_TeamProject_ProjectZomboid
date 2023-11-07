using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range; //습득 가능한 최대 거리 

    private bool pickupActivated = false; //습득 가능할 시 true

    private RaycastHit hitinfo; //충돌체 정보 저장.

    //아이템 레이어에만 반응하도록 레이어 마스크 설정 
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Text actionText;
    [SerializeField]
    private Inventory theinventory;

    private void Update()
    {
        CheckItem();
        TryAction();
    }
    private void TryAction()
    {
        if (Input.GetMouseButtonDown(1)) // 마우스 오른쪽 버튼 (1) 입력 확인
        {
            CheckItem();
            CanPickUp();
        }
    }
    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hitinfo.transform !=null)
            {
                Debug.Log(hitinfo.transform.GetComponent<ItemPickup>().item.itemName + "휙득");
                theinventory.AcquireItem(hitinfo.transform.GetComponent<ItemPickup>().item);
                Destroy(hitinfo.transform.gameObject);
                infoDisAppear();
            }
        }
    }
    private void CheckItem()
    {
        Vector3 rayStartPos = transform.position + new Vector3(0, 0.1f, 0);
        if (Physics.Raycast(rayStartPos, transform.TransformDirection(Vector3.forward), out hitinfo, range, layerMask))
        {
            if (hitinfo.transform.tag=="Item")
            {
                ItemInfoAppear();
            }
        }
        else
        {
            infoDisAppear();
        }
    }
    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitinfo.transform.GetComponent<ItemPickup>().item.itemName + "휙득";
    }
    private void infoDisAppear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        // 기즈모로 레이 시각화
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * range);
    }
}

