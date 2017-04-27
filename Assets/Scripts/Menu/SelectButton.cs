using Assets.Scripts.Engines;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.Menu
{
    class SelectButton: MonoBehaviour
    {
        [SerializeField]
        private Button Button;

        private int Index { get; set; }

        public void Set(int index, Transform parent, Texture2D texture, UnityEngine.Events.UnityAction onClick)
        {
            Index = index;
            transform.SetParent(parent);
            Button.image.sprite =  Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
            Button.onClick.AddListener(_onClick);
            Button.onClick.AddListener(onClick);
        }

        private void _onClick()
        {
            GameEngine.SelectImageIndex = Index;
        }

        private void Start()
        {
            if (!Button) Button = GetComponent<Button>();
        }

    }
}
