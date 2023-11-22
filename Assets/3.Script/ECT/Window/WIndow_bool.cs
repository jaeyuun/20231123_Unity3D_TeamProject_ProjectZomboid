using UnityEngine;

public class WIndow_bool : MonoBehaviour
{
    public bool isOpen = false;
    public bool isBroken = false;
    Animator animator;

    private void Start()
    {
        TryGetComponent(out animator);
    }

    public void WindowAnimation()
    {
        isOpen = !isOpen;
        animator.SetBool("isOpen", isOpen);
        gameObject.transform.GetChild(0).gameObject.SetActive(!isOpen);
        gameObject.transform.GetChild(1).gameObject.SetActive(!isOpen);
    }
}
