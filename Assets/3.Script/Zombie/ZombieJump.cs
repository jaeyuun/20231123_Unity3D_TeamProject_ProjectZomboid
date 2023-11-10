using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieJump : MonoBehaviour
{
    private ZombieController zombieController;
    private ZombieFieldOfView zombieFieldOfView;
    public Vector3 myPos;
    public float lookingAngle;
    public Vector3 lookDir;
    public float viewRadius = 2f;

    private Vector3 targetPos;
    

    private void Awake()
    {
        TryGetComponent(out zombieController);
        TryGetComponent(out zombieFieldOfView);
    }

    private void Start()
    {
        myPos = zombieFieldOfView.zombiePos;
        lookingAngle = zombieFieldOfView.lookingAngle;
        lookDir = zombieFieldOfView.lookDir;
    }

    private void Update()
    {
    }

    
}
