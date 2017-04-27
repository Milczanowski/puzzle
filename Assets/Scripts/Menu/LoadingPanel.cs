using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    class LoadingPanel: MonoBehaviour
    {
        [SerializeField]
        private Image Image;

        public void SetProgres(float progress)
        {
            int prog = (int)(600 * progress);
            Image.rectTransform.sizeDelta = new Vector2(prog, 10);
          //  Image.rectTransform.position = new Vector3(-prog / 2, -100);
        }

    }
}
