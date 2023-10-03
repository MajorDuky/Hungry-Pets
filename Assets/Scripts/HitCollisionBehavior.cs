using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollisionBehavior : MonoBehaviour
{
    private Player player;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            player.HitByEnemy(damage);
        }
    }
}
