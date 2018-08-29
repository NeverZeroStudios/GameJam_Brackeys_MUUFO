using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverZeroStudios
{
    public class CameraHolder : MonoBehaviour
    {
       
        public Camera _camera;
        Transform m_transform;
        public Cow cow;
        
        float followSpeed = 4;
        float damp = 0.1f;

        public void Init()
        {
            m_transform = this.transform;

        }

        public void Tick(float delta)
        {
            Vector3 targetPosition = cow.m_transform.position;
            Vector3 lerpPostions = Vector3.Lerp(m_transform.position, targetPosition, (followSpeed * damp));
            m_transform.position = lerpPostions;


            m_transform.LookAt(cow.transform);
        }

    }
}

