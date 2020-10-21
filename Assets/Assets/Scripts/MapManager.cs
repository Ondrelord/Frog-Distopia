using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileOccupancy
{
    Free,
    Unit,
    Building,
    Water,
    Blocked
}


public class MapManager : MonoBehaviour
{
    SimpleGrid<TileOccupancy> OccupancyGrid;
    [SerializeField] Tilemap tilemap = null;

    [SerializeField] List<Sprite> impassableTiles = new List<Sprite>();
    [SerializeField] List<Sprite> waterTiles = new List<Sprite>();

    Vector2 mapSize;

    public GameObject textTest;

    // Start is called before the first frame update
    void Start()
    {
        mapSize = transform.Find("MapSize").localScale;
        OccupancyGrid = new SimpleGrid<TileOccupancy>((int)mapSize.x, (int)mapSize.y, 1.6f, transform.position);

        Grid tileMapGrid = tilemap.transform.parent.GetComponent<Grid>();

        for (int x = 0; x < mapSize.x; ++x)
            for (int y = 0; y < mapSize.y; ++y)
            {
                if (impassableTiles.Contains(tilemap.GetSprite(new Vector3Int(x, y, 0))))
                    OccupancyGrid.SetValue(x, y, TileOccupancy.Blocked);
                else if (waterTiles.Contains(tilemap.GetSprite(new Vector3Int(x, y, 0))))
                    OccupancyGrid.SetValue(x, y, TileOccupancy.Water);

            }
        //DebugVisualizeOccupancyGrid();
    }

    // Update is called once per frame
    void Update()
    {
    }


    void DebugVisualizeOccupancyGrid()
    {
        for (int x = 0; x < mapSize.x; ++x)
            for (int y = 0; y < mapSize.y; ++y)
            {
                Vector2 textPos = OccupancyGrid.CellToWorldPosition(x, y);
                textPos.x += 0.8f; textPos.y += 0.8f;
                GameObject GO = Instantiate(textTest, textPos, Quaternion.identity, transform);
                GO.GetComponentInChildren<TextMeshProUGUI>().text = OccupancyGrid.GetValue(x, y).ToString();
            }
    }
}
