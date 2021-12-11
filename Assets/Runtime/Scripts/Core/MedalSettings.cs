using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Medal
{
    [field: SerializeField]
    public Sprite MedalSprite { get; private set; }
    [field: SerializeField]
    public int MinScore { get; private set; }
}

[CreateAssetMenu(menuName = "Data/MedalSettings")]
public class MedalSettings : ScriptableObject
{
    [field: SerializeField]
    public Medal[] Medals { get; private set; }
}
