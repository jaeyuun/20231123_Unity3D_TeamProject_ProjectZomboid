# Project Zomboid
> Project Zomboid 모작입니다.   
> 플레이어가 좀비 아포칼립스 세계에서 살아남는 게임입니다.
## Project
### Unity 3D TeamProject   
RPG, Survival
#### 1. 제작 기간
2023.11.01 ~ 2023.11.23
#### 2. 참여 인원 (4인)
- 김민호 ([BANIKIM](https://github.com/BANIKIM))
  > 플레이어, 장비 및 차량 상호작용
- 박수진 ([Sujiii1](https://github.com/Sujiii1))
  > 맵 레벨링, 맵 상호작용, 시간
- 박준영 ([0Baek](https://github.com/0Baek))
  > 인벤토리, 저장
- 이재윤 ([jaeyuun](https://github.com/jaeyuun))
  > 좀비, 맵 상호작용, 설정
## Function
- `플레이어` : 이동, 공격, 상호작용, 제한된 시야에서 좀비 감지, 사망
- `좀비` : 제한된 시야에서 플레이어 감지 후 추적, 랜덤 타깃 추적
- `맵` : 우클릭 상호작용, 장비와 상호작용, 차량 상호작용, 시간에 따른 하늘 변경
- `인벤토리` : 음식, 장비 저장, 버리기 기능
- `저장` : 재시작 시 저장된 정보 불러오기
- `설정` : 음량 조절
## Tech Stack
`C#`, `LitJson`
## Environment
- Unity 2020.3.36f1
- Visual Studio 2019
