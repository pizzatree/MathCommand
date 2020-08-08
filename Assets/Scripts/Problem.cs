﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Problem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI overheadText = null;

    [SerializeField]
    private string tempText = " = 1 + 1";

    private char problemChar;
    private bool active;

    public void AssignVariable(char variable)
    {
        problemChar = variable;
        overheadText.text = problemChar + tempText;
    }

    private void Update()
    {
        if(active)
        {
            ; // check answer
        }
    }
}