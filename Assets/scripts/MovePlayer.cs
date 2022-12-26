using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MovePlayer : MonoBehaviour
{
    private MovementController _movementController;
    private List<GameObject> hexagonObjects;
    public GameObject player;
    public float speed = 5.0f;

    void Start()
    {
        _movementController = GetComponent<MovementController>();
        hexagonObjects = _movementController.hexagonObjects;
        player = GameObject.Find("Player");
    }

    void Update()
    {
        hexagonObjects = hexagonObjects
            .OrderBy(go => Vector2.Distance(go.transform.position, player.transform.position)).ToList();
        foreach (var hex in hexagonObjects)
        {
            Debug.Log(hex.name);
        }
    }
}