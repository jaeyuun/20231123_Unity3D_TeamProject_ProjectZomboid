using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Level_Up : MonoBehaviour
{
    int level = 0;

    //레벨업기능
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

    //레벨체크기능
    public int Level_check(GameObject[] gameObjects)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].transform.GetChild(1).gameObject.activeSelf == true)
            {

            }
            else
            {
                level = i;
                break;                               
            }
        }
        return level;
    }
}
