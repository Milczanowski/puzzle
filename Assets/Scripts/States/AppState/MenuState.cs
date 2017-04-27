using System.Collections;


namespace Assets.Scripts.States.AppState
{
    class MenuState: State
    {
        private static MenuState instance;

        public MenuState():base()
        {
            instance = this;
        }

        public MenuState(State parent ):base(parent)
        {
            instance = this;
        }

        protected override IEnumerator Init()
        {
            yield return Menu.MenuManager.ShowMenu();
            yield return base.Init();
        }

        public static UnityEngine.Events.UnityAction OnClick { get { return instance.OnSelectButtonClick; } }

        private void OnSelectButtonClick()
        {
            Parent.Activate<GameLoadingState>();
        }

        protected override IEnumerator End()
        {
            Menu.MenuManager.HideMenu();

            yield return base.End();
        }

    }
}
