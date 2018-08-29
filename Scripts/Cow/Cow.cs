using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverZeroStudios
{
    public class Cow : MonoBehaviour
    {

        public Planet planet;

        [HideInInspector]
        public Transform m_transform;
        public Transform mesh;
        Rigidbody rigi;
        public float moveSpeed = 7;
        public bool isAlive;
        float moveAmount;
        GameManager gm;

        Vector3 moveDir;
        public void Init()
        {
            gm = FindObjectOfType<GameManager>();

            planet = FindObjectOfType<Planet>();

            m_transform = transform;
            for (int i = 0; i < planet.spawnPoints.Length; i++)
            {
                int index = Random.Range(0, planet.spawnPoints.Length - 1);
                m_transform.position = planet.spawnPoints[index].position;

            }

            rigi = GetComponent<Rigidbody>();

        }

        public bool onGround;
        bool OnGround()
        {
            Vector3 origin = m_transform.position;

            Vector3 dir = -m_transform.up;
            Debug.DrawRay(origin, dir, Color.red);

            float dis = 0.5f;
            RaycastHit hit;

            if (Physics.Raycast(origin, dir, out hit, dis))
            {

                return true;
            }

            return false;
        }

      
        public void Tick(float delta)
        {
            onGround = OnGround();
            planet.Attract(m_transform);

            if (!isAlive)
            {
                gm.GameOver(mesh.gameObject,rigi);
                return;
            }

            if (!onGround)
            {
                gameObject.AddComponent<FindGround>();
                return;
            }
           
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            moveDir = new Vector3(h, 0, v).normalized;
            

        }

        

        public void FixedTick(float delta)
        {
            rigi.MovePosition(rigi.position + m_transform.TransformDirection(moveDir) * moveSpeed * delta);
        }


    }
}
