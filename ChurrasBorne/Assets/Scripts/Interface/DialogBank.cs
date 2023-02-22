using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBank : MonoBehaviour
{
    public static string[] english_bank =
    {
        "",                                                        /* 0 */
        "teste um",                                                /* 1 */
        "You've got your first meatstick!",                        /* 2 */
        "Your health has been completely restored.",               /* 3 */
        "Your health has been completely restored, " +
        "and if possible, a meat has been added to your stick.",   /* 4 */
        "Your health has been completely restored, " +
        "and if possible, meat have been added to your stick."     /* 5 */
    };

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

    public static string[] spanish_bank =
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
        },

        { // 3 - Bruxinha, primeiro encontro
            "Uaaaaaahhh, uaaaaaahhhh-",
            "...",
            "uaaaaaahhhhhhh-",
            "...",
            "",
            "",
            "",
            "",
        },

        { // 4 - Bruxinha, segundo encontro
            "Abuh, buuuh...",
            "...",
            "^u^",
            "...",
            "",
            "",
            "",
            "",
        },

        { // 5 - Bruxinha, hub
            "Oi onii-chan owo!!",
            "...",
            "Ooown, você não lembra de mim, onii-chan? ÓwÒ, isso é cruel demais úmù!! Eu sou a bruxinha que você salvou na caverna de gelo assustadora ( >×<)",
            "...",
            "Como eu estou vocalizando essas expressões? Não se preocupe com isso, Onii-chan! O que importa é que você me salvou! >w< ♡♡♡",
            "Enfim, estou muito agradecida! Arigatô onii-chan!! Se precisar de magia eu estou aqui! UwU!!!",
            "Inclusive, você deveria olhar como está aquela caverna hoje em dia, está um terror que só >.>",
            "",
        }

    };

    public static string[][,] portuguese_dialog_bank_new = new string[][,]
    {
        new string[,] { // 0 - Ferreiro, primeiro encontro
            { "right", "Ferreira", "Ferreira_1", "Socorro!! Por favor, alguém? Qualquer um, por favor! Este Monstro vai me matar!!"},
            { "", "", "", ""}
        },
        new string[,] { // 1 - Ferreiro, depois do boss
            { "right", "Ferreira", "Ferreira_1", "Ah, céus, obrigado..."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Ferreira", "Ferreira_1", "Um santuário? Neste desastre de mundo?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right", "Ferreira", "Ferreira_1", "UM CHURRASCO??? Não... você está certo, qualquer coisa é melhor que ficar encalhado nessa floresta com estes monstros... você não é um deles... é?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Ferreira", "Ferreira_1", "Bem, te encontro lá."},
            { "", "", "", ""},
        },
        new string[,] { // 2 - Ferreiro, hub
            { "right", "Ferreira", "Ferreira_1", "Olá amigo, é ótimo lhe ver aqui, e agradeço de novo por me resgatar. Acho que não me apresentei, meu nome é Ferreira, sou um Ferreiro de primeira linha, ou ao menos... eu era... anntes de todo esse desastre."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Ferreira", "Ferreira_1", "Fico feliz em poder lhe ajudar com o que quer que precise."},
            { "", "", "", ""},
        },
        new string[,] { // 3 - VAZIO
            { "left", "???", "Sabet_1", "Dialogo não implementado ainda."},
            { "", "", "", ""},
        },
        new string[,] { // 4 - VAZIO
            { "left", "???", "Sabet_1", "Dialogo não implementado ainda."},
            { "", "", "", ""},
        },
        new string[,] { // 5 - VAZIO
            { "right", "Bruna", "Bruna_1", "Oi Cavaleirinho! Acho que aquela situação não era a melhor pra um primeiro encontro, então vamos começar de novo. Prazer, eu sou Bruna, a Bruxa, e estou te devendo uma já que você salvou minha vida e tals."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right", "Bruna", "Bruna_1", "Entendi, você é do tipo caladão, legal... legal..."},
            { "", "", "", ""},
        },
    };
    /*{
        { // 0 - Ferreiro, primeiro encontro
            { "right", "Ferreira", "Ferreira_1", "EI VOCÊ, VOCÊ É UM CAVALEIRO? POR FAVOR, MATE ESSE MONSTRO!!!"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Ferreira", "Ferreira_1", "O quê? Se dá pra fazer churrasco com a carne dele? ...Eu creio que sim?"},
            { "left", "Sabet", "Sabet_1", "..."},
            { "", "", "", ""}
        },
        { // 1 - Ferreiro, depois do boss
            { "right", "Ferreira", "Ferreira_1", "Ah, céus, obrigado..."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Ferreira", "Ferreira_1", "Um santuário? Neste desastre de mundo?"},
            { "right", "Ferreira", "Ferreira_1", "UM CHURRASCO??? Não... você está certo, qualquer coisa é melhor que ficar encalhado nessa floresta com estes monstros... você não é um deles... é?"},
            { "right", "Ferreira", "Ferreira_1", "..."},
            { "", "", "", ""}
        },
    };*/

    /*portuguese_dialog_bank_new[0] = { "right", "Ferreira", "Ferreira_1", "EI VOCÊ, VOCÊ É UM CAVALEIRO? POR FAVOR, MATE ESSE MONSTRO!!!"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Ferreira", "Ferreira_1", "O quê? Se dá pra fazer churrasco com a carne dele? ...Eu creio que sim?"},
            { "left", "Sabet", "Sabet_1", "..."},
            { "", "", "", ""};*/
}
