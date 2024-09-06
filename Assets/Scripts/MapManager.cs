using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public Vector3 activeTilePosition;
    public Hashtable activeGridMap;

    [SerializeField] private Tilemap LevelOneMap;

    [SerializeField] private Tilemap LevelTwoMap;
    
    [SerializeField] private Tilemap LevelThreeMap;

    private static MapManager instance;


    private Tilemap pathMap;

    private int curMapLevel = 1;

    public static MapManager Instance
    {
		get
        {
			if (instance == null)
            {
				instance = FindObjectOfType<MapManager>();
			}
			return instance;
		}
	}

    void Start()
    {
	    activeTilePosition = Vector3.zero;
        activeGridMap = new Hashtable();
        SetToActiveAvailMap();
	}

    void Update()
    {
        if (GameManager.Instance.currentLevel != curMapLevel)
        {
			SetToActiveAvailMap();
		}
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPosition = pathMap.WorldToCell(mousePosition);

        TileBase tile = pathMap.GetTile(gridPosition);
        if (Input.GetMouseButton(0) && tile != null)
        {
			//Debug.Log("Hovering over Tile Position: " + gridPosition);
            activeTilePosition = pathMap.GetCellCenterWorld(gridPosition);
		}
        else
        {
            activeTilePosition = Vector3.zero;
        }
    }

    void SetToActiveAvailMap()
    {
		switch(GameManager.Instance.currentLevel)
        {
			case 1:
				pathMap = LevelOneMap;
                curMapLevel = 1;
				break;
			case 2:
				pathMap = LevelTwoMap;
                curMapLevel = 2;
				break;
			case 3:
				pathMap = LevelThreeMap;
                curMapLevel = 3;
				break;
		}
	}
}
