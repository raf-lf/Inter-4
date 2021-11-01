using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue System/Dialogue")]
public class Dialogue : ScriptableObject
{
    [TextArea(3, 5)]
    public string[] lines = new string[0];
    public expressions[] linesExpression = new expressions[0];

}
