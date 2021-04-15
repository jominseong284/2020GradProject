using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bounty
{
    public int attack;
    public int getGold;
    public int getRuby;
    public int score;
    public Sprite sprite;
}
[CreateAssetMenu(fileName = "BountySO", menuName = "Scriptable Object/BountySO")]
public class BountySO : ScriptableObject
{
    public Bounty[] items;
}

