using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
	public GameObject hightlightFrame;

	public List<GameObject> deck;
	public List<GameObject> handPrefabs;
	public List<GameObject> hand;
	public int row;
	public int column;
	public int handSize;
	public GameObject target;

	public bool drawComplete = false;

	public Vector3 destinationPos;

	// show target
	public GameObject linePrefab;
	GameObject myLine;

	

	private void Start()
	{
		handPrefabs = new List<GameObject>();
		destinationPos = transform.position;
		myLine = Instantiate(linePrefab);
	}

	private void OnMouseOver()
	{
		if (GameManager.me.state == GameManager.me.selectCharacter ||
			GameManager.me.state == GameManager.me.arrangeHand)
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (hightlightFrame.activeSelf)
				{
					//hightlightFrame.SetActive(false);
					//GameManager.me.state = GameManager.me.selectCharacter;
					//GetComponent<BoxCollider2D>().enabled = false;
				}
				else
				{
					GameManager.me.cs = gameObject.GetComponent<CharacterScript>();
					hightlightFrame.SetActive(true); // highlight character
													 //destinationPos = transform.position;
					
					// set card manager's [current hand]
					for (int i = 0; i < handSize; i++)
					{
						CardManager.me.currentHand.Add(hand[i]);
						CardManager.me.cardHolders.Add(hand[i].transform.position);
					}

					// set card manager's [selected chara]
					CardManager.me.selectedChara = gameObject;

					// show hand
					foreach (GameObject card in hand)
					{
						card.GetComponent<SpriteRenderer>().enabled = true;
					}

					// change state
					GameManager.me.state = GameManager.me.arrangeHand;
					//GetComponent<BoxCollider2D>().enabled = true;
				}
			}
		}
	}

	private void Update()
	{
		if (target != null)
		{
			GameManager.me.ShowTarget(myLine,transform.position, target.transform.position);
		}

		if (GameManager.me.state == GameManager.me.draw)
		{
			if (handPrefabs.Count == 0)
			{
				handPrefabs = GameManager.me.DrawCharacterHand(deck, gameObject);
			}
			else if (handPrefabs.Count == handSize)
			{
				drawComplete = true;
			}
		}
		else if (GameManager.me.state == GameManager.me.arrangeHand)
		{
			if (destinationPos != transform.position)
			{
				// need to check if the destination is available to move to

				transform.position = destinationPos;
				GameManager.me.state = GameManager.me.selectCharacter;
				hightlightFrame.SetActive(false);
			}
			//else if (Input.GetMouseButtonDown(1))
			//{
			//	GameManager.me.state = GameManager.me.selectCharacter;
			//	hightlightFrame.SetActive(false);
			//}
		}
	}
}