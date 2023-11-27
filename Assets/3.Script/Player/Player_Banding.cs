using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_Banding : MonoBehaviour, IPointerClickHandler//우클릭 마우스 이벤트 쓰기위해서 IPointerClickHandler
{
    [SerializeField] private GameObject Band;
    [SerializeField] private Player_Move player;
    [SerializeField] private Inventory inventory;
    public bool isBanding = false;
    private bool isBandageOk = false;
    private int slot_num=0;



    public void OnPointerClick(PointerEventData eventData)
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.slots[i].itemName == "Bandage1")
            {
                isBandageOk = true;
                slot_num = i;
            }
            
        }
        for (int i = 0; i < inventory.quickSlots.Length; i++)
        {
            if (inventory.quickSlots[i].itemName == "Bandage1")
            {
                isBandageOk = true;
                slot_num = i;
            }

        }

        if (eventData.button == PointerEventData.InputButton.Right&& !isBanding && isBandageOk)
        {
            player.animator.SetBool("isMaking", true);
            StartCoroutine(player_anim_co());
            inventory.slots[slot_num].SetSlotCount(-1);
            inventory.quickSlots[slot_num].SetSlotCount(-1);
        }
        else if(eventData.button == PointerEventData.InputButton.Right && isBanding)
        {
            Band.SetActive(false);
            isBanding = false;
        }
    }

    private IEnumerator player_anim_co()
    {
        yield return new WaitForSeconds(1.5f);
        player.animator.SetBool("isMaking", false);
        Band.SetActive(true);
        isBanding = true;
    }
}
