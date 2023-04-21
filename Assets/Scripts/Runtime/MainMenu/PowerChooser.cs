using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Nazio_LT.Tools.UI;

public class PowerChooser : MonoBehaviour
{
    [System.Serializable]
    public class PowerButton
    {
        [SerializeField] private NButton button;
        [SerializeField] private Powers type;

        public void Link(PowerChooser _chooser) => button.onClick.AddListener(() =>
        {
            _chooser.Choose(type, button, this);
            button.interactable = false;
        });

        public void Restore()
        {
            button.interactable = true;
        }

        public NButton Button => button;
    }

    [SerializeField] private PowerButton[] powerButtons;
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private NButton removeButton, returnToMenuButton;
    [SerializeField] private Transform buttonParents;
    private List<Powers> choosedPowers = new();
    private List<Button> removePowerButton = new();

    private void Awake()
    {
        returnToMenuButton.onClick.AddListener(Back);
        foreach (PowerButton _button in powerButtons)
        {
            _button.Link(this);
        }
    }

    public void Choose(Powers _type, NButton _removePowerButton, PowerButton _powerButton)
    {
        NButton _button = Instantiate(removeButton, transform.position, Quaternion.identity, buttonParents);
        removePowerButton.Add(_button);
        _button.onClick.AddListener(() => {
            _powerButton.Restore();
            choosedPowers.Remove(_type);
            removePowerButton.Remove(_button);
            Destroy(_button.gameObject);
        });

        _button.SetLabel(_powerButton.Button.LabelText);

        choosedPowers.Add(_type);

        if (choosedPowers.Count == 3) mainMenu.Play(choosedPowers.ToArray());
    }

    public void Back()
    {
        foreach (var _button in removePowerButton)
        {
            Destroy(_button.gameObject);
        }
        foreach (var _button in powerButtons)
        {
            _button.Restore();
        }
        mainMenu.Back();

        removePowerButton = new();
        choosedPowers = new();
    }
}