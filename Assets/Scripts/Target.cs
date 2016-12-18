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
            Debug.Log("allowed to collect?");
            Debug.Log(GameManager.instance.GameState);
            return GameManager.instance.GameState == GameManager.State.HIT && player.CollectedObject == null;
        }

        public override bool CanDrop(Player player)
        {
            return Input.GetKeyUp("f");
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
