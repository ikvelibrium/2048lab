using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Tile : MonoBehaviour
{
    public int posX;
    public int posY;

    public int Value;

    public int Points => Value == 0 ? 0 : (int)Mathf.Pow(2, Value);

    public bool IsEmpty => Value == 0;
    public bool HasMerged;

    public int MaxValue = 11;


    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text text;

    // view controller
    public void SetValue(int x, int y, int value)
    {
        
        posX = x; 
        posY = y; 
        Value = value;

        UpdateCell();
    }
    public void IncreaseValue()
    {
        Value++;
        HasMerged = true;

        UpdateCell();
    }
     
    public void ResetMerege()
    {
        HasMerged = false;
    }

    public void Merge(Tile otherTile)
    {
        otherTile.IncreaseValue();
        SetValue(posX, posY, 0);

        UpdateCell();
    }

    public void MoveToTile(Tile target)
    {
        target.SetValue(target.posX, target.posY, Value);
        SetValue(posX, posY, 0);

        UpdateCell();
    }
    public void UpdateCell()
    {
        if (text.text == null)
        {
            text.text = " ";
        } else
        {
            text.text =  Points.ToString();
        }
    }
}
