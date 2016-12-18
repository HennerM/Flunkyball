using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    public abstract class AbstractCollectable : NetworkBehaviour, ICollectable
    {
        public abstract bool AllowedToCollect(Player player);
        public abstract bool CanDrop(Player player);
        public abstract void OnCollect(Player player);
        public abstract void OnDrop(Player player);
    }
}
