using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MovementController : MonoBehaviour
{
    public GameObject missilePrefab;
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
                wasClicked = true;
                StartCoroutine(movement(player));
            }
            else if(wasClicked)
            {
                wasClicked = false;
                hexagonObjects.Clear();
                Debug.Log("Cleared the array");
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (Camera.main != null && wasClicked == false)
            {
                GameObject missile = Instantiate(missilePrefab, player.transform.position, Quaternion.identity);
                wasClicked = true;
                StartCoroutine(movement(missile));
            }
            else if(wasClicked)
            {
                wasClicked = false;
                hexagonObjects.Clear();
                Debug.Log("Cleared the array");
            }
        }
    }

    private List<GameObject> rayCast(GameObject type)
    {
        RaycastHit2D[] hits = Physics2D.LinecastAll((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), type.transform.position);
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
        return hexagonObjects = hexagonObjects
            .OrderBy(go => Vector2.Distance(go.transform.position, type.transform.position)).ToList();
    }
    
    IEnumerator movement(GameObject type)
    {
        rayCast(type);
        hexagonObjects.RemoveAt(0);

        for (int i = 0; i < hexagonObjects.Count; i++)
        {
            type.transform.position = hexagonObjects[i].transform.position;
            yield return new WaitForSeconds(0.2f);
        }

        if (type.name != "Player")
        {
            Destroy(type);
        }
    }
}