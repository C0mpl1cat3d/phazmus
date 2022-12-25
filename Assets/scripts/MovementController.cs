using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Camera camera;
    GameObject player;
    public float speed = 1f;
    public Vector2 destination;
    private Rigidbody2D rb;
    private bool isMoving = false;

    void Start()
    {
    player = GameObject.Find("Player"); 
    rb = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            RaycastHit2D hit = Physics2D.Raycast(camera.ScreenToWorldPoint(mouse), mouse);
            if (hit.collider != null && isMoving == false)
            {
                Debug.Log(hit.transform.position);
                destination = hit.transform.position;

                Vector2 direction = destination - (Vector2)player.transform.position;
                direction.Normalize();


                // Calculate the angle of the direction vector
                float angle = Mathf.Atan2(direction.y, direction.x);

                float index = angle / (Mathf.PI / 3);

                index = Mathf.Round(index);
                angle = index * (Mathf.PI / 3);
                direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                
                rb.AddForce(direction * speed);
                isMoving = true;
            }
            else if(isMoving == true)
            {
                rb.velocity = Vector2.zero;
                isMoving = false;
            }

            void test()
            {
                Debug.Log("test");
            }


        }
    }
}