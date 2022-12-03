using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBank : MonoBehaviour
{

    public static string[] portuguese_bank =
    {
        "",                                                     /* 0 */
        "teste um",                                             /* 1 */
        "Você agora possui seu primeiro espetinho!",            /* 2 */
        "Sua vida foi completamente restaurada.",               /* 3 */
        "Sua vida foi completamente restaurada, " +
        "e um espeto foi reposto se possível em sua espada.",   /* 4 */
        "Sua vida foi completamente restaurada, " +
        "e espetos foram repostos se possível em sua espada."   /* 5 */
    };

    public static string[,] portuguese_dialog_bank =
    {
        { // 0 - Ferreiro, primeiro encontro
            "EI VOCÊ, VOCÊ É UM CAVALEIRO? POR FAVOR, MATE ESSE MONSTRO!!!",
            "...",
            "O quê? Se dá pra fazer churrasco com a carne dele?... eu creio que sim?",
            "...",
            "",
            "",
            "",
            "",
        },

        { // 1 - Ferreiro, depois do boss
            "Obrigado, eu me perdi na floresta procurando por materiais, eu sou Ferreira, o ferreiro.",
            "...",
            "Um santuário além da floresta...?",
            "...",
            "É seguro? Bem, obrigado, eu vou pra lá então. Se precisar de meus serviços é só pedir.",
            "...",
            "Sim, isso inclui assar carnes.",
            "",
        },

        { // 2 - Ferreiro, hub
            "Ah, amigo! Pensei que nunca mais fosse ver você!",
            "...",
            "Como assim, por que eu estou velho? Anos se passaram, eu que deveria perguntar por que você está tão jovem, hahaha.",
            "...",
            "De qualquer forma, devo-lhe avisar que a clareira por onde nos conhecemos está bem diferente hoje em dia.",
            "...",
            "Ah, sim, você pode investigar isso, mas talvez seja um pouco mais difícil do que naquela época...",
            "",
        }
    };

}
