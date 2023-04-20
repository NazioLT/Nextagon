using UnityEngine;
using Nazio_LT.Tools.UI;
using TMPro;

[System.Serializable]
public class Power
{
    [SerializeField] private NButton button;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private int count = 0;

    private System.Action powerCallBack;

    public virtual void Init(HexagonGrid _grid, System.Action _callback)
    {
        powerCallBack = _callback;
        _grid.onUpdateDisplay += UpdateDisplay;

        button.onClick.AddListener(TryUse);
        UpdateDisplay();
    }

    public void GainCount() => count++;
    public void RemoveCount(int _count = 1) => count -= _count;
    public void UpdateDisplay()
    {
        countText.text = count.ToString();
    }

    private void TryUse()
    {
        if (count <= 0) return;

        powerCallBack();
    }

    public int Count => count;
}
