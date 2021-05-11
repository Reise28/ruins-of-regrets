using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // Fields ------------------------------------------------------------------

    [SerializeField] private Transform player;
    private Vector3 position;

    // Unity API ---------------------------------------------------------------

    void Awake() {
        if (!player)
            player = FindObjectOfType<Player>().transform;
    }

    void Update() {
        Vector3 newposition = transform.position;
        newposition.x = player.position.x;
        transform.position = Vector3.Lerp(transform.position, newposition, Time.deltaTime);
    }
}
