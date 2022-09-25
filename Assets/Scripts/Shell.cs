using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] private float maxLifeTime = 3f;
    float lifeTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(GameTags.ENEMY))
        {
            EnemyController eController = other.GetComponent<EnemyController>();
            eController.TakeDamage(GameController.Instance.ShellDamage);
        }

        Deactivate();
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            lifeTime += Time.deltaTime;

            if (lifeTime >= maxLifeTime)
                Deactivate();
        }
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
        lifeTime = 0;
    }
}
