using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InitialParameters", menuName = "InitialParameters", order = 0)]
public class InitialParameters : ScriptableObject
{
    [Header("Stalker Settings")]
    public StalkerEnemy[] CompleteStalkerEnemyOptionList; //Lista de posibles modelos para el Stalker.

    public int stalkerOnTheDoorCapacity = 6;
    public Vector2 timeBetweenPossiblesAttacks = new Vector2(20, 50);
    public int maxStalkerCount = 14;
    [Range(0, 100)] public float stalkerSpawnProbability = 100;

    [Space(12), Header("Follower Settings")]
    //public FollowerEnemy[] CompleteFollowerOptionList; //Lista de posibles modelos para el Follower.
    public int maxFolllowerCount = 1;

}
