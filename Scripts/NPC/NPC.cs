using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private List<Transform> listPointsForMove;

    private Vector3 nowDestination;

    private bool inWait;

    private void Start()
    {
        nowDestination = listPointsForMove[Random.Range(0, listPointsForMove.Count)].position;
        agent.SetDestination(nowDestination);
    }

    private void Update()
    {

        if(Vector3.Distance(agent.gameObject.transform.position, nowDestination) > 0.05f)
        {   
            if(animator)
                animator?.SetTrigger("Walk");
        }
        else
        {
            if (animator)
                animator?.SetTrigger("Idle");

            if(!inWait)
                StartCoroutine(HandlerWalk());
        }
    }

    private void SetNewRandomDestination()
    {
        nowDestination = listPointsForMove[Random.Range(0, listPointsForMove.Count)].position;
        agent.SetDestination(nowDestination);
    }

    private IEnumerator HandlerWalk()
    {
        inWait = true;
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        SetNewRandomDestination();
        inWait = false;
    }


}
