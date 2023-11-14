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


    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && !inVehicle && player_isCar.iscar)
        {
            EnterVehicle(); // 차량에 탑승
            rig.isKinematic = false;//키네마틱을 비활성화 시켜 움직이게 함
        }
        else if (Input.GetKeyDown(KeyCode.E) && inVehicle)
        {
            ExitVehicle(); // 차량에서 내림
            rig.isKinematic = true;//키네마틱을 활성화 시켜 움직이게 함
        }
    }



    void EnterVehicle()
    {
        // 플레이어를 차량 내부에 위치
        Player.transform.position = Vehicle.transform.position;
        Player.SetActive(false);

        // 차량 조작 활성화
        Vehicle.GetComponent<VehicleController>().enabled = true;
        Carmer.GetComponent<Camera_Controller>().enabled = false;
        Carmer.GetComponent<Camera_Controller_Car>().enabled = true;


        inVehicle = true;
    }

    void ExitVehicle()
    {
        // 플레이어를 차량 외부에 위치
        Player.transform.position = Vehicle.transform.position + new Vector3(2, 0, 0);
        Player.SetActive(true);

        // 차량 조작 비활성화
        Vehicle.GetComponent<VehicleController>().enabled = false;
        Carmer.GetComponent<Camera_Controller>().enabled = true;
        Carmer.GetComponent<Camera_Controller_Car>().enabled = false;

        inVehicle = false;
    }
}