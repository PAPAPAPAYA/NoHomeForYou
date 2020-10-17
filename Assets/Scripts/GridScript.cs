using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
	public int row;
	public int column;

	private void Update()
	{
		if (GameManager.me.state == GameManager.me.selectGrid)
		{
			for (int i = 0; i < GameManager.me.enemies.Count; i++)
			{
				if (row == GameManager.me.enemies[i].GetComponent<EnemyScript>().row &&
					column == GameManager.me.enemies[i].GetComponent<EnemyScript>().col)
				{
					GetComponent<BoxCollider2D>().enabled = false;
				}
				else
				{
					GetComponent<BoxCollider2D>().enabled = true;
				}
			}
		}
		else
		{
			GetComponent<BoxCollider2D>().enabled = false;
		}
	}

	private void OnMouseOver()
	{
		if (GameManager.me.state == GameManager.me.selectGrid)
		{
			if (Input.GetMouseButtonDown(0))
			{
				//print(GameManager.me.state);
				print(row);
				print(column);
				print(GameManager.me.cs.row);
				print(GameManager.me.cs.column);
				// check if this grid is next to the character
				if (row >= GameManager.me.cs.row - 1 &&
					row <= GameManager.me.cs.row + 1 &&
					column >= GameManager.me.cs.column - 1 &&
					column <= GameManager.me.cs.column + 1)
				{
					GameManager.me.destinationPos = transform.position;
					GameManager.me.cs.row = row;
					GameManager.me.cs.column = column;
					GameManager.me.cs = null;
				}
			}
		}
	}
}