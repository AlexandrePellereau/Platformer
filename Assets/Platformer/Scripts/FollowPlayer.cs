using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameManager player;
    
    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        transform.position = new Vector3(playerPosition.x, playerPosition.y, transform.position.z);
    }
}
