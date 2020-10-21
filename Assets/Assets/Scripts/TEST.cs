using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TEST : MonoBehaviour
{
    public SimpleGrid<bool> grid;
    public Tilemap map;
    public TilemapCollider2D tileCol;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 mapPos = map.CellToWorld(map.origin);
        Vector3 mapSize = map.CellToWorld(map.size);
        print(map.cellBounds);
        //Vector3 position = new Vector3(map.cellBounds.position.x * map.cellSize.x * map.transform.parent.localScale.x, map.cellBounds.position.y * map.cellSize.y * map.transform.parent.localScale.y);
        //grid = new SimpleGrid<bool>(map.cellBounds.size.x, map.cellBounds.size.y, map.cellSize.x * map.transform.parent.localScale.x, position);
        grid = new SimpleGrid<bool>((int)mapSize.x, (int)mapSize.y, map.cellSize.x * map.transform.parent.localScale.x, mapPos);
        //grid.SetupUsingInspector();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print(grid.GetValue(Utils.GetMouseWorldPosition()));
        }
        if (Input.GetMouseButtonDown(1))
        {
            grid.SetValue(Utils.GetMouseWorldPosition(), true);
        }


    }
}
