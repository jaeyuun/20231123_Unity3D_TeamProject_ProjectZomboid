using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryUI;

    public void OpenInventory()
    {
        inventoryUI.SetActive(true);
    }

    public void CloseInventory()
    {
        inventoryUI.SetActive(false);
    }
}