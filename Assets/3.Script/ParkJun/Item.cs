using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public ItemType itemType; // 아이템 유형
    public string itemName; //이름 
    [TextArea]
    public string itemDesc; //아이템 설명 
    public Sprite itemImage; //이미지
    public GameObject itemPrefab; //아이템 프리펩
    public string weight; // 아이템의 무게
    public float itemweight;

    public string weaponType; 

    public enum ItemType
    {
        Equipment,
        Used,
        objectUsed,
        Ingredient,
        ETC
    }
}
