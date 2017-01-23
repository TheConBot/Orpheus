using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWave : MonoBehaviour {

    public Transform Wave;
    public Vector3 offset;

	void Update () {
        transform.position = Wave.transform.position + offset;
	}
}
