using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindGround : MonoBehaviour {

    public bool lerpInit;
    private void Awake()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -transform.up,out hit, Mathf.Infinity))
        {
            transform.position = Vector3.Lerp(transform.position, hit.point, 2 * Time.deltaTime);
            
        }
        lerpInit = true;
    }

    private void Update()
    {
        if(lerpInit)
            Destroy(this);
    }
}
