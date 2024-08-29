using UnityEngine;
using TMPro;

public class InfoText : View
{
    [field: SerializeField] public TextMeshProUGUI Title { get; private set; }

    public void Set(string text)
    {
        Title.SetText(text);
    }
}
