using Assets.Scripts.Engines;
using System.Collections;

namespace Assets.Scripts.States.AppState
{
    class GameLoadingState:State
    {
        protected override IEnumerator Init()
        {
            Menu.MenuManager.ShowLoadingPanel();

            yield return GameEngine.Generate(onFinish, Menu.MenuManager.OnProgress);

            yield return base.Init();
        }

        private void onFinish()
        {

            Parent.Activate<GameState>();
        }


        protected override IEnumerator End()
        {
            Menu.MenuManager.HideLoadingPanel();

            return base.End();
        }
    }
}
