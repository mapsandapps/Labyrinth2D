using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathing : MonoBehaviour
{
  [SerializeField] LevelConfig levelConfig;
  List<Transform> waypoints;
  [SerializeField] float moveSpeed = 2f;
  int waypointIndex = 0;

  // Start is called before the first frame update
  void Start()
  {
    waypoints = levelConfig.GetWaypoints();
    transform.position = waypoints[waypointIndex].transform.position;
  }

  void Move()
  {
    if (waypointIndex <= waypoints.Count - 1)
    {
      var targetPosition = waypoints[waypointIndex].transform.position;
      var movementThisFrame = moveSpeed * Time.deltaTime;
      transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

      if (transform.position == targetPosition)
      {
        waypointIndex++;
      }
    }
    else
    {
      // TODO: advance to next level
      Debug.Log("advance to next level");
    }
  }

  // Update is called once per frame
  void Update()
  {
    Move();
  }
}
