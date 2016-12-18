using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    public class PlayerSkill : NetworkBehaviour
    {
        private int drinkingCapacity = 100;
        private int drinkingSpeed = 100;
        private int runningSpeed = 100;
        private int throwingStrength = 10;
        private bool dead = false;


        public int DrinkingCapacty { get; set; }
        public int DrinkingSpeed { get; set; }
        public int RunningSpeed { get; set; }
        public int ThrowingStrength { get; set; }
        public bool Dead { get; set; }
    }
}
