using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [Tooltip("How fast the boss moves from point to point.")]
    public float moveSpeed = 5f;

    [Tooltip("How long the boss waits at a destination before moving again.")]
    public float pauseDuration = 2f;

    private List<Vector3> possibleMoveTargetPositions = new List<Vector3>();

    private int lastPositionIndex = -1;

    void Start()
    {
        BossSpawnLocation location = FindAnyObjectByType<BossSpawnLocation>();
        if (location)
        {
            GameObject spawnLocationRef = location.gameObject;

            if (spawnLocationRef != null)
            {
                foreach (Transform child in spawnLocationRef.transform)
                {
                    possibleMoveTargetPositions.Add(child.position);
                }

                transform.position = spawnLocationRef.transform.position;

                if (possibleMoveTargetPositions.Count > 1)
                {
                    StartCoroutine(MoveRoutine());
                }
            }
        }
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            int newIndex;

            do newIndex = Random.Range(0, possibleMoveTargetPositions.Count);
            while (newIndex == lastPositionIndex);

            lastPositionIndex = newIndex;

            Vector3 targetPosition = possibleMoveTargetPositions[newIndex];

            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                yield return null;
            }

            transform.position = targetPosition;

            yield return new WaitForSeconds(pauseDuration);
        }
    }
}