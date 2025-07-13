using UnityEngine.SceneManagement;

namespace ProjetoIV.Util
{
    public class SceneLoader : Singleton<SceneLoader>
    {
        private static Scene m_targetScene;
        public enum Scene
        {
            SCN_Intro,
            SCN_Menu,
            SCN_Game,
        }
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void Load(Scene p_targetScene)
        {
            SceneManager.LoadScene(p_targetScene.ToString(), LoadSceneMode.Single);
        }
    }
}