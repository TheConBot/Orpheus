using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleEnemy : MonoBehaviour {

    public float scaleRate;

	private void Update () {
        transform.localScale = new Vector2(transform.localScale.x - (Time.deltaTime * scaleRate), transform.localScale.y - (Time.deltaTime * scaleRate));
	}
}
