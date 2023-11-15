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
    /* private float defaultStiffness;  // 기본 타이어 마찰력을 저장할 변수*/
    private float Oil=24f; //기름


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
        else if (motorInput != 0 && isStart_up && Oil>0)
        {
            car_Sound.Drive();
            ApplyInput(motorInput, steeringInput);
            //Wheel_spin(steeringInput);
            Oil -= 0.1f*Time.deltaTime;//기름다는거
            Debug.Log("기름쓰는중 : "+Oil);
        }

        //브레이크
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // 쉬프트 키가 눌려진 경우, 브레이크를 동작시킵니다.
            ApplyBrake();
            car_Sound.Brake();
        }
        else
        {
            // 쉬프트 키가 눌려지지 않은 경우, 브레이크 토크를 0으로 설정하여 브레이크를 해제합니다.
            frontLeftWheel.brakeTorque = 0;
            frontRightWheel.brakeTorque = 0;
            rearLeftWheel.brakeTorque = 0;
            rearRightWheel.brakeTorque = 0;
        }
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

    private void ApplyBrake()
    {
        float brakeForce = motorForce;  // 브레이크 힘은 일반적으로 모터 힘과 동일하게 설정합니다.
        frontLeftWheel.brakeTorque = brakeForce;
        frontRightWheel.brakeTorque = brakeForce;
        rearLeftWheel.brakeTorque = brakeForce;
        rearRightWheel.brakeTorque = brakeForce;
    }







}