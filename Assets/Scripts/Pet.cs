using Unity.VisualScripting;
using UnityEngine;

public class Pet : LivingEntity
{
    private Player player;
    [SerializeField] private Animator animator;
    [SerializeField] private float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer(player.transform.position);
        RotateTowardsPlayer(player.transform.position);
        AttackPlayer(player.transform.position);
    }

    /// <summary>
    /// Method that moves the entity towards the player's position
    /// </summary>
    /// <param name="playerPos">Vector3 : current player position</param>
    void MoveTowardsPlayer(Vector3 playerPos)
    {
        if(!animator.GetBool("isWalking"))
        {
            animator.SetBool("isWalking", true);
        }
        Vector3 playerDirection = Vector3.MoveTowards(transform.position, playerPos, Speed * Time.deltaTime);
        Vector3 newPos = new Vector3(playerDirection.x, 0, playerDirection.z);
        transform.position = newPos;
    }

    /// <summary>
    /// Method that rotates the entity towards the player
    /// </summary>
    /// <param name="playerPos">Vector 3 : current player position</param>
    void RotateTowardsPlayer(Vector3 playerPos)
    {
        Vector3 direction = playerPos - transform.position;
        float singlestep = rotationSpeed * Time.deltaTime;
        Vector3 newRotation = Vector3.RotateTowards(transform.forward, direction, singlestep, 0f);
        transform.rotation = Quaternion.LookRotation(newRotation);
    }
    
    /// <summary>
    /// Method that triggers the attack animation when the entity is near the player
    /// </summary>
    /// <param name="playerPos">Vector 3 : current player position</param>
    void AttackPlayer(Vector3 playerPos)
    {
        float distance = Vector3.Distance(playerPos, transform.position);
        if (distance <= 20f)
        {
            animator.SetTrigger("attack");
        }
    }
}
