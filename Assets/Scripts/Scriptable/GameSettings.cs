using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Settings/Difficulty Settings", fileName = "Game Settings X Difficulty", order = 0)]
public class GameSettings : ScriptableObject
{
    [Header("Game Settings")]


    [SerializeField] public float StalkerAttackMultiplier = 1;
    [SerializeField] public Vector2 AttackTimeCountRange = new Vector2(10, 25);



    [SerializeField] public GonzalezManager GonzalezPrefab;




}
