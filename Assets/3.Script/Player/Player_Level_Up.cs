using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Level_Up : MonoBehaviour
{
    public void Level_up(GameObject[] gameObjects)
    {

        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].transform.GetChild(1).gameObject.activeSelf == true)
            {

            }
            else
            {
                gameObjects[i].transform.GetChild(1).gameObject.SetActive(true);
                break;
            }
        }
  
         
    }
}
