using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    Node[,] grid;
    public GameObject nodeObjectPrefab;
    public GameObject PathObject;
    public GameObject RedFlag;
    public GameObject GreenFlag;

    float xSize = 1f;
    float ySize = 1f;
    float xStart = -5.67f;
    float yStart = 3f;

    public void CreatePath(int length){
        grid =  new Node[8,8];
        for(int i = 0; i < 8; i++){
            for(int j = 0; j < 8; j++){
                grid[j,i] = new Node(new GridPosition(i, j));
            }
        }

        List<Node> path = CreateRoute(length);
        CreateNodesInPathAsObjects(path);
    }
    void CreateNodesInPathAsObjects(List<Node> path){

        foreach(Node n in path){
            Vector3 flagPosition = new Vector3(xStart + xSize*n.position.x, yStart - ySize*n.position.y, 0f);
            GameObject newFlag = Instantiate(nodeObjectPrefab, flagPosition, Quaternion.identity);
            newFlag.transform.parent = PathObject.transform;
        }
        Node first = path[0];
        Node last = path[path.Count-1];

        Vector3 GreenFlagPosition = new Vector3(xStart + xSize*first.position.x, yStart - ySize*first.position.y, 0f);
        Instantiate(GreenFlag, GreenFlagPosition, Quaternion.identity);

        Vector3 RedFlagPosition = new Vector3(xStart + xSize*last.position.x, yStart - ySize*last.position.y, 0f);
        Instantiate(RedFlag, RedFlagPosition, Quaternion.identity);
    }

    GridPosition CreateEndPoint(){
        int x = Random.Range(0,7);
        return new GridPosition(x,7);
    }

    GridPosition CreateStartPoint(){
        int x = 0;
        int y = Random.Range(0,7);
        return new GridPosition(y,x);
    }

    List<Node> CreateRoute(int length){
        GridPosition startPosition = CreateStartPoint();
        GridPosition targetPosition = CreateEndPoint();
        Node currentNode = grid[startPosition.y, startPosition.x];
        
        int currentLength = 0;

        List<Node> path = new List<Node>();
        path.Add(currentNode);

        bool NotAtEnd = true;
        string LastDirection = "";

        int catchLoops = 0;

        while(catchLoops < 100){
            catchLoops += 1;

            if(length == currentLength)
                break;
            if(length - currentLength > DistanceFromPosition(currentNode, targetPosition)){
                LastDirection = GetDirection(currentNode, LastDirection);
                Node newNode = GetNewNode(currentNode, LastDirection);
                path.Add(newNode);
                currentLength += 1;
                currentNode = newNode;
            }else{
                Node newNode = GetNewNodeTowardsPoint(currentNode, targetPosition);
                path.Add(newNode);
                currentLength += 1;
                currentNode = newNode;
            }
        }

        return path;
        
    }
    int DistanceFromPosition(Node currentNode, GridPosition position){
        int xDiff = Mathf.Abs(position.x - currentNode.position.x);
        int yDiff = Mathf.Abs(position.y - currentNode.position.y);
        return xDiff + yDiff;
    }

    string GetDirection(Node n, string lastDirection){
        int i = 0;
        while(i < 100){
            i++;
            int randomInt = Random.Range(0,4);
            switch(randomInt){
                case 0:
                    if(lastDirection != "right" && n.position.x > 0)
                        return "left";
                    break;
                case 1:
                    if(lastDirection != "left" &&  n.position.x < 7)
                        return "right";
                    break;
                case 2:
                    if(lastDirection != "up" && n.position.y < 7) 
                        return "down";
                    break;
                case 3:
                    if(lastDirection != "down" && n.position.y > 0)
                        return "up";
                    break;
            }
        }
        Debug.Log("Get direction exceded loop limit");
        return "";
    }

    Node GetNewNode(Node n, string direction){
        switch(direction){
            case "left":
                return grid[n.position.y, n.position.x-1];
            case "right":
                return grid[n.position.y, n.position.x+1];
            case "up":
                return grid[n.position.y-1, n.position.x];
            case "down":
                return grid[n.position.y+1, n.position.x];
        }
        Debug.Log("Direction invalid");
        return n;
    }

    Node GetNewNodeTowardsPoint(Node n, GridPosition position){
        int xDiff = position.x - n.position.x;
        int yDiff = position.y - n.position.y;
        if(position.y != n.position.y){
            if(position.y > n.position.y){
                return grid[n.position.y+1, n.position.x];
            }else{
                return grid[n.position.y-1, n.position.x];
            }
        }
        if(position.x != n.position.x){
            if(position.x > n.position.x){
                return grid[n.position.y, n.position.x+1];
            }else{
                return grid[n.position.y, n.position.x-1];
            }
        }
        return n;
    }
}

class Node{
    public GridPosition position;

    public Node(GridPosition pos){
        position = pos;
    }
}

class GridPosition{
    public int x;
    public int y;
    public GridPosition(int x, int y){
        this.x = x;
        this.y = y;
    }
}
