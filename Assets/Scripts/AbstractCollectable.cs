using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{

    public abstract class AbstractCollectable : NetworkBehaviour, ICollectable
    {
        [SyncVar]
        private GameObject holder = null;

        public abstract bool AllowedToCollect(Player player);
        public abstract bool CanDrop(Player player);
        public abstract void OnCollect(Player player);
        public abstract void OnDrop(Player player);

        public void Collect(Player entity)
        {
            CmdPaint(entity.gameObject);
            Debug.Log(this.holder);
            this.OnCollect(entity);
        }

        public void Drop(Player entity)
        {
            this.holder = null;
            this.OnDrop(entity);
        }


        public virtual void Update()
        {
            if (this.holder != null)
            {
                var camera = holder.GetComponent<PlayerController>().cam;
                var position = camera.transform.forward;
                position.x *= 2f;
                position.z *= 2f;
                this.transform.position = position + camera.transform.position;
            }
        }

        [ClientRpc]
        void RpcPaint(GameObject obj)
        {
            Debug.Log("Rpc");
            this.holder = obj; // this is the line that actually makes the change in color happen
        }

        [Command]
        void CmdPaint(GameObject obj)
        {
            Debug.Log("Cmd");
//            var objNetId = obj.GetComponent<NetworkIdentity>();        // get the object's network ID
//            objNetId.AssignClientAuthority(connectionToClient);    // assign authority to the player who is changing the color
            RpcPaint(obj);                                    // usse a Client RPC function to "paint" the object on all clients
//            objNetId.RemoveClientAuthority(connectionToClient);    // remove the authority from the player who changed the color
        }


    }


}
