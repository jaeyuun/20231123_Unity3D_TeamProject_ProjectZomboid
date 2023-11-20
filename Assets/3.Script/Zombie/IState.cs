using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void Idle();
    void Die();
    IEnumerator Jump();
    void WakeUp();
    IEnumerator Stun();
}
