using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerMovementParameters")]
public class PlayerMovementParameters : ScriptableObject
{
    [field: SerializeField]
    public float PlayerSpeed { get; private set; } = 2.5f;

    [field: SerializeField]
    public float FlapForce { get; private set; } = 10;

    [field: SerializeField]
    public float Gravity { get; private set; } = 40;

    [field: SerializeField]
    public float FlapAngleDegress { get; private set; } = 30;

    [field: SerializeField]
    public float RotateDownSpeed { get; private set; } = 200;

}
   


