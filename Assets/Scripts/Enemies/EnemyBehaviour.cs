using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class EnemyBehaviour : MonoBehaviour
{
    public abstract void ApplyEffect(GameObject player);

    public abstract void Destroy();
}