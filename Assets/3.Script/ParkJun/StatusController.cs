using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    // 체력
    [SerializeField]
    private int hp;  // 최대 체력. 유니티 에디터 슬롯에서 지정할 것.
    private int currentHp;

    // 스태미나
    [SerializeField]
    private int sp;  // 최대 스태미나. 유니티 에디터 슬롯에서 지정할 것.
    private int currentSp;

    // 스태미나 증가량
    [SerializeField]
    private int spIncreaseSpeed;

    // 스태미나 재회복 딜레이 시간
    [SerializeField]
    private int spRechargeTime;
    private int currentSpRechargeTime;

    // 스태미나 감소 여부
    private bool spUsed;

    // 방어력
    [SerializeField]
    private int dp;  // 최대 방어력. 유니티 에디터 슬롯에서 지정할 것.
    private int currentDp;

    // 배고픔
    [SerializeField]
    private int hungry;  // 최대 배고픔. 유니티 에디터 슬롯에서 지정할 것.
    private int currentHungry;

    // 배고픔이 줄어드는 속도
    [SerializeField]
    private int hungryDecreaseTime;
    private int currentHungryDecreaseTime;

    // 목마름
    [SerializeField]
    private int thirsty;  // 최대 목마름. 유니티 에디터 슬롯에서 지정할 것.
    private int currentThirsty;

    // 목마름이 줄어드는 속도
    [SerializeField]
    private int thirstyDecreaseTime;
    private int currentThirstyDecreaseTime;

    [SerializeField]
    private GameObject openStatus;

    [SerializeField]
    private Image image_Gauge;
    [SerializeField]
    private Text[] text_Update;
    private const int  DP = 0, SP = 1, HUNGRY = 2, THIRSTY = 3;

    private void Start()
    {
        currentHp = hp;
        currentDp = dp;
        currentSp = sp;
        currentHungry = hungry;
        currentThirsty = thirsty;
    }
    private void Update()
    {
   
            Hungry();
            Thirsty();
            GagueUpdate();
            TextUdate();
      
     
    }
  
    public void increaseHP(int _count)
    {
        if (currentHp + _count < hp)
        {
            currentHp += _count;
        }
        else
        {
            currentHp = hp;
        }

    }
    public void DecreaseHP(int _count)
    {
        //방어력을 대신 먼저 깎임
        if (currentDp > 0)
        {
            DecreaseDP(_count);
            return;
        }
        currentHp -= _count;
        if (currentHp <= 0)
        {
            Debug.Log("캐릭터의 hp가 0입니다.");
        }
    }
    public void increaseDP(int _count)
    {
        if (currentDp + _count < dp)
        {
            currentDp += _count;
        }
        else
        {
            currentDp = dp;
        }

    }

    public void DecreaseDP(int _count)
    {

        currentDp -= _count;
        if (currentDp <= 0)
        {
            Debug.Log("캐릭터의 dp가 0입니다.");
        }
    }
    public void increaseSP(int _count)
    {
        if (currentSp + _count < sp)
        {
            currentSp += _count;
        }
        else
        {
            currentSp = sp;
        }

    }

    public void DecreaseSP(int _count)
    {

        currentSp -= _count;
        if (currentSp <= 0)
        {
            Debug.Log("캐릭터의 dp가 0입니다.");
        }
    }


    public void increaseHungry(int _count)
    {

        if (currentHungry + _count < hungry)
        {
            currentHungry += _count;
        }
        else currentHungry = hungry;
    }
    public void DecreaseHungry(int _count)
    {
        if (currentHungry - _count < 0)
        {
            currentHungry = 0;
        }
        else
        {
            currentHungry -= _count;
        }


    }


    public void increaseThirsty(int _count)
    {

        if (currentThirsty + _count < thirsty)
        {
            currentThirsty += _count;
        }
        else currentThirsty = thirsty;
    }
    public void DecreaseThirsty(int _count)
    {
        if (currentThirsty - _count < 0)
        {
            currentThirsty = 0;
        }
        else
        {
            currentThirsty -= _count;
        }


    }


    private void Hungry()
    {
        if (currentHungry > 0)
        {
            if (currentHungryDecreaseTime <= hungryDecreaseTime)
            {
                currentHungryDecreaseTime++;
            }
            else
            {
                currentHungry--;
                currentHungryDecreaseTime = 0;
            }
        }
        else
            Debug.Log("배고픔 수치가 0이 되었습니다.");
    }
    private void Thirsty()
    {
        if (currentThirsty >0)
        {
            if (currentThirstyDecreaseTime <= thirstyDecreaseTime)
            {
                currentThirstyDecreaseTime++;
            }
            else
            {
                currentThirsty--;
                currentThirstyDecreaseTime = 0;
            }
        }
        else
            Debug.Log("목마름 수치가 0이 되었습니다.");
    }
    private void GagueUpdate()
    {
        image_Gauge.fillAmount=(float)currentHp / hp;
    }
    private void TextUdate()
    {

        text_Update[DP].text = "방어력 " + currentDp + " / " + dp;
        text_Update[SP].text = "스태미너: " + currentSp + " / " + sp;
        text_Update[HUNGRY].text = "배고픔: " + currentHungry + " / " + hungry;
        text_Update[THIRSTY].text = "목마름: " + currentThirsty + " / " + thirsty;
        
    }
    public void ToggleStat()
    {

        openStatus.SetActive(!openStatus.activeSelf);

    }



}
