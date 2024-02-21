using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class FieldSpawner : MonoBehaviour
{
    public float TileSize;
    public float TileSpacing;
    public int FieldSize;

    [SerializeField] private int startTiels;
    [SerializeField] private Tile _tile;
    [SerializeField] RectTransform _rt;

    private Tile[,] _field;

    private bool _tilesBeenMooved;
    private void Start()
    {
        CreateLvl();
    }
    public void CreateMap()
    {
        
        _field = new Tile[FieldSize, FieldSize];

        float fieldWidth = FieldSize * ( TileSize + TileSpacing) + TileSpacing;

        _rt.sizeDelta = new Vector2(fieldWidth, fieldWidth);

        float startX = -(fieldWidth / 2) + (TileSize / 2) + TileSpacing;       
        float startY = (fieldWidth / 2) - (TileSize / 2) - TileSpacing; 
       
        for (int i = 0; i < FieldSize; i++)
        {
            for (int j = 0; j < FieldSize; j++)
            {
                 Tile tile  = Instantiate(_tile, transform, false);
                 Vector2 position = new Vector2(startX + (i * (TileSize + TileSpacing)), startY - (j * (TileSize + TileSpacing)));
               
                tile.transform.localPosition = position;
                _field[i,j] = tile;

                tile.SetValue(i, j, 0);
            }
        }
    }

    

    public void OnInput(Vector2 direction)
    {
        _tilesBeenMooved = false;
        ResetTilesMerge();

        Move(direction);
        
        if (_tilesBeenMooved == true)
        {
           
            CreateRandomTile();
        }
    }

    private void Move(Vector2 direction)
    {
        
        int startXY = direction.x > 0 || direction.y < 0 ? FieldSize - 1 : 0;
        int dir = direction.x != 0 ? (int)direction.x : -(int)direction.y;

        for (int i = 0; i < FieldSize; i++)
        {
            for (int j = startXY; j >= 0 && j < FieldSize; j -= dir)
            {
                Tile tile = direction.x != 0 ? _field[j, i] : _field[i, j];
                
                if (tile.IsEmpty)
                {
                    continue;
                }             
                Tile tileToMerge = FindTileToMerge(tile, direction);
                if (tileToMerge != null)
                {
                    tile.Merge(tileToMerge);
                    _tilesBeenMooved = true;

                    continue;
                }
               
                Tile emptyTile = FindEmptyTile(_tile, direction);
                if (emptyTile != null)
                {
                    Debug.Log("sdad");
                    tile.MoveToTile(emptyTile);
                    _tilesBeenMooved = true;
                }
                
            }
           
        }
        Debug.Log("Done");

    }

    private Tile FindTileToMerge(Tile tile, Vector2 direction)
    {
        int startX = tile.posX + (int)direction.x;
        int startY = tile.posY - (int)direction.y;

        for (int i = startX, j = startY; i >= 0 && i < FieldSize && j >= 0 && j < FieldSize; i += (int)direction.x, j -= (int)direction.y)
        {
            if (_field[i,j].IsEmpty)
            {
                continue;
            }
            if (_field[i, j].Value == tile.Value && !_field[i,j].HasMerged)
            {
                return _field[i, j];
            }

            break;
        }

        return null;
    }

    private Tile FindEmptyTile(Tile tile, Vector2 direction)
    {
        
        Tile emptyTile = null;

        int startX = tile.posX + (int)direction.x;
        int startY = tile.posY - (int)direction.y;

        for (int i = startX, j = startY; i >= 0 && i < FieldSize && j >= 0 && j < FieldSize; i += (int)direction.x, j -= (int)direction.y)
        {
            if (_field[i,j].IsEmpty)
            {
                emptyTile = _field[i, j];
              
            } else
            {
                
                break;
            }
           
        }
        return emptyTile;
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

        for (int i = 0; i < startTiels; i++)
        {
            CreateRandomTile();
        }
    }

    private void CreateRandomTile()
    {
        List<Tile> _emptyTiles = new List<Tile>();

        for (int i = 0; i < FieldSize; i++)
        {
            for (int j = 0; j < FieldSize; j++)
            {
                if (_field[i,j].IsEmpty)
                {
                    _emptyTiles.Add(_field[i, j]);
                }
            }
        }

        int value = Random.Range(0, 10) == 0 ? 2 : 1;
        Tile tile = _emptyTiles[Random.Range(0, _emptyTiles.Count)];
        tile.SetValue(tile.posX, tile.posY, value);
    }
    private void ResetTilesMerge()
    {
       
        for (int i = 0; i < FieldSize; i++)
        {
            for (int j = 0; j < FieldSize; j++)
            {
                _field[i, j].ResetMerege();
            }
        }
    }
}
