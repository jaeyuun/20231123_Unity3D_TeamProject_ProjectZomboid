using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_Btn : MonoBehaviour
{
    [Header ("information_situation")]
    [SerializeField] private GameObject situation;
    [Header("skill_Stats")]
    [SerializeField] private GameObject Stats;


    public void OnBtn_skill()
    {
        situation.SetActive(false);
        Stats.SetActive(true);
    }

    public void OnBtn_Status()
    {
        situation.SetActive(true);
        Stats.SetActive(false);
    }
}
