using UnityEngine;
using Unity.Netcode;

public class ServerPlayerMovement : NetworkBehaviour
{

    [SerializeField] private float m_playerSpeed;
    [SerializeField] private Transform m_playerTransform;

    public CharacterController m_characterController;
    private PlayerInput m_playerInput;

    private void Awake()
    {
        m_playerInput = new();
        m_playerInput.Enable();
    }

    private void Update()
    {
        Vector2 moveInput = m_playerInput.Player.Move.ReadValue<Vector2>();

        if (IsServer && IsLocalPlayer)
        {
            Move(moveInput);
        }
        else if (IsClient && IsLocalPlayer)
        {
            MoveServerRPC(moveInput);
        }

    }

    private void Move(Vector2 input)
    {
        Vector2 calcMove = input.x * m_playerTransform.right + input.y * m_playerTransform.up;

        m_characterController.Move(calcMove * m_playerSpeed* Time.deltaTime);
    }

    [Rpc(SendTo.Server)]
    private void MoveServerRPC(Vector2 input)
    {
        Move(input);
    }

}
