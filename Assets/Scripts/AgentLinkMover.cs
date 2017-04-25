using UnityEngine;
using System.Collections;

public enum OffMeshLinkMoveMethod
{
    Parabola
}

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class AgentLinkMover : MonoBehaviour
{
    public float height, duration;
    public OffMeshLinkMoveMethod method = OffMeshLinkMoveMethod.Parabola;
    IEnumerator Start()
    {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.autoTraverseOffMeshLink = false;

        while (true)
        {
            if (agent.isOnOffMeshLink)
            {
                if (method == OffMeshLinkMoveMethod.Parabola)
                    yield return StartCoroutine(Parabola(agent, height, duration));
                agent.CompleteOffMeshLink();
            }
            yield return null;
        }
    }

    IEnumerator Parabola(UnityEngine.AI.NavMeshAgent agent, float height, float duration)
    {
        UnityEngine.AI.OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
        float normalizedTime = 0.0f;
        while (normalizedTime < 1.0f)
        {
            float yOffset = height * 4.0f * (normalizedTime - normalizedTime * normalizedTime);
            agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
    }
}