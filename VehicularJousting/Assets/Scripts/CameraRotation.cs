using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
	[Header("Rotation Settings")]
    public float rotation = .01f;
    public Transform rotateAroundObj;

    Vector3 rotateAround;
//    int interval = 0;

	//Used for the menus - camera rotates around the point

    void Start()
    {
        rotateAround = rotateAroundObj.position;
        transform.position = new Vector3(rotateAround.x, transform.position.y, rotateAround.z - 15);
    }

    void Update()
    {
        //Rotates the camera around a gameObject from the scene
		transform.RotateAround(rotateAround, Vector3.up, rotation);

		//Different camera rotations - didn't use


//        if (rotation > 1)
//        {
//            if (interval > 2)
//            {
//                interval = 0;
//            }
//            else
//            {
//                interval++;
//            }
//        }
//        switch (interval)
//        {
//            case 1:
//                transform.RotateAround(rotateAround, Vector3.zero, 0);
//                break;
//            case 0:
//                transform.RotateAround(rotateAround, Vector3.up, rotation);
//                break;
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
//        }
    }
}
