using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.Events;
public class PlayerManager : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] float mouseSensibility = 10f;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float maxDistanceToGround;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Image blackScreen;
    [SerializeField] private Text blackText;

    [SerializeField] private GameObject flashlight;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private AudioSource gramophoneAudio;

    [Header("Audio Source Clips")]

    [SerializeField] private AudioClip tenseMusic;


    CinemachineVirtualCamera mainCamera;

    private float horizontalCameraAxis;
    private float verticalCameraAxis;
    private float xRotation;

    private Transform playerTransform;
    private CharacterController playerCharacterController;
    private AudioSource playerAudio;

    private float horizontalMove;
    private float verticalMove;
    private Vector3 directionMovement;
    private Vector3 fallingVerticalDirection;

    private bool playerIsGrounded;
    private bool isAlive = true;
    private float gravity = -9.8f;
    private float onGroundGravity = -0.05f;

    public bool isActive = true;

    private event UnityAction<bool> onGamePauseContinue;

    public static PlayerManager ThePlayer;

    [SerializeField] Canvas escMenu;
    [SerializeField] private ControlsOptions controls;

    [Range(-1, 1)] private float horizontalAxis;
    [Range(-1, 1)] private float verticalAxis;

    private void Awake()
    {
        mainCamera = GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();
        playerTransform = GetComponent<Transform>();
        playerCharacterController = GetComponent<CharacterController>();
        playerAudio = GetComponent<AudioSource>();
        ThePlayer = this;
        GameManager.OnGameOver += OnGameOverHandler;
        GameManager.OnGameReset += OnGameResetHandler;
    }

    private void OnGamePauseContinue(bool isPaused)
    {
        escMenu.enabled = !isPaused;
        isActive = isPaused;
        Cursor.lockState = (CursorLockMode) isPaused.GetHashCode();
        Time.timeScale = (float) isPaused.GetHashCode();

        foreach(AudioSource audio in FindObjectsOfType<AudioSource>())
        {
            if (isPaused) audio.Play();
            else audio.Pause();
        }

        //if (isPaused) gramophoneAudio.Play();
        //else gramophoneAudio.Pause();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        PlayerOnTheGround();
    }

    private void Update()
    {
        if (isAlive && isActive) { CameraMovement(); PlayerMovement(); }
        PlayerInputManager();
    }

    void CameraMovement()
    {
        horizontalCameraAxis = Input.GetAxis("Mouse X") * mouseSensibility * 10 * Time.deltaTime;
        verticalCameraAxis = Input.GetAxis("Mouse Y") * mouseSensibility * 10 * Time.deltaTime;
        xRotation -= verticalCameraAxis;
        xRotation = Mathf.Clamp(xRotation, -75f, 55f);
        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerTransform.Rotate(Vector3.up * horizontalCameraAxis);
    }

    private void PlayerMovement()
    {
        //horizontalMove = Input.GetAxis("Horizontal");
        //verticalMove = Input.GetAxis("Vertical");
        //directionMovement = transform.right * horizontalMove + transform.forward * verticalMove;

        if (Input.GetKey(controls.MoveForward) && verticalAxis < 1) { verticalAxis += 10 * Time.deltaTime; }
        else if (Input.GetKey(controls.MoveBack) && verticalAxis > -1) { verticalAxis -= 10 * Time.deltaTime; }
        else { verticalAxis = 0; }
        if (Input.GetKey(controls.MoveLeft) && horizontalAxis > -1) {horizontalAxis -= 10 * Time.deltaTime; }
        else if (Input.GetKey(controls.MoveRight) && horizontalAxis < 1) { horizontalAxis += 10 * Time.deltaTime; }
        else { horizontalAxis = 0; }

        directionMovement = (transform.right * horizontalAxis + transform.forward * verticalAxis).normalized;
        if (directionMovement.magnitude > 0.01f)
        {
            playerCharacterController.Move((directionMovement + fallingVerticalDirection) * playerSpeed * Time.deltaTime);
            playerAnimator.SetBool("Walking", true);
        }
        else
        {
            playerAnimator.SetBool("Walking", false);
        }
    }
    private void PlayerInputManager()
    {
        if (Input.GetKeyDown(controls.Flashlight) && !escMenu.enabled) flashlight.gameObject.SetActive(!flashlight.gameObject.activeInHierarchy);
        
        if (Input.GetKeyDown(KeyCode.Escape) && isAlive) OnGamePauseContinue(escMenu.enabled);
    }

    private void PlayerOnTheGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, maxDistanceToGround, groundLayer))
        {
            playerIsGrounded = true;
            fallingVerticalDirection.y = onGroundGravity;
        }
        else
        {
            playerIsGrounded = false;
            fallingVerticalDirection.y += gravity * Time.deltaTime;
        }
    }

    private void OnGameOverHandler()
    {
        isAlive = false;
        transform.LookAt(new Vector3(DeathManager.TheDeathManager.transform.position.x, this.transform.position.y, DeathManager.TheDeathManager.transform.position.z));
        playerAudio.PlayOneShot(tenseMusic);
    }

    private void OnGameResetHandler()
    {
        isAlive = true;
    }


    //private void ThePlayerIsDead(Transform enemy)
    //{
    //    blackScreen.enabled = true;
    //    blackText.enabled = true;
    //    transform.LookAt(new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z));
    //    isAlive = false;
        
    //    Debug.Log("El jugador ha muerto");
    //    Debug.Log(blackScreen.isActiveAndEnabled);
    //    playerAudio.PlayOneShot(jumpScare_Sound);


    //}

}

