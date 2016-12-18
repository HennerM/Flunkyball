using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.Networking;
using System;

namespace Assets.Scripts
{

    public class Target : AbstractCollectable
    {

        private bool onGround = false;

        private Rigidbody rb;

        public event TargetFellDown targetFellDown;

        public override bool AllowedToCollect(Player player)
        {
            return true;
            //return this.onGround && GameManager.instance.gameState == GameManager.State.HIT && player.CollectedObject == null;
        }

        public override bool CanDrop(Player player)
        {
            return true;
        }

        public override void OnCollect(Player player)
        {
            
        }

        public override void OnDrop(Player player)
        {
            // logik prüfen.
        }

        // Use this for initialization

        // Update is called once per frame
        void Update()
        {

            if (Mathf.Abs(this.transform.rotation.z) > 0.5)
            {
                if (targetFellDown != null)
                {
                    targetFellDown();
                }
                this.onGround = true;
            }

            //if (this.transform.rotation)

        }
    }
}
