using UnityEngine;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

	public int tileX;
	public int tileY;
    public TileMap map;

    int target_x, target_y;

    Vector3 target;

	public List<TileMap.Node> currentPath = null;
    void Update()
    {
		Debug.Log (" count : " + count);
		if (currentPath != null)
        {
            int currNode = 0;
            Vector3 start = transform.position;
            Vector3 end = transform.position;
            while (currNode < currentPath.Count - 1)
            {
                start = map.TileCoordToWorldCoord(currentPath[currNode].x, currentPath[currNode].y)+ new Vector3(0,0,-1.1f);
                end = map.TileCoordToWorldCoord(currentPath[currNode + 1].x, currentPath[currNode + 1].y)+ new Vector3(0,0,-1.1f);
               // transform.position = Vector3.MoveTowards(transform.position, end, Time.deltaTime);
				Vector3 temp = new Vector3(end.x, 3 , end.y);
                //Debug.Log("End " + end + "transform" + transform.position);
                Debug.DrawLine(new Vector3(start.x,3,start.y), temp, Color.red);
                currNode++;
            }
         //   transform.position = Vector3.MoveTowards(transform.position, end, Time.deltaTime);

           
       }
    }

    public void UpdateCurrentPath(List<TileMap.Node> _list)
    {
        currentPath = _list;
        StartCoroutine(moveToTarget());
    }
	private int count = 0;
    IEnumerator<int> moveToTarget()
    {
        int currNode = 0;
		count ++;
        Vector3 start = transform.position;
        Vector3 end = transform.position;
		Debug.Log ("current path" +currentPath.Count);
		if (currentPath.Count != null) {
			while (currNode < currentPath.Count - 1) {
				start = map.TileCoordToWorldCoord (currentPath [currNode].x, currentPath [currNode].y);
				end = map.TileCoordToWorldCoord (currentPath [currNode + 1].x, currentPath [currNode + 1].y);
				Vector3 temp = new Vector3 (end.x, transform.position.y, end.y);
				transform.position = Vector3.MoveTowards (transform.position, temp, Time.deltaTime * 0.2f);
				Debug.Log("End " + end + "transform" + transform.position);
				if (Vector3.Distance (transform.position, end) < 0.5f)
					currNode++;
				yield return 0;
			}
		}
		count --;
        yield return 0;
    }

    public void SendTargetCoordinates(TileMap.Node q)
    {
        target_x = q.x;
        target_y = q.y;
        target = new Vector3(q.x, q.y);
    }

}
