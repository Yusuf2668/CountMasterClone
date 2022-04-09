using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData", order = 1)]
public class PlayerType : ScriptableObject
{
    public float runSpeed;

    [Header("Players")]
    public float moveSpeed;
}
