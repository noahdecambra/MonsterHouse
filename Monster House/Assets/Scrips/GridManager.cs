using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private pathgenerator pathgenerator;

    public GameObject pathTile;

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
            int pathSize = pathCells.Count;
        }

        StartCoroutine(LayPathCells(pathCells));
    }
    private IEnumerator LayPathCells (List<Vector2Int> pathCells)
    {
        foreach (Vector2Int pathCell in pathCells)
        {
            Instantiate(pathTile, new Vector3(pathCell.x, 0f, pathCell.y), Quaternion.identity);
            yield return new WaitForSeconds(0.4f);
        }

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
