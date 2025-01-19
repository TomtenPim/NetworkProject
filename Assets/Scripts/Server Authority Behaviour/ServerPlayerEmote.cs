using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Threading;
using System.Collections;
using System.Net.Sockets;

public class ServerPlayerEmote : NetworkBehaviour
{
    [SerializeField] private Transform m_playerTransform;
    [SerializeField] private Vector2 m_playerPosition;

    [SerializeField] List<GameObject> Emote;

    private PlayerInput m_playerInput;

    private void Awake()
    {
        m_playerInput = new();
        m_playerInput.Enable();
        m_playerInput.Player.Angry.started += OnAngryStarted;
        m_playerInput.Player.Happy.started += OnHappyStarted;
        m_playerInput.Player.Shocked.started += OnShockedStarted;
    }

    IEnumerator WaitforXSecondsToDespawnGameObject(float Xseconds, GameObject gameObject)
    {
        yield return new WaitForSeconds(Xseconds);
        gameObject.GetComponent<NetworkObject>().Despawn(true);
    }

    /*
    private void OnAngryStarted(InputAction.CallbackContext context)
    {
        m_playerPosition = new Vector2(m_playerTransform.position.x, m_playerTransform.position.y);
        if (IsServer && IsLocalPlayer)
        {
            SpawnAngry(m_playerPosition);
        }
        else if (IsClient && IsLocalPlayer)
        {
            SpawnAngryRPC(m_playerPosition);
        }
    }

    private void SpawnAngry(Vector2 playerPossition)
    {
        GameObject Angry = Instantiate(Emote[0], new Vector3(playerPossition.x, playerPossition.y, 0), Quaternion.identity, transform);
        
        Angry.GetComponent<NetworkObject>().Spawn(true);
        Debug.Log("Angry");
        StartCoroutine(WaitforXSecondsToDespawnGameObject(1, Angry));
    }


    [Rpc(SendTo.Server)]
    private void SpawnAngryRPC(Vector2 playerPossition)
    {
        m_playerPosition = new Vector2(m_playerTransform.position.x, m_playerTransform.position.y);
        SpawnAngry(playerPossition);
    }









    private void OnHappyStarted(InputAction.CallbackContext context)
    {
        m_playerPosition = new Vector2(m_playerTransform.position.x, m_playerTransform.position.y);
        if (IsServer && IsLocalPlayer)
        {
            SpawnHappy(m_playerPosition);
        }
        else if (IsClient && IsLocalPlayer)
        {
            SpawnHappyRPC(m_playerPosition);
        }
    }

    private void SpawnHappy(Vector2 playerPossition)
    {
        GameObject Happy = Instantiate(Emote[1], new Vector3(playerPossition.x, playerPossition.y, 0), Quaternion.identity, transform);

        Happy.GetComponent<NetworkObject>().Spawn(true);
        Debug.Log("Happy");
        StartCoroutine(WaitforXSecondsToDespawnGameObject(1, Happy));
    }


    [Rpc(SendTo.Server)]
    private void SpawnHappyRPC(Vector2 playerPossition)
    {
        m_playerPosition = new Vector2(m_playerTransform.position.x, m_playerTransform.position.y);
        SpawnHappy(playerPossition);
    }


    private void OnShockedStarted(InputAction.CallbackContext context)
    {
        m_playerPosition = new Vector2(m_playerTransform.position.x, m_playerTransform.position.y);
        if (IsServer && IsLocalPlayer)
        {
            SpawnShocked(m_playerPosition);
        }
        else if (IsClient && IsLocalPlayer)
        {
            SpawnShockedRPC(m_playerPosition);
        }
    }

    private void SpawnShocked(Vector2 playerPossition)
    {
        GameObject Shocked = Instantiate(Emote[2], new Vector3(playerPossition.x, playerPossition.y, 0), Quaternion.identity, transform);

        Shocked.GetComponent<NetworkObject>().Spawn(true);
        Debug.Log("Shocked");
        StartCoroutine(WaitforXSecondsToDespawnGameObject(1, Shocked));
    }


    [Rpc(SendTo.Server)]
    private void SpawnShockedRPC(Vector2 playerPossition)
    {
        m_playerPosition = new Vector2(m_playerTransform.position.x, m_playerTransform.position.y);
        SpawnShocked(playerPossition);
    }

    */

    private void OnAngryStarted(InputAction.CallbackContext context)
    {
        m_playerPosition = new Vector2(m_playerTransform.position.x, m_playerTransform.position.y);
        if (IsServer && IsLocalPlayer)
        {
            SpawnEmote(m_playerPosition, 0);
        }
        else if (IsClient && IsLocalPlayer)
        {
            SpawnEmoteRPC(m_playerPosition, 0);
        }
    }
    private void OnHappyStarted(InputAction.CallbackContext context)
    {
        m_playerPosition = new Vector2(m_playerTransform.position.x, m_playerTransform.position.y);
        if (IsServer && IsLocalPlayer)
        {
            SpawnEmote(m_playerPosition, 1);
        }
        else if (IsClient && IsLocalPlayer)
        {
            SpawnEmoteRPC(m_playerPosition, 1);
        }
    }
    private void OnShockedStarted(InputAction.CallbackContext context)
    {
        m_playerPosition = new Vector2(m_playerTransform.position.x, m_playerTransform.position.y);
        if (IsServer && IsLocalPlayer)
        {
            SpawnEmote(m_playerPosition, 2);
        }
        else if (IsClient && IsLocalPlayer)
        {
            SpawnEmoteRPC(m_playerPosition, 2);
        }
    }

    private void SpawnEmote(Vector2 playerPossition, int EmotePrefabID)
    { 
        GameObject emote = Instantiate(Emote[EmotePrefabID], new Vector3(playerPossition.x, playerPossition.y, 0), Quaternion.identity, transform);
        emote.GetComponent<NetworkObject>().Spawn(true);
        Debug.Log("Angry");
        StartCoroutine(WaitforXSecondsToDespawnGameObject(1, emote));
    }

    [Rpc(SendTo.Server)]
    private void SpawnEmoteRPC(Vector2 playerPossition, int EmotePrefabID)
    {
        m_playerPosition = new Vector2(m_playerTransform.position.x, m_playerTransform.position.y);
        SpawnEmote(playerPossition, EmotePrefabID);
    }


}

