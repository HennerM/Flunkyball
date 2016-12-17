using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{

    public delegate void ShotsFired();

    public interface IProjectile
    {

        void ShotFired();

        event ShotsFired OnShotFired;
    }
}
