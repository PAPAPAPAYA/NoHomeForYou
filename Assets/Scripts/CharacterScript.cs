using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
	public GameObject hightlightFrame;

	public List<CardScriptableObject> deck;
	public List<CardScriptableObject> hand;
	public int row;
	public int column;
	public int handSize;
	public GameObject target;

	private void Start()
	{
		hand = new List<CardScriptableObject>();
	}

	private void OnMouseOver()
	{
		if (GameManager.me.state == GameManager.me.selectCharacter ||
			GameManager.me.state == GameManager.me.selectGrid)
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (hightlightFrame.activeSelf)
				{
					hightlightFrame.SetActive(false);
					GameManager.me.state = GameManager.me.selectCharacter;
					GetComponent<BoxCollider2D>().enabled = false;
				}
				else
				{
					GameManager.me.cs = gameObject.GetComponent<CharacterScript>();
					hightlightFrame.SetActive(true);
					GameManager.me.destinationPos = transform.position;
					GameManager.me.state = GameManager.me.selectGrid;
					GetComponent<BoxCollider2D>().enabled = true;
				}
			}
		}
	}

	private void Update()
	{
		
	}
}