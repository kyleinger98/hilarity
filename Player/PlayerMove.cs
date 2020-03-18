using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public int[] damageTable = { 10, 10, 20, 30 };
    WeaponWheel weaponWheel;
    [Header("Movement Variables")]
    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;
    [SerializeField] private float movementSpeed;

    private CharacterController characterController;
    private BulletPistol bulletPistol;

    [SerializeField] private AnimationCurve jumpFall;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;

    [SerializeField] private KeyCode crouchKey;



    private bool isCrouching = false;
    private float OriginalHeight;
    [SerializeField] private float CrouchHeight = 0.3f;

    private bool isJumping;
    private int randomAttack = 1;
    public int numberOfAttacks = 6;

    public float gravity = 5f;

    // Create animators
    Animator animator;
    Animator peaShooterAnimator;

    // Combat Variables
    // Creating the new pivot point so that we can attack AI
    [Header("Weapon Variables")]
    public GameObject PlayerPivot;

    public float playerHealth = 99f;

    public float playerMeleeRange = 10f;

    public float playerGunRange = 0.0000001f;

    public float attackForce = 50f;

    public bool cantShoot;

    // UI 
    // Player health slider
    public Slider healthSlider;
    public Text healthCounter;

    // Player Ammo text
    public GameObject PeaShootingCenter;
    
    public Text peaShooterAmmoCount;

    // Weapons
    // MELEE
    [Header("Weapons")]
    public Animator selectedWeapon;
    public Animator[] weapons;

    // GUNS
    // Pistol
    public New_Weapon_Recoil_Script recoil;
    //public New_Weapon_Recoil_Script Recoil;
    private float nextTimeToFire = 0.5f;
    public float fireRate = 0.5f;
    Camera cam;
    public static int IgnoreRayCastLayer;
    public float maxAmmo = 6f;
    public float _currentAmmo;
    public float reloadTime = 1f;

    // Shotgun
    // number of bullets to shoot
    private BulletPistol[] cartridgeSize;
    public float maxShotgunAmmo = 6f;
    public float _currentShotgunAmmo;
    public float shotgunReloadTime = 1f;

    //public ParticleSystem muzzleFlash;
    //public GameObject pistolBulletPrefab;
    public GameObject pistolBarrel;
    public GameObject shotgunBarrel;

    public bool isBreathing = false;
    public bool isInspecting = false;
    private bool isReloading = false;
    private Vector3 moveDirection = Vector3.zero;
    public Camera mainCamera;
    public BulletPistol bulletPrefab;
    public BulletPistol shotgunPrefab;

    // Gun Audio
    //[Header("Audio")]
    //AudioSource gunAS;
    //public AudioClip shootAC;

    public void SetHealth()
    {
        healthSlider.value = playerHealth;
        healthCounter.text = playerHealth.ToString();
    }

    public void SetAmmo()
    {
        peaShooterAmmoCount.text = _currentAmmo.ToString();
    }

    private void Awake()
    {
        // Assign to components
        characterController = GetComponent<CharacterController>();
        OriginalHeight = characterController.height;
        bulletPistol = GetComponent<BulletPistol>();
        //gunAS = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        weaponWheel = GetComponent<WeaponWheel>();

        
    }

    public void Start()
    {
        // Health UI
        healthSlider = GameObject.Find("HealthBar").GetComponent<Slider>();
        healthCounter = GameObject.Find("HealthText").GetComponent<Text>();
        healthCounter.text = playerHealth.ToString();

        // Get peaShooterAnimator:
        peaShooterAnimator = PeaShootingCenter.GetComponent<Animator>();

        // Ammo UI
        _currentAmmo = maxAmmo;
        GameObject g = GameObject.Find("PeaShooterAmmo");
        if(g != null){
            peaShooterAmmoCount = GameObject.Find("PeaShooterAmmo").GetComponent<Text>();
            peaShooterAmmoCount.text = _currentAmmo.ToString();
        }

        // Camera
        cam = GetComponentInChildren<Camera>();
    }


    private void Update()
    {
        // Call player movement method
        PlayerMovement();
        SetHealth();
        SetAmmo();

        // Disable shooting if reload animation is playing
        if (peaShooterAnimator.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
            cantShoot = true;
        else
            cantShoot = false;

        // Reload with 0 ammo remaining
        if (_currentAmmo == 0 || Input.GetKeyDown(KeyCode.R))
        {
            Reload();
            //nextTimeToFire = Time.time + 1f / fireRate;
        }

        // Check to see if the player has attacked an AI:
        if (Input.GetButtonDown("Fire1") && Time.time > nextTimeToFire && !isReloading && _currentAmmo > 0)
        {
            // Calls the attack
            PlayerAttack();

            // Reset next time to fire according to fire rate
            nextTimeToFire = Time.time + 1f / fireRate;

            recoil.Fire();
        }

        // Prevent player having over 100 health
        if (playerHealth > 99)
        {
            playerHealth = 99;
        }


    }

    void Reload()
    {
        peaShooterAnimator.SetTrigger("needsReload");
        _currentAmmo = maxAmmo;
    }

    void StartReloading()
    {
        isReloading = true;
    }


    void fixedUpdate()
    {
        
    }



    private void PlayerMovement()
    {
        float vertInput = Input.GetAxis(verticalInputName) * movementSpeed;
        float horizInput = Input.GetAxis(horizontalInputName) * movementSpeed;

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;
        Vector3 hz = forwardMovement + rightMovement;
        moveDirection.x = hz.x;
        moveDirection.z = hz.z;
        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);

        JumpInput();
        CrouchInput();
    }

    private void JumpInput()
    {
        if (Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
            basicJump();
        }
    }
    private void basicJump()
    {

        if (characterController.isGrounded)
        {
        
            moveDirection.y = jumpMultiplier;
        }

        isJumping = false;
    }


    private void CrouchInput()
    {
        if (Input.GetKeyDown(crouchKey))
        {
            isCrouching = !isCrouching;

            CheckCrouch();
        }
    }

    void CheckCrouch()
    {
        if (isCrouching == true)
        {
            characterController.height = CrouchHeight;
            movementSpeed = 2;
        }
        else
        {
            characterController.height = OriginalHeight;
            movementSpeed = 4;
        }
    }



    public void PlayerAttack()
    {
        // Set forward to actually project forwards
        Vector3 forward = Camera.main.transform.forward; //transform.TransformDirection(Vector3.forward);
        // Set the origin as the GameObject created earlier, Pivot
        Vector3 origin = PlayerPivot.transform.position;
        Debug.Log("weaponChoice " + (int)weaponWheel.weaponChoice);
        selectedWeapon = weapons[(int)weaponWheel.weaponChoice];
        // CODE FOR MELEE ATTACKS
        // Check using a weapon
        if (selectedWeapon != null)
        {
            randomAttack = Random.Range(1, numberOfAttacks + 1);
            // Play animation
            selectedWeapon.SetTrigger("Attack" + randomAttack);

            // If using any melee weapons fire this raycast to attack:
            if ((int)weaponWheel.weaponChoice == 1 || (int)weaponWheel.weaponChoice == 2)
            {

                // Create a new raycasthit
                RaycastHit hit;
                if (Physics.Raycast(origin, forward, out hit, playerMeleeRange))
                {
                    {
                        if ((hit.transform.gameObject.tag == "AI"))
                            //Send info to AI
                            hit.transform.gameObject.SendMessage("TakeDamage", damageTable[(int)weaponWheel.weaponChoice]);
                    }
                }
            }

            // SHOOTING PISTOL
            if ((int)weaponWheel.weaponChoice == 3 && !cantShoot)
            {
                // Shoot bullet from pistol barrel
                BulletPistol newBullet = Instantiate(bulletPrefab.gameObject).GetComponent<BulletPistol>();
                                
                // For shooting at the center of the screen
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                    
                //Make the new bullet start at camera
                newBullet.transform.position = pistolBarrel.transform.position;
                    
                //Set bullet direction
                newBullet.SetDirection(ray.direction);
                    
                // Decrease ammo count
                _currentAmmo--;

                // Play particle effects

                // Play sound
            }

            // SHOOTING SHOTGUN
            if ((int)weaponWheel.weaponChoice == 4 && !cantShoot)
            {
                // Used to spawn multiple bullets
                cartridgeSize = new BulletPistol[6];

                // Create random numbers for shell spread
                int spreadX = Random.Range(-35, 36);
                int spreadY = Random.Range(-35, 36);

                // For shooting at the center of the screen
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition + new Vector3(spreadX, spreadY, 0));

                // FOR EVERY SHELL
                for (int i = 0; i <cartridgeSize.Length; i++)
                {
                    // Spawn shell
                    BulletPistol newShell = Instantiate(shotgunPrefab.gameObject).GetComponent<BulletPistol>();
                    //Make the new shell start at camera
                    newShell.transform.position = shotgunBarrel.transform.position;
                    //Set shell direction
                    newShell.SetDirection(ray.direction);
                    //Assign array indexes to each shell created
                    cartridgeSize[i] = newShell;

                    // Reset spread values
                    spreadX = Random.Range(-50, 51);
                    spreadY = Random.Range(-50, 51);

                    // Alter next shell's direction
                    ray = mainCamera.ScreenPointToRay(Input.mousePosition + new Vector3(spreadX, spreadY, 0));
                }

                // Decrease ammo count
                _currentAmmo--;

                // Play particle effects

                // Play sound
            }
        }
    }

    public void playerTakeDamage(float amount)
    {
        Debug.Log( "Player has taken " + amount + " damage");
        playerHealth -= amount;
        if (playerHealth <= 0f)
        {
            // Play death animation here

            // Wait a short while and then restart the scene
            StartCoroutine(endOfLevelDelay());

            // Print the name of the enemy that we have killed!
            Debug.Log("Player has died!");
        }
    }

    IEnumerator endOfLevelDelay()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(3);
        Application.LoadLevel(Application.loadedLevel);
    }

}


        

