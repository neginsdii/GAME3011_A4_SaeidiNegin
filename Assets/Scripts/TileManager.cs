using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TileManager : MonoBehaviour
{
    private static TileManager instance;
    public static TileManager Instance { get { return instance; } }

    public MatrixGenerator matrix;
    public Tile SelectTile;
    public int Index =0;
    public int turn = 0;

    public Button StartOverButton;
    public AudioSource AudioSource;
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
        AudioSource = GetComponent<AudioSource>();
        StartOverButton.onClick.AddListener(StartOver);
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
            if(!AudioSource.isPlaying)
            AudioSource.Play();
            tile.Selected.enabled = true;
            tile.Highlighted.enabled = false;
            if (Index % 2 == 1)
            {
                for (int i = 0; i < matrix.NumberOfRows; i++)
                {
                    if (i != (int)tile.indecies.x)
                    {
                        if (!matrix.tiles[i, (int)tile.indecies.y].Selected.isActiveAndEnabled)
                            matrix.tiles[i, (int)tile.indecies.y].Highlighted.enabled = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < matrix.NumberOfColumns; i++)
                {
                    if (i != (int)tile.indecies.y)
                    {
                        if (!matrix.tiles[(int)tile.indecies.x, i].Selected.isActiveAndEnabled)
                            matrix.tiles[(int)tile.indecies.x, i].Highlighted.enabled = true;
                    }
                }
            }
        }

    }

    public bool CheckForTiles()
	{
        if(SelectTile.code == matrix.seq[Index] )
		{
           
            if (SelectedTiles.Count > 0)
            {
                if (Index % 2 == 1 && SelectedTiles[SelectedTiles.Count - 1].indecies.y == SelectTile.indecies.y)
                {
                    SelectedTiles.Add(SelectTile);
                    Index++;
                    ClearHighlightedTiles();
                    return true;
                }
                if (Index % 2 == 0 && SelectedTiles[SelectedTiles.Count - 1].indecies.x == SelectTile.indecies.x)
                {
                    SelectedTiles.Add(SelectTile);
                    Index++;
                    ClearHighlightedTiles();
                    return true;
                }
            }
            else
			{
                SelectedTiles.Add(SelectTile);
                Index++;
                return true;
            }
        }
        return false;
	}

    public void ClearHighlightedTiles()
	{
        for (int i = 0; i < matrix.NumberOfRows; i++)
        {
            for (int j = 0; j < matrix.NumberOfColumns; j++)
            {

                matrix.tiles[i, j].Highlighted.enabled = false;

            }

        }
    }

    public void ClearSelectedTiles()
    {
        for (int i = 0; i < matrix.NumberOfRows; i++)
        {
            for (int j = 0; j < matrix.NumberOfColumns; j++)
            {

                matrix.tiles[i, j].Selected.enabled = false;

            }

        }
    }

    public void StartOver()
	{
        SelectedTiles.Clear();
        Index = 0;
        ClearHighlightedTiles();
        ClearSelectedTiles();
	}
}
