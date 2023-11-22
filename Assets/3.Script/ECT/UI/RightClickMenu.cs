using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RightClickMenu : MonoBehaviour, IPointerClickHandler, IClickState
{
    private GameObject rightClickMenu; // UI 보여줄 위치
    private RectTransform rightClickRect;

    [SerializeField] private Button buttonPrefab;
    private List<Button> rightClickButtons = new List<Button>();
    private Button buttonList;
    private Text buttonText;
    private int buttonCount = 0;
    private RaycastHit hitObject;

    public bool isAim = false; // player 조준이 아닐 때

    private void Awake()
    {
        rightClickMenu = transform.GetChild(0).gameObject;
        rightClickRect = rightClickMenu.GetComponent<RectTransform>();
    }

    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button.Equals(PointerEventData.InputButton.Right) && !isAim)
        {
            ListClear();
            rightClickMenu.transform.position = new Vector2(pointerEventData.position.x + 150f, pointerEventData.position.y - (buttonCount * 50f));
            rightClickMenu.SetActive(true);
            OnPointerObject();
        }
        else if (pointerEventData.button.Equals(PointerEventData.InputButton.Left))
        {
            ListClear();
        }
    }

    private void ListClear()
    {
        if (buttonCount > 0)
        {
            for (int i = 0; i < rightClickButtons.Count; i++)
            {
                Destroy(rightClickButtons[i].gameObject);
            }
            rightClickButtons.Clear();
            rightClickMenu.SetActive(false);
        }
    }

    private void OnPointerObject()
    { // Raycast Point를 통해 오브젝트 목록 가져오기
        RaycastHit[] hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = Physics.RaycastAll(ray);

        if (hit != null)
        {
            for (int i = 0; i < hit.Length; i++)
            { // 우클릭 시 첫번째로 잡히는 오브젝트만 반환
                hitObject = hit[i];
                if (hit[i].collider.CompareTag("Window")) // window hit 추가... todo
                {
                    break;
                }
            }
        }
        // Player Tag는 쉬는 것
        // Door Tag 열기
        // Window 부수기, 열기, 유리치우기, 넘어가기 등...
        // 메뉴 개수
        switch (hitObject.collider.tag)
        {
            case "Window":
                WindowClick();
                break;
            default: // player, sound, untagged 등
                // player 관련 상호작용
                PlayerClick();
                break;
        }
    }

    private void ClickMenuLoad(string[] menu)
    {
        buttonCount = menu.Length;
        rightClickRect.sizeDelta = new Vector2(300, 100 * buttonCount); // State에 따른 버튼 카운트 변화, 버튼 개수만큼 높이가 늘어남
        //  button 해당 상호작용하는 메뉴만큼 생성 button text 맞게 변경해주기
        for (int i = 0; i < buttonCount; i++)
        {
            buttonList = Instantiate(buttonPrefab, rightClickMenu.transform);
            rightClickButtons.Add(buttonList);
            buttonText = buttonList.transform.GetChild(0).gameObject.transform.GetComponent<Text>();
            buttonText.text = menu[i];
        }
    }

    #region Click State
    public void PlayerClick()
    {
        string[] menu = { "휴식하기" }; // Text, 오브젝트마다 달라짐
        ClickMenuLoad(menu);
    }

    public void WindowClick()
    {
        string[] menu = { "창문 열기", "창문 닫기" };
        ClickMenuLoad(menu);

        WIndow_bool window = hitObject.collider.transform.GetComponent<WIndow_bool>();
        rightClickButtons[0].onClick.AddListener(window.WindowAnimation);
        rightClickButtons[1].onClick.AddListener(window.WindowAnimation);
    }
    #endregion
}