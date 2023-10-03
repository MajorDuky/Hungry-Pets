using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Pet : LivingEntity
{
    private Player player;
    [SerializeField] private Animator animator;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Slider hungerBar;
    [SerializeField] private HitCollisionBehavior hitCollisionBehavior;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        GameManager = GameObject.FindAnyObjectByType<GameManager>();
        hitCollisionBehavior.damage = Damage;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bone"))
        {
            int bulletDmg = other.gameObject.GetComponent<BoneAmmoProperties>().damage;
            if (bulletDmg != 0)
            {
                bool isAlive = TakeDamage(bulletDmg);
                float valueToRetrieveFromSlider = (float)bulletDmg / MaxHealth;
                hungerBar.value -= valueToRetrieveFromSlider;
                if (!isAlive)
                {
                    DeactivateOnDeath();
                }
            }
            other.gameObject.SetActive(false);
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    /// <summary>
    /// Method that deactivate the current gameobject
    /// </summary>
    private void DeactivateOnDeath()
    {
        GameManager.AddFedEnemy();
        Health = MaxHealth;
        hungerBar.value = 1;
        gameObject.SetActive(false);
    }
}
