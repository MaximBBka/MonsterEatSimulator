using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerSpawner Spawner;
    [SerializeField] private WindowMobileController _windowMobileController;
    public Player Player;
    public Skin skin;
    public Transform SpawnPos;
    [SerializeField] private Transform TargetMove;
    [SerializeField] private Rigidbody TargetRb;
    private float yaw = 0f;
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] public CinemachineFramingTransposer cinemachine;
    public float minZoom;
    public float maxZoom;
    public float speedScroll = 5f;
    public bool StayCamera = false;
    public bool IsMobile;

    private void Start()
    {
        cinemachine = cam.GetCinemachineComponent<CinemachineFramingTransposer>();
        if (skin == null)
        {
            Player = Spawner.Spawner(Player);
            Player.PlayerCallback += PlayerUP;
            Player.Eat(0);
        }
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            return;
        }
        IsMobile = Application.isMobilePlatform;

        if (IsMobile)
        {
            _windowMobileController.OnCameraSwitch += SwitchCamera;
            _windowMobileController.Show();
        }
    }

    private void OnDestroy()
    {
        if (IsMobile)
        {
            _windowMobileController.OnCameraSwitch -= SwitchCamera;
        }
    }

    public void Zoom()
    {
        float scrollDelta = IsMobile ? -_windowMobileController.Zoom : Input.mouseScrollDelta.y;
        float newZoom;
        if (IsMobile)
        {
            newZoom = cinemachine.m_CameraDistance + (-scrollDelta) / 10f;
        }else
        {
            newZoom = cinemachine.m_CameraDistance + (-scrollDelta) * speedScroll;
        }   
        cinemachine.m_CameraDistance = Mathf.Clamp(newZoom, minZoom, maxZoom);
    }

    public void Update()
    {
        Zoom();
        if (Input.GetMouseButtonDown(1))
        {
            SwitchCamera();
        }

        Rotate();
        if (StayCamera)
        {
            if (skin == null)
            {
                Player.transform.localRotation = Quaternion.Euler(0, cam.transform.localRotation.eulerAngles.y, 0);
            }
            else
            {
                if (skin.skin.SOIndex == 5)
                {
                    skin.transform.localRotation = Quaternion.Euler(0, -180 + cam.transform.localRotation.eulerAngles.y, 0);
                    return;
                }
                skin.transform.localRotation = Quaternion.Euler(0, cam.transform.localRotation.eulerAngles.y, 0);
            }

        }
    }

    private void SwitchCamera()
    {
        StayCamera = !StayCamera;
        if (StayCamera)
        {
            cam.LookAt = TargetMove;
            var Pov = cam.AddCinemachineComponent<CinemachinePOV>();
            Pov.m_VerticalAxis.m_MinValue = 0f;
            Pov.m_VerticalAxis.m_MaxSpeed = 500f;
            Pov.m_VerticalAxis.Value = 25f;
            Pov.m_HorizontalAxis.m_MaxSpeed = 500f;
        }
        else
        {
            cam.LookAt = null;
            cam.DestroyCinemachineComponent<CinemachinePOV>();
            Quaternion newRotation = Quaternion.Euler(25, 0, 0);
            StartCoroutine(BackCamera(newRotation));
            if (skin == null)
            {
                Player.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                if(skin.skin.SOIndex == 5)
                {
                    skin.transform.localRotation = Quaternion.Euler(0, 180, 0);
                    return;
                }
                skin.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

        }
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            return;
        }
        Move();
        if ((IsMobile ? _windowMobileController.Move.y : Input.GetAxis("Vertical")) == 0)
        {
            if (skin == null)
            {
                if (Player.animator != null)
                {
                    Player.animator.SetBool("IsIdle", true);
                    Player.animator.SetBool("IsWalk", false);
                }
            }
            else
            {
                if (skin.animator != null)
                {
                    skin.animator.SetBool("IsIdle", true);
                    skin.animator.SetBool("IsWalk", false);
                }
            }
        }
    }

    public void Move() // Добавить разворот объекта
    {
        float verticalInput = IsMobile ? _windowMobileController.Move.y : Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.forward * verticalInput;
        if (skin == null)
        {
            TargetRb.velocity = moveDirection.normalized * Player.player.Speed;
        }
        else
        {
            TargetRb.velocity = moveDirection.normalized * skin.skin.Speed;
        }

        if (skin == null)
        {
            if (Player.animator != null)
            {
                Player.animator.SetBool("IsWalk", true);
                Player.animator.SetBool("IsIdle", false);
            }
        }
        else
        {
            if (skin.animator != null)
            {
                skin.animator.SetBool("IsWalk", true);
                skin.animator.SetBool("IsIdle", false);
            }
        }
    }

    public void Rotate()
    {
        float horizontalInput = IsMobile ? _windowMobileController.Rotate.x : Input.GetAxis("Horizontal");
        yaw = horizontalInput;
        if (skin == null)
        {
            TargetMove.Rotate(new Vector3(0, yaw * Player.player.RotationSpeed * Time.deltaTime, 0));
        }
        else
        {
            TargetMove.Rotate(new Vector3(0, yaw * skin.skin.RotationSpeed * Time.deltaTime, 0));
        }

    }

    public void PlayerUP()
    {
        Player.PlayerCallback -= PlayerUP;
        Spawner.DeSpawner(Player);
        Player player = Spawner.Spawner(Player);
        Player = player;
        cinemachine.m_TrackedObjectOffset = Player.player.CameraSetup;
        Player.PlayerCallback += PlayerUP;
        Player.Eat(0);
    }

    public IEnumerator BackCamera(Quaternion startPos)
    {
        while (startPos != transform.localRotation)
        {
            cam.transform.localRotation = Quaternion.Lerp(transform.localRotation, startPos, 10f * Time.deltaTime);
            yield return null;
        }
    }
}