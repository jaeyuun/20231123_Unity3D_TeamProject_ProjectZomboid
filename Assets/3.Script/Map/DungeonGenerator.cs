using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class DugeonGenerator
{

    List<RoomNode> allNodesCollection = new List<RoomNode>();
    private int dungeonWidth;       // 던전의 너비
    private int dungeonLength;      // 던전의 길이


    public DugeonGenerator(int dungeonWidth, int dungeonLength)     //던전의 너비와 길이를 초기화
    {
        this.dungeonWidth = dungeonWidth;
        this.dungeonLength = dungeonLength;
    }

    //던전 레이아웃 계산
    public List<Node> CalculateDungeon(int maxIterations, int roomWidthMin, int roomLengthMin, 
                                       float roomBottomCornerModifier, float roomTopCornerMidifier, 
                                       int roomOffset, int corridorWidth)
    {
        BinarySpacePartitioner bsp = new BinarySpacePartitioner(dungeonWidth, dungeonLength);
        allNodesCollection = bsp.PrepareNodesCollection(maxIterations, roomWidthMin, roomLengthMin);

        List<Node> roomSpaces = StructureHelper.TraverseGraphToExtractLowestLeafes(bsp.RootNode);

        //RoomGenerator 초기화 및 주어진 공간에 방 생성
        RoomGenerator roomGenerator = new RoomGenerator(maxIterations, roomLengthMin, roomWidthMin);
        List<RoomNode> roomList = roomGenerator.GenerateRoomsInGivenSpaces(roomSpaces, roomBottomCornerModifier, roomTopCornerMidifier, roomOffset);

        // 생성된 방과 노드 컬렉션을 기반으로 복도 생성
        CorridorsGenerator corridorGenerator = new CorridorsGenerator();
        var corridorList = corridorGenerator.CreateCorridor(allNodesCollection, corridorWidth);

        //// 방과 복도의 리스트를 결합하고 반환
        return new List<Node>(roomList).Concat(corridorList).ToList();
    }
}