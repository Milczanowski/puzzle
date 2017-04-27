using Assets.Scripts.Engines;
using System.Collections;


namespace Assets.Scripts.States.AppState
{
    class ExitGameState:State
    {
        protected override IEnumerator Init()
        {
            Menu.GameMenu.ShowExitPanel();
            Menu.MenuManager.SetGameMenu(onExitClick, onNoClick);
            yield return base.Init();
        }

        private void onNoClick()
        {
            Parent.Activate<GameState>();
        }

        private void onExitClick()
        {
            Parent.Activate<MenuState>();
            GameEngine.Clear();
        }

        protected override IEnumerator End()
        {
            Menu.GameMenu.HideExitPanel();
            yield return base.End();
        }
    }
}
