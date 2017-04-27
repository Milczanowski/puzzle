using Assets.Scripts.Engines;
using System.Collections;
using System;
using Assets.Scripts.Menu;

namespace Assets.Scripts.States.AppState
{
    class GameState:State
    {
        protected override IEnumerator Init()
        {
            GameEngine.OnDone = OnDone;
            Menu.MenuManager.ShowGameMenu();
            Menu.MenuManager.SetGameMenu(onExitClick, ()=> { });
            GameEngine.SetPuzzleEnabled(true);
             yield return base.Init();
        }

        private void OnDone()
        {
            Parent.Activate<GameStateFinish>();
        }

        private void onExitClick()
        {
            Parent.Activate<ExitGameState>();
        }

        protected override void Update()
        {
            CameraControler.Update();
            GameEngine.Update();
            base.Update();
        }

        protected override IEnumerator End()
        {
            GameEngine.SetPuzzleEnabled(true);
            PuzzleMenu.SetActive(false);
            Menu.MenuManager.HideGameMenu();
            yield return base.End();
        }
    }
}
