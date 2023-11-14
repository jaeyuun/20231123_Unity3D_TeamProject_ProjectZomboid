using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_Banding : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject Band;
    private bool isBanding = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right&& !isBanding)
        {
            Band.SetActive(true);
            isBanding = true;
        }
        else if(eventData.button == PointerEventData.InputButton.Right && isBanding)
        {
            Band.SetActive(false);
            isBanding = false;
        }
    }
}
