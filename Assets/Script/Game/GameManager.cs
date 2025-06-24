using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // El enumerador de GameState debe tener todos los posibles estados de un juego,
    // por ejemplo, el estado de derrota, victoria, minimapa, batalla, etcetera
    // Preferiblemente el enumerador debe estar fuera del GameManager para evitar
    // que tengamos que pasar por el para comparar, por ejemplo:
    // GameManager.CurrentState == GameManager.GameState.Gameplay
    public enum GameState
    {
        // El primer estado seria el mas importante ya que de este dependeria el 
        // comportamiento inicial del juego, pero esto se soluciona con la variable
        // initialState mencionada mas abajo.
        Menu,
        Gameplay,
        Pause,
        Defeat,
        Victory,
        GameOver
    }

    // El GameManager es singleton, pero no para utilizar la instancia, 
    // si no que es porque solo queremos que haya un solo GameManager en la escena.
    // Aqui es requerido utilizar la clase Singleton que se implemento en Assets/Scripts/Utilities
    public class GameManager : Singleton<GameManager>
    {
        // El evento se ejecutara cuando cambiemos el estado y le enviará una señal
        // a todos quienes esten suscritos con el nuevo estado.
        public static event System.Action<GameState> OnStateChange;

        // El estado inicial cambiará el estado actual a este en la función Start.
        [SerializeField] private GameState m_initialState;

        // La propiedad CurrentState nos permite acceder desde cualquier otra clase para
        // poder comparar nuestro estado, por ejemplo:
        // if (GameManager.CurrentState == GameState.Gameplay)
        public static GameState CurrentState { get; private set; } = (GameState)(-1);

    // Esta es una propiedad que si o si se debe implementar desde el Singleton,
    // no es relevante para esta clase.
    // Aun asi, no recomiendo utilizar DontDestroyOnLoad en el GameManager.
    // Y tambien recomiendo no usarlo en general a no ser que sea casos especificos,
    // por ejemplo, el fade entre escenas de un SceneManager.
    protected override bool persistent => false;

    // Aca establecere que el estado inicial sera el indicado desde la variable.
    // Recomiendo que sea en el Start, ya que por el orden de ejecucion de scripts,
    // si esto está en el Awake, hará que se ejecute antes de que los objetos ejecuten
    // OnEnable, lo que hará que no alcancen a suscribirse al evento OnStateSwitch.
    private void Start()
        {
            SwitchState(m_initialState);
        }

        // Esta funcion es la encargada de cambiar nuestro estado desde cualquier otra
        // clase, por ejemplo, al morir.
        // if (health == 0) 
        // {
        //      GameManager.SwitchState(GameState.GameOver);
        // }
        public static void SwitchState(GameState state)
        {
            // Esta es una comparacion necesaria para evitar que se ejecuten los eventos
            // dos veces
            if (state == CurrentState) return;

            // Aqui actualizaremos la propiedad y ejecutaremos el evento.
            CurrentState = state;
            OnStateChange?.Invoke(state);
        }
    }