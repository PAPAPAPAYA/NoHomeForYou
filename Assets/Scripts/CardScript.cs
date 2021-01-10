using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    public string namae;
    public int id;
    public int counter;
    public int power;
    public int rarity;
    public int level;
    public enum CardType { 打击, 姿态, 恢复, 投掷, 神圣, 祝福, 奥术, 斩击, 突刺, 冥思, 铁血, 电能, 温度, 召唤, 自然 };
    public CardType type;
    public CardType trigger;
    [TextArea]
    public string description;

	public float popUpAmount;
	public float rearrangeY;
	public Vector3 ogPos;
	public Vector3 ogScale;

	private bool dragging = false;

	public int index; // set in card manager, based on card's position

	private bool rearranged = false;

	#region not yet implemented
	// usage limitation
	// pool
	// familiarity
	#endregion

	private void Start()
	{
		ogPos = transform.position;
		ogScale = transform.localScale;
	}

	private void Update()
	{
		print(dragging);
		print(CardManager.me.cardPicked);
		//print(index);
		if (transform.position == ogPos &&
			CardManager.me.cardPicked == null &&
			rearranged == false)
		{
			CardManager.me.RearrangeCurrentHand();
			rearranged = true;
		}
	}


	private void OnMouseOver()
	{
		if (GetComponent<SpriteRenderer>().enabled)
		{
			if (dragging && CardManager.me.cardPicked != null)
			{
				
			}
			else if (transform.position.y < ogPos.y + popUpAmount)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y + 10f * Time.deltaTime, transform.position.z);
			}
		}
	}

	private void OnMouseDrag()
	{
		CardManager.me.cardPicked = gameObject;
		rearranged = false;
		Vector3 mousePos_screen = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
		Vector3 mousePos_world = Camera.main.ScreenToWorldPoint(mousePos_screen);
		float yClamp;
		yClamp = Mathf.Max(transform.position.y, transform.position.y + CardManager.me.arrangeThreshold_y);
		
		if (mousePos_world.y <= yClamp + GetComponent<SpriteRenderer>().bounds.size.y / 2)
		{
			transform.position = mousePos_world;
			transform.position = new Vector3(transform.position.x, yClamp, transform.position.z);
			dragging = true;
		}
		else if (mousePos_world.y > yClamp + GetComponent<SpriteRenderer>().bounds.size.y / 2)
		{
			dragging = false;
			CardManager.me.cardPicked = null;
		}
	}

	private void OnMouseUpAsButton()
	{
		if (dragging)
		{
			dragging = false;
		}
		if (CardManager.me.cardPicked == gameObject)
		{
			CardManager.me.cardPicked = null;
			
		}
	}

	private void OnMouseExit()
	{
		if (!dragging && CardManager.me.cardPicked == null)
		{
			StartCoroutine(BackToOg_Y());
			StartCoroutine(BackToOg_X());
		}
	}

	private IEnumerator BackToOg_Y()
	{
		float myY = transform.position.y;
		while (!Mathf.Approximately(transform.position.y, ogPos.y))
		{
			myY = Mathf.Lerp(myY, ogPos.y, 10f * Time.deltaTime);
			transform.position = new Vector3(transform.position.x, myY, transform.position.z);
			if (Mathf.Abs(transform.position.y - ogPos.y) < 0.1f)
			{
				transform.position = ogPos;
			}
			yield return null;
		}
	}

	private IEnumerator BackToOg_X()
	{
		float myX = transform.position.x;
		while (!Mathf.Approximately(transform.position.x, ogPos.x))
		{
			myX = Mathf.Lerp(myX, ogPos.x, 10f * Time.deltaTime);
			transform.position = new Vector3(myX, transform.position.y, transform.position.z);
			if (Mathf.Abs(transform.position.x - ogPos.x) < 0.1f)
			{
				transform.position = new Vector3(ogPos.x, transform.position.y, transform.position.z);
			}
			yield return null;
		}
	}
}