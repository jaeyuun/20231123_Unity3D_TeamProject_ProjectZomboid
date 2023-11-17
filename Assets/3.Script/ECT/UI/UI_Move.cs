using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Move : MonoBehaviour
{
    private void Update()
    {
        av();
    }
    private IEnumerator av()
    {
        yield return null;
        for (int i = 0; i < 10; i++)
        {
            transform.position = new Vector3(transform.position.x + i, transform.position.y, transform.position.z);
        }
        yield return null;
        for (int q = 0; q < -10; q++)
        {
            transform.position = new Vector3(transform.position.x - q, transform.position.y, transform.position.z);
        }

        yield return new WaitForSeconds(2f);
    }
}
