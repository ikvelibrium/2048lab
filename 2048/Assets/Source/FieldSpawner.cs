using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class FieldSpawner : MonoBehaviour
{
    public float TileSize;
    public float TileSpacing;
    public int FieldSize;


    [SerializeField] private Tile _tile;
    [SerializeField] RectTransform _rt;

    private Tile[,] _field;

    private void Start()
    {
        CreateLvl();
    }
    public void CreateMap()
    {
        
        _field = new Tile[FieldSize, FieldSize];

        float fieldWidth = FieldSize * ( TileSize + TileSpacing) + TileSpacing;

        _rt.sizeDelta = new Vector2(fieldWidth, fieldWidth);

        float startX = -(-fieldWidth / 2) + (TileSize / 2) + TileSpacing;
        float startY = (-fieldWidth / 2) + (TileSize / 2) + TileSpacing; //константа с 0.5

        for (int i = 0; i < FieldSize; i++)
        {
            for (int j = 0; j < FieldSize; j++)
            {
                 Tile tile  = Instantiate(_tile, transform, false);
                 Vector2 position = new Vector2(startX + (i * (TileSize + TileSpacing)), startY * (j * (TileSize + TileSpacing)));
                 tile.transform.position = position;
                _field[i,j] = tile;

                tile.SetValue(i, j, 0);
            }
        }
    }
    public void CreateLvl()
    {
        if (_field == null)
        {
          
            CreateMap();
        }
      
        for (int i = 0;i < FieldSize; i++)
        {
            for (int j = 0; j < FieldSize; j++)
            {
                
                _field[i, j].SetValue(i, j, 0);
            }
        }
    }
}
