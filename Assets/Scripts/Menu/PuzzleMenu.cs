using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    class PuzzleMenu : MonoBehaviour
    {
        private static PuzzleMenu instance { get; set; }

        [SerializeField]
        Rotate RotateButton;

        [SerializeField]
        Move MoveButton;

        private void Awake()
        {
            if (!instance) instance = this;
        }

        public static void SetAction(Action rotate, Action move, Action up, Vector3 position)
        {
            if (instance.RotateButton)
                instance.RotateButton.Set(rotate);
            instance.MoveButton.Set(move, up);
            instance.transform.position = position;
        }

        public static void SetActive(bool active)
        {
            instance.RotateButton.gameObject.SetActive(active);
            //instance.MoveButton.gameObject.SetActive(active);
        }


        public static void SetPosition(Vector3 position)
        {
            instance.transform.position = position;
        }

    }
}
