using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace CardGame.Cards.UI
{
    public class ArtLoader : MonoBehaviour
    {
        const string _uri = "https://picsum.photos/256/256";
        const int spriteWidth = 256;
        const int spriteHeight = 256;

        public void LoadSprite(Action<Sprite> loadCallback)
        {
            StartCoroutine(LoadTexture(texture =>
            {
                Rect rec = new Rect(0, 0, spriteWidth, spriteHeight);
                var sprite = Sprite.Create(texture, rec, Vector2.zero, 1);
                loadCallback(sprite);
            }));
        }

        private IEnumerator LoadTexture(Action<Texture2D> loadCallback)
        {
            using (var request = UnityWebRequestTexture.GetTexture(_uri))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Error loading Texture '{request.uri}': {request.error}", this);
                }
                else
                {
                    var texture = DownloadHandlerTexture.GetContent(request);
                    loadCallback(texture);
                }
            }
        }
    }
}
