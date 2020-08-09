﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class Problem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI equationTF = null, 
                            variableTF = null,
                            inputTF    = null;

    [SerializeField]
    private SpriteRenderer targetGraphic;

    private iQuestion question;
    private char variable;

    private bool listening = false, acceptingLetters = false;
    private StringBuilder inputString = new StringBuilder();

    private void Update()
    {
        if(listening)
            ReadInput();
    }

    private void ReadInput()
    {
        if(Input.GetKeyDown(KeyCode.Backspace) && inputString.Length > 0)
        {
            inputString.Length = inputString.Length - 1;
            UpdateInputText();
        }

        if(acceptingLetters)
            for(char letter = 'a'; letter <= 'z'; ++letter)
            {
                if(Input.GetKeyDown(letter.ToString()))
                {
                    inputString.Append(letter);
                    UpdateInputText();
                }
            }

        for(char letter = '0'; letter <= '9'; ++letter)
        {
            if(Input.GetKeyDown(letter.ToString()))
            {
                inputString.Append(letter);
                UpdateInputText();
            }
        }
    }

    private void UpdateInputText()
        => inputTF.text = inputString.ToString();

    public void AssignProperties(char variable, iQuestion question, int difficulty)
    {
        this.question = question;
        this.variable = variable;
        variableTF.text = variable + " =";
        equationTF.text = question.Question(difficulty);

        var eqTextSize = new Vector2(equationTF.text.Length, equationTF.rectTransform.rect.y);
        equationTF.rectTransform.sizeDelta = eqTextSize;

        SanitizeInput(question);
    }

    private void SanitizeInput(iQuestion question)
    {
        switch(question)
        {
            case Add a:
            case Subtract b:
            case Multiply c:
            case Divide d:
                acceptingLetters = false;
                break;
            default:
                break;
        }
    }

    public void Answer()
    {
        bool accepted = question.Answer(inputTF.text);

        if(accepted)
        {
            SendMessageUpwards("FreeFromDictionary", variable, SendMessageOptions.RequireReceiver);
            Destroy(gameObject);
        }
    }
    public void Select()
    {
        listening = true;
        targetGraphic.enabled = true;
    }

    public void Deselect()
    {
        listening = false;
        Answer();
        inputString.Clear();
        inputTF.text = "";
        targetGraphic.enabled = false;
    }
}
