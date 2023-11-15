using UnityEngine;

public class WindowActive : MonoBehaviour
{
    public float radius = 0.15f;
    private GameObject window;
    private int hitCount = 0;
    [SerializeField] private AudioClip bottele;
    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 1.5f, radius);
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + Vector3.up * 1.5f, radius);

        if(Input.GetKeyDown(KeyCode.E))
        {
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.CompareTag("Window"))
                {
                    //Debug.Log("hit");

                    window = collider.gameObject.transform.GetChild(0).gameObject;
                    audio.PlayOneShot(bottele);
                    window.SetActive(false);
                }

                if (collider.gameObject.CompareTag("WindowHit"))
                {
                    WIndow_bool window = collider.GetComponentInChildren<WIndow_bool>();
                    if (window != null)
                    {
                        if (hitCount >= 3)
                        {
                            Debug.Log(hitCount);
                            hitCount++;
                            window.isOpen = !window.isOpen;
                        }
                        
                    }




                }
            }
        }
          
            
        
    }
}
