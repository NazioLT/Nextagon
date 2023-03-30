#if UNITY_EDITOR
using UnityEditor;
using Nazio_LT.Tools.Core.Internal;

namespace Nazio_LT.Tools.UI.Internal
{
    public static class NUICreateUtilty
    {
        [MenuItem("GameObject/UI/Nazio_LT/NSlider")]
        public static void CreateNSlider(MenuCommand _cmd) => CreateUtilty.CreatePrefab("NSlider", false);
        [MenuItem("GameObject/UI/Nazio_LT/NToggle")]
        public static void CreateNToggle(MenuCommand _cmd) => CreateUtilty.CreatePrefab("NToggle", false);
        [MenuItem("GameObject/UI/Nazio_LT/NDropdown")]
        public static void CreateNDropdown(MenuCommand _cmd) => CreateUtilty.CreatePrefab("NDropdown", false);
        [MenuItem("GameObject/UI/Nazio_LT/NButton")]
        public static void CreateNButton(MenuCommand _cmd) => CreateUtilty.CreatePrefab("NButton", false);
        [MenuItem("GameObject/UI/Nazio_LT/NInputField")]
        public static void CreateNInputField(MenuCommand _cmd) => CreateUtilty.CreatePrefab("NInputField", false);
        [MenuItem("GameObject/UI/Nazio_LT/NAnimatedPanel")]
        public static void CreateNAnimatedPanel(MenuCommand _cmd) => CreateUtilty.CreatePrefab("NAnimatedPanel", false);
        [MenuItem("GameObject/UI/Nazio_LT/NUIManager")]
        public static void CreateNUIManager(MenuCommand _cmd) => CreateUtilty.CreatePrefab("NUIManager", false);
    }
}
#endif