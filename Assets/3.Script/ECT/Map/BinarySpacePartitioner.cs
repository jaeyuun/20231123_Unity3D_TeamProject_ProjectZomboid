using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

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
        }return listToReturn;
    } 

    private void SplitTheSpace(RoomNode currentNode, List<RoomNode> listToReturn , int roomwidthMin, int roomLengthMin, Queue<RoomNode> graph)
    {
        Line line = GetLineDividingSpace(
            currentNode.BottomLeftAreaCorner,
            currentNode.TopRightAreaCorner,
            roomwidthMin,
            roomLengthMin
            );
        RoomNode node1, node2;
        if(line.Orientation == Orientation.Horiszontal)
        {
            node1 = new RoomNode(currentNode.BottomLeftAreaCorner,
                new Vector2Int(currentNode.TopRightAreaCorner.x, line.Coordinates.y),
                currentNode,
                currentNode.TreeLayerIndex+1);

            node2 = new RoomNode(new Vector2Int(line.Coordinates.x, currentNode.BottomLeftAreaCorner.y),
                currentNode.TopRightAreaCorner,
                currentNode,
                currentNode.TreeLayerIndex + 1);
        }
        else
        {
            node1 = new RoomNode(currentNode.BottomLeftAreaCorner,
                new Vector2Int(line.Coordinates.x, currentNode.TopRightAreaCorner.y),
                currentNode,
                currentNode.TreeLayerIndex + 1);

            node2 = new RoomNode(new Vector2Int(line.Coordinates.x, currentNode.BottomLeftAreaCorner.y),
                currentNode.TopRightAreaCorner,
                currentNode,
                currentNode.TreeLayerIndex + 1);
        }
        AddNewNodeToCollections(listToReturn, graph, node1);
        AddNewNodeToCollections(listToReturn, graph, node2);

    }

    private void AddNewNodeToCollections(List<RoomNode> listToReturn, Queue<RoomNode> graph, RoomNode node)
    {
        listToReturn.Add(node);
        graph.Enqueue(node);
    }

    private Line GetLineDividingSpace(Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner, int roomwidthMin, int roomLengthMin)
    {
        Orientation orientation;
        bool lengthStatus = (topRightAreaCorner.y - bottomLeftAreaCorner.y) >= 2 * roomLengthMin;
        bool widthStaus = (topRightAreaCorner.x - bottomLeftAreaCorner.x) >= 2 * roomwidthMin;
        if(lengthStatus && widthStaus)
        {
            orientation = (Orientation)(Random.Range(0, 2));
        }
        else if (widthStaus)
        {
            orientation = Orientation.Horiszontal;
        }
        else
        {
            orientation = Orientation.Horiszontal;
        }
        return new Line(orientation, GetCooldinatesFororitention
            (orientation, 
            bottomLeftAreaCorner, 
            topRightAreaCorner, 
            roomwidthMin, 
            roomLengthMin));
       // throw new NotImplementedException();
    }

    private Vector2Int GetCooldinatesFororitention(Orientation orientation, Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner, int roomwidthMin, int roomLengthMin)
    {
        Vector2Int cooldinates = Vector2Int.zero;
        if(orientation == Orientation.Horiszontal)
        {
            cooldinates = new Vector2Int(
                0,
                Random.Range(
                    (bottomLeftAreaCorner.y + roomLengthMin),
                    (topRightAreaCorner.y - roomLengthMin)));
        }
        else
        {
            cooldinates = new Vector2Int(
                 Random.Range(
                    (bottomLeftAreaCorner.y + roomwidthMin),
                    (topRightAreaCorner.y - roomwidthMin)), 0);

        }
        return cooldinates;
    }
}