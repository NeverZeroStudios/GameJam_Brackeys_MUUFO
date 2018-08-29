using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverZeroStudios
{
    [CreateAssetMenu(menuName = "NeverZeroStudios/Planet Infos/Create Planet Info")]
    public class PlanetInfo : ScriptableObject
    {
        [Header("Planet Scale")]
        public Vector3 scale;
        [Header("Lerp - From:To")]
        public Vector3 minSize;
        public Vector3 maxSize;
        public float speed = 5;
        public float duration = 6f;
        [HideInInspector]
        public bool repeatable = true;
        [Header("Material")]
        public Material planetMaterial;
        [Header("Physics")]
        public float gravity = -9.81f;
        

    }
}
