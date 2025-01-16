using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;

public class ProjectileManager : NetworkBehaviour
{
    public static ProjectileManager Instance;
    public List<Projectile> projectiles = new List<Projectile>();
    [SerializeField] List<Projectile> projectilePrefab;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        foreach (Projectile p in projectiles)
        {
            p.UpdateMovement();
        }

    }
    public void SpawnProjectile(int projectilePrefabId, Vector2 position, Vector2 target)
    {
        Projectile newProjectile = Instantiate(projectilePrefab[projectilePrefabId], position, Quaternion.identity, transform);
        //NetworkObject newProjectile = NetworkManager.SpawnManager.InstantiateAndSpawn(projectilePrefab[projectilePrefabId]);
        newProjectile.m_velocity = position - target;

        newProjectile.GetComponent<NetworkObject>().Spawn(true);

        //var networkObject = NetworkManager.SpawnManager.InstantiateAndSpawn(projectilePrefab[projectilePrefabId]);
        projectiles.Add(newProjectile);
    }
}
