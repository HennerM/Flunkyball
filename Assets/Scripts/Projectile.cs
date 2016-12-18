using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    public class Projectile : NetworkBehaviour
    {
        public event ShotsFired OnShotFired;

        public void FireShot(Vector3 transformVector)
        {
            var rb = GetComponent<Rigidbody>();
            rb.AddForce(transformVector, ForceMode.Force);
            rb.useGravity = true;

            if (this.OnShotFired != null)
            {
                this.OnShotFired();
            }
        }
    }
}
