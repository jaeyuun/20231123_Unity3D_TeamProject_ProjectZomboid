using System;
using System.Collections.Generic;
using UnityEngine;

internal class DungeonGenerator
{
    RoomNode rootNode;
    List<RoomNode> allSpaceNodes = new List<RoomNode>();
    private int dungeonWidth;
    private int dungeonLength;

    public DungeonGenerator(int dungeonWidth, int dungeonLength)
    {
        this.dungeonWidth = dungeonWidth;
        this.dungeonLength = dungeonLength;

        
    }

   /* public List<Node> CalculateRooms(int maxIterations, int roomWidthMin, int roomLengthMin)
    {
        BinarySpacePartitioner bsp = new BinarySpacePartitioner(dungeonWidth, dungeonLength);
        allSpaceNodes = bsp.PrepareNodesCollection(maxIterations, roomWidthMin, roomLengthMin);
        List<Node> roomSpace = StructureHelper.TraverseGraphToExtractLowestLeafes(bsp.RoomNode);  // RootNode

       *//* RoomGenerator roomGecerator = new RoomGenerator(maxIterations, roomLengthMin, roomWidthMin);
        List<RoomNode> roomlist = roomGecerator.GenerRooomInGivenSpace(roomSpace);
        return new List <Node>(allSpaceNodes);*//*
    }*/
}