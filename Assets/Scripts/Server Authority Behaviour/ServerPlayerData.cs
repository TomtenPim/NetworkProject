using Unity.Netcode;
using UnityEngine;

public class ServerPlayerData : NetworkBehaviour
{
    public int m_id;
    public int m_networkId;
    [SerializeField] GameObject m_player;

    private void Awake()
    {

        m_networkId = m_player.GetComponent<NetworkObject>().GetInstanceID();
        PlayerManager.Instance.m_players.Add(m_player);
    }

}

