using UnityEngine;

public class WIndow_bool : MonoBehaviour
{
    public bool isOpen = false;

    Animator animator;

    private void Start()
    {
        TryGetComponent(out animator);
    }

    private void Update()
    {
        animator.SetBool("isOpen", isOpen);
    }
}
