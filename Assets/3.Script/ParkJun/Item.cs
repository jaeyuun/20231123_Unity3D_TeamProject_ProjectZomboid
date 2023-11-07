using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public ItemType itemType; // 아이템 유형
    public string itemName; //이름 
    public Sprite itemImage; //이미지
    public GameObject itemPrefab; //아이템 프리펩

    public string weaponType; 

    public enum ItemType
    {
        Equipment,
        Used,
        Ingredient,
        ETC
    }
}
