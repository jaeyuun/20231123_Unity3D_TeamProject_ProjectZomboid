using System.Collections.Generic;
using System;
using UnityEngine;

internal class BinarySpacePartitioner
{
    RoomNode rootNode;
   
    public RoomNode RoomNode { get => rootNode; }

    public BinarySpacePartitioner(int dungeonWidth,  int dungeonLength)
    {
        /* this.dungeonWidth = dungeonWidth;
         this.dungeonLength = dungeonLength;*/
        this.rootNode = new RoomNode(new Vector2Int(0, 0), new Vector2Int(dungeonWidth, dungeonLength), null, 0);
    }

    public List<RoomNode> PrepareNodesCollection(int maxIterations, int roomwidthMin, int roomLengthMin)
    {
        Queue<RoomNode> graph = new Queue<RoomNode>();
        List<RoomNode> listToReturn = new List<RoomNode>();
        graph.Enqueue(this.rootNode);
        listToReturn.Add(this.rootNode);
        int iterations = 0;
        while(iterations<maxIterations && graph.Count>0)
        {
            iterations++;
            RoomNode currentNode = graph.Dequeue();
            if(currentNode.Width >= roomwidthMin*2 || currentNode.Length >=roomLengthMin*2)
            {
                SplitTheSpace(currentNode, listToReturn, roomwidthMin, roomLengthMin, graph);
            }
        }
    } 

    private void SplitTheSpace(RoomNode currentNode, List<RoomNode> listToReturn , int roomwidthMin, int roomLengthMin)
    {
        Line line = GetLineDividingSpace(
            currentNode.BottomLeftAreaCorner,
            currentNode.TopRightAreaCorner,
            roomwidthMin,
            roomLengthMin
            );
    }

    private Line GetLineDividingSpace(Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner, int roomwidthMin, int roomLengthMin)
    {
        throw new NotImplementedException();
    }


}