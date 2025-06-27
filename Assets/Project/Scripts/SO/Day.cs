using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CFG_Day_0", menuName = "Scriptable Objects/Day")]
public class Day : ScriptableObject
{
    public string day;
    public float start;
    public float end;
    public float delayBeforeNextDay;

    public List<Customer> customers;
}
