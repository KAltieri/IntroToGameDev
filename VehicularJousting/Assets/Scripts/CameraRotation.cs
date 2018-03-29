using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    public float rotation = .01f;
    public Transform rotateAroundObj;

    Vector3 rotateAround;
    int interval = 0;

    // Use this for initialization
    void Start()
    {
        rotateAround = rotateAroundObj.position;
        transform.position = new Vector3(rotateAround.x, transform.position.y, rotateAround.z - 15);
    }

    // Update is called once per frame
    void Update()
    {
        if (rotation > 1)
        {
            if (interval > 2)
            {
                interval = 0;
            }
            else
            {
                interval++;
            }
        }
        Debug.Log(rotation);
        switch (interval)
        {
            case 1:
                transform.RotateAround(rotateAround, Vector3.zero, 0);
                break;
            case 0:
                transform.RotateAround(rotateAround, Vector3.up, rotation);
                break;
                //    case 3:
                //        rotation += Time.deltaTime * 2;
                //        transform.RotateAround(rotateAround, Vector3.right, rotation);
                //        break;
                //    case 4:
                //        rotation += Time.deltaTime * 2;
                //        transform.RotateAround(rotateAround, Vector3.right, rotation);
                //        break;
                //    case 5:
                //        rotation += Time.deltaTime * 2;
                //        transform.RotateAround(rotateAround, Vector3.right, rotation);
                //        break;
                //}
        }
    }
}
