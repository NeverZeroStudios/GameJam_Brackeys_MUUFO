using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverZeroStudios
{
    public class UFOController : MonoBehaviour
    {
        [HideInInspector]
        public Transform target;
        Transform m_transform;
        public Planet planet;
        public float moveSpeed = 4;

        void Awake()
        {
            
            m_transform = transform;
            planet = FindObjectOfType<Planet>();
            for (int i = 0; i < planet.spawnPoints.Length; i++)
            {
                int index = Random.Range(0, planet.spawnPoints.Length - 1);
                m_transform.position = planet.spawnPoints[index].position;

            }
            target = FindObjectOfType<Cow>().transform;
            Destroy(this.gameObject, 15f);
        }


        void Update()
        {
            //onGround = OnGround();
            if (planet)
            {
                planet.Attract(m_transform);
            }
            else
            {
                Debug.Log("missing attracter");
            }
          
        }

        private void FixedUpdate()
        {
            if (target == null)
                return;
            float dist = Vector3.Distance(m_transform.position, target.transform.position);
            if (dist < 90 && dist > 0.1f)
            {
                Vector3 targetPosition = Vector3.Lerp(m_transform.position, target.transform.position, moveSpeed * 0.2f * Time.deltaTime);
                m_transform.position = targetPosition;
            }
            
        }

        //public bool onGround;
        //bool OnGround()
        //{
        //    Vector3 origin = m_transform.position;
        //    Vector3 dir = -m_transform.up;
        //    Debug.DrawRay(origin, dir, Color.red);

        //    float dis = 8f;
        //    RaycastHit hit;

        //    if (Physics.Raycast(origin, dir, out hit, dis))
        //    {
        //        m_transform.position = hit.point;
        //        return true;
        //    }

        //    return false;
        //}
    }
}
