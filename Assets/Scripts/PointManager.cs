using NUnit.Framework;
using Unity.Netcode;
using UnityEngine;

public class PointManager : NetworkBehaviour
{
    public static PointManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void ScorePoints()
    {
           
    }



}
