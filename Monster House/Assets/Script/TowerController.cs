using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum towerLevel
{
    Level1,Level2,Level3,Level4
};
public class TowerController : MonoBehaviour
{
    public int currentTowerLevel;
    private Transform _buildLocation;

    public GameObject buildable;

    protected float _buildTime;

    public towerLevel currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        if (buildable.name == "Tombstone")
        {
            if (buildable.GetComponent<TombstoneLogic>().currentBuildable != null)
            {
                buildable = buildable.GetComponent<TombstoneLogic>().currentBuildable;
            }
        }
        StartCoroutine(Builder(FindBuildLocation(), buildable, 1));
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Upgrade()
    {
        if (buildable.name == "Tombstone")
        {
            buildable.GetComponent<TombstoneLogic>().Upgrade();
        }
    }

    private Transform FindBuildLocation()
    {
        var nearestBuildSiteDistance = Mathf.Infinity;
        var buildableLocations = wavePoints.points;
        foreach (var location in buildableLocations)
        {
            var distanceToLocation = Vector3.Distance(transform.position, location.transform.position);
            if (distanceToLocation < nearestBuildSiteDistance)
            {
                nearestBuildSiteDistance = distanceToLocation;
                _buildLocation = location;
            }
        }

        return _buildLocation;
    }

    IEnumerator Builder(Transform buildSite, GameObject constructable, float timeToBuild)
    {
        GameObject currentBuild = null;
        yield return new WaitForSeconds(timeToBuild);
        currentBuild = Instantiate(constructable, buildSite);
        currentBuild.transform.parent = gameObject.transform;
        while (currentBuild != null)
        {
            yield return new WaitForSeconds(1);
        }
    }
   
}

