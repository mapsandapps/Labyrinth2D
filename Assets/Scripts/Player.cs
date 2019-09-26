using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  private Vector3 touchPosition;
  private Vector3 direction;
  private Rigidbody2D rb;
  [SerializeField] float moveSpeed = 300f;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    CheckForInput();
  }

  private void Move()
  {
    touchPosition.z = 0;
    direction = touchPosition - transform.position;
    rb.velocity = new Vector2(direction.x, direction.y) * moveSpeed * Time.deltaTime;
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
        rb.velocity = Vector2.zero;
      }
    }
    else
    {
      rb.velocity = Vector2.zero;
    }
  }
}
