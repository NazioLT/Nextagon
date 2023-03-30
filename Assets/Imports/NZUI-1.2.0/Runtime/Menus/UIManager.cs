using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Nazio_LT.Tools.UI
{
    [AddComponentMenu("Nazio_LT/UI/UIManager"), RequireComponent(typeof(AnimatedPanel))]
    public class UIManager : MonoBehaviour
    {
        [System.Serializable]
        protected struct MenuPanel
        {
            public string name;
            public AnimatedPanel panel;
            public bool stuck;
        }

        [SerializeField] private bool autoClosePanel;
        [SerializeField] protected string mainPanel;
        [SerializeField] protected MenuPanel[] panels;
        protected Dictionary<string, MenuPanel> panelsDict = new Dictionary<string, MenuPanel>();
        private string openedPanelID;

        public AnimatedPanel globalPanel { private set; get; }
        public AnimatedPanel currentSelectedPanel { private set; get; }
        private UnityEngine.EventSystems.EventSystem eventSystem;

        protected virtual void Start()
        {
            globalPanel = GetComponent<AnimatedPanel>();

            InitPanels();
        }

        #region Inputs Methods

        public void RightShoulderPerformed(InputAction.CallbackContext _ctx) => currentSelectedPanel?.RightShoulder(_ctx);
        public void LeftShoulderPerformed(InputAction.CallbackContext _ctx) => currentSelectedPanel?.LeftShoulder(_ctx);

        #endregion

        #region Public Methods

        /// <summary>Switch the active panel to another.</summary>
        public void SwitchPanel(string _panelName)
        {
            CloseAllPanel(_panelName);
            GetPanel(_panelName).SelectPanel();

            openedPanelID = _panelName;
        }

        /// <summary>Open the UI Main Canvas.</summary>
        public virtual void Open()
        {
            globalPanel.SelectPanel();
            CloseAllPanel(mainPanel);
            SwitchPanel(mainPanel);
        }

        /// <summary>Close the UI Main Canvas.</summary>
        public void Close()
        {
            CloseAllPanel("");
            globalPanel.Hide();
            
            eventSystem.SetSelectedGameObject(null);
        }

        #endregion

        /// <summary>Close all active Panels </summary>
        protected void CloseAllPanel(string _exceptionKey)
        {
            foreach (var _item in panelsDict)
            {
                if (_exceptionKey != null && _item.Key == _exceptionKey || _item.Value.stuck) continue;

                GetPanel(_item.Key).Hide();
            }
        }

        private void InitPanels()
        {
            panelsDict = new Dictionary<string, MenuPanel>();
            foreach (var _panel in panels)
            {
                if (panelsDict.ContainsKey(_panel.name)) continue;

                AnimatedPanel _UIPanel = _panel.panel;

                _UIPanel.Init();

                if (autoClosePanel)
                {
                    if (!_panel.stuck) _UIPanel.Instant(0f);
                    if (_panel.name == mainPanel) _panel.panel.Instant(1f);
                }

                panelsDict.Add(_panel.name, _panel);
            }
        }

        #region ShortCut Methods

        public void MainPanel() => SwitchPanel("Main");

        #endregion

        protected AnimatedPanel GetPanel(string _key) => panelsDict[_key].panel;
    }
}