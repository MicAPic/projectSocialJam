using Audio;
using Coffee.UIEffects;
using DG.Tweening;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameOverManager : MonoBehaviour
    {
        public static GameOverManager Instance { get; private set; }

        public bool IsGameOver { get; private set; }
        
        [SerializeField]
        private Animator lightsAnimator;
        [SerializeField]
        private PlayerView playerView;
        [SerializeField]
        private GameObject monsterPrefab;
        [SerializeField]
        private AudioClip gameOverClip;
        [SerializeField]
        private UITransitionEffect transitionEffect;
        [SerializeField]
        private GameObject dialogueBox;
        [SerializeField]
        private float transitionDuration = 5.0f;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void GameOver()
        {
            if (IsGameOver) return;
            
            IsGameOver = true;
            lightsAnimator.SetTrigger("Blink");
            InputLimiter.Instance.LimitInput(true);
            AudioManager.Instance.StopBGM();
            
            AudioManager.Instance.ForcePlaySoundEffectAt(gameOverClip, playerView.transform.position);
            dialogueBox.SetActive(false);
            Instantiate(monsterPrefab, playerView.monsterSpawnPoint);
            
            Debug.Log("Game Over Triggered");
            
            DOTween.To(() => transitionEffect.effectFactor, 
                x => transitionEffect.effectFactor = x, 
                1.0f, 
                transitionDuration
            ).SetDelay(3.0f).OnComplete(() => SceneManager.LoadScene("Main"));
            
            AudioManager.Instance.FadeOutAll(transitionDuration + 3.0f);
        }
    }
}
