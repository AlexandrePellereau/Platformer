using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public Vector2 minPosition;
    
    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 newPos = new Vector3(playerPosition.x, playerPosition.y, transform.position.z);
        if (newPos.x < minPosition.x)
            newPos.x = minPosition.x;
        if (newPos.y < minPosition.y)
            newPos.y = minPosition.y;
        transform.position = newPos;
    }
}
