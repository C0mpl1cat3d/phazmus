using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MovementController : MonoBehaviour
{
    public List<GameObject> hexagonObjects = new List<GameObject>();
    public GameObject player;
    private bool wasClicked = false;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Camera.main != null && wasClicked == false)
            {
                RaycastHit2D[] hits = Physics2D.LinecastAll((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), player.transform.position);
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider != null && hit.collider.CompareTag("hexagon"))
                    {
                        if (!hexagonObjects.Contains(hit.collider.gameObject))
                        {
                            hexagonObjects.Add(hit.collider.gameObject);
                        }
                    }
                }
                wasClicked = true;
                StartCoroutine(movement());
            }
            else if(wasClicked)
            {
                wasClicked = false;
                hexagonObjects.Clear();
                Debug.Log("Cleared the array");
            }
        }
    }

    private List<GameObject> order()
    {
        return hexagonObjects = hexagonObjects
            .OrderBy(go => Vector2.Distance(go.transform.position, player.transform.position)).ToList();
    }
    
    IEnumerator movement()
    {
        order();
        hexagonObjects.RemoveAt(0);

        for (int i = 0; i < hexagonObjects.Count; i++)
        {
            player.transform.position = hexagonObjects[i].transform.position;
            yield return new WaitForSeconds(0.2f);
        }
    }
}