using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverZeroStudios
{
    public class Planet : MonoBehaviour
    {
        public PlanetInfo planetInfo;
        public Cow cow;
        [HideInInspector]
        public Transform m_transform;
        [Header("Spawn")]
        public Transform[] spawnPoints;

        public void Attract(Transform body)
        {
            Vector3 gravUp = (body.position - m_transform.position).normalized;
            Vector3 bodyUp = body.up;

            body.GetComponent<Rigidbody>().AddForce(gravUp * planetInfo.gravity * 50);

           
            Quaternion tarRot = Quaternion.FromToRotation(bodyUp, gravUp) * body.rotation;
          
            body.rotation = Quaternion.Slerp(body.rotation, tarRot, 50 * Time.deltaTime);
        }

        Vector3 minScale;
        Vector3 maxScale;
        public void Awake()
        {
            m_transform = this.transform.GetChild(0);
        }


        IEnumerator Start()
        {

            minScale = planetInfo.minSize;
            maxScale = planetInfo.maxSize;
            minScale += transform.localScale;


            while (planetInfo.repeatable)
            {
                yield return LerpScale(maxScale, minScale, planetInfo.duration);
                yield return LerpScale(minScale, maxScale, planetInfo.duration);
               
            }
        }
        
        IEnumerator LerpScale(Vector3 a, Vector3 b, float time)
        {
            float i = 0.0f;
            float rate = (1.0f / time) * planetInfo.speed;
            while (i < 1.0f)
            {
                i += Time.deltaTime * rate;
                transform.localScale = Vector3.Lerp(a, b, i);
                yield return null;
            }
        }

    }
}
