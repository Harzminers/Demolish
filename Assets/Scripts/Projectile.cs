using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject _deathEffect;
    [SerializeField] GameObject _bulletHole;
    [SerializeField] LayerMask _instantProjectileDestruction;
    void OnCollisionEnter(Collision collision)
    {
        if (_instantProjectileDestruction == (_instantProjectileDestruction | (1 << collision.gameObject.layer)))
        {
            if (_deathEffect != null)
            {
                Instantiate(_deathEffect,
                    transform.position,
                    Quaternion.identity);
            }

            //GameObject BulletHole = Instantiate(_bulletHole,
            //    collision.GetContact(0).point,
            //    Quaternion.identity);

            //BulletHole.transform.rotation *= Quaternion.FromToRotation(BulletHole.transform.up, collision.GetContact(0).normal);

            Destroy(gameObject);
        }
        IProjectileTarget target = collision.gameObject.GetComponent<IProjectileTarget>();
        if (target != null) 
        {
            target.OnProjectileHit();
        }  
    }
}
