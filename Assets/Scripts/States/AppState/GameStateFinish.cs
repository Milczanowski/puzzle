using Assets.Scripts.Menu;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.States.AppState
{
    class GameStateFinish: State
    {
        protected override IEnumerator Init()
        {
            Menu.MenuManager.ShowDonePanel();
            Menu.MenuManager.ShowGameMenu();
            PuzzleMenu.SetActive(false);
            yield return base.Init();
        }

        protected override IEnumerator End()
        {
            Menu.MenuManager.HideDonePanel();
            Menu.MenuManager.HideGameMenu();
            yield return base.End();
        }
    }
}
