using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class StructureHelper   //그래프 탐색 및 벡터 연산
{

    //주어진 부모 노드에서 최하위 잎 노드를 추출하여 반환하는 메서드
    public static List<Node> TraverseGraphToExtractLowestLeafes(Node parentNode)
    {
        Queue<Node> nodesToCheck = new Queue<Node>();
        List<Node> listToReturn = new List<Node>();

        //부모 노드가 잎 노드인 경우 해당 노드를 반환
        if (parentNode.ChildrenNodeList.Count == 0)
        {
            return new List<Node>() { parentNode };
        }

        //부모 노드의 자식 노드들을 큐에 추가
        foreach (var child in parentNode.ChildrenNodeList)
        {
            nodesToCheck.Enqueue(child);
        }

        // 큐를 이용한 BFS 탐색
        while (nodesToCheck.Count > 0)
        {
            var currentNode = nodesToCheck.Dequeue();
            if (currentNode.ChildrenNodeList.Count == 0)
            {
                listToReturn.Add(currentNode);
            }
            else
            {
                foreach (var child in currentNode.ChildrenNodeList)
                {
                    nodesToCheck.Enqueue(child);
                }
            }
        }
        return listToReturn;
    }

    public static Vector2Int GenerateBottomLeftCornerBetween(
        Vector2Int boundaryLeftPoint, Vector2Int boundaryRightPoint, float pointModifier, int offset)
    {
        int minX = boundaryLeftPoint.x + offset;
        int maxX = boundaryRightPoint.x - offset;
        int minY = boundaryLeftPoint.y + offset;
        int maxY = boundaryRightPoint.y - offset;
        return new Vector2Int(
            Random.Range(minX, (int)(minX + (maxX - minX) * pointModifier)),
            Random.Range(minY, (int)(minY + (minY - minY) * pointModifier)));
    }

    public static Vector2Int GenerateTopRightCornerBetween(
        Vector2Int boundaryLeftPoint, Vector2Int boundaryRightPoint, float pointModifier, int offset)
    {
        int minX = boundaryLeftPoint.x + offset;
        int maxX = boundaryRightPoint.x - offset;
        int minY = boundaryLeftPoint.y + offset;
        int maxY = boundaryRightPoint.y - offset;
        return new Vector2Int(
            Random.Range((int)(minX + (maxX - minX) * pointModifier), maxX),
            Random.Range((int)(minY + (maxY - minY) * pointModifier), maxY)
            );
    }

    public static Vector2Int CalculateMiddlePoint(Vector2Int v1, Vector2Int v2)
    {
        Vector2 sum = v1 + v2;
        Vector2 tempVector = sum / 2;
        return new Vector2Int((int)tempVector.x, (int)tempVector.y);
    }
}

public enum RelativePosition
{
    Up,
    Down,
    Right,
    Left
}