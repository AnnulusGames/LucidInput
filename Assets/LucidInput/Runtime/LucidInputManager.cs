using UnityEngine;
#if USE_INPUTSYSTEM
using UnityEngine.InputSystem.EnhancedTouch;
#endif

namespace AnnulusGames.LucidTools.InputSystem
{
    [AddComponentMenu("")]
    [DefaultExecutionOrder(-99)]
    internal class LucidInputManager : MonoBehaviour
    {
        private static LucidInputManager instance;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            instance = new GameObject("[Lucid Input]").AddComponent<LucidInputManager>();
            DontDestroyOnLoad(instance);

#if USE_INPUTSYSTEM
            EnhancedTouchSupport.Enable();
#endif

            LucidInput.Initialize();
        }

        private void Update()
        {
            LucidInput.Update();
        }
    }

}