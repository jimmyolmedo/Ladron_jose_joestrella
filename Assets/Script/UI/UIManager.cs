using System.Collections;
using System.Collections.Generic;
using UnityEngine;

   // Un UIManager hereda de PanelsManager, para asi poder crearle un Singleton 
    // y tener control de los paneles principales de nuestro juego.
    // Es preferible que herede de PanelsManager, ya que esto nos permite
    // utilizar esta clase para controlar subpaneles, por ejemplo, para 
    // el subpanel de armas, armaduras, y pociones de un inventario. 
    public class UIManager : PanelsManager
    {
        // Esta es la version generica para implementar un singleton sin poder heredar.
        public static UIManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;
        }
    }