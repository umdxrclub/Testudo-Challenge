using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HyperlinkOpener : MonoBehaviour, IPointerClickHandler
{
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Method based on post by Jonathan on Delta Dream
    // https://deltadreamgames.com/unity-tmp-hyperlinks/
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.GetTouch(0).position, null);
        if (linkIndex != -1)
        { // was a link clicked?
            TMP_LinkInfo linkInfo = text.textInfo.linkInfo[linkIndex];

            // open the link id as a url, which is the metadata we added in the text field
            Application.OpenURL(linkInfo.GetLinkID());
        }
    }
}