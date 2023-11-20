using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCreator : MonoBehaviour
{
    public int dungeonWidth, dungeonLength;
    public int roomWidthMiin, roomLengthMin;
    public int maxIterations;
    public int corridorWidth;
    public Material material;

    MeshRenderer meshRenderer;

    /*    private void Start()
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

            if(meshRenderer != null)
            {
                Mesh mesh = meshRenderer.GetComponent<MeshFilter>().mesh; //게임 오브젝트에 대한 기본적인 메쉬 데이터를 저장
            }
        }*/



    private void Start()
    {
        CreateDungeon();
    }
    private void CreateDungeon()
    {
        DungeonGenerator generator = new DungeonGenerator(dungeonWidth, dungeonLength);
        // var listoOfRooms = generator.CalculateRooms(maxIterations, roomWidthMiin, roomLengthMin);

    }


}