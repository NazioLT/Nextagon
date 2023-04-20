using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerChooser : MonoBehaviour
{
    [System.Serializable]
    private class PowerButton
    {
        [SerializeField] private Button button;
        [SerializeField] private Powers type;

        public void Link(PowerChooser _chooser) => button.onClick.AddListener(() =>
        {
            _chooser.Choose(type);
            button.interactable = false;
        });
    }

    [SerializeField] private PowerButton[] powerButtons;
    [SerializeField] private MainMenu mainMenu;
    private List<Powers> choosedPowers = new();

    private void Awake()
    {
        foreach (PowerButton _button in powerButtons)
        {
            _button.Link(this);
        }
    }

    public void Choose(Powers _type)
    {
        choosedPowers.Add(_type);

        if (choosedPowers.Count == 3) mainMenu.Play(choosedPowers.ToArray());
    }
}