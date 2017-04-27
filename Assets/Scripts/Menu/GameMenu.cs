using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    class GameMenu: MonoBehaviour
    {
        private static GameMenu instance { get; set; }

        [SerializeField]
        private Button ExitButton;

        [SerializeField]
        private ExitPanel ExitPanel;

        private void Start()
        {
            if (!instance) instance = this;
        }

        public void Set(UnityEngine.Events.UnityAction onExitClick, UnityEngine.Events.UnityAction onNoClick)
        {
            ExitButton.onClick.RemoveAllListeners();
            ExitButton.onClick.AddListener(onExitClick);
            ExitPanel.Set(onExitClick, onNoClick);
        }

        public static void HideExitPanel()
        {
            instance.ExitPanel.gameObject.SetActive(false);
            instance.ExitButton.gameObject.SetActive(true);
        }

        public static void ShowExitPanel()
        {
            instance.ExitButton.gameObject.SetActive(false);
            instance.ExitPanel.gameObject.SetActive(true);
        }
    }
}
