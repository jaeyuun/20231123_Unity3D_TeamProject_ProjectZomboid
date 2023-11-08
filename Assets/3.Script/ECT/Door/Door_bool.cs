using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_bool : MonoBehaviour
{
    public bool isOpen = false;
    

    Animator ani;

    private void Start()
    {
        TryGetComponent(out ani);
    }

    private void Update()
    {
        ani.SetBool("isOpen", isOpen);
    }
}
