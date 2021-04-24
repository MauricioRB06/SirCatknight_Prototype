using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="nrePlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject // Heredar de ScriptableObject nos permitira crear un Asset a partir de este Script
{
    [Header("Move State")]

    public float movementVelocity = 50f;
}

