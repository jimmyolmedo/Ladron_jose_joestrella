using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // Un panel es una clase que tendra que tener toda carpeta que guarde un conjunto de paneles dentro de la UI.
    // Por ejemplo, todas los conjuntos importantes deben tener un panel, como el Game Status, Inventario, Pause, Menú, etcetera.
    // Game Status: 
    //     Player Health
    //     Current Level
    //     Movement Tutorial
    //
    // Pause:
    //     Title
    //     Background
    //     Buttons
    // 
    // Settings :
    //     Title
    //     Layout
    //     Close Button
    public class Panel : MonoBehaviour
    {
        // Este sera el identificador con que el detectaremos si el objeto se tiene que abrir o cerrar.
        [Header("Panel Settings")]
        [SerializeField] private string m_identifier;

        // Propiedad para acceder a la variable privada.
        public string Identifier => m_identifier;

        // Funcion que se ejecutara cuando el identificador del panelsManager sea igual
        // al de esta clase.
        public virtual void Open()
        {
            gameObject.SetActive(true);
        }

        // Funcion que se ejecutara cuando el identificador no sea el mismo que el de esta clase.
        public virtual void Close()
        {
            gameObject.SetActive(false);
        }

        // Esta funcion es especificamente para habilitar o desabilitar el objeto instantaneamente 
        // ya que si usamos la funcion Open o Close se ejecutará la animación de cerrar
        // cuando nosotros pongamos Play y haya un panel abierto que no sea el inicial.
        public virtual void Set(bool active)
        {
            gameObject.SetActive(active);
        }
    }