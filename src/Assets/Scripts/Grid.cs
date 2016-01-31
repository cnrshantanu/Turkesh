using UnityEngine;
using System.Collections;

public class Node
{
	
	public bool walkable;
	public Vector3 worldPosition;
	
	public Node(bool _walkable, Vector3 _worldPos)
	{
		walkable = _walkable;
		worldPosition = _worldPos;
	}
}

public class Grid : MonoBehaviour
{
    public GameObject player;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
       // Debug.Log("X"+ gridWorldSize.x);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
		Vector3 worldBottomLeft = (transform.position - (new Vector3(nodeRadius,0,0))- ( new Vector3(0,0,nodeRadius)));

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint);
            }
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    public Vector2 PlayerNodeValue()
    {
        Node playerNode = NodeFromWorldPoint(player.transform.position);
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        //Debug.Log(" True Player pos: " + new Vector2((Mathf.Abs(worldBottomLeft.x - Mathf.RoundToInt(player.transform.position.x / nodeDiameter))), Mathf.Abs(worldBottomLeft.z - Mathf.RoundToInt(player.transform.position.z / nodeDiameter))));

//        foreach (Node n in grid)
//        {
//            if (playerNode == n)
//            {
//                //return new Vector2(Mathf.RoundToInt(player.transform.position.x/nodeDiameter), Mathf.RoundToInt(player.transform.position.y / nodeDiameter));
//				Debug.Log ("True player pos:" + new Vector2((Mathf.Abs(transform.position.x - Mathf.RoundToInt(player.transform.position.x / nodeDiameter))), Mathf.Abs(transform.position.z - Mathf.RoundToInt(player.transform.position.z / nodeDiameter))));
//				//return new Vector2((Mathf.Abs(transform.position.x - Mathf.RoundToInt(player.transform.position.x / nodeDiameter))), Mathf.Abs(transform.position.z - Mathf.RoundToInt(player.transform.position.z / nodeDiameter)));
//
//            }
//
//        }
		//Debug.Log ("True player pos:" + new Vector2((Mathf.Abs(transform.position.x - Mathf.RoundToInt(player.transform.position.x / nodeDiameter))), Mathf.Abs(transform.position.z - Mathf.RoundToInt(player.transform.position.z / nodeDiameter))));
		return new Vector2((Mathf.Abs(transform.position.x - Mathf.RoundToInt(player.transform.position.x / nodeDiameter))), Mathf.Abs(transform.position.z - Mathf.RoundToInt(player.transform.position.z / nodeDiameter)));
    }

//    void OnDrawGizmos()
//    {
//        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
//
//        if (grid != null)
//        {
//            Node playerNode = NodeFromWorldPoint(player.transform.position);
//            //playerNode.
//            foreach (Node n in grid)
//            {
//                Gizmos.color = (n.walkable) ? Color.white : Color.red;
//                if (playerNode == n)
//                {
//                    Gizmos.color = Color.green;
//                    // Debug.Log("player position" + 
//                    PlayerNodeValue();//);
//				}
//                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
//            }
//        }
//    }
    void Update()
    {
        //Debug.Log("Player position");
    }
}
    
