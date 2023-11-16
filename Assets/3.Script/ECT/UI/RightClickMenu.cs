using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RightClickMenu : MonoBehaviour, IPointerClickHandler, IClickState
{
    private GameObject rightClickMenu; // UI 보여줄 위치
    private RectTransform rightClickRect;
    private float objectDistance;
    private float minDistance = 0;
    private RaycastHit minHit;

    [SerializeField] private GameObject buttonPrefab;
    private class RightClickButton
    {
        public GameObject button;
        public Text buttonText;
    }
    private int buttonCount = 0;
    private RightClickButton[] rightClickButtons;
    private Transform objectPos;
    private GameObject hitObject;
    private string[] objectList = { "Window", "Door", "Fence" }; // 상호 작용하는 오브젝트 종류

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
            OnPointerObject();
            rightClickMenu.transform.position = new Vector2(pointerEventData.position.x + 150f, pointerEventData.position.y - 50f);
            rightClickMenu.SetActive(true);
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
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
        RaycastHit[] hit;

        /*
            상호 작용하는 것만 인식
            CompareTag() 할 게 있다면 바로 상호작용할 hitObject = hit[i].collider... return
            switch (hitObject) {
                case "":
                    break;
                case "":
                    break;
            }
        */

        hit = Physics.RaycastAll(ray);
        if (hit != null)
        {
            for (int i = 0; i < hit.Length; i++)
            { // 마우스 클릭 시 좌표와 태그가 있는 오브젝트 사이 간 거리가 제일 가까운 오브젝트와 상호작용 하기 위한 비교 과정
                // CompareTag()
                if (hit.Length > 1)
                {
                    objectPos = hit[i].collider.gameObject.transform.parent;
                } else
                {
                    objectPos = hit[i].collider.gameObject.transform;
                }
                
                objectDistance = Vector2.Distance(Input.mousePosition, objectPos.position);
                Debug.Log($"{hit[i].collider.gameObject.name}'s Hit Position: {hit[i].collider.gameObject.transform.position}");
                if (minDistance == 0 || minDistance > objectDistance)
                {
                    // minDistance == 0은 처음, 이후 min과 object distance를 비교
                    minDistance = objectDistance;
                    minHit = hit[i];
                }
            }
        }

        Debug.Log($"Min Position: {minHit.collider.tag}");
        // minHit에 대한 메뉴 출력... todo
        // Player Tag는 쉬는 것
        // Door Tag 열기
        // Window 부수기, 열기, 유리치우기, 넘어가기 등...
        // 메뉴 개수
        if (minHit.transform.gameObject.CompareTag("Player"))
        {
            PlayerClick();
        } else if (minHit.transform.gameObject.CompareTag("Door"))
        {
            DoorClick();
        }
    }

    private void ClickMenuLoad(string[] menu)
    {
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

    #region Click State
    public void PlayerClick()
    {
        string[] menu = { "Player1", "Player2", "Player3" }; // Text, 오브젝트마다 달라짐
        ClickMenuLoad(menu);
    }

    public void DoorClick()
    {
        string[] menu = { "Door1", "Door2", "Door3", "Door4" }; // Text, 오브젝트마다 달라짐
        ClickMenuLoad(menu);
    }
    #endregion
}