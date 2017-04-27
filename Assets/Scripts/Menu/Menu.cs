using Assets.Scripts.AssetsManagers;
using Assets.Scripts.Connections;
using Assets.Scripts.States.AppState;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    class Menu: MonoBehaviour
    {
        [SerializeField]
        private RectTransform Content;

        [SerializeField]
        private SelectButton buttonPrefab;

        [SerializeField]
        private InputField urlToImege;

        [SerializeField]
        private Text progressText;

        private List<SelectButton> Buttons { get; set; }

        private void Start()
        {
            urlToImege.onEndEdit.AddListener(onEndEditURLToImage);
        }

        private void onEndEditURLToImage(string url)
        {
            ConnectionManager.GetTexture2D(url, OnSuccess, OnFail, OnProgress);
        }

        private void OnProgress(float obj)
        {
            progressText.text = String.Format("Download: {0:P2}.", obj);
        }

        private void OnFail(FailData obj)
        {
            progressText.text = obj.Error;
        }

        private void OnSuccess(Texture2D obj)
        {
            progressText.text = "Done";
            AssetsManager.PuzzleImages.Add(obj, Settings.PuzzleFile + Buttons.Count);
            AddImageButton(obj, MenuState.OnClick);
        }

        public IEnumerator LoadImages()
        {
            if (Buttons == null) Buttons = new List<SelectButton>();

            if (Buttons.Count == 0)
            {
                while (true)
                {
                    try
                    {
                        AddImageButton(AssetsManager.PuzzleImages[Settings.PuzzleFile + Buttons.Count], MenuState.OnClick);
                    }
                    catch (AssetNotFound e)
                    {
                        Debug.Log(e);
                        break;
                    }
                }
            }

            yield return null;
        }

        private void AddImageButton(Texture2D texture, UnityEngine.Events.UnityAction onClick)
        {
            SelectButton button = Instantiate(buttonPrefab);
            button.Set(Buttons.Count, Content, texture , onClick);
            Buttons.Add(button);

            Content.sizeDelta = new Vector2(200 * Buttons.Count, Content.sizeDelta.y);
        }
    }
}
