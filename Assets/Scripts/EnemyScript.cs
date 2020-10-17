using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
	public int row;
	public int col;

	private void OnMouseOver()
	{
		if (GameManager.me.state == GameManager.me.selectGrid)
		{
			if (Input.GetMouseButtonDown(0))
			{
				GameManager.me.cs.target = gameObject;
			}
		}
	}
}
