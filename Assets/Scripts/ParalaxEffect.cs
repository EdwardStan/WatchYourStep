using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ParalaxEffect : MonoBehaviour
{
    [SerializeField] private RawImage[] img; // 0 - sky, 1 - mountains, 2 - lands, 3 - Clouds
    [SerializeField] private float[] _x, _y;

    public GameManager manager;

    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0 && !manager.GameStarted) { return; }

        if (img[0] != null) { img[0].uvRect = new Rect(img[0].uvRect.position + new Vector2(_x[0], _y[0]) * Time.deltaTime, img[0].uvRect.size); }
        if (img[1] != null) { img[1].uvRect = new Rect(img[1].uvRect.position + new Vector2(_x[1], _y[1]) * Time.deltaTime, img[1].uvRect.size); }
        if (img[2] != null) { img[2].uvRect = new Rect(img[2].uvRect.position + new Vector2(_x[2], _y[2]) * Time.deltaTime, img[2].uvRect.size); }
        if (img[3] != null) { img[3].uvRect = new Rect(img[3].uvRect.position + new Vector2(_x[3], _y[3]) * Time.deltaTime, img[3].uvRect.size); }
    }
}
