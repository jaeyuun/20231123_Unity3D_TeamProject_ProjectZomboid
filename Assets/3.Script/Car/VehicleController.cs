using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public WheelCollider frontLeftWheel, frontRightWheel; // 앞 바퀴 두 개
    public WheelCollider rearLeftWheel, rearRightWheel; // 뒷 바퀴 두 개
    public Car_Sound car_Sound;

    public float motorForce = 50f; // 모터 힘
    public float steeringAngle = 30f; // 스티어링 각도
    private bool isStart_up=false;

    private void Start()
    {
        car_Sound = GetComponent<Car_Sound>();
    }

    private void FixedUpdate()
    {
        var motorInput = Input.GetAxis("Vertical") * motorForce; // 수직 입력(키보드의 W와 S 또는 위쪽 화살표와 아래쪽 화살표 키)
        var steeringInput = Input.GetAxis("Horizontal") * steeringAngle; // 수평 입력(키보드의 A와 D 또는 왼쪽 화살표와 오른쪽 화살표 키)

        if (!isStart_up)
        {
            car_Sound.Start_up();
            isStart_up = true;
        }
        else if(motorInput!=0&&isStart_up)
        {
            car_Sound.Drive();
            ApplyInput(motorInput, steeringInput);            
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
        frontLeftWheel.steerAngle = steeringInput;
        frontRightWheel.steerAngle = steeringInput;
    }
}