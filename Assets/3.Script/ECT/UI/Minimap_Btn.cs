using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap_Btn : MonoBehaviour
{
    [Header("UI 넣어주세요")]
    public GameObject Minimap_on;
    //public GameObject Minimap_off;

    [Header("플레이어 미니맵넣주세요")]
    public GameObject Minimap;



    public void Btn_On()
    {
        Minimap_on.SetActive(true);
        Minimap.SetActive(true);
    }

    public void Btn_Off()
    {
        Minimap_on.SetActive(false);
        Minimap.SetActive(false);
    }
}
