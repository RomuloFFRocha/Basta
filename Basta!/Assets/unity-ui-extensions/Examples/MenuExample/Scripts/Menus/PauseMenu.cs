namespace UnityEngine.UI.Extensions.Examples
{
    public class PauseMenu : SimpleMenu<PauseMenu>
    {
        public static bool GameIsPaused = false;
        public GameObject pauseMenuUI;


        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;

        }
        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }

        public void Sair()
        {
            Application.Quit();
        }
    }
}