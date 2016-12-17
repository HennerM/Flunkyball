using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Projectile : MonoBehaviour, IProjectile
    {
        public event ShotsFired OnShotFired;

        public void ShotFired()
        {
            if (this.OnShotFired != null)
            {
                this.OnShotFired();
            }
        }
    }
}
