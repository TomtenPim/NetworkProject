using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class ServerPlayerShoot : NetworkBehaviour
{
    [SerializeField] public int projectileType = 0;
    [SerializeField] private Transform m_playerTransform;
    [SerializeField] private Vector2 m_playerPosition; 
    [SerializeField] private Vector2 m_mousePosition;

    private PlayerInput m_playerInput;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        m_playerInput = new();
        m_playerInput.Enable();
        m_playerInput.Player.Attack.started += OnAttackStarted;
    
        m_playerPosition = new Vector2(m_playerTransform.position.x, m_playerTransform.position.y);
    }

    private void OnAttackStarted(InputAction.CallbackContext context)
    {
        
        m_mousePosition = cam.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        

        if (IsServer && IsLocalPlayer)
        {
            m_playerPosition = new Vector2(m_playerTransform.position.x, m_playerTransform.position.y);
            Shoot(projectileType, m_playerPosition, m_mousePosition);
        }
        else if (IsClient && IsLocalPlayer)
        {
            ShootServerRPC(m_mousePosition);
        }
    }


    private void Shoot(int projectilePrefabId, Vector2 playerPossition, Vector2 targetPossition)
    {
        ProjectileManager.Instance.SpawnProjectile(projectilePrefabId, playerPossition, targetPossition);
        Debug.Log("Fungerar än");
    }

    [Rpc(SendTo.Server)]
    private void ShootServerRPC(Vector2 mousePosition)
    {
        m_playerPosition = new Vector2(m_playerTransform.position.x, m_playerTransform.position.y);
        Shoot(projectileType, m_playerPosition, mousePosition);
    }

}
