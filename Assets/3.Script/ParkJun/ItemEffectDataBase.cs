using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ItemEffect
{
    public string itemName; // 아이템의 이름 (키값) 
    [Tooltip("HP,SP,DP,HUNGRY,THIRSTY만 가능합니다")]
    public string[] part; //부위 효과 
    public int[] num; //수치 

}

public class ItemEffectDataBase : MonoBehaviour
{
    [SerializeField]
    private ItemEffect[] itemEffects;

    //필요한 컴포넌트 
    [SerializeField]
    private StatusController thePlayerStatus;
    [SerializeField]
    private Inventory theinven;
    [SerializeField]
    private SlotToolTip theSlotToolTip;

    private const string HP = "HP", SP = "SP", DP = "DP", ATT = "ATT", Hungry = "HUNGRY", THIRSTY = "THIRSTY", BAG = "BAG";

    public void ShowToolTip(Item _item,Vector3 _pos)
    {
        theSlotToolTip.ShowToolTip(_item,_pos);
    }
    public void HideToolTip()
    {
        theSlotToolTip.HideToolTip();
    }

    public void UseItem(Item _item)
    {
        if (_item.itemType==Item.ItemType .Used)
        {
            for (int x = 0; x < itemEffects.Length; x++)
            {
                if (itemEffects[x].itemName==_item.itemName)
                {
                    for (int y = 0; y < itemEffects[x].part.Length; y++)
                    {
                        switch (itemEffects[x].part[y])
                        {
                            case HP:
                                thePlayerStatus.increaseHP(itemEffects[x].num[y]);
                                break;
                            case SP:
                                thePlayerStatus.increaseSP(itemEffects[x].num[y]);
                                break;
                            case DP:
                                thePlayerStatus.increaseDP(itemEffects[x].num[y]);
                                break;
                            case Hungry:
                                thePlayerStatus.increaseHungry(itemEffects[x].num[y]);
                                break;
                            case THIRSTY:
                                thePlayerStatus.increaseThirsty(itemEffects[x].num[y]);
                                break;
                        }
                    }
                    return; //for문이 만족할때까지 만족한게 없으면 끝내버리기
                }
            }
        }
        if (_item.itemType == Item.ItemType.objectUsed)
        {
            for (int x = 0; x < itemEffects.Length; x++)
            {
                if (itemEffects[x].itemName == _item.itemName)
                {
                   
                    for (int y = 0; y < itemEffects[x].part.Length; y++)
                    {
                        switch (itemEffects[x].part[y])
                        {
                            case ATT:
                                thePlayerStatus.increaseATT(itemEffects[x].num[y]);
                                break;
                            default:
                                break;
                        }
                    }
                    return; //for문이 만족할때까지 만족한게 없으면 끝내버리기
                }
            }
        }
    }
}
