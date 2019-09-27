using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  [Header("Player controls")]
  private Vector3 nextPathTarget;
  [SerializeField] Vector3 targetDirection;
  private Vector3 touchPosition;
  [SerializeField] Vector3 pullDirection;
  [SerializeField] float pullAccuracy;
  [SerializeField] float maxSpeed = 2f;
  float moveSpeed = 0f;

  [Header("Level & path")]
  [SerializeField] LevelConfig levelConfig;
  List<Transform> waypoints;
  int waypointIndex = 0;

  // TODO: i think i can remove the player's rigidbody
  // TODO: also remove pathing from player

  // Start is called before the first frame update
  void Start()
  {
    waypoints = levelConfig.GetWaypoints();
    transform.position = waypoints[waypointIndex].transform.position;
  }

  // Update is called once per frame
  void Update()
  {
    CheckProgress();
    CheckForInput();
  }

  private float AngleBetweenVectors(Vector2 vec1, Vector2 vec2) // 0 - 180
  {
    Vector2 vec1Rotated90 = new Vector2(-vec1.y, vec1.x);
    return Vector2.Angle(vec1, vec2);
  }

  private void CheckProgress()
  {
    if (waypointIndex <= waypoints.Count - 1)
    {
      nextPathTarget = waypoints[waypointIndex].transform.position;
      var movementThisFrame = moveSpeed * Time.deltaTime;
      transform.position = Vector2.MoveTowards(transform.position, nextPathTarget, movementThisFrame);
      targetDirection = nextPathTarget - transform.position;

      if (transform.position == nextPathTarget)
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

  private void Move()
  {
    touchPosition.z = 0;
    pullDirection = touchPosition - transform.position;

    pullAccuracy = AngleBetweenVectors(targetDirection, pullDirection);
    float speedModifier = Mathf.Max(90 - pullAccuracy, 0) / 90; // 0 - 1
    moveSpeed = 2 * speedModifier;
  }

  private void StopMoving()
  {
    moveSpeed = 0;
  }

  private void CheckForInput()
  {
    if (Input.GetMouseButton(0)) // keyboard & mouse
    {
      touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      Move();
    }
    else if (Input.touchCount > 0) // mobile
    {
      Debug.Log(Input.touchCount);
      Touch touch = Input.GetTouch(0);
      touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
      Move();

      if (touch.phase == TouchPhase.Ended)
      {
        StopMoving();
      }
    }
    else
    {
      StopMoving();
    }
  }
}
