using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceMaker : MonoBehaviour
{
	static public InterfaceMaker me;
    public GameObject gridPrefab;
	public int width;
	public int length;
	public GameObject[,] grids;
	public Vector3 startPos;
	public float x_interval;
	public float y_interval;

	private void Start()
	{
		me = this;
		grids = new GameObject[length, width];
		for (int i = 0; i < length; i++)
		{
			for (int j = 0; j < width; j++)
			{
				grids[i,j] = Instantiate(gridPrefab);
				grids[i, j].GetComponent<GridScript>().row = i;
				grids[i, j].GetComponent<GridScript>().column = j;
				grids[i, j].transform.position = new Vector3(startPos.x + x_interval * i, startPos.y + y_interval * j, 0);
			}
		}
	}
}
