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
        private bool isThrowingUp = false;
        private bool isSleeping = false;
        private int runningSpeed = 100;
        private int throwingStrength = 10;


        public int DrinkingCapacty { get; set; }
        public int DrinkingSpeed { get; set; }
        public int RunningSpeed { get; set; }
        public bool IsThrowingUp { get; set; }
        public bool IsSleeping { get; set; }
        public int ThrowingStrength { get; set; }

    }
}
