using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    class ExitPanel: MonoBehaviour
    {
        [SerializeField]
        private Button YesButton;

        [SerializeField]
        private Button NoButton;


        public void Set(UnityEngine.Events.UnityAction onYes, UnityEngine.Events.UnityAction onNo)
        {
            YesButton.onClick.RemoveAllListeners();
            NoButton.onClick.RemoveAllListeners();

            YesButton.onClick.AddListener(onYes);
            NoButton.onClick.AddListener(onNo);
        }
    }
}
