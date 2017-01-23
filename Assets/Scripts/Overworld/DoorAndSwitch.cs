using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAndSwitch : MonoBehaviour {

    public Sprite switchOn;
    public Sprite doorOpen;
    public GameObject door;
    public AudioSource switch_audio;

    private bool wasFlipped;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !wasFlipped)
        {
            GetComponent<SpriteRenderer>().sprite = switchOn;
            door.GetComponent<SpriteRenderer>().sprite = doorOpen;
            door.GetComponent<Collider2D>().isTrigger = true;
            switch_audio.Play();
            wasFlipped = true;
        }
    }
}
