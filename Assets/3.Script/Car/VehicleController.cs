using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [Header("휠콜라이더")]
    public WheelCollider frontLeftWheel, frontRightWheel; // 앞 바퀴 두 개
    public WheelCollider rearLeftWheel, rearRightWheel; // 뒷 바퀴 두 개
    public Car_Sound car_Sound;



    [Header("애니메이션")]
    public GameObject frontLeftWheel_Ain, frontRightWheel_Ain; // 앞 바퀴 두 개
    public GameObject rearLeftWheel_Ain, rearRightWheel_Ain; // 뒷 바퀴 두 개

    [Header("라이트")]
    [SerializeField] private GameObject Light_R;
    [SerializeField] private GameObject Light_L;
    private bool Light = false;

    public float motorForce = 1000; // 모터 힘
    public float steeringAngle = 45f; // 스티어링 각도
    private bool isStart_up = false;
    private float Rot = 0f;

    private void Start()
    {
        car_Sound = GetComponent<Car_Sound>();
    }

    private void FixedUpdate()
    {
        var motorInput = Input.GetAxis("Vertical") * motorForce; // 수직 입력(키보드의 W와 S 또는 위쪽 화살표와 아래쪽 화살표 키)
        var steeringInput = Input.GetAxis("Horizontal") * steeringAngle; // 수평 입력(키보드의 A와 D 또는 왼쪽 화살표와 오른쪽 화살표 키)

        if (Input.GetKeyDown(KeyCode.F))//라이트 켜고 끄기
        {
            if (!Light)
            {
                Light_R.SetActive(true);
                Light_L.SetActive(true);
                Light = true;
            }
            else
            {
                Light_R.SetActive(false);
                Light_L.SetActive(false);
                Light = false;
            }
        }

        if (!isStart_up)
        {
            car_Sound.Start_up();
            isStart_up = true;
        }
        else if (motorInput != 0 && isStart_up)
        {
            car_Sound.Drive();
            ApplyInput(motorInput, steeringInput);
            //Wheel_spin(steeringInput);

        }

        /*        frontLeftWheel.steerAngle = Input.GetAxis("Horizontal") * steeringAngle;
                frontRightWheel.steerAngle = Input.GetAxis("Horizontal") * steeringAngle;*/

    }

    private void ApplyInput(float motorInput, float steeringInput)
    {
        // 모터 토크 적용
        frontLeftWheel.motorTorque = motorInput;
        frontRightWheel.motorTorque = motorInput;
        rearLeftWheel.motorTorque = motorInput;
        rearRightWheel.motorTorque = motorInput;

        // 스티어링 각도 적용
        float a = frontLeftWheel.steerAngle = steeringInput;
        float b = frontRightWheel.steerAngle = steeringInput;

        Rot += 5f;
        Quaternion rotation_L = Quaternion.Euler(Rot, a - 80f, 0);
        Quaternion rotation_R = Quaternion.Euler(Rot, b + 80f, 0);
        

        frontLeftWheel_Ain.transform.rotation = rotation_L;
        frontRightWheel_Ain.transform.rotation = rotation_R;
        rearLeftWheel_Ain.transform.Rotate(Rot,0,0);
        rearRightWheel_Ain.transform.Rotate(Rot, 0, 0);
    }









}