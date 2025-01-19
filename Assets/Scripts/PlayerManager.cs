using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
    public static PlayerManager Instance;
    public List<GameObject> m_players = new List<GameObject>();
    void Awake()
    {
        Instance = this;
    }

    public void CheckRemainingPlayercount()
    {
        int remaingingPlayers = 0;

        foreach (GameObject p in m_players)
        {
            if (p.activeSelf)
            {
                remaingingPlayers++;
            }
        }
        
        if (remaingingPlayers <= 1)
        {
            CheckWinnerOfROund();  
        }

    }

    public void CheckWinnerOfROund()
    {

    }
}
