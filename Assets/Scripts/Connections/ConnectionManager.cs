using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Connections
{
    class ConnectionManager: MonoBehaviour
    {
        private static ConnectionManager instance;

        
        private void Awake()
        {
            if (!instance)
                instance = this;
        }

        public static void GetTexture2D(string url, Action<Texture2D> onSuccess, Action<FailData> onFail, Action<float> onProgress)
        {
            instance.StartCoroutine(Connection.GetTexture(url, onSuccess, onFail, onProgress));
        }

    }
}
