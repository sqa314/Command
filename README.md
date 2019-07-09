# Command
1:1 대전 게임 Command 오픈소스
## 1. 개요
[![Video Label](http://img.youtube.com/vi/fmd-7fw2pIU/0.jpg)](https://youtu.be/fmd-7fw2pIU)<br>
이미지을 클릭하시면 영상으로 이동합니다.<br>
다운로드 : <https://play.google.com/store/apps/details?id=com.sdy.Pdffme><br>
> #게임 이름 : Command<br>
> #게임 장르 : 캐쥬얼 대전 게임<br>
> #개발 인원 : 1인 개발<br>
> #개발 엔진 : Unity3D<br>
> #대상 기종 : Android(Mobile)<br>
> #개발 언어 : C#<br>
> #라이브러리 : PUN2
## 2. 분석
### 2.1 기본 조작
#### 2.1.1 이동
+ 화면의 왼쪽을 터치하여 발생하는 모든 동작을 이야기 합니다.
+ 화면을 터치 후 움직이는 방향과 거리에 따라 Player의 이동 을 결정합니다.
+ Player의 이동방향과는 별개로, 커맨드 조작을 위한 별도의 방향과 거리가 적용됩니다.
+ 부드럽게 움직이는 이동방향 기준점과는 다르게, 커맨드 조작 기준점은 방향이 확정되면 그 즉시 기준점을 변경합니다.
+ 커맨드 조작이 될 때마다 현재 커맨드 입력사항이 갱신되며 공격시 발사되는 스킬이 변경됩니다.
+ 다른 플레이어의 이동 컴포넌트는 네트워크 룸에 입장함과 동시에 삭제됩니다.
<https://github.com/sqa314/Command/blob/master/project/Assets/Resource/scripts/Touch.cs>
#### 2.1.2 스킬
+ 화면 오른쪽을 터치하여 발생하는 모든 동작을 이야기 합니다.
+ 미리 사용자가 지정한 4개의 커맨드가 존재합니다.
+ 만약 현재 입력된 이동이 스킬 커맨드와 일치한다면 공격시 스킬이 발사됩니다.
+ 만약 현재 입력된 이동이 없거나, 스킬 커맨드와 일치하지 않는다면 기본공격이 발사됩니다.
+ 만약 오른쪽 터치의 길이가 짧다면, 한점을 클릭한 것으로 인식하고 그곳으로 점멸합니다.
+ 점멸은 이동과는 별개의 동작이며, 일정거리 이상을 클릭할 경우 해당 방향으로 최대사거리만큼 이동합니다.
+ 다른 플레이어의 스킬 컴포넌트는 네트워크 룸에 입장함과 동시에 삭제됩니다.
<https://github.com/sqa314/Command/blob/master/project/Assets/Resource/scripts/AutoAttack.cs>
<https://github.com/sqa314/Command/blob/master/project/Assets/Resource/scripts/Skill1.cs>
<https://github.com/sqa314/Command/blob/master/project/Assets/Resource/scripts/Skill2.cs>
<https://github.com/sqa314/Command/blob/master/project/Assets/Resource/scripts/Skill3.cs>
<https://github.com/sqa314/Command/blob/master/project/Assets/Resource/scripts/Skill4.cs>
#### 2.1.3 피격
+ Player가 다른 Player에게 피격당할 경우 서로 동일한 거리, 방향으로의 반응을 주는데, 서버지연으로 인해 이 동작이 다르게 반응하기도 합니다.
+ 그러한 상황에서도 Onwer Player의 움직임에 따라 빠르게 동기화 되므로 플레이에 문제는 없습니다.
+ 피격 관련 컴포넌트는 모두가 같이 공유하고, 스킬 투사체의 소유자에 따라 다르게 반응합니다.
<https://github.com/sqa314/Command/blob/master/project/Assets/Resource/scripts/Hit.cs>
### 2.2 RPC
+ 초기에는 플레이어가 발사한 투사체를 다른 플레이어에게 위치, 크기, 각도를 동기화시키는 방식으로 진행했습니다.
+ 동기화 방식은 끊김이 발생하여 온라인 서버를 사용하는 상황에서는 어렵다는 것을 알게되었습니다.
+ 그래서 RPC방식을 통해 다른 모든 플레이어에게 서버 시간과 초기위치, 초기방향을 통해 각자 스스로 투사체를 생성하도록 했습니다.
+ RPC 방식을 사용함에 따라 투사체의 이동이 자연스러워졌고 충돌역시 정상적으로 이루어졌습니다.
<https://github.com/sqa314/Command/blob/master/project/Assets/Resource/scripts/PlayerRPC.cs>
### 2.3 Network
+ 네트워크는 Photon Server를 사용하였으며 Photon Engine에서 제공하는 PUN2 라이브러리를 적극적으로 활용하였습니다.
+ 모든 매칭이 랜덤매칭으로 이루어지는 이 게임 특성상 로비의 입장과 룸의 입장을 구별할 필요가 없어 로비입장이 완료되면 곧바로 룸에 입장합니다.
+ 만약 룸이 존재한다면 룸에 참여하게 되고, 없다면 자동으로 새로운 룸을 생성합니다.
+ 룸을 생성한 플레이어가 MasterClient가 되며, 플레이어가 도중에 룸에서 나가면, 다른 플레이어가 Masterclient를 양도받습니다.
+ MasterClient는 게임 시작의 권한은 가집니다.
<https://github.com/sqa314/Command/blob/master/project/Assets/Resource/scripts/Player.cs>
<https://github.com/sqa314/Command/blob/master/project/Assets/Resource/scripts/network/NetworkConnectionManager.cs>
<https://github.com/sqa314/Command/blob/master/project/Assets/Resource/scripts/GameManager.cs>
### 2.4 UI
+ 모든 UI는 유니티 기본 Canvas 기반으로 제작하였으며, 별도의 라이브러리는 활용하지 않았습니다.
+ 커맨드 조작에 따른 화살표 UI는 Touch컴포넌트에 직접적인 연관이 있으므로 Touch내부에서 조절합니다.
+ 현재 입력된 커맨드가 스킬커맨드와 일치할 경우, 해당 스킬 슬롯 UI의 색깔이 변경됩니다.
+ Arena는 충돌을 위해 UI가 아닌 일반 오브젝트로 생성되었습니다.
<https://github.com/sqa314/Command/blob/master/project/Assets/Resource/scripts/UIdefault.cs>
<https://github.com/sqa314/Command/blob/master/project/Assets/Resource/scripts/PushSkill.cs>
### 2.5 Data
+ Save & Load되는 데이터는 4개의 스킬커맨드, BGM On-Off, SFX On-Off로 총 6개이며, 모두 유니티 제공 API인 PlayerPrefs기능을 통해 구현하였습니다.
+ 모든 데이터는 GetInt, SetInt를 통해 Save & Load되었으며, 게임이 시작되면 Load, 변경사항이 생기면 그 즉시 Save됩니다.
<https://github.com/sqa314/Command/blob/master/project/Assets/Resource/scripts/PushSkill.cs>
<https://github.com/sqa314/Command/blob/master/project/Assets/Resource/scripts/UINetField.cs>
### 2.6 Resource
+ 모든 이미지 리소스들은 상업목적을 포함한 자유 이용 허락저작물입니다.
+ 모든 사운드 리소스들은 상업목적을 포함한 자유 이용 허락저작물입니다.
## 3. 결론
이 게임은 유니티를 이용한 3D로그라이크 게임이 리소스 문제로 인해 원하는 방향으로 갈 수 없겠다는 판단으로<br>
비교적 자유롭게 사용할 수 있는 리소스가 많은 2D게임으로 전환하기 위한 시험작입니다.<br>
개발 중간까지 네트워크 연결을 고려하지 않은 채 코딩하는 바람에, 온라인으로 노선을 변경한 이후에는 코드를 완전히 다시 작성해야 했습니다.<br>
어려움이 많았지만, 게시되게 되어 기쁩니다.
