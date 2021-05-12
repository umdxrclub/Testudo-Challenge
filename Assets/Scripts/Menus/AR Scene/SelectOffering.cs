using System.Collections;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class SelectOffering : SimpleMenu<SelectOffering>
{
    public HorizontalScrollSnap scrollSnap;
    public float selectedItemScale = 1.0f;
    private int selectedIndex;

    private void Start()
    {
        StartCoroutine(ScreenSetupCoroutine());
    }

    private IEnumerator ScreenSetupCoroutine()
    {
        // Disable the objects before making the changes, so that they don't jump around the screen
        foreach (GameObject child in scrollSnap.ChildObjects)
        {
            child.SetActive(false);
        }

        // Wait for next frame, after default item initialization by the Horizontal Scroll Snap
        yield return null;

        foreach (GameObject child in scrollSnap.ChildObjects)
        {
            // Set the anchor and pivot to the center while keeping current position
            RectTransform childTransform = child.GetComponent<RectTransform>();
            Vector3 pos = childTransform.localPosition;
            childTransform.anchorMin = new Vector2(0.5f, 0.5f);
            childTransform.anchorMax = new Vector2(0.5f, 0.5f);
            childTransform.pivot = new Vector2(0.5f, 0.5f);
            childTransform.localPosition = pos;

            // If this is the selected item (i.e. the first child), adjust its scale
            if (child == scrollSnap.CurrentPageObject().gameObject)
            {
                child.transform.localScale = new Vector3(selectedItemScale, selectedItemScale, 1);
            }

            // We're done making changes, show the GameObject
            child.SetActive(true);

            // Wait short amount of time before moving to next child for dramatic opening effect
            yield return new WaitForSeconds(0.025f);
        }
    }

    public void UpdateSelectedItemUI(int index)
    {
        scrollSnap.ChildObjects[selectedIndex].transform.localScale = Vector3.one;
        selectedIndex = index;
        scrollSnap.ChildObjects[selectedIndex].transform.localScale = new Vector3(selectedItemScale, selectedItemScale, 1);
    }
}
