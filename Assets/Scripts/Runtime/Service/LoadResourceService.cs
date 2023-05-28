using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Services
{
    public static class LoadResourceService
    {
        private const string _pathToPrefab = "Prefabs/";
        private const string _pathToSprite = "Sprites/";

        public static async UniTask<GameObject> LoadPrefab(string path)
        {
            var pathLoad = _pathToPrefab + path;

            var prefab = await Resources.LoadAsync<GameObject>(pathLoad) as GameObject;

            if (prefab == null) Debug.LogError($"LoadResourceService :: LoadPrefab() not correct path {pathLoad}");

            return prefab;
        }

        public static async UniTask<Sprite> LoadSprite(string path)
        {
            var pathLoad = _pathToSprite + path;

            var sprite = await Resources.LoadAsync<Sprite>(pathLoad) as Sprite;

            if (sprite == null) Debug.LogError($"LoadResourceService :: LoadSprite() not correct path {pathLoad}");

            return sprite;
        }
    }
}
