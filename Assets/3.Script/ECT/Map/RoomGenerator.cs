using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator
{
    private int maxIteration;
    private int roomLengthMin;
    private int roomWidthMin;

    public RoomGenerator(int maxIteration, int roomLengthMin, int roomWidthMin)
    {
        this.maxIteration = maxIteration;
        this.roomLengthMin = roomLengthMin;
        this.roomWidthMin = roomWidthMin;
    }
   /* public List<RoomNode> GenerateRoomsInGivenSpace(List<Node> roomSpace)
    {
        List<RoomNode> listToReturn = new List<RoomNode>();
        foreach (var space in roomSpace)
        {
            Vector2Int newBottomLeftPoint = StructureHelper.GenerateBottomLeftCornerBetween(space.BottomLeftAreaCorner, space.TopRightAreaCorner, 0.1f, 1);
        };


    }*/
}