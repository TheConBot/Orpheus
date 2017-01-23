using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{

    public Vector2 moveSpeed;

    private void Update()
    {
        transform.position = new Vector2(transform.position.x - (Time.deltaTime * -moveSpeed.x), transform.position.y - (Time.deltaTime * -moveSpeed.y));
    }
}
