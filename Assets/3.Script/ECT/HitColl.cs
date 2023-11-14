using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitColl : MonoBehaviour
{
    [Header("B_pos, hit오브젝트")]
    public Transform Hit_pos;//피튀는곳
    public GameObject hit;//피오브젝트선언

    [Header("피격(사운드클립)")]
    private AudioSource audioSource;
    public AudioClip Hit_Sound;
    public AudioClip Die_Sound;

    [Header("플레이어 넣어주세요")]
    public Player_Attack player;

    [Header("데미지 부위")]
    public GameObject[] BodyDmg;



    public Player_Fog player_Fog;//좀비 다 보이게 하기
    public GameObject Bleeding;
    

    public HP hp;
    private float Player_HP;
    public bool isDie = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GetComponentInParent<Player_Attack>();
        hp = GetComponentInParent<HP>();
        Player_HP=hp.Start_HP(Player_HP);
        Debug.Log(Player_HP);
        player_Fog = GetComponentInParent<Player_Fog>();

    }

  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ZombieAttack") && !isDie)
        {
           
            StartCoroutine(Hit_co(Hit_pos));
          
            Debug.Log(Player_HP);

            if (Player_HP <= 0 && !isDie)
            {
                
                player.GetComponent<Player_Move>().enabled = false;
                player.GetComponent<Player_Attack>().enabled = false;
                player.anim.SetLayerWeight(1, 0);//상체 애니메이션 재생해제
                player.anim.SetTrigger("isDie");
                Debug.Log("끄아아아아아아아앜!");
                audioSource.PlayOneShot(Die_Sound);
                player_Fog.viewAngle = 360f;
                player_Fog.ViewRadius = 50f;
                isDie = true;
                //StartCoroutine(Die_Zombie_co());
               

            }
        }
       
    }

    //좀비용 피튀는거

    public void Zombie_Hit(Transform Hit_pos)
    {
        StartCoroutine(Hit_co(Hit_pos));
    }

/*    private IEnumerator Die_Zombie_co()
    {
        yield return new WaitForSeconds(3f);
       
        isDie = true;
    }*/

    public IEnumerator Hit_co(Transform Hit_pos)
    {
        Instantiate(hit, Hit_pos.transform.position, Hit_pos.transform.rotation);
        player.anim.SetTrigger("isHit");
        audioSource.PlayOneShot(Hit_Sound);
        Player_HP = hp.Damage(10f, Player_HP);
        bodyDmg();
        yield return new WaitForSeconds(1f);
    }

    //랜덤으로 상처를 켬
    private void bodyDmg()
    {
        int a = Random.Range(0, BodyDmg.Length);
        Bleeding.SetActive(true);
        BodyDmg[a].SetActive(true);
    }
}
