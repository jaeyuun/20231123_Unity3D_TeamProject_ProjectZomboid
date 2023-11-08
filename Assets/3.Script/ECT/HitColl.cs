using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitColl : MonoBehaviour
{
    [Header("B_pos, hit오브젝트")]
    public Transform Hit_pos;//피튀는곳
    public GameObject hit;//피오브젝트선언


    public void Hit()//피뿜는 메서드
    {
        Instantiate(hit, Hit_pos.transform.position, Hit_pos.transform.rotation);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ZombieAttack") && other.gameObject.CompareTag("ZombieAttack"))
        {
            Hit();
            Debug.Log("으악 아프다");
        }
    }
}
