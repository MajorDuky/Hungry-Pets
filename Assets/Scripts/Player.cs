using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : LivingEntity
{
    private float _horizontalInput;
    private float _verticalInput;
    [SerializeField] private float turnSpeed;
    private float minTurnAngle = -90.0f;
    private float maxTurnAngle = 90.0f;
    private float rotX;
    [SerializeField] private MainUIHandler mainUIHandler;
    // Start is called before the first frame update
    void Start()
    {
        mainUIHandler.UpdateHealthAmount(Health, MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
        HandlePlayerMovement(_horizontalInput, _verticalInput);
        HandlePlayerRotation();
    }

    /// <summary>
    /// Method that handles the player's movements based on it's horizontal / vertical inputs
    /// Ps : the camera will follow this translation (FollowPlayer.cs)
    /// </summary>
    /// <param name="horizontalInput"></param>
    /// <param name="verticalInput"></param>
    private void HandlePlayerMovement(float horizontalInput, float verticalInput)
    {
        if (horizontalInput != 0 || verticalInput != 0)
        {
            Vector3 translationX = horizontalInput * Vector3.right;
            Vector3 translationZ = verticalInput * Vector3.forward;
            Vector3 translation = new Vector3(translationX.x, 0, translationZ.z);
            Vector3 worldSpaceTranslation = EntityTransform.TransformDirection(Speed * Time.deltaTime * translation);
            EntityTransform.Translate(new Vector3(worldSpaceTranslation.x, 0, worldSpaceTranslation.z), Space.World);
        }
    }

    /// <summary>
    /// Method that handles the player's rotation in a local space
    /// PS : the camera will follow this rotation (FollowPlayer.cs)
    /// </summary>
    private void HandlePlayerRotation()
    {
        float y = Input.GetAxis("Mouse X") * turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * turnSpeed;
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
        Vector3 rotation = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
        transform.eulerAngles = rotation;
    }

    public void HitByEnemy(int damage)
    {
        TakeDamage(damage);
        mainUIHandler.UpdateHealthAmount(Health, MaxHealth);
    }
}
