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
	public List<Vector3> initialPoses;
	public List<int> initialRow;
	public List<int> initialCol;
	public Vector3 enm_initialPos1;
	public int enm_initialRow;
	public int enm_initialCol;

	public int state = 0;
	public int draw = 1;
	public int selectCharacter = 2;
	public int selectGrid = 3;

	public List<CardScriptableObject> hand;
	public List<CardScriptableObject> playerHand;
	public List<CardScriptableObject> temp;

	// positioning characters
	public CharacterScript cs;

	// show target
	public GameObject line;

	// initialize characters
	public List<GameObject> p_charaPrefabs;

	private void Start()
	{
		me = this;
		for (int i = 0; i < p_charaPrefabs.Count; i++)
		{
			GameObject thisChara = Instantiate(p_charaPrefabs[i]);
			thisChara.transform.position = initialPoses[i];
			thisChara.GetComponent<CharacterScript>().row = initialRow[i];
			thisChara.GetComponent<CharacterScript>().column = initialCol[i];
			characters.Add(thisChara);
		}
		
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
			//if (characters[0].GetComponent<CharacterScript>().hand.Count == 0)
			//{
			//	characters[0].GetComponent<CharacterScript>().hand = DrawCharacterHand(characters[0].GetComponent<CharacterScript>().deck, characters[0]);
			//}
			if (player.GetComponent<PlayerScript>().hand.Count == 0)
			{
				player.GetComponent<PlayerScript>().hand = DrawPlayerHand(player.GetComponent<PlayerScript>().deck, player);
			}
			if (player.GetComponent<PlayerScript>().hand.Count == player.GetComponent<PlayerScript>().handSize)
			{
				int drawCompleteNum = 0;
				for (int i = 0; i < characters.Count; i++)
				{
					if (characters[i].GetComponent<CharacterScript>().drawComplete)
					{
						drawCompleteNum++;
					}
				}
				if (drawCompleteNum == characters.Count)
				{
					state = selectCharacter;
				}
			}
		}
		else if (state == selectGrid)
		{
			//if (destinationPos != cs.transform.position)
			//{
			//	// need to check if the destination is available to move to

				//	characters[0].transform.position = destinationPos;
				//	state = selectCharacter;
				//	characters[0].GetComponent<CharacterScript>().hightlightFrame.SetActive(false);
				//}
			if (Input.GetMouseButtonDown(1))
			{
				state = selectCharacter;
				for (int i = 0; i < characters.Count; i++)
				{
					characters[i].GetComponent<CharacterScript>().hightlightFrame.SetActive(false);
				}
				
			}
		}
	}

	public List<CardScriptableObject> DrawCharacterHand(List<CardScriptableObject> deck, GameObject owner)
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
			temp.Clear();
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
			temp.Clear();
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

	public void ShowTarget(Vector3 start, Vector3 end)
	{
		//GameObject myLine = Instantiate(line);
		//myLine.GetComponent<LineRenderer>().SetPosition(0, start);
		//myLine.GetComponent<LineRenderer>().SetPosition(1, end);
		line.GetComponent<LineRenderer>().SetPosition(0, start);
		line.GetComponent<LineRenderer>().SetPosition(1, end);
	}
}
