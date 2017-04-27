using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Connections
{
    class Connection
    {
        protected internal static IEnumerator GetTexture(string url, Action<Texture2D> onSuccess, Action<FailData> onFail, Action<float> onProgress)
        {
            WWW www = new WWW(url);

            while (!www.isDone)
            {
                if (onProgress != null)
                    onProgress(www.progress);

                yield return null;
            }

            if (www.error !=null)
            {
                if (onFail != null)
                    onFail(new FailData(www.error));
            }else
                if (onSuccess != null)
                    onSuccess(www.texture);

            yield return null;
        }
    }
}
