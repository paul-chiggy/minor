using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletDamage = 50f;
    private float waited = 0f;
    public float WaitTime = 2f;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "knight")
        {
            other.gameObject.GetComponent<Knight>().TakeDamage(BulletDamage);
            _getMeToPool();
        }
        if(other == GameManager.Instance.MouseManager.MapCollider)
        {
            _getMeToPool();
        }
    }

    private void Update()
    {
        if (!this.gameObject.activeSelf) return;
        waited += Time.deltaTime;

        if(waited >= WaitTime) {
            _getMeToPool();
            waited = 0;
        }
    }

    private void _getMeToPool()
    {
        this.gameObject.SetActive(false);
        GameManager.Instance.ObjectPooler.PooledMap[ObjectPooler.UnitType.BULLET].Add(this.gameObject);
    }
}
