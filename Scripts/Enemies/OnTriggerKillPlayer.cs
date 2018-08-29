using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverZeroStudios
{
    public class OnTriggerKillPlayer : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Cow")
            {
                other.GetComponent<Cow>().isAlive = false;
            }
        }
    }
}
