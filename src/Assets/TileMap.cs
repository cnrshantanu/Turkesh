using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TileMap : MonoBehaviour {

	public GameObject selectedUnit;

    public TileType[] tileTypes;


    int[,] tiles;
	Node[,] graph;


	public int mapSizeX = 10;
	public int mapSizeY = 10;

	void Start() {
		selectedUnit.GetComponent<Unit>().tileX = (int)selectedUnit.transform.position.x;
     	selectedUnit.GetComponent<Unit>().tileY = (int)selectedUnit.transform.position.z;   tiles = new int[mapSizeX, mapSizeY];
     	selectedUnit.GetComponent<Unit>().map = this;   GenerateMapAndShit();
		GenerateShortestPathGraph();
		GenerateGridAndShit();
       
	}

	void Update(){
	
	}

	void GenerateMapAndShit() {
		int x,y;
		
		for(x=0; x < mapSizeX; x++) {
			for(y=0; y < mapSizeX; y++) {
				tiles[x,y] = 0;
			}
		}

        tiles[1, 2] = 1;
        tiles[1, 3] = 1;
        tiles[1, 4] = 1;
        tiles[1, 5] = 1;
        tiles[1, 6] = 1;

	}

	public class Node {
		public List<Node> neighbours;
		public int x;
		public int y;

		public Node() {
			neighbours = new List<Node>();
		}

		public float DistanceTo(Node n) {
			if(n == null) {
				Debug.LogError("WTF?");
			}

			return Vector2.Distance(
					new Vector2(x, y),
					new Vector2(n.x, n.y)
				);
		}
	}


	void GenerateShortestPathGraph() {
		
		graph = new Node[mapSizeX,mapSizeY];

		for(int x=0; x < mapSizeX; x++) {
			for(int y=0; y < mapSizeX; y++) {
				graph[x,y] = new Node();
				graph[x,y].x = x;
				graph[x,y].y = y;
			}
		}


		
		for(int x=0; x < mapSizeX; x++) {
			for(int y=0; y < mapSizeX; y++) {

                if (x > 0)
                {
                    graph[x, y].neighbours.Add(graph[x - 1, y]);
                    if (y > 0)
                        graph[x, y].neighbours.Add(graph[x - 1, y - 1]);
                    if (y < mapSizeY - 1)
                        graph[x, y].neighbours.Add(graph[x - 1, y + 1]);
                }
                if (x < mapSizeX - 1)
                {
                    graph[x, y].neighbours.Add(graph[x + 1, y]);
                    if (y > 0)
                        graph[x, y].neighbours.Add(graph[x + 1, y - 1]);
                    if (y < mapSizeY - 1)
                        graph[x, y].neighbours.Add(graph[x + 1, y + 1]);
                }
				if(y > 0)
					graph[x,y].neighbours.Add( graph[x, y-1] );
				if(y < mapSizeY-1)
					graph[x,y].neighbours.Add( graph[x, y+1] );
			}
		}
	}

    public bool UnitCanEnterTile(int x, int y)
    {

        return tileTypes[tiles[x, y]].isWalkable;
    }


    public float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY)
    {

        TileType tt = tileTypes[tiles[targetX, targetY]];

        if (UnitCanEnterTile(targetX, targetY) == false)
            return Mathf.Infinity;

        float cost = tt.movementCost;

        if (sourceX != targetX && sourceY != targetY)
        {
            cost += 0.001f;
        }

        return cost;

    }

    void GenerateGridAndShit() {
		for(int x=0; x < mapSizeX; x++) {
			for(int y=0; y < mapSizeX; y++) {
                TileType tt = tileTypes[tiles[x, y]];
                GameObject go = (GameObject)Instantiate(tt.tilePrefabs, new Vector3(x,0,y), Quaternion.identity);
            
                
			}
		}
	}

    public Vector3 TileCoordToWorldCoord(int x, int y) {
		return new Vector3(x, y, 0);
	}

	public void GeneratePathTo(int a, int b) {

        selectedUnit.GetComponent<Unit>().tileX = (int)selectedUnit.transform.position.x;
        selectedUnit.GetComponent<Unit>().tileY = (int)selectedUnit.transform.position.z;
        selectedUnit.GetComponent<Unit>().currentPath = null;

//		Debug.Log (selectedUnit.GetComponent<Unit>().tileX  + " tile" + selectedUnit.GetComponent<Unit>().tileY );
//		Debug.Log ((int)selectedUnit.transform.position.x + " " +  (int)selectedUnit.transform.position.y);

		Dictionary<Node, float> dist = new Dictionary<Node, float>();
		Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

	
		List<Node> unvisited = new List<Node>();
		
		Node source = graph[
		                    selectedUnit.GetComponent<Unit>().tileX, 
		                    selectedUnit.GetComponent<Unit>().tileY
		                    ];
		
		Node target = graph[
		                    a, 
		                    b
		                    ];

		//Debug.Log (x+ "fsdf" +y);
        selectedUnit.GetComponent<Unit>().SendTargetCoordinates(target);
		
		dist[source] = 0;
		prev[source] = null;

		
		foreach(Node v in graph) {
			if(v != source) {
				dist[v] = Mathf.Infinity;
				prev[v] = null;
			}

			unvisited.Add(v);
		}

		while(unvisited.Count > 0) {
			
			Node u = null;

			foreach(Node possibleU in unvisited) {
				if(u == null || dist[possibleU] < dist[u]) {
					u = possibleU;
				}
			}

			if(u == target) {
				break;	
			}

			unvisited.Remove(u);

			foreach(Node v in u.neighbours) {
                float alt = dist[u] + CostToEnterTile(u.x, u.y, v.x, v.y) ;
				if( alt < dist[v] ) {
					dist[v] = alt;
					prev[v] = u;
				}
			}
		}


		if(prev[target] == null) {
			return;
		}

		List<Node> currentPath = new List<Node>();

		Node curr = target;

		
		while(curr != null) {
			currentPath.Add(curr);
			curr = prev[curr];
		}

		

		currentPath.Reverse();

        foreach(Node v in currentPath)
        {
           // Debug.Log(v.x + " " + v.y);
        }

		selectedUnit.GetComponent<Unit>().UpdateCurrentPath(currentPath);
	}

}
