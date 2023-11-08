using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel_Anim : MonoBehaviour
{
    public WheelCollider frontLeftWheel, frontRightWheel; // ¾Õ ¹ÙÄû µÎ °³
    public WheelCollider rearLeftWheel, rearRightWheel; // µÞ ¹ÙÄû µÎ °³
    private float Rot = 0f;

    private void FixedUpdate()
    {
        
    }


    private void Wheel_spin()//¹ÙÄû ¾ÕÀ¸·Î ±¼¸®±â
    {
        Rot += 0.1f;
        frontLeftWheel.transform.Rotate(Rot, 0, 0);
        frontRightWheel.transform.Rotate(Rot, 0, 0);
        rearLeftWheel.transform.Rotate(Rot, 0, 0);
        rearRightWheel.transform.Rotate(Rot, 0, 0);
    }
}
