using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Vampire.Control;


namespace Vampire.Control
{

    public class PlayerController : MonoBehaviour
    {
        NavMeshAgent nm;
        Vector3 destination;
        Mover move;
        // Start is called before the first frame update
        void Start()
        {
            nm = GetComponent<NavMeshAgent>();
            move = GetComponent<Mover>();
        }

        // Update is called once per frame
        void Update()
        {
            if (InteractWithMovement())
                return;
        }

        private bool InteractWithMovement()
        {
            RaycastHit moveHit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out moveHit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    move.StartMoveAction(moveHit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

    }
}
