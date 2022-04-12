using UnityEngine;
using UnityEngine.UI;

namespace Tetris.Data
{
    public class SceneData : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Text scoreText;
        [SerializeField] private Text highScoreText;
        [SerializeField] private GameObject pauseScreen;

        public Camera MainCamera => mainCamera;
        public Text ScoreText => scoreText;
        public Text HighScoreText => highScoreText;
        public GameObject PauseScreen => pauseScreen;
    }
}