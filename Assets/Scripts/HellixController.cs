using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HellixController : MonoBehaviour
{
    private Vector2 lastTapPosition;


    private Vector3 startRotation;

    public Transform topTransform;
    public Transform goalTransform;

    public GameObject helixLevelPrefab;

    public List<Stage> allStages = new List<Stage>();

    public float helixDistance;

    private List<GameObject> spawnedLevels = new List<GameObject>();


    private void Awake()
    {
        startRotation = transform.localEulerAngles;
        helixDistance = topTransform.localPosition.y - goalTransform.localPosition.y;

        LoadStage(0);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 currentTapPosition = Input.mousePosition;

            if (lastTapPosition == Vector2.zero)
            {
                lastTapPosition = currentTapPosition;
            }

            float distance = lastTapPosition.x - currentTapPosition.x;
            lastTapPosition = currentTapPosition;

            transform.Rotate(Vector3.up * distance);
        }

        if (Input.GetMouseButtonUp(0))
        {
            lastTapPosition = Vector2.zero;
        }
    }
    
    public void LoadStage(int stageNumber)
    {
        Stage stage = allStages[Mathf.Clamp(stageNumber, 0, allStages.Count - 1)];

        if (stage == null)
        {
            Debug.LogError("No se encontró el stage: ");
            return;
        }

        Camera.main.backgroundColor = allStages[stageNumber].stageBackgroundColor;

        FindFirstObjectByType<BallController>().GetComponent<Renderer>().material.color = allStages[stageNumber].stageBallColor;

        transform.localEulerAngles = startRotation;

        foreach (GameObject go in spawnedLevels)
        {
            Destroy(go);
        }

        float levelDistance = helixDistance / stage.levels.Count;

        float spawnPosY = topTransform.localPosition.y;

        for (int i = 0; i < stage.levels.Count; i++)
        {
            spawnPosY -= levelDistance;
            GameObject level = Instantiate(helixLevelPrefab, transform);
            level.transform.localPosition = new Vector3(0, spawnPosY, 0);

            spawnedLevels.Add(level);
        }
    }
}
