using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothspeed = 0.125f;

    private void LateUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        if (target != null)
        {
            if (target == PlayerManager.instance)
            {
                if (!PlayerManager.instance.isAlive)
                    return;
            }

            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
    }
}
