using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleInteract : MonoBehaviour
{
    public GameObject Player;
    public Player_isCar player_isCar;
    public GameObject Vehicle;
    public GameObject Carmer;
    public Rigidbody rig;
    private bool inVehicle = false;
    /*[Header("승하차")]
    public AudioClip Car_in;
    public AudioClip Car_out;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }*/

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !inVehicle && player_isCar.iscar)
        {

            StartCoroutine(EnterVehicle()); // 차량에 탑승
            
        }
        else if (Input.GetKeyDown(KeyCode.E) && inVehicle)
        {
            StartCoroutine(ExitVehicle()); // 차량에서 내림
        }
    }

   private IEnumerator EnterVehicle()
    {
        // audioSource.PlayOneShot(Car_in);
        MusicController.instance.PlaySFXSound("Car_InOut");
        yield return new WaitForSeconds(1f);
        // 플레이어를 차량 내부에 위치
        Player.transform.position = Vehicle.transform.position;
        Player.SetActive(false);

        // 차량 조작 활성화
        Vehicle.GetComponent<VehicleController>().enabled = true;
        Carmer.GetComponent<Camera_Controller>().enabled = false;
        Carmer.GetComponent<Camera_Controller_Car>().enabled = true;

        rig.isKinematic = false;//키네마틱을 비활성화 시켜 움직이게 함
        inVehicle = true;
    }

    private IEnumerator ExitVehicle()
    {
        // audioSource.PlayOneShot(Car_out);
        MusicController.instance.PlaySFXSound("Car_InOut");
        yield return new WaitForSeconds(1f);

        // 플레이어를 차량 외부에 위치
        Player.transform.position = Vehicle.transform.position + new Vector3(2, 0, 0);
        Player.SetActive(true);

        // 차량 조작 비활성화
        Vehicle.GetComponent<VehicleController>().enabled = false;
        Carmer.GetComponent<Camera_Controller>().enabled = true;
        Carmer.GetComponent<Camera_Controller_Car>().enabled = false;
        rig.isKinematic = true;//키네마틱을 활성화 시켜 움직이게 함
        inVehicle = false;
    }
}