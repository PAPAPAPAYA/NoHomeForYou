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

	public bool drawComplete = false;

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
		if (target != null)
		{
			GameManager.me.ShowTarget(transform.position, target.transform.position);
		}

		if (GameManager.me.state == GameManager.me.draw)
		{
			if (hand.Count == 0)
			{
				hand = GameManager.me.DrawCharacterHand(deck, gameObject);
			}
		}
		else if (GameManager.me.state == GameManager.me.selectGrid)
		{
			if (GameManager.me.destinationPos != transform.position)
			{
				// need to check if the destination is available to move to

				transform.position = GameManager.me.destinationPos;
				GameManager.me.state = GameManager.me.selectCharacter;
				hightlightFrame.SetActive(false);
			}
			else if (Input.GetMouseButtonDown(1))
			{
				GameManager.me.state = GameManager.me.selectCharacter;
				hightlightFrame.SetActive(false);
			}
		}
	}
}