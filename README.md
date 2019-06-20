# Last-Shelter

한성대학교 이지은 교수님의 가상현실과 증강현실 강의를 듣고 제출한 최종 프로젝트 입니다.<br>
PPT에 자세한 내용이 있습니다.<br>

- 연동되는 장비: **HTC VIVE**, **PC**에서 시뮬레이션이 가능합니다.
- VIVE 장비를 쓰고 보는 화면과 PC에서 시뮬레이션 하는 화면이 다르기 때문에 2가지 버전으로 만들었습니다.
- Unity 프로젝트 생성 후 하단의 패키지 중 하나를 골라 import하면 됩니다.
  - LastShelter_Package_Simulator
  - LastShelter_Package_VR

- import 방법
  1. 새 Unity 프로젝트 생성
  2. Project 창에서 Assets/Scenes 삭제
  3. 메뉴 상단바에서 Assets-Import Package-Custom Package 패키지 선택 -> Import All
  4. Project 창에서 Assets/Scenes/Stage 더블 클릭
  
- Simulator 활성화 
  - 메뉴 상단바에서 Edit-Preferences-VIU Settings-Supporting Device -> Simulator 체크
 
- Simulator 키 맵핑
  - 캐릭터 회전: ESC
  - 카메라 회전: 0
  - 오른손 전환: 1
    - 레이저 토글: 마우스 오른쪽 버튼 클릭
    - 상하좌우 이동: Shift + 마우스 상하좌우 드래그
    - 점프: 마우스 중간 버튼 클릭
    - 총알 발사: 마우스 왼쪽 버튼 클릭
  - 왼손 전환: 2
    - 레이저 토글: 마우스 오른쪽 버튼 클릭
    - 미니맵 토글: m 키
    - 총알 발사: 마우스 왼쪽 버튼 클릭
  
- VIVE 장비 연동
  - 메뉴 상단바에서 Edit-Preferences-VIU Settings-Supporting Device -> VIVE 체크, Add OpenVR Package
  
- VIVE 키 맵핑
  - 오른손
    - 레이저 토글: 트랙패드 클릭
    - 상하좌우 이동: 트랙패드 터치
    - 점프: 그립 버튼
    - 총알 발사: 트리거 버튼
  - 왼손
    - 레이저 토글: 트랙패드 클릭
    - 미니맵 토글: 메뉴 버튼
    - 총알 발사: 트리거 버튼
