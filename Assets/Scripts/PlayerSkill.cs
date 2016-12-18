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


        public int DrinkingCapacty { get { return drinkingCapacity; } }
        public int DrinkingSpeed { get { return drinkingSpeed; } set { drinkingSpeed = value; } }
        public int RunningSpeed { get { return runningSpeed; } set { runningSpeed = value; } }
        public int ThrowingStrength { get { return throwingStrength; } set { throwingStrength = value; } }
        public bool Dead { get { return dead; } set { dead = value; } }
    }
}
