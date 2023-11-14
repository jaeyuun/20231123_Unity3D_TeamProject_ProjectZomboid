using UnityEngine;
using UnityEngine.EventSystems;

public class RightClickMenu : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject rightClickMenu;

    private void Awake()
    {
        rightClickMenu = transform.GetChild(0).gameObject;
    }

    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button.Equals(PointerEventData.InputButton.Right))
        {
            rightClickMenu.transform.position = new Vector2(pointerEventData.position.x + 150f, pointerEventData.position.y - 150f);
            rightClickMenu.SetActive(true);
        }
    }
}