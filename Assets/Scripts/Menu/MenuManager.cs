using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    class MenuManager: MonoBehaviour
    {
        private static MenuManager instance { get; set; }

        [SerializeField]
        private RectTransform Content;

        [SerializeField]
        private GameObject IntroPanel;

        [SerializeField]
        private Menu MenuPanel;

        [SerializeField]
        private LoadingPanel LoadingPanel;

        [SerializeField]
        private GameMenu GameMenu;

        [SerializeField]
        private GameObject DonePanel;

        private List<SelectButton> Buttons { get; set; }

        public static Action<float> OnProgress { get { return instance.LoadingPanel.SetProgres; } }

        private void Awake()
        {
            if (!instance)
                instance = this;
        }


        internal static void HideLoadingPanel()
        {
            instance.LoadingPanel.gameObject.SetActive(false);
        }

        public static IEnumerator ShowMenu()
        {
            yield return instance.MenuPanel.LoadImages();
            instance.IntroPanel.SetActive(false);
            instance.MenuPanel.gameObject.SetActive(true);
        }

        public static void HideMenu()
        {
            instance.MenuPanel.gameObject.SetActive(false);
        }

        public static void ShowLoadingPanel()
        {
            instance.LoadingPanel.SetProgres(0);
            instance.LoadingPanel.gameObject.SetActive(true);
        }

        public static void ShowGameMenu()
        {
            instance.GameMenu.gameObject.SetActive(true);
        }

        public static void HideGameMenu()
        {
            instance.GameMenu.gameObject.SetActive(false);
        }

        public static void ShowDonePanel()
        {
            instance.DonePanel.gameObject.SetActive(true);
        }

        public static void HideDonePanel()
        {
            instance.DonePanel.gameObject.SetActive(false);
        }


        public static void SetGameMenu(UnityEngine.Events.UnityAction onExitClick, UnityEngine.Events.UnityAction onNoClick)
        {
            instance.GameMenu.Set(onExitClick, onNoClick);
        }
    }
}
