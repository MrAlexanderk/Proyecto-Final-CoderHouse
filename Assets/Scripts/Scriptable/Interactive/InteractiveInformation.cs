using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "UI Interactive Information", fileName = "Information", order = 0)]
public class InteractiveInformation : ScriptableObject
{
    [SerializeField] Image normalCursor;
    [SerializeField] Image interactiveCursor;


}
