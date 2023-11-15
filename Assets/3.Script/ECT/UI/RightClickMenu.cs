using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RightClickMenu : MonoBehaviour, IPointerClickHandler, IClickState
{
    private GameObject rightClickMenu;
    private RectTransform rightClickRect;
    private float objectDistance;
    private float minDistance = 0;
    private RaycastHit minHit;

    [SerializeField] private Button buttonPrefab;
    private class RightClickButton
    {
        public Button button;
        public Text buttonText;
    }
    private int buttonCount = 0;
    private RightClickButton[] rightClickButtons;

    private void Awake()
    {
        rightClickMenu = transform.GetChild(0).gameObject;
        rightClickRect = rightClickMenu.GetComponent<RectTransform>();
    }

    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button.Equals(PointerEventData.InputButton.Right))
        {
            rightClickMenu.transform.position = new Vector2(pointerEventData.position.x + 150f, pointerEventData.position.y - 50f);
            Debug.Log(pointerEventData.position);
            rightClickMenu.SetActive(true);
            OnPointerObject();
        } else if (pointerEventData.button.Equals(PointerEventData.InputButton.Left))
        {
            rightClickMenu.SetActive(false);
            if (buttonCount > 0)
            {
                for (int i = 0; i < buttonCount; i++)
                {
                    Destroy(rightClickMenu.transform.GetChild(i));
                }
            }
        }
    }

    private void MenuSizeUpdate(int buttonCount)
    { // 버튼 개수만큼 높이가 늘어남
        rightClickRect.sizeDelta = new Vector2(300, 100 * buttonCount); // STate에 따른 버튼 카운트 변화
    }

    private void OnPointerObject()
    { // Raycast Point를 통해 오브젝트 목록 가져오기
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ray의 좌표와 클릭 시 좌표를 일치시키면 될듯...
        RaycastHit[] hit;

        hit = Physics.RaycastAll(ray);
        if (hit != null)
        {
            for (int i = 0; i < hit.Length; i++)
            { // 마우스 클릭 시 좌표와 태그가 있는 오브젝트 사이 간 거리가 제일 가까운 오브젝트와 상호작용 하기 위한 비교 과정
                if (hit[i].transform.gameObject.CompareTag("Untagged")) continue; // 상호작용 안되는 오브젝트 추가 예정
                objectDistance = Vector2.Distance(Input.mousePosition, hit[i].transform.position);

                Debug.Log(hit[i].transform.position);
                if (minDistance == 0 || minDistance > objectDistance)
                {
                    // minDistance == 0은 처음, 이후 min과 object distance를 비교
                    minDistance = objectDistance;
                    minHit = hit[i];
                }
            }
        }

        Debug.Log($"{minHit.transform.tag} Hit: {minHit.transform.position}");
        // minHit에 대한 메뉴 출력... todo
        // Player Tag는 쉬는 것
        // Door Tag 열기
        // Window 부수기, 열기, 유리치우기, 넘어가기 등...
        // 메뉴 개수
        if (minHit.transform.gameObject.CompareTag("Player"))
        {
            DoorClick();
        }
    }

    #region Click State
    public void DoorClick()
    {
        string[] menu = { "menu1", "menu2", "menu3" }; // Text, 오브젝트마다 달라짐
        buttonCount = menu.Length;
        //  button 갖고와서 butto text 변경해주기
        for (int i = 0; i < buttonCount; i++)
        {
            rightClickButtons[i].button = Instantiate(buttonPrefab);
            rightClickButtons[i].button.transform.SetParent(rightClickMenu.transform);
            rightClickButtons[i].buttonText = rightClickButtons[i].button.transform.GetChild(0).GetComponent<Text>();
            rightClickButtons[i].buttonText.text = menu[i];
        }
        MenuSizeUpdate(buttonCount);
    }
    #endregion
}