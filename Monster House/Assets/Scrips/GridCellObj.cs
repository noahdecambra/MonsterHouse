using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridCell", menuName = "TowerDefence/Grid Cell")]
public class GridCellObj : ScriptableObject 
{

    public enum CellType {Path, Ground}

    public CellType celltype;

    public GameObject cellPrefab;
    public int yRotation;

}
