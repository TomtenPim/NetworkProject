using System.Collections;
using Unity.Netcode;
using Unity.Services.Vivox;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;
public class Projectile : NetworkBehaviour
{
    private float m_speed = -15;
    public Vector2 m_velocity;
    private bool m_colliding;
    private bool m_justSpawned = true;
    [SerializeField] private int m_bounces = 6;

    private void Awake()
    {
        StartCoroutine(NoLongerJustSpawnedAfterXSeconds(0.2f));
    }

    IEnumerator NoLongerJustSpawnedAfterXSeconds(float Xseconds)
    {
        yield return new WaitForSeconds(Xseconds);
        m_justSpawned = false;
    }



    public virtual void UpdateMovement()
    {
        transform.position += new Vector3(m_velocity.normalized.x, m_velocity.normalized.y, 0) * m_speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {



        if (collision.gameObject.CompareTag("Wall"))
        {

            BounceOnBorder(collision);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (m_justSpawned)
            {
                Debug.Log("Hit prevented due to JustSpawened");
                return;
            }
            HitPlayer(collision);
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            HitBullet(collision);
        }
    }

    public void BounceOnBorder(Collision2D collision)
    {
        Debug.Log("Väggstuds");

        Vector2 collitionSide = ((collision.collider.ClosestPoint(this.transform.position)) - (new Vector2(this.transform.position.x, this.transform.position.y))).normalized;

        m_bounces--;

        if (m_bounces <= 0)
        {
            Destroy(gameObject);
            ProjectileManager.Instance.projectiles.Remove(this);
        }

        if (collitionSide.x > 1 / Mathf.Sqrt(2))
        {
            m_velocity.x = Mathf.Abs(m_velocity.x);
        }
        else if (collitionSide.x < -1 / Mathf.Sqrt(2))
        {
            m_velocity.x = -Mathf.Abs(m_velocity.x);
        }

        if (collitionSide.y > 1 / Mathf.Sqrt(2))
        {
            m_velocity.y = Mathf.Abs(m_velocity.y);
        }
        else if (collitionSide.y < -1 / Mathf.Sqrt(2))
        {
            m_velocity.y = -Mathf.Abs(m_velocity.y);
        }
    }

    private void HitBullet(Collision2D collision)
    {
        Debug.Log("Kulträff");

        Destroy(gameObject);
        ProjectileManager.Instance.projectiles.Remove(this);
    }

    private void HitPlayer(Collision2D collision)
    {
        Debug.Log("Spelarträff");

        Destroy(gameObject);
        ProjectileManager.Instance.projectiles.Remove(this);

        if (IsServer && IsLocalPlayer)
        {
            KillPlayer(collision.gameObject.GetComponent<NetworkObject>().GetInstanceID());
        }
        else if (IsClient && IsLocalPlayer)
        {
            KillPlayerRPC(collision.gameObject.GetComponent<NetworkObject>().GetInstanceID());
        }


        
    }

    private void KillPlayer(int playerID)
    {
        //NetworkServer.FindLocalObject(playerId);
    }

    [Rpc(SendTo.Server)]
    private void KillPlayerRPC(int playerID)
    {
        KillPlayer(playerID);
    }
}


