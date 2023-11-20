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
    public List<RoomNode> GenerateRoomsInGivenSpace(List<Node> roomSpace)
    {
        List<RoomNode> listToReturn = new List<RoomNode>();
        foreach (var space in roomSpace)
        {
            Vector2Int newBottomLeftPoint = StructureHelper.GenerateBottomLeftCornerBetween(
                space.BottomLeftAreaCorner, space.TopRightAreaCorner, 0.1f, 1);

            Vector2Int newTopRightPoint = StructureHelper.GenerateTopRightCornerBetween(
                space.BottomLeftAreaCorner, space.TopRightAreaCorner, 0.9f, 1);

            space.BottomLeftAreaCorner = newBottomLeftPoint;
            space.BottomRightAreaCorner = newTopRightPoint;
            space.BottomRightAreaCorner = new Vector2Int(newTopRightPoint.x, newBottomLeftPoint.y);
            space.TopLeftAreaCorner = new Vector2Int(newBottomLeftPoint.x, newTopRightPoint.y);
            listToReturn.Add((RoomNode)space);
        }
        return listToReturn;
    }
}