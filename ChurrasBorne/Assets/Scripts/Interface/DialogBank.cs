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

    public static string[,,] portuguese_dialog_bank_new =
    {
        { // 0 - Ferreiro, primeiro encontro
            { "right", "Ferreira", "Ferreira_1", "beat the h*mophobe the game we fuckinG STAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAN HOLY SHIT i LOVE cibele oh my god this theme makes me go W*T AS F*CK OOOOOOOOOOOOOOOOOOOOOOOOOH but LIKE OOOOOOOOOOH her design HER DESIGN EHVYWG9HOJBG oooooO23Y801789#&*#)@*$&) and her theme uaup AUU[P.... god i wish she was a real len'en oh my fucking god OOH GOD oh god GODgod GoDS............................. oahefwrhpdjlsnkbhiposejkfdl"},
            { "left" , "Sabet", "Sabet_1", "bom saber chapa"},
            { "left" , "Sabet", "Sabet_1", "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book."},
            { "right", "Ferreira", "Ferreira_1", "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto"},
            { "", "", "", ""}
        },
    };

}
