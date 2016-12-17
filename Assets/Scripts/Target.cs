using UnityEngine;
using System.Collections;
using Assets.Scripts;
namespace Assets.Scripts
{

    public class Target : MonoBehaviour, ITarget
    {

        private Rigidbody rb;

        public event TargetFellDown targetFellDown;

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
            }

            //if (this.transform.rotation)

        }
    }
}
