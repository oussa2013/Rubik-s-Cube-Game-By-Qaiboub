using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    Vector3 localRetation = new Vector3(-90,0,0);
    bool cameraDisabled = false,
         retateDisabled = false;
    public CubeManager cubeMang;
    List<GameObject> pieces = new List<GameObject>(),
                     planes = new List<GameObject>();


	void LateUpdate ()
    {
        if (Input.GetMouseButton(0))
        {
            if (!retateDisabled)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if(Physics.Raycast(ray,out hit, 100))
                {
                    cameraDisabled = true;

                    if(pieces.Count<2 && !pieces.Exists(x => x == hit.collider.transform.parent.gameObject) &&
                       hit.transform.parent.gameObject != cubeMang.gameObject)
                    {
                        pieces.Add(hit.collider.transform.parent.gameObject);
                        planes.Add(hit.collider.gameObject);
                    }
                    else if(pieces.Count == 2)
                    {
                        cubeMang.detectRetate(pieces, planes);
                        retateDisabled = true;
                    }
                }
            }

            if (!cameraDisabled)
            {
                retateDisabled = true;
                localRetation.x += Input.GetAxis("Mouse X") * 5;
                localRetation.y += Input.GetAxis("Mouse Y") * -5;
                localRetation.y = Mathf.Clamp(localRetation.y, -90, 90);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            pieces.Clear();
            planes.Clear();
            cameraDisabled = retateDisabled = false;
        }

        Quaternion qtr = Quaternion.Euler(localRetation.y, localRetation.x, 0);
        transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, qtr, Time.deltaTime * 5);
		
	}
}
