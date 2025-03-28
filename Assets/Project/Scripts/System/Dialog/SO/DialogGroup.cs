using System.Collections.Generic;
using RatSpeak;
using UnityEngine;

[CreateAssetMenu(fileName = "CFG_0_DialogGroup_St00_Gr00", menuName = "Scriptable Objects/Dialogs/DialogGroup")]
public class DialogGroup : ScriptableObject
{
    public List<Dialog> dialogs = new List<Dialog>();
}