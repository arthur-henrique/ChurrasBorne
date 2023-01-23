using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Items : MonoBehaviour
{
    public static Inventory_Items instance;
    public Sprite img_chave_simples;

    public class chave_simples
    {
        public string name = "Chave Simples";
        public Sprite imagem = Inventory_Items.instance.img_chave_simples;
        public string desc = "Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                             "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." +
                             "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";
    }

    private void Start()
    {
        instance = this;
    }
}
