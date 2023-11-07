using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ZombieData", fileName = "ZombieData")]
public class ZombieData : ScriptableObject
{
    /*
        체력, 메시 렌더러, 스크림 좀비 유무
     */
    // public float zombieHp = 5f;
    public Mesh skinnedMesh;
    public bool isScreamZombie = false;
}
