using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class TileManager : MonoBehaviour
{
    private static TileManager instance;
    public static TileManager Instance { get { return instance; } }

    public MatrixGenerator matrix;
    public Tile SelectTile;
    public int Index =0;
    public int turn = 0;

    public Button StartOverButton;
    public Button ExitButton;
    public GameObject SeqPanel;
    public GameObject seqCodePrefab;

    public float posX = 0;
    public GameObject[] seqCodes;

    public TextMeshProUGUI textMessage;
    public AudioSource AudioSource;
    public List<Tile> SelectedTiles = new List<Tile>();

    public TimerCountDown timer;
    public bool GameEnded = false;
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
        ExitButton.onClick.AddListener(OnExitButton);
        seqCodes = new GameObject[matrix.lengthOfSequence];
        textMessage.text = "";
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
           
            CheckGameEnded();
            tile.Selected.enabled = true;
            tile.Highlighted.enabled = false;
            if (!GameEnded)
            {
                if (!AudioSource.isPlaying)
                    AudioSource.Play();
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
                    AddtoSeqPanel(SelectTile.code);
                    Index++;
                    ClearHighlightedTiles();
                    return true;
                }
                if (Index % 2 == 0 && SelectedTiles[SelectedTiles.Count - 1].indecies.x == SelectTile.indecies.x)
                {
                    SelectedTiles.Add(SelectTile);
                    AddtoSeqPanel(SelectTile.code);
                    Index++;
                    ClearHighlightedTiles();
                    return true;
                }
            }
            else
			{
                SelectedTiles.Add(SelectTile);
                AddtoSeqPanel(SelectTile.code);
                Index++;
                return true;
            }
        }
        else 
        {
            if (!GameEnded)
            {
                textMessage.text = "You lost 5 seconds";
                timer.DeductTime(5);

                StartCoroutine(clearTextMessage());
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
        if (!GameEnded)
        {
            SelectedTiles.Clear();
            ClearSeqCode();
            Index = 0;
            timer.DeductTime(5);
            textMessage.text = "You lost 5 seconds";
            StartCoroutine(clearTextMessage());
            ClearHighlightedTiles();
            ClearSelectedTiles();
        }
	}

    public void AddtoSeqPanel(string cd)
	{
        seqCodes[Index] = Instantiate(seqCodePrefab, SeqPanel.transform);
        seqCodes[Index].GetComponentInChildren<TextMeshProUGUI>().text = cd;
 
    }
    public void ClearSeqCode()
	{
		for (int i = 0; i < Index; i++)
		{
            Destroy(seqCodes[i].gameObject);
		}
	}

    public void OnExitButton()
	{
        SceneManager.LoadScene("Start");
	}

    public void CheckGameEnded()
	{
        if(Index==matrix.lengthOfSequence)
		{
            textMessage.text = "You hacked the system";
            GameEnded = true;
            Data.skillLevel++;
            if (Data.skillLevel >= 2)
                Data.skillLevel = 2;
		}
	}

    IEnumerator clearTextMessage()
	{
        yield return new WaitForSeconds(1.0f);
        textMessage.text = "";
	}
}
