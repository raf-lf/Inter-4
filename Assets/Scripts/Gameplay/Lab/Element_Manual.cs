using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element_Manual : LabElement
{
    public RectTransform contentParent;
    public GameObject currentContent;

    public override void CloseElement()
    {
        base.CloseElement();

        if (currentContent != null)
            Destroy(currentContent);
    }

    public void SetContent(GameObject content)
    {
        if (currentContent != null)
            Destroy(currentContent);
        
        if (content != null)
            currentContent = Instantiate(content, contentParent);

    }
}
