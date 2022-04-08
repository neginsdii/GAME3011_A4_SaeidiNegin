using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Tile : MonoBehaviour, IPointerDownHandler
{

    public Image Highlighted;
    public Image Selected;
    public string code;
    public TextMeshProUGUI codeTxt;
    public Vector2 indecies;

    public RectTransform rect;
    void Start()
    {
        Highlighted.enabled= false;
        Selected.enabled = false;
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FillTile(string _code, Vector2 pos, Vector2 ind)
	{
        code = _code;
        indecies = ind;
        codeTxt.text = code;
        rect.anchoredPosition = pos;
    }

	public void OnPointerDown(PointerEventData eventData)
	{
        TileManager.Instance.SelectTiles(this);
	}
}
