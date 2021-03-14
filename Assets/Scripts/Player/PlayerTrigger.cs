using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!transform.parent)
            return;
        
        GameObject collider = collision.gameObject;
        collider.GetComponent<EnemyBehaviour>().ApplyEffect(transform.parent.gameObject);
    }
}
