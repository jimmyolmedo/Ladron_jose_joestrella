using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // Un PanelsManager es el encargado de abrir el o los paneles que nosotros queramos, 
    // y al mismo tiempo, cerrará todos los que no sean los que queramos.
    public class PanelsManager : MonoBehaviour
    {
        // El identificador principal hará que al habilitar el objeto, se abra ese panel.
        [SerializeField] private string m_initialIdentifier;

        // Y dentro de la lista de paneles, debemos referenciar desde el inspector
        // todos los paneles que queramos que este panelsManager maneje.
        // Por ejemplo, tengo Menu, Inventario, Game Over, Victoria
        // Si uso SwitchState("Inventario"), 
        // Se abrirá el panel con el identificador "Inventario"
        // Y se cerraran los paneles "Menu", "Game Over", "Victoria", siempre que esten abiertos.
        [SerializeField] private List<Panel> m_panels;

        // Esta propiedad nos permite desde cualquier otra clase obtener el identificador actual 
        // del panelsManager, sin permitir que pueda ser modificado desde fuera.
        public string CurrentIdentifier { get; private set; }

        // Dentro de OnEnable haremos que se establezca el panel inicial.
        private void OnEnable()
        {
            // Aca, a parte del identificador inicial, sobreescribire el valor por defecto 
            // del segundo parametro de SwitchPanel. 
            // Esto no fue pasado en clases para no sobrecargar de informacion.
            SwitchPanel(m_initialIdentifier, true);
        }

        // La funcion SwitchPanel activará y desactivará los paneles con animaciones
        // Pero, al tener un booleano llamado instant, podremos abrir los paneles instantaneamente, 
        // sin requerir pasar por una animacion.
        // Podremos usar SwitchPanel("Nombre", true);
        // O, si queremos utilizar su comportamiento por defecto, SwitchPanel("Nombre");
        // Ya que creamos una sobrecarga solo con un identificador, la cual ejecutara la funcion con el booleano.
        public void SwitchPanel(string identifier) => SwitchPanel(identifier, false);

        public void SwitchPanel(string identifier, bool instant)
        {
            CurrentIdentifier = identifier;

            // Aca recorreremos todos los paneles dentro de la lista de paneles.
            foreach (Panel panel in m_panels)
            {
                // Aca creare un bool que será true si el identificador es el mismo del panel.
                bool active = panel.Identifier == identifier;

                // Si establecemos que instant sea igual a true, abriremos o cerraremos
                // el panel instantaneamente con la funcion Set, para luego continuar.
                // La palabra reservada continue nos permite no seguir ejecutando la iteracion,
                // y saltar a la proxima.
                // Si usaramos return, toda la funcion SwitchPanel dejaria de ejecutarse.
                if (instant)
                {
                    panel.Set(active);
                    continue;
                }

                // Entonces, si no es instantaneo, abriremos el panel de manera normal
                // al tener el mismo identificador, o lo cerraremos si es distinto.
                if (active) panel.Open();
                else panel.Close();
            }
        }
    }