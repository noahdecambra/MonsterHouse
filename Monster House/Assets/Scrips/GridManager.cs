using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private pathgenerator pathgenerator;

    public GridCellObj[] pathCellsObj;
    public GridCellObj[] sceneryCellObj;

    public int gridwidth = 8;
    public int gridHeight = 16;
    public int minPathLength = 30;

    // Start is called before the first frame update
    void Start()
    {
        pathgenerator = new pathgenerator(gridwidth, gridHeight);

        List<Vector2Int> pathCells = pathgenerator.generatePath();
        int pathSize = pathCells.Count;

        while (pathSize < minPathLength)
        {
            pathCells = pathgenerator.generatePath();
            pathSize = pathCells.Count;
        }

        StartCoroutine(LayPathCells(pathCells));
        StartCoroutine(LaySceneryCells());
    }
    private IEnumerator LayPathCells (List<Vector2Int> pathCells)
    {
        foreach (Vector2Int pathCell in pathCells)
        {
            int neighbourValue = pathgenerator.getCellNeighbourValue(pathCell.x, pathCell.y);
            GameObject pathTile = pathCellsObj[neighbourValue].cellPrefab;
            GameObject pathTileCell = Instantiate(pathTile, new Vector3(pathCell.x, 0f, pathCell.y), Quaternion.identity);
            pathTileCell.transform.Rotate(0f, pathCellsObj[neighbourValue].yRotation, 0f, Space.Self);
            
            yield return new WaitForSeconds(0.4f);
        }

        yield return null;
    }

    
    IEnumerator LaySceneryCells()
    {
        for (int x = 0; x < gridwidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (pathgenerator.CellIsFree(x, y))
                {
                    int randomSceneryCellIndex = Random.Range(0, sceneryCellObj.Length);
                    Instantiate(sceneryCellObj[randomSceneryCellIndex].cellPrefab, new Vector3(x, 0f, y), Quaternion.identity);
                    yield return new WaitForSeconds(0.4f);
                }
            }
        }

        yield return null;
    }
    
}
