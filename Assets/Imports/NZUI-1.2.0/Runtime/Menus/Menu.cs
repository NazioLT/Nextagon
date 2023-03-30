using UnityEngine;

namespace Nazio_LT.Tools.UI
{
    [AddComponentMenu("Nazio_LT/UI/Menu")]
    public class Menu : UIManager
    {
        [SerializeField] private OptionsSettings options;

        #region UnityMethods

        protected override void Start()
        {
            base.Start();

            options.Init(this);
        }

        #endregion

        #region Public Methods

        public void Quit() => Application.Quit();

        #endregion

        #region ShortCut Methods

        public void Options() => SwitchPanel("Options");
        public void Credits() => SwitchPanel("Credits");

        #endregion
    }
}