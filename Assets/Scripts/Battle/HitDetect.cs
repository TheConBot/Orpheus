using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitDetect : MonoBehaviour {

    public int lives = 3;
    public Image[] lives_images;
    private bool stillInCollider;
    public SpawnEnemy spawnEnemy;
    public AudioSource hit;

	private void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.tag)
        {
            case "Enemy":
                if (stillInCollider) return;
                stillInCollider = true;
                lives--;
                hit.Play();
                lives_images[lives].enabled = false;
                if(lives <= 0)
                {
                    spawnEnemy.StartCoroutine(spawnEnemy.GameOver());
                    gameObject.SetActive(false);
                }
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Enemy":
                stillInCollider = false;
                break;
        }
    }
}
