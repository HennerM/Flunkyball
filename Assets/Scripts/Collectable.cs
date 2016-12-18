using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public interface ICollectable
    {

        void OnCollect(Player player);

        void OnDrop(Player player);

        bool AllowedToCollect(Player player);
        
        bool CanDrop(Player player);
    }
}
