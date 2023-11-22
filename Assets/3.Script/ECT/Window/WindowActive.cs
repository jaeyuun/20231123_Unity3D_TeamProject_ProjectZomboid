using UnityEngine;

public class WindowActive : MonoBehaviour
{
    public float radius = 1f;
    private GameObject window;
    private int hitCount = 0;
    /*[SerializeField] private AudioClip bottele;
    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }*/

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 1.5f, 0), radius);
    }

    private void Update()
    {
        WindowBroke();
    }

    private void WindowBroke()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + new Vector3(0, 1.5f, 0), radius);
        if (Input.GetKeyDown(KeyCode.Y))
        {
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Window"))
                {
                    window = collider.gameObject.transform.GetChild(0).gameObject;
                   
                    // audio.PlayOneShot(bottele);
                    MusicController.instance.PlaySFXSound("Player_Hit");
                    window.SetActive(false);
                }

                if (collider.CompareTag("WindowHit"))
                {
                    WIndow_bool window = collider.GetComponentInChildren<WIndow_bool>();
                    if (window != null)
                    {
                        hitCount++;
                        if (hitCount >= 3)
                        {
                            window.isOpen = !window.isOpen;
                            hitCount = 0;
                        }
                    }
                }
            }
        }
    }
}
