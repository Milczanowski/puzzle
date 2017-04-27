using Assets.Scripts.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    class Rotate: MonoBehaviour
    {
        private Action Action { get; set; }

        public void Set(Action action)
        {
            Action = action;
        }
        private void OnMouseDown()
        {
            Action();
        }

        private void OnMouseExit()
        {
            PuzzleMenu.SetActive(false);
            GameEngine.Selected = null;
        }
    }

}
