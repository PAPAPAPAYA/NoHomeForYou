using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	public List<CardScriptableObject> deck;
	public List<CardScriptableObject> hand;
	public int handSize;
	public GameObject cardHolder;
	public bool allShown = false;
	public Vector3 cardHolder_initPos;
	public float cardHolder_interval;

	private void Update()
	{
		//for (int i = 0; i < hand.Count; i++)
		//{
		//	if (!allShown)
		//	{
		//		GameObject thisCardHolder = Instantiate(cardHolder);
		//		thisCardHolder.GetComponent<SpriteRenderer>().sprite = hand[i].sprite;
		//		thisCardHolder.transform.position = new Vector3(cardHolder_initPos.x + cardHolder_interval * i,
		//														cardHolder_initPos.y,
		//														cardHolder_initPos.z);
		//	}
		//	if (i== hand.Count - 1)
		//	{
		//		allShown = true;
		//	}
		//}
	}
}
