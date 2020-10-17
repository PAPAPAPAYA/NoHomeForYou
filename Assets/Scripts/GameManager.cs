using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	static public GameManager me; // singleton
	public GameObject player; // 玩家
    public List<GameObject> characters; // 角色
	public List<GameObject> enemies; // 敌人角色
	public Vector3 initialPos1;
	public List<int> initialRow;
	public List<int> initialCol;
	public Vector3 enm_initialPos1;
	public int enm_initialRow;
	public int enm_initialCol;

	public int state = 0;
	public int draw = 1;
	public int selectCharacter = 2;
	public int selectGrid = 3;

	public Vector3 destinationPos;

	public List<CardScriptableObject> hand;
	public List<CardScriptableObject> playerHand;
	public List<CardScriptableObject> temp;

	// positioning characters
	public CharacterScript cs;

	// show target
	public GameObject line;

	private void Start()
	{
		me = this;
		characters[0].transform.position = initialPos1;
		characters[0].GetComponent<CharacterScript>().row = initialRow[0];
		characters[0].GetComponent<CharacterScript>().column = initialCol[0];
		enemies[0].transform.position = enm_initialPos1;
		enemies[0].GetComponent<EnemyScript>().row = enm_initialRow;
		enemies[0].GetComponent<EnemyScript>().col = enm_initialCol;
		state = draw;
	}

	private void Update()
	{
		if (characters[0].GetComponent<CharacterScript>().target != null)
		{
			ShowTarget(characters[0].transform.position, characters[0].GetComponent<CharacterScript>().target.transform.position);
		}
		
		if (state == draw)
		{
			if (characters[0].GetComponent<CharacterScript>().hand.Count == 0)
			{
				characters[0].GetComponent<CharacterScript>().hand = DrawCharacterHand(characters[0].GetComponent<CharacterScript>().deck, characters[0]);
			}
			if (player.GetComponent<PlayerScript>().hand.Count == 0)
			{
				player.GetComponent<PlayerScript>().hand = DrawPlayerHand(player.GetComponent<PlayerScript>().deck, player);
			}
			if (characters[0].GetComponent<CharacterScript>().hand.Count == characters[0].GetComponent<CharacterScript>().handSize &&
				player.GetComponent<PlayerScript>().hand.Count == player.GetComponent<PlayerScript>().handSize)
			{
				state = selectCharacter;
			}
		}
		else if (state == selectGrid)
		{
			if (destinationPos != characters[0].transform.position)
			{
				// need to check if the destination is available to move to
				
				characters[0].transform.position = destinationPos;
				state = selectCharacter;
				characters[0].GetComponent<CharacterScript>().hightlightFrame.SetActive(false);
			}
			else if (Input.GetMouseButtonDown(1))
			{
				state = selectCharacter;
				characters[0].GetComponent<CharacterScript>().hightlightFrame.SetActive(false);
			}
		}
	}

	private List<CardScriptableObject> DrawCharacterHand(List<CardScriptableObject> deck, GameObject owner)
	{
		hand.Clear();
		CopyList(deck, temp);
		for (int i = 0; i < owner.GetComponent<CharacterScript>().handSize; i++)
		{
			int rn = UnityEngine.Random.Range(0, temp.Count);
			hand.Add(temp[rn]);
			temp.RemoveAt(rn);
		}
		if (hand.Count == owner.GetComponent<CharacterScript>().handSize)
		{
			return hand;
		}
		else
		{
			return null;
		}
	}

	private List<CardScriptableObject> DrawPlayerHand(List<CardScriptableObject> deck, GameObject owner)
	{
		playerHand.Clear();
		CopyList(deck, temp);
		for (int i = 0; i < owner.GetComponent<PlayerScript>().handSize; i++)
		{
			int rn = UnityEngine.Random.Range(0, temp.Count);
			playerHand.Add(temp[rn]);
			temp.RemoveAt(rn);
		}
		if (playerHand.Count == owner.GetComponent<PlayerScript>().handSize)
		{
			return playerHand;
		}
		else
		{
			return null;
		}
	}

	private void CopyList(List<CardScriptableObject> from, List<CardScriptableObject> to)
	{
		for (int i = 0; i < from.Count; i++)
		{
			to.Add(from[i]);
		}
	}

	void ShowTarget(Vector3 start, Vector3 end)
	{
		//GameObject myLine = Instantiate(line);
		//myLine.GetComponent<LineRenderer>().SetPosition(0, start);
		//myLine.GetComponent<LineRenderer>().SetPosition(1, end);
		line.GetComponent<LineRenderer>().SetPosition(0, start);
		line.GetComponent<LineRenderer>().SetPosition(1, end);
	}
}
