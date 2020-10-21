using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SimpleGrid<CellType>
{
    CellType[,] cells;

    bool settedUp = false;

    [SerializeField] int width;
    [SerializeField] int height;

    [SerializeField] float cellSize;

    [SerializeField] Vector3 position;

    // Sets up grid.
    public SimpleGrid(int width, int height, float cellSize, Vector3 position)
    {
        if (settedUp)
            return;
        settedUp = true;

        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.position = position;

        cells = new CellType[width, height];
        UpdateDebugDraw();
    }

    // Sets up grid according to inspector values!
    public void SetupUsingInspector()
    {
        if (settedUp)
            return;
        settedUp = true;

        cells = new CellType[width, height];
        UpdateDebugDraw();
    }

    public void UpdateDebugDraw()
    {
        for (int x = 0; x < width; ++x)
            for (int y = 0; y < height; ++y)
            {
                Debug.DrawLine(CellToWorldPosition(x, y), CellToWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(CellToWorldPosition(x, y), CellToWorldPosition(x + 1, y), Color.white, 100f);
            }
        Debug.DrawLine(CellToWorldPosition(0, height), CellToWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(CellToWorldPosition(width, 0), CellToWorldPosition(width, height), Color.white, 100f);
    }

    

    public void SetValue(int x, int y, CellType cellValue)
    {
        if (x >= 0 && x < width &&
               y >= 0 && y < height)
            cells[x, y] = cellValue;
    }

    public void SetValue(Vector3 worldPosition, CellType cellValue)
    {
        int x, y;
        WorldToCellPosition(worldPosition, out x, out y);
        SetValue(x, y, cellValue);
    }

    public CellType GetValue(int x, int y)
    {
        if (x >= 0 && x < width &&
            y >= 0 && y < height)
            return cells[x, y];
        else
            return default(CellType);
    }

    public CellType GetValue(Vector3 worldPosition)
    {
        int x, y;
        WorldToCellPosition(worldPosition, out x, out y);
        return GetValue(x, y);
    }

    public Vector3 CellToWorldPosition(int x, int y) => new Vector3(x, y) * cellSize + position;
    public void WorldToCellPosition(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition.x - position.x) / cellSize);
        y = Mathf.FloorToInt((worldPosition.y - position.y) / cellSize);
    }
}
