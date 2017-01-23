using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadesMove : MonoBehaviour {

    public Vector2 moveSpeed;
    public float switchTime = 2;
    public bool flipX = true;
    public bool flipY = true;

    private void Start()
    {
        StartCoroutine(Flip());
    }

    private void Update()
    {
        transform.position = new Vector2(transform.position.x - (Time.deltaTime * -moveSpeed.x), transform.position.y - (Time.deltaTime * -moveSpeed.y));
    }

    private IEnumerator Flip()
    {
        yield return new WaitForSeconds(switchTime);
        if (flipX && !flipY)
        {
            moveSpeed = new Vector2(-moveSpeed.x, moveSpeed.y);
        }
        else if(!flipX && flipY)
        {
            moveSpeed = new Vector2(moveSpeed.x, -moveSpeed.y);
        }
        else if(flipX && flipY)
        {
            moveSpeed = -moveSpeed;
        }
    }
}
