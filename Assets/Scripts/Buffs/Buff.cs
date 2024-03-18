using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Paddle")
            ApplyBuffEffect();

        if (other.gameObject.tag == "DestroyWall" || other.gameObject.tag == "Paddle")
        {
            BuffsManager.Instance.spawnedBuffs.Remove(this);
            Destroy(gameObject);
        }
    }

    protected abstract void ApplyBuffEffect();
}
