using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceTestudo : SimpleMenu<PlaceTestudo>
{
    [Header("Asset References")]
    public GameObject testudoPrefab;
    public GameObject testudoOnBlockPrefab;
    public Material previewMaterial;

    [Header("Switching Thresholds")]
    public float maxHeight;
    public float maxDistance;

    private ARRaycastManager m_RaycastManager;
    private List<ARRaycastHit> m_Hits;
    private Vector2 screenCenter;
    private Button placeButton;
    private GameObject currentPreviewObject;
    private bool isTestudoOnBlock;

    // Start is called before the first frame update
    void Start()
    {
        m_RaycastManager = (ARRaycastManager) FindObjectOfType(typeof(ARRaycastManager));
        m_Hits = new List<ARRaycastHit>();
        screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        placeButton = GetComponentInChildren<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if user is pointing towards a plane
        if (m_RaycastManager.Raycast(screenCenter, m_Hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = m_Hits[0].pose;

            float heightDiff = Mathf.Abs(Camera.main.transform.position.y - hitPose.position.y);
            float distanceDiff = Vector3.Distance(Camera.main.transform.position, hitPose.position);

            // If a user is close to the hit point, show the turtle by itself (testudoPrefab).
            // Otherwise show the full statue (testudoOnBlockPrefab).
            if (heightDiff < maxHeight && distanceDiff < maxDistance)
            {
                if (currentPreviewObject != null && isTestudoOnBlock)
                {
                    Destroy(currentPreviewObject);
                    currentPreviewObject = null;
                }

                if (currentPreviewObject == null)
                {
                    currentPreviewObject = Instantiate(testudoPrefab, hitPose.position, Quaternion.identity);
                    ApplyPreviewMaterial();
                    isTestudoOnBlock = false;
                }
            }
            else
            {
                if (currentPreviewObject != null && !isTestudoOnBlock)
                {
                    Destroy(currentPreviewObject);
                    currentPreviewObject = null;
                }

                if (currentPreviewObject == null)
                {
                    currentPreviewObject = Instantiate(testudoOnBlockPrefab, hitPose.position, Quaternion.identity);
                    ApplyPreviewMaterial();
                    isTestudoOnBlock = true;
                }
            }

            // Set preview object's position to the point user is looking at
            currentPreviewObject.transform.position = hitPose.position;

            // Make preview object face the user
            Vector3 directionToCamera = Camera.main.transform.position - currentPreviewObject.transform.position;
            directionToCamera.y = 0.0f;
            currentPreviewObject.transform.rotation = Quaternion.LookRotation(directionToCamera);

            // Enable placement fucntionality
            placeButton.interactable = true;
        }
        else
        {
            // Disable placement functionality
            placeButton.interactable = false;

            // Destroy the preview object, if it exists
            if (currentPreviewObject != null)
            {
                Destroy(currentPreviewObject);
                currentPreviewObject = null;
            }
        }
    }

    private void ApplyPreviewMaterial()
    {
        MeshRenderer[] renderers = currentPreviewObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.material = previewMaterial;
        }
    }

    public void Place()
    {
        GameObject objToPlace = isTestudoOnBlock ? testudoOnBlockPrefab : testudoPrefab;
        Instantiate(objToPlace, currentPreviewObject.transform.position, currentPreviewObject.transform.rotation);
        Destroy(currentPreviewObject);
        // Show Next Page
    }

    public override void OnBackPressed()
    {
    }
}
