using Assets.Scripts.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    class Move : MonoBehaviour
    {
        private Action Action { get; set; }

        private Action UpAction { get; set; }

        public void Set(Action action, Action upAction)
        {
            Action = action;
            UpAction = upAction;
        }

        private void OnMouseDrag()
        {
            Action();
        }

        private void OnMouseExit()
        {
            PuzzleMenu.SetActive(false);
            GameEngine.Selected = null;
        }

        private void OnMouseUp()
        {
            UpAction();
        }
    }
}
