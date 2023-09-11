using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform Player;
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Player.transform.position;
        transform.rotation = Player.transform.rotation;
    }
}
