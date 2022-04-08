using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private static TileManager instance;
    public static TileManager Instance { get { return instance; } }

    public MatrixGenerator matrix;
    public Tile SelectTile;
    public int Index =0;
    public List<Tile> SelectedTiles = new List<Tile>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        matrix = GetComponent<MatrixGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectTiles(Tile tile)
	{
        if (SelectedTiles.Count >= matrix.lengthOfSequence) return;
        SelectTile = tile;
        if (CheckForTiles())
        {
            tile.Selected.enabled = true;
            tile.Highlighted.enabled = false;
            for (int i = 0; i < matrix.NumberOfRows; i++)
			{
                if (i != (int)tile.indecies.x)
                {
                    if (!matrix.tiles[i, (int)tile.indecies.y].Selected.isActiveAndEnabled)
                    matrix.tiles[i, (int)tile.indecies.y].Highlighted.enabled = true;
                }
			}
            for (int i = 0; i < matrix.NumberOfColumns ; i++)
            {
                if (i != (int)tile.indecies.y)
                {
                    if (!matrix.tiles[(int)tile.indecies.x, i].Selected.isActiveAndEnabled)
                        matrix.tiles[(int)tile.indecies.x, i].Highlighted.enabled = true;
                }
            }
        }

    }

    public bool CheckForTiles()
	{
        if(SelectTile.code == matrix.seq[Index] )
		{
            SelectedTiles.Add(SelectTile);
            Index++;
            return true;
		}
        return false;
	}
}
