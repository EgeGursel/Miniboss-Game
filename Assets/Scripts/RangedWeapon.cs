using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    // ROTATE AIM DIRECTION (RANGED)
    private Transform player;
    private Vector3 mousePos;
    private Vector3 objectPos;
    private float angle;

    private void Start()
    {
        player = transform.parent.transform;
    }
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

        objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (player.rotation.y < 0)
        {
            if (angle < -120 || angle > 120)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }
        else
        {
            if (angle < 60 && angle > -60)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }
    }
}
