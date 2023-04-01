using UnityEngine;
using TMPro;

public class FPS : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Update()
    {
        text.text = Mathf.FloorToInt(1f / Time.deltaTime).ToString();
    }
}