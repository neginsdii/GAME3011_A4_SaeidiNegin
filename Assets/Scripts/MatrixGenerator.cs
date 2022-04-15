using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MatrixGenerator : MonoBehaviour
{

    [Header("Tile Properties")]
    public Tile[,] tiles;
    public int NumberOfRows;
    public int NumberOfColumns;
    public Vector2 offset;
    public float TileSize;

    [Header("References to Game Objects")]
    [SerializeField]
    private Tile tilePrefab;
    [SerializeField]
    private GameObject MatrixPanel;
    public TextMeshProUGUI sequenceText;
    public TextMeshProUGUI SkillLevel;

    public string[] Codes;
    System.Random random = new System.Random();
    public List<string> seq = new List<string>();
    public int lengthOfSequence;
    // Start is called before the first frame update
    void Start()
    {
        InitializeGame();
        tiles = new Tile[NumberOfRows, NumberOfColumns];
        GenerateMatrix();
        CreateSequence();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateMatrix()//generate the board
    {
        Vector2 pos = Vector2.zero;
        for (int i = 0; i < NumberOfRows; i++)
        {
            for (int j = 0; j < NumberOfColumns; j++)
            {

                tiles[i, j] = Instantiate(tilePrefab, MatrixPanel.transform);

                tiles[i, j].FillTile(Codes[ random.Next(0, Codes.Length)], new Vector2(pos.x + j * TileSize + offset.x, pos.y + offset.y), new Vector2(i, j));
            }
            pos.y -= TileSize;
        }

    } 

    public void CreateSequence()
	{
        int r = random.Next(0,NumberOfRows);
        int c = random.Next(0, NumberOfColumns);
        seq.Add(tiles[r, c].code);
        int rnd = 0;
        for (int i = 0; i < lengthOfSequence-1; i++)
		{
           
            if(rnd==0)
			{
			    int j = random.Next(0, NumberOfRows);
                while (tiles[j,c].code == tiles[r, c].code)
					{
                       
                     j = random.Next(0, NumberOfRows);
                    }
                r = j;
                seq.Add(tiles[r, c].code);
                rnd = 1;
            }
            else
			{
                int j = random.Next(0, NumberOfColumns);
                while (tiles[r, j].code == tiles[r, c].code)
                {

                    j = random.Next(0, NumberOfColumns);

                }
                c = j;
                seq.Add(tiles[r, c].code);
                rnd = 0;
            }
		}

		for (int i = 0; i < seq.Count; i++)
		{
            sequenceText.text += seq[i] + " ";
		}
    }

    public void InitializeGame()
	{
        switch(Data.skillLevel)
		{
            case 0:
                SkillLevel.text = "Skill Level : Novice";
                break;
            case 1:
                SkillLevel.text = "Skill Level : Advanced";
                break;
            case 2:
                SkillLevel.text = "Skill Level : Master";
                break;

		}

        switch (Data.LevelOfDifficulty)
		{
            case 0:
                NumberOfRows = NumberOfColumns = 6;
                lengthOfSequence = 4;
               offset = new Vector2(425, -300);
                break;
            case 1:
                NumberOfRows = NumberOfColumns = 9;
                lengthOfSequence = 6;
                offset = new Vector2(250, -100);

                break;
            case 2:
                NumberOfRows = NumberOfColumns = 11;
                lengthOfSequence = 8;
                 offset = new Vector2(100, -100);
                break;

        }
    }
}