using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
	static public CardManager me;
    public List<GameObject> currentHand; // copy list when character is picked
	public GameObject selectedChara; // set it when copying list
    public GameObject cardPicked; // set when a card is being dragged; reset to null if no card is being dragged
	private CardScript cP_cs;
    public Vector3 card_atTargetPos; // set when the card picked is below certain y 
	public float arrangeThreshold_y;
	public float arrangeThreshold_x;
	public List<Vector3> cardHolders; // stores card slot positions, set when current hand is set
	public List<int> cardHolder_indexes; // stores card holder numbers

	private void Awake()
	{
		me = this;
	}

	private void Update()
	{
		// set cards' indexes
		for (int i = 0; i < currentHand.Count; i++)
		{
			for (int j = 0; j < currentHand.Count; j++)
			{
				if (currentHand[i].transform.position == cardHolders[j])
				{
					currentHand[i].GetComponent<CardScript>().index = j;
				}
			}
		}

		if (cardPicked != null) // if a card is picked up
		{
			cP_cs = cardPicked.GetComponent<CardScript>();

			// set card at target pos
			if (cardPicked.transform.position.y < arrangeThreshold_y)
			{
				foreach(Vector3 slot in cardHolders)
				{
					if (slot.x > cardPicked.transform.position.x - arrangeThreshold_x &&
						slot.x < cardPicked.transform.position.x + arrangeThreshold_x)
					{
						
						card_atTargetPos = slot;
					}
				}
			}

			// move cards
			if (card_atTargetPos != null)
			{
				foreach (GameObject card in currentHand)
				{
					CardScript cs = card.GetComponent<CardScript>();
					if (cs.index > cP_cs.index && // if card is to the right of the card picked
						card.transform.position.x <= card_atTargetPos.x) // and if it is to the left of the target card or is the target card
					{
						// set the card position to the position of the card on its left
						cP_cs.index = cs.index;
						cP_cs.ogPos = cardHolders[cP_cs.index];
						StartCoroutine(MoveCard(card.transform.position, cardHolders[cP_cs.index - 1], card));
					}
					else if (cs.index < cP_cs.index && // if card is to the left of the card picked
							 card.transform.position.x >= card_atTargetPos.x) // and if it is to the right of the target card or is the target card
					{
						// set the card position to the position of the card on its right
						cP_cs.index = cs.index;
						cP_cs.ogPos = cardHolders[cP_cs.index];
						StartCoroutine(MoveCard(card.transform.position, cardHolders[cP_cs.index + 1], card));
					}
				}
			}
		}
		else // if no card is picked up
		{
			
		}
	}

	private IEnumerator MoveCard(Vector3 currentPos, Vector3 targetPos, GameObject card)
	{
		float myX = currentPos.x;
		while (Mathf.Abs(currentPos.x - targetPos.x) > 0.05f)
		{
			myX = Mathf.Lerp(myX, targetPos.x, 10f * Time.deltaTime);
			card.transform.position = new Vector3(myX, targetPos.y, targetPos.z);
			if (Mathf.Abs(card.transform.position.x - targetPos.x) < 0.1f)
			{
				currentPos = targetPos;
				card.transform.position = new Vector3(targetPos.x, targetPos.y, targetPos.z);
				card.GetComponent<CardScript>().ogPos = targetPos;
			}
			yield return null;
		}
	}

	public void RearrangeCurrentHand()
	{
		// rearrange the list
		List<GameObject> tempHand = new List<GameObject>();
		for (int i = 0; i < cardHolders.Count; i++)
		{

			tempHand.Add(currentHand[i]);
		}
		if (tempHand.Count == cardHolders.Count)
		{
			currentHand.Clear();
		}
		for (int i = 0; i < cardHolders.Count; i++)
		{
			foreach (GameObject card in tempHand)
			{
				if (card.GetComponent<CardScript>().index == i)
				{
					currentHand.Add(card);
				}
			}
		}
	}
}
