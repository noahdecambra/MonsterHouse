using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    private Transform _buildLocation;

    public GameObject buildable;

    protected float _buildTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Builder(FindBuildLocation(), buildable, 1));
    }

    // Update is called once per frame
    void Update()
    {
       
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
        currentBuild.transform.parent= gameObject.transform;
        while (currentBuild != null)
        {
            yield return new WaitForSeconds(1);
        }
    }
}
