using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Item : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Item")
        {
            Debug.Log("¸Ô¾îÁö³Ä");

            DataManager.instance.GetBeef();
             
        }
        if (other.tag == "Weapon")
        {
            Debug.Log("¸Ô¾îÁö³Ä");
            Player_UI player_ui = FindObjectOfType<Player_UI>();
            DataManager.instance.GetBat();
            player_ui.UsingBat();
            DataManager.instance.PlayerSaveData();
        }
        if (other.tag=="Zombie")
        {
            Debug.Log(DataManager.instance.nowPlayer.health);
            Player_UI player_ui = FindObjectOfType<Player_UI>();
            player_ui.Damage();
            DataManager.instance.PlayerSaveData();
        }

    }
  

}
