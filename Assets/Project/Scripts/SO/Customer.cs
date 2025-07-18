using System;
using System.Collections.Generic;
using UnityEngine;
using RatSpeak;

[CreateAssetMenu(fileName = "CFG_Customer_0", menuName = "Scriptable Objects/Customer")]
public class Customer : ScriptableObject
{
    public Character character;
    public float timeToAppear;
    public Ingredient ingredient;
    public string feelingKey;
    public List<DialogKey> dialogs;
}

[Serializable]
public class DialogKey
{
    public string key;
    public DialogID id;
}