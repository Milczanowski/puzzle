using Assets.Scripts.States.AppState;
using System.Collections;

namespace Assets.Scripts.States
{
    class Root : State
    {
        public Root()
        {
            AddState<MenuState>(this);
            AddState<GameState>(this);
            AddState<GameLoadingState>(this);
            AddState<ExitGameState>(this);
            AddState<GameStateFinish>(this);
        }

        protected internal IEnumerator Enable()
        {
            yield return _Activate<MenuState>();
        }

        protected internal void _Update()
        {
            Update();
        }
    }
}
