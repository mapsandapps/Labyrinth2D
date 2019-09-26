using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level Config")]
public class LevelConfig : ScriptableObject
{
  [SerializeField] GameObject backgroundPrefab;
  [SerializeField] GameObject playerPrefab;
  [SerializeField] GameObject pathPrefab;

  public GameObject GetBackgroundPrefab()
  { return backgroundPrefab; }

  public GameObject GetPlayerPrefab()
  { return playerPrefab; }

  public List<Transform> GetWaypoints()
  {
    var waypoints = new List<Transform>();

    foreach (Transform waypoint in pathPrefab.transform)
    {
      waypoints.Add(waypoint);
    }

    return waypoints;
  }

}
