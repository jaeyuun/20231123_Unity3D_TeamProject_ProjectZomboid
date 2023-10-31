# Unity_3D_Team_ProjectZomboid
 좀보이드프로젝트 모작

### 형식   
[타입이름] 내용  



### 타입 이름	내용     
Init	프로잭트 생성    
Feat	새로운 기능에 대한 커밋   
Fix	버그 수정에 대한 커밋   
Build	빌드 관련 파일 수정 / 모듈 설치 또는 삭제에 대한 커밋   
Remove	코드(파일) 삭제    
Docs	문서 수정에 대한 커밋 



### 파일명 규칙  
오브젝트명_구현목적   
Player_Move.cs



### 변수나 클래스 규칙    
메소드명 -> 대문자로 시작   
변수 -> 소문자로 시작   



### 주석 규칙 

```C#
//(어떤 이유때문에 작성한 메서드인지)플레이어 죽음 메소드     
public void Die()     
    {   //if문 조건 작성 (XXX의 조건이 만족하면 들어온다)       
        if (GameManager.FindObjectOfType<GameManager>().TryGetComponent(out GameManager gm))
        {     
            gm.EndGame(); //게임 매니저로 이동     
        }    
    }     
//20231024 본인이름작성      
```
