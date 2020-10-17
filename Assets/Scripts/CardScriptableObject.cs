using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardScriptableObject : ScriptableObject
{
    public string namae;
    public int counter;
    public int power;
    public Sprite sprite;
    public int rarity;
    public int level;
    public enum CardType {打击, 姿态,恢复,投掷,神圣, 祝福, 奥术, 斩击, 突刺, 冥思, 铁血, 电能, 温度, 召唤, 自然 };
    public CardType type;
    public CardType trigger;
    [TextArea]
    public string description;

	#region not yet implemented
	// usage limitation
	// pool
	// familiarity
	#endregion
}
