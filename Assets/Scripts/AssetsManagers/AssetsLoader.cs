using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AssetsManagers
{
    public class AssetsLoader<T> where T : UnityEngine.Object
    {
        private string path;
        private Dictionary<string, T> textures;

        public string Path
        {
            get { return path; }
            protected internal set { path = value; }
        }

        public T this[string name]
        {
            get
            {
                if (name == null) return default(T);
                if (!Textures.ContainsKey(name))
                {
                    var asset = Resources.Load<T>(path + name);

                    if (asset)
                        Textures.Add(name, asset);
                    else
                        throw new AssetNotFound("Asset not found:" + path + name);
                }
                return Textures[name];
            }
        }

        protected internal Dictionary<string, T> Textures
        {
            get { return textures; }
            set { textures = value; }
        }

        public AssetsLoader(string path)
        {
            Path = path;
            Textures = new Dictionary<string, T>();

        }

        public void Add(T obj, string name)
        {
            if (Textures.ContainsKey(name)) throw new AssetAlreadyExists(name);
            Textures.Add(name, obj);
        }

        public void Clear()
        {
            Textures.Clear();
        }
    }
}
