using UnityEngine;

public class WIndow_bool : MonoBehaviour
{
    public bool isOpen = false;
    public bool isBroken = false;
    private bool one = false;
    Animator animator;

    private void Start()
    {
        TryGetComponent(out animator);
    }

    public void WindowAnimation()
    {
        isOpen = !isOpen;
        if (!isBroken)
        {
            animator.SetBool("isOpen", isOpen);
            gameObject.transform.GetChild(0).gameObject.SetActive(!isOpen);
        }
        else if(isBroken)
        {
 
        }
    }

    public void WindowSetActive()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
           if(!isBroken)
            {
                isBroken = true;
                MusicController.instance.PlaySFXSound("Window_Bottele");
                isOpen = !isOpen;
                WindowSetActive();
            }
    
        }
    }
}
