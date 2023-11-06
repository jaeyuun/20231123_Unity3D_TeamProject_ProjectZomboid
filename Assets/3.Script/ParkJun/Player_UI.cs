using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player_UI : MonoBehaviour
{
    public Text name;
    public Text attack;
    public Text health;
    
    // public Text maxhealth;
    public Text ZombieCount;

    private void Start()
    {
        DataManager.instance.PlayerLoadData();
        name.text += DataManager.instance.nowPlayer.name;

        //DataManager.instance.nowPlayer.health = DataManager.instance.nowPlayer.maxhealth;
        health.text = $"체력 : {DataManager.instance.nowPlayer.health}/{DataManager.instance.nowPlayer.maxhealth}";
        attack.text = $"공격력 : {DataManager.instance.nowPlayer.attack}";
        ZombieCount.text = $"제거한 좀비 수 :{DataManager.instance.nowPlayer.ZombieCount}";
    }
    public void UsingBeef()
    {
       
        health.text = $"체력 : {DataManager.instance.nowPlayer.health}/{DataManager.instance.nowPlayer.maxhealth}";
        DataManager.instance.nowPlayer.health += 10;
        DataManager.instance.PlayerSaveData();

    }
    public void UsingBat()
    {
        DataManager.instance.nowPlayer.attack += 20;
        attack.text = $"공격력 : {DataManager.instance.nowPlayer.attack}";
        DataManager.instance.PlayerSaveData();
       
    }
    public void OffBat()
    {
        DataManager.instance.nowPlayer.attack =10;
        attack.text = $"공격력 : {DataManager.instance.nowPlayer.attack}";
        DataManager.instance.PlayerSaveData();
    }
    public void UsingSword()
    {
        DataManager.instance.nowPlayer.attack += 30;
        attack.text = $"공격력 : {DataManager.instance.nowPlayer.attack}";
        DataManager.instance.PlayerSaveData();
    }
    public void OffSword()
    {
        DataManager.instance.nowPlayer.attack = 10;
        attack.text = $"공격력 : {DataManager.instance.nowPlayer.attack}";
        DataManager.instance.PlayerSaveData();
    }
    public void Damage()
    {
        DataManager.instance.nowPlayer.health -= 10;
        health.text = $"체력 : {DataManager.instance.nowPlayer.health}/{DataManager.instance.nowPlayer.maxhealth}";
        DataManager.instance.PlayerSaveData();
    }

}
