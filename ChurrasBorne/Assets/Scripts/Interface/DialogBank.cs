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
        "",                                                               /* 0 */
        "teste um",                                                       /* 1 */
        "¡Ya tienes tu primer pincho!",                                   /* 2 */
        "Su vida fue completamente restaurada.",                          /* 3 */
        "Su vida ha sido completamente restaurada, " +
        "y un pincho ha sido reemplazado si es posible en su espada. ",   /* 4 */
        "Su vida ha sido completamente restaurada, " +
        "y los pinchos han sido reemplazados si es posible en su espada." /* 5 */
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

    public static string[][,] english_dialog_bank_new = new string[][,]
    {
        new string[,] { // 0 - Ferreiro, primeiro encontro
            { "right", "Smith", "Ferreira_1", "Help! God, please, someone? Anyone, please! This monster is gonna kill me!!"},
            { "", "", "", ""}
        },
        new string[,] { // 1 - Ferreiro, depois do boss
            { "right", "Smith", "Ferreira_1", "Oh God, thanks..."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "A sanctuary? In this hellhole of a world?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right", "Smith", "Ferreira_1", "A BARBECUE!? No... yer right, anything is better than being stranded in this forest with these monsters... yer not one of them... are ya?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "Well... meet you there."},
            { "", "", "", ""},
        },
        new string[,] { // 2 - Ferreiro, hub
            { "right", "Smith", "Ferreira_1", "Hey friend! Great to see ya here, and thanks again for rescuing me. I don't think I introduced myself yet, my name is Smith, I'm a top ranked Blacksmith, or at least... I was... before all o' this happened."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "Happy to assist with whatever ya need."},
            { "", "", "", ""},
        },
        new string[,] { // 3 - Ferreiro, hub 2
            { "right" , "Smith", "Ferreira_1", "'Ey Boss, what can I do for ya in this day?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "The access to the ice cave? Yeah, I can work out a key for it, but I gotta warn ya, it's dangerous to go alone, heard some rummors that there are big venomous creatures there, and I'd hate to see the guy that saved me die so young."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "A'ight, yer funeral."},
            { "", "", "", ""},
        },
        new string[,] { // 4 - Ferreiro, hub 3
            { "right" , "Smith", "Ferreira_1", "There he is! Just the guy I wanted to see!"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "Well, I have good news and bad news."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "Well, the good news is that I managed to remember an old technique to improve armors like yers, even if leather isn't exactly my area of expertise."},
            { "left" , "Sabet", "Sabet_1", "...?"},
            { "right" , "Smith", "Ferreira_1", "The bad news is that the tools I need for the job are still on that forest ye rescued me from, and by what I hear, the big ugly beast ye saved me from is back, bigger and uglier than ever."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "Well, I don't want ye to be riskin' yer life, but if ye can manage to bring me the tools, I'll get yer armor enhanced right quick."},
            { "", "", "", ""},
        },
        new string[,] { // 5 - Ferreiro, hub 4, interação quando a quest ainda não foi concluída
            { "right" , "Smith", "Ferreira_1", "Ey boss! Working hard?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "Yeah, wish I could say the same, feel so useless 'round here..."},
            { "", "", "", ""},
        },
        new string[,] { // 6 - Ferreiro, hub 4, interação quando conclui a quest
            { "right" , "Smith", "Ferreira_1", "Ey Boss, you good?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "Oh! My tools! COME TO PAPA!!"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "Oh, right, as promised, lemme see that armor of yers for a bit and I'll get it good and sturdy real nice."},
            { "right" , "Smith", "Ferreira_1", "There it is Boss, all in good shape with the best I had to work with for a hero such as yerself."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "Haha, yer welcome."},
            { "", "", "", ""},
        },
        new string[,] { // 7 - Ferreiro, hub 5, interações entre quests
            { "right" , "Smith", "Ferreira_1", "G'day boss! Passing by to see how li'l ol' me is doing?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "Hehe, yeah, crazy stuff."},
            { "", "", "", ""},
        },
        new string[,] { // 8 - Ferreiro, hub, quando o encapuzado está presente
            { "right" , "Smith", "Ferreira_1", "Ey Boss... look, I ain't one to judge by the looks, but that fella over there ain't beef I'd put in my grill, be careful 'round him, will ya?"},
            { "", "", "", ""},
        },
        new string[,] { // 9 - Bruna, primeiro encontro
            { "right" , "Bruna", "Bruna_1", "Aaaaaaaaaah! Oi, you there, a little help?"},
            { "", "", "", ""},
        },
        new string[,] { // 10 - Bruna, depois do resgate
            { "right" , "Bruna", "Bruna_1", "Whew, thanks mate, for a second I thought I was about to be spider tucker, or worse, a bloody ugly spider."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "What? A sanctuary? A barbie? Crikey, the eclipse got you crazy too eh? Bah, any place is better than this esky looking cave yeah? I hate this bloody cold, well, se ya there beauty!"},
            { "", "", "", ""},
        },
        new string[,] { // 11 - Bruna, hub
            { "right" , "Bruna", "Bruna_1", "Oi, Li'l Knightie! I think that dunny of a cave was hardly the place for a first encounter, so let's start over. G'day, my name is Bruna, the Witch, and I owe ya one since you saved my life and all that."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "Got it, you don't use ya laughing gear much, cool... cool..."},
            { "", "", "", ""},
        },
        new string[,] { // 12 - Bruna, hub 2
            { "right" , "Bruna", "Bruna_1", "Oi Barbie boy, what can I do for ye in this... day? night?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "The magic seal to the city? Well, it was probably set up by that dipstick of a spider that put 'er there, I reckon it'd be easy enough to ram through, why?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "You wanna go to the city? The big smoke infested by crazy bogans and cultist blokes? The city that is cactus after all this apoceclipse shenanigans?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "A'ight. Good luck mate, don't go dying on me!"},
            { "", "", "", ""},
        },
        new string[,] { // 13 - Bruna, hub, quest
            { "right" , "Bruna", "Bruna_1", "Ey, Barbie boy, can I borrow ya for a minute?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "I know that I already owe ya big time for, y'know, having saved my life and yada yada yada, but if I could ask for a wee favour I could help ya on this adventure of yours, whaddya say?"},
            { "left" , "Sabet", "Sabet_1", "...?"},
            { "right" , "Bruna", "Bruna_1", "Alrighty, I knew you'd understand. Basically, I wasn't dragged by that uggo monster, I was there studying a new variety of these bogans that spread since the Eclipse to see if I can find a common source in the magic and maybe revert the Eclipse."},
            { "left" , "Sabet", "Sabet_1", "...?"},
            { "right" , "Bruna", "Bruna_1", "I know I said I hate the bloody cold, but we don't always do what we like when making progress. As I was saying, I went there to research until that eight legged dunny kidnapped me, and when you rescued me I kinda forgot my notebook there..."},
            { "left" , "Sabet", "Sabet_1", "...?"},
            { "right" , "Bruna", "Bruna_1", "Put a sock in it! Anyways, if you go there and get it for me, I'll give ya a copy of the book, maybe knowing what these wankers are will help you beat them faster."},
            { "left" , "Sabet", "Sabet_1", "...?"},
            { "right" , "Bruna", "Bruna_1", "I knew you'd see my point, now go off then."},
            { "", "", "", ""},
        },
        new string[,] { // 14 - Bruna, hub, quest incompleta
            { "right" , "Bruna", "Bruna_1", "Hey Barbie boy, how's the adventure?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "Whaddya mean you still haven't got my notebook? It's chockers with important stuff, c'mon, on your bike!"},
            { "", "", "", ""},
        },
        new string[,] { // 15 - Bruna, hub, quest completa
            { "right" , "Bruna", "Bruna_1", "Hey Barbie boy, how's the adventure?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "IT WAS OPEN!? Did... didja read it?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "Well good! And as promised, here's a copy with the important bits about these freaks."},
            { "", "", "", ""},
        },
        new string[,] { // 16 - Bruna, hub, interação entre quests
            { "right" , "Bruna", "Bruna_1", "G'day B Boy, how are ya?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "'S NOT A DIARY! IT'S A JOURNAL WITH IMPORTANT MAGIC NOTES!... in which I write about my feelings and day to day activities..."},
            { "", "", "", ""},
        },
        new string[,] { // 17 - Bruna, hub, interação quando o encapuzado está no HUB
            { "right" , "Bruna", "Bruna_1", "Oi Barbie boy, I'm the last person to judge a book by it's cover, but I dunno if I trust this new bloke, I mean, man's got moons for eyes, like, c'mon the apocalipse came because of an eclipse! Well, sleep with an eye open while near this guy, got it?"},
            { "", "", "", ""},
        },
        new string[,] { // 18 - Pagodeiros, primeiro encontro
            { "right" , "Paulo", "Paulo_1", "Oh God! It wasn't a bird! T'was a trap!"},
            { "right" , "Gomes", "Gomes_1", "It may be too late, but the truth is.. I don't want to die!"},
            { "right" , "De Jesus", "DeJesus_1", "Oi, Skewer Boy! Turn that frown upside down, get your chin up and defeat this evil!"},
            { "", "", "", ""},
        },
        new string[,] { // 19 - Pagodeiros, após o resgate
            { "right" , "Paulo", "Paulo_1", "Hoo boy, I thought we were going to die."},
            { "right" , "Gomes", "Gomes_1", "I told you it wasn't a good idea following a bird to hell knows where."},
            { "right" , "De Jesus", "DeJesus_1", "Hey, skewer knight, thanks."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Gomes", "Gomes_1", "What? A safe haven? A barbecue? Did we go crazy and not notice?"},
            { "right" , "De Jesus", "DeJesus_1", "Don't know about that, looking at the guy, there's more to him than meets the eye."},
            { "right" , "Paulo", "Paulo_1", "I trust the skewer kid, you look after me, I look after you."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Paulo", "Paulo_1", "Alrighty, meet you there. C'mon boys!"},
            { "", "", "", ""},
        },
        new string[,] { // 20 - Paulo, primeira interação
            { "right" , "Paulo", "Paulo_1", "Skewer lad! Good to see you here safe. Pleasure to meet, my name is Paulo, I'm part of this trio of men that now owe you their lives."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Paulo", "Paulo_1", "Don't you worry, we'll find a way to repay you."},
            { "", "", "", ""},
        },
        new string[,] { // 21 - Gomes, primeira interação
            { "right" , "Gomes", "Gomes_1", "Oh, hey barbecue boy, I wanted to thank you for having saved us from that thing, let me introduce myself, my name is Gomes, I am part of this band."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Gomes", "Gomes_1", "You can rest assured, we will help you, after all, Barbecue with music is great, and barbecue without music is nothing at all."},
            { "", "", "", ""},
        },
        new string[,] { // 22 - De Jesus, primeira interação
            { "right" , "De Jesus", "DeJesus_1", "Skewer Knight, my thanks. Let me introduce myself, they call me De Jesus, I am a part of this trio that now resides in your sanctuary."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "De Jesus", "DeJesus_1", "Ha, we think about music and you thinkk about barbecues. Go on, break a leg."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "De Jesus", "DeJesus_1", "No, not literally... it's just a saying."},
            { "", "", "", ""},
        },
        new string[,] { // 23 - Paulo. interação repetida com qualquer um antes de ter interagido ao menos uma vez com todos
            { "right" , "Paulo", "Paulo_1", "Fam, thanks again for saving us."},
            { "", "", "", ""},
        },
        new string[,] { // 24 - Gomes. interação Repetida com qualquer um antes de ter interagido ao menos uma vez com todos
            { "right" , "Gomes", "Gomes_1", "Fam, thanks again for saving us."},
            { "", "", "", ""},
        },
        new string[,] { // 25 - De Jesus, interação Repetida com qualquer um antes de ter interagido ao menos uma vez com todos
            { "right" , "De Jesus", "DeJesus_1", "Fam, thanks again for saving us."},
            { "", "", "", ""},
        },
        new string[,] { // 26 - Segunda interação com qualquer um dos pagodeiros depois de já ter interagido com todos
            { "right" , "Paulo", "Paulo_1", "Argh, if I can't have music I'd rather have nothing at all, I just wanna live in peace, whatever it takes."},
            { "right" , "Gomes", "Gomes_1", "This place is surrounded by hardship and tears, and yet, it's full of hope in a better world and a barbecue to celebrate it."},
            { "right" , "De Jesus", "DeJesus_1", "Skewer Knight, you help us believe better days will come and that our time is on the horizon, you must bring back humanity's dream, drinking cold beer and hit one of them ladies with your friends."},
            { "", "", "", ""},
        },
        new string[,] { // 27 - Interação com qualquer um com o Encapuzado no HUB
            { "right" , "Paulo", "Paulo_1", "This hooded weirdo seems it went his whole life walking under stairs, it's giving me the heebie jeebies."},
            { "right" , "Gomes", "Gomes_1", "I don't fully trust this hooded guy, he rubs me off the wrong way..."},
            { "right" , "De Jesus", "DeJesus_1", "The saying goes \"don't judge as to not be judged\", but this hooded person is really bringing the wrong vibe to the sanctuary."},
            { "", "", "", ""},
        },
        new string[,] { // 28 - Encapuzado, primeira interação
            { "right" , "Hooded Guy", "Encapuzado_1", "Ah, good morning, Mr. Knight."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Hooded Guy", "Encapuzado_1", "Oh well, you're right, morning, afternoon, evening, these words have little to no meaning since the Eclipse. But do tell me, what is your business here?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Hooded Guy", "Encapuzado_1", "I see, well, good luck on your journey."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Hooded Guy", "Encapuzado_1", "Oh no, no, no, I am fine just the way I am, but we'll surely meet each other again, see you soon."},
            { "", "", "", ""},
        },
        new string[,] { // 29 - Encapuzado, interação repetida no meio da fase
            { "right" , "Hooded Guy", "Encapuzado_1", "Oh no, don't worry about little old me, really, go on with your journey, if destiny deems it so, our paths shall be intertwined once again."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "", "", "", ""},
        },
        new string[,] { // 30 - Encapuzado, primeira interação no HUB
            { "right" , "Hooded Guy", "Encapuzado_1", "Good morning, my good sir knight, or would it be good evening? Well, I am truly relieved to see you returning safe."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Hooded Guy", "Encapuzado_1", "Oh, I was just walking about when I stumbled across this sanctuary and the people within it, very kind people they are by the way."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Hooded Guy", "Encapuzado_1", "A barbecue? Hahahaha, what an ingenious idea, I do hope you accomplish your goal."},
            { "", "", "", ""},
        },
        new string[,] { // 31 - Encapuzado, segunda Interação no HUB
            { "right" , "Hooded Guy", "Encapuzado_1", "Hello, sir knight, what brings you to talk to little old me? Do you truly have nothing better to do than to talk to a stranger like me?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Hooded Guy", "Encapuzado_1", "Oh yes, the Eclipse Dungeon. If you would be so kind as to satiate my curiosity... why go there?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Hooded Guy", "Encapuzado_1", "Of course, a Barbecue wouldn't be as nice without the sun..."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Hooded Guy", "Encapuzado_1", "Oh, yes, I do know how to enter the dungeon. Well, even though I don't have the key, I heard rumors that there is a secret passage, a shortcut of sorts. It is well guarded, but I do believe a warrior such as yourself has little to fear there."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Hooded Guy", "Encapuzado_1", "Oh it's nothing, really, You did save me from dying of boredom after all."},
            { "", "", "", ""},
        },
        new string[,] { // 32 - Encapuzado, interação no meio da fase 3.5
            { "right" , "Hooded Guy", "Encapuzado_1", "Here, sir knight, the shortcut is just up ahead, come quickly!"},
            { "", "", "", ""},
        },
        new string[,] { // 33 - Encapuzado, interação repetida no meio da fase 3.5
            { "right" , "Hooded Guy", "Encapuzado_1", "Go on sir knight! We wouldn't want it to close now would we?"},
            { "", "", "", ""},
        },
        new string[,] { // 34 - Encapuzado, quando você encontra ele na sala do Boss Final
            { "right" , "Hooded Guy", "Encapuzado_1", "Hello, good sir knight, I was getting tired of waiting."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Hooded Guy", "Encapuzado_1", "I must admit, you truly are stronger than I could ever imagine, when I brought you back I didn't expect you to be so fun to watch, I mean, hahaha, a barbecue? Ha, it's such an absurd idea seeing the way the world is now, but off you go, punging yourself head first into danger to find plymates for your little game."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Hooded Guy", "Encapuzado_1", "You just don't get it, do you? Well, monologuing isn't getting us anywhere, if you want your sun back to do your little barbecue, you will have to beat me first."},
            { "", "", "", ""},
        },
        new string[,] { // 35 - Encapuzado, quando é derrotado, mas você não completou todas as side quests
            { "right" , "Hooded Guy", "Encapuzado_1", "Haha... hahaha... you imbecile... you'll be gone... along with the eclipse goes you and everything it brought... you truly were... blinded by this accursed barbecue..."},
            { "", "", "", ""},
        },
        new string[,] { // 36 - Encapuzado, quando você encontra ele na sala do Boss Final
            { "right" , "Hooded Guy", "Encapuzado_1", "Haha... hahaha... you imbecile... you'll be gone... along with the eclipse goes you and everything it brought..."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Hooded Guy", "Encapuzado_1", "Wha... how?... hahaha... you just can't be boring... live the rest of your miserable little life... Sabet..."},
            { "", "", "", ""},
        },
        new string[,] { // 37 - VAZIA
            { "left", "???", "Sabet_1", "Dialog not implemented yet."},
            { "", "", "", ""},
        },
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
            { "right", "Ferreira", "Ferreira_1", "Olá amigo, é ótimo lhe ver aqui, e agradeço de novo por me resgatar. Acho que não me apresentei, meu nome é Ferreira, sou um Ferreiro de primeira linha, ou ao menos... eu era... antes de todo esse desastre."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Ferreira", "Ferreira_1", "Fico feliz em poder lhe ajudar com o que quer que precise."},
            { "", "", "", ""},
        },
        new string[,] { // 3 - Ferreiro, hub 2
            { "right" , "Ferreira", "Ferreira_1", "Opa meu chefe, em que posso ajudar neste dia?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Ferreira", "Ferreira_1", "O acesso pra caverna de gelo? Posso manufaturar uma chave pra ela sim, mas devo avisar, é um lugar bastante perigoso, ouvi rumores de que há bichos venenosos gigantes lá, e eu odiaria ver o cara que me salvou morrendo tão jovem."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Ferreira", "Ferreira_1", "Tudo bem, tudo bem, você que manda..."},
            { "", "", "", ""},
        },
        new string[,] { // 4 - Ferreiro, hub 3
            { "right" , "Ferreira", "Ferreira_1", "Olha aí, justo quem eu queria ver!"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Ferreira", "Ferreira_1", "Bem, tenho boas e más notícias."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Ferreira", "Ferreira_1", "A boa é que eu consegui lembrar de uma velha técnica para melhorar armaduras como a sua, mesmo que couro não seja minha especialidade."},
            { "left" , "Sabet", "Sabet_1", "...?"},
            { "right" , "Ferreira", "Ferreira_1", "Bem, a má é que as ferramentas que eu preciso estão na Floresta da qual você me resgatou, e pelo que parece, aquele bicho grande e feio que ia me matar voltou maior e mais feio."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Ferreira", "Ferreira_1", "Bem, não quero que se ponha em risco, mas se conseguir trazer as ferramentas pra mim eu melhoro a sua armadura num instante!"},
            { "", "", "", ""},
        },
        new string[,] { // 5 - Ferreiro, hub 4, interação quando a quest ainda não foi concluída
            { "right" , "Ferreira", "Ferreira_1", "Opa patrão, beleza? Trabalhando duro?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Ferreira", "Ferreira_1", "É, queria poder dizer o mesmo, mas minhas ferramentas ficaram na floresta, uma lástima."},
            { "", "", "", ""},
        },
        new string[,] { // 6 - Ferreiro, hub 4, interação quando conclui a quest
            { "right" , "Ferreira", "Ferreira_1", "Opa chefia, beleza? Trabalhando duro?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Ferreira", "Ferreira_1", "Oh!! Minhas ferramentas!!! Vem pro papai!"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Ferreira", "Ferreira_1", "Ah, sim, claro, como prometido, me deixe ver sua armadura por um segundo e eu lhe devolvo ela nos trinques!"},
            { "right" , "Ferreira", "Ferreira_1", "Prontinho chefe, tudo nos trinques usando os melhores materiais que eu tenho, perfeito pra um herói como você."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Ferreira", "Ferreira_1", "Haha, disponha."},
            { "", "", "", ""},
        },
        new string[,] { // 7 - Ferreiro, hub 5, interações entre quests
            { "right" , "Ferreira", "Ferreira_1", "Bom dia chefe, passando pra ver como estou?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Ferreira", "Ferreira_1", "Haha, pois é, loucura mesmo."},
            { "", "", "", ""},
        },
        new string[,] { // 8 - Ferreiro, hub, quando o encapuzado está presente
            { "right" , "Ferreira", "Ferreira_1", "Oi chefe... olha, eu não sou de julgar por aparências, mas aquele sujeito novo não é bife que se coma, por favor, tenha cuidado."},
            { "", "", "", ""},
        },
        new string[,] { // 9 - Bruna, primeiro encontro
            { "right" , "Bruna", "Bruna_1", "Aaaaaaaaaah! Você aí, uma ajudinha?"},
            { "", "", "", ""},
        },
        new string[,] { // 10 - Bruna, depois do resgate
            { "right" , "Bruna", "Bruna_1", "Ufa, obrigada, por um segundo achei que ia virar comida de aranha, e pior, de uma aranha feia."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "Que? Um santuário? Churrasco? O Eclipse te deixou maluco também? Ah, mas qualquer lugar é melhor que essa maldita caverna, odeio o frio, bem, te vejo lá bonitão."},
            { "", "", "", ""},
        },
        new string[,] { // 11 - Bruna, hub
            { "right" , "Bruna", "Bruna_1", "Oi Cavaleirinho! Acho que aquela situação não era a melhor pra um primeiro encontro, então vamos começar de novo. Prazer, eu sou Bruna, a Bruxa, e estou te devendo uma já que você salvou minha vida e tals."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "Entendi, você é do tipo caladão, legal... legal..."},
            { "", "", "", ""},
        },
        new string[,] { // 12 - Bruna, hub 2
            { "right" , "Bruna", "Bruna_1", "Oi maninho do churras, o que eu posso fazer por você neste... dia? noite?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "O selo mágico pra cidade? Bem, provavelmente foi aquela aranha horrorosa que colocou lá, parece simples o suficiente pra eu quebrar, por quê?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "Você quer ir pra cidade? A cidade infestada de mutantes e cultistas? A cidade que tá só o caco desde que aconteceu todo esse apocaeclipse?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "Tá bom. Boa sorte, vê se não morre."},
            { "", "", "", ""},
        },
        new string[,] { // 13 - Bruna, hub, quest
            { "right" , "Bruna", "Bruna_1", "Opa, Churrasquinho, posso falar com você um minuto?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "Eu sei que eu já estou te devendo uma por, sabe, ter salvado minha vida e blá blá blá, mas se eu puder te pedir um favorzinho eu posso ajudar nessa sua aventura aí, o que me diz?"},
            { "left" , "Sabet", "Sabet_1", "...?"},
            { "right" , "Bruna", "Bruna_1", "Sabia que você entenderia. Basicamente, eu não fui arrastada praquela caverna pelo bicho feio, eu estava lá pra estudar uma nova variante desses selvagens que se espalharam desde o Eclipse pra ver se acho uma origem comum da magia e talvez consiga reverter o Eclipse."},
            { "left" , "Sabet", "Sabet_1", "...?"},
            { "right" , "Bruna", "Bruna_1", "Eu sei que eu disse que odeio frio, mas precisamos sacrificar coisas pelo bem maior. Como eu estava dizendo, eu fui lá pra pesquisar até que fui capturada pela baranga de oito patas, e quando você me resgatou eu meio que esqueci meu bloco de notas lá..."},
            { "left" , "Sabet", "Sabet_1", "...?"},
            { "right" , "Bruna", "Bruna_1", "Cala a boca! Enfim, se você for lá buscar eu te dou uma cópia do livro, talvez facilite você enfrentar esses bichos feios se você souber com o que você está lidando."},
            { "left" , "Sabet", "Sabet_1", "...?"},
            { "right" , "Bruna", "Bruna_1", "Sabia que você veria meu ponto, agora vai lá, eu espero."},
            { "", "", "", ""},
        },
        new string[,] { // 14 - Bruna, hub, quest incompleta
            { "right" , "Bruna", "Bruna_1", "Oi Churrasquinho, como está sendo a aventura?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "Como assim você ainda não pegou o bloco de notas? Tem coisas importantes lá, sabia? Acelera aí meu filho!"},
            { "", "", "", ""},
        },
        new string[,] { // 15 - Bruna, hub, quest completa
            { "right" , "Bruna", "Bruna_1", "Oi Churrasquinho, como está indo?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "O LIVRO ESTAVA ABERTO!? Você... você leu?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "É bom mesmo! Bem, como prometido, toma aqui uma cópia com os detalhes importantes sobre as aberrações."},
            { "", "", "", ""},
        },
        new string[,] { // 16 - Bruna, hub, interação entre quests
            { "right" , "Bruna", "Bruna_1", "Oi Churrasquinho, como que vai na luta?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "NÃO É UM DIÁRIO! É UM CADERNO DE ANOTAÇÕES MÁGICAS IMPORTANTÍSSIMAS!... no qual eu escrevo meus sentimentos e acontecimentos do dia a dia..."},
            { "", "", "", ""},
        },
        new string[,] { // 17 - Bruna, hub, interação quando o encapuzado está no HUB
            { "right" , "Bruna", "Bruna_1", "Ei Churras boy, eu sou a última pessoa que julgaria pela aparência, mas eu não sei se eu confio nesse cara novo não, ele tem literais luas nos olhos, tipo, alô? O apocalipse veio por causa de um eclipse! Bem, só dorme com um olho aberto enquanto ele estiver aqui beleza?"},
            { "", "", "", ""},
        },
        new string[,] { // 18 - Pagodeiros, primeiro encontro
            { "right" , "Paulo", "Paulo_1", "Meu Deus! Não era pássaro, era cilada!"},
            { "right" , "Gomes", "Gomes_1", "Talvez seja tarde, mas a realidade é que eu não quero morrer!"},
            { "right" , "De Jesus", "DeJesus_1", "Aí cavaleiro do Espetinho! Deixa de lado esse baixo atral, erga a cabeça, enfrente o mal!"},
            { "", "", "", ""},
        },
        new string[,] { // 19 - Pagodeiros, após o resgate
            { "right" , "Paulo", "Paulo_1", "Eita rapaz, achei que a gente ia morrer."},
            { "right" , "Gomes", "Gomes_1", "Eu avisei que não ia dar bom seguir um passarinho no fim do mundo."},
            { "right" , "De Jesus", "DeJesus_1", "Aí cavaleiro do espetinho, valeu mesmo."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Gomes", "Gomes_1", "Quê? Um lugar seguro? Um churrasco? A gente ficou louco e não percebeu?"},
            { "right" , "De Jesus", "DeJesus_1", "Sei não, olhando pra esse cara aí, os olhos dele dizem coisas que você não vê."},
            { "right" , "Paulo", "Paulo_1", "Eu confio no guri do espetinho, cuida de mim que eu cuido de você."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Paulo", "Paulo_1", "Beleza, a gente se encontra lá, vamos rapazes!"},
            { "", "", "", ""},
        },
        new string[,] { // 20 - Paulo, primeira interação
            { "right" , "Paulo", "Paulo_1", "Guri do espetinho! Que bom te ver aqui seguro. Prazer, meu nome é Paulo, sou membro desse trio de pagodeiros que agora te deve a vida."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Paulo", "Paulo_1", "Relaxa, a gente vai dar um jeito de pagar esse favor."},
            { "", "", "", ""},
        },
        new string[,] { // 21 - Gomes, primeira interação
            { "right" , "Gomes", "Gomes_1", "Opa, e aí garoto do churrasco, queria agradecer por ter nos salvado daquela coisa, deixa eu me apresentar, meu nome é Gomes, faço parte desse trio de pagodeiros que lhe fala."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Gomes", "Gomes_1", "Pode crer que vamos te ajudar, afinal, Churrasco com pagode é muito bom, e churrasco sem pagode não é nada."},
            { "", "", "", ""},
        },
        new string[,] { // 22 - De Jesus, primeira interação
            { "right" , "De Jesus", "DeJesus_1", "Cavaleiro do espetinho, minha sincera gratidão. Deixe-me me apresentar, me chama De Jesus, sou parte dessa tríade de pagodeiros que está agora em seu santuário."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "De Jesus", "DeJesus_1", "Ha, a gente pensando em pagode e você pensando em churrasco, mas lembre-se sempre cavaleiro do espeto, Ousadia pra vencer, alegria pra viver."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "De Jesus", "DeJesus_1", "Não é nada, só um ditado antigo."},
            { "", "", "", ""},
        },
        new string[,] { // 23 - Paulo. interação repetida com qualquer um antes de ter interagido ao menos uma vez com todos
            { "right" , "Paulo", "Paulo_1", "Chapa, obrigado de novo por ter nos salvado."},
            { "", "", "", ""},
        },
        new string[,] { // 24 - Gomes. interação Repetida com qualquer um antes de ter interagido ao menos uma vez com todos
            { "right" , "Gomes", "Gomes_1", "Chapa, obrigado de novo por ter nos salvado."},
            { "", "", "", ""},
        },
        new string[,] { // 25 - De Jesus, interação Repetida com qualquer um antes de ter interagido ao menos uma vez com todos
            { "right" , "De Jesus", "DeJesus_1", "Chapa, obrigado de novo por ter nos salvado."},
            { "", "", "", ""},
        },
        new string[,] { // 26 - Segunda interação com qualquer um dos pagodeiros depois de já ter interagido com todos
            { "right" , "Paulo", "Paulo_1", "Se for pra ficar sem pagode pra mim não dá, eu quero mais é viver em paz e ser feliz, seja como for."},
            { "right" , "Gomes", "Gomes_1", "Este lugar é cercado de luta e suor, mas mais ainda, é cheio de esperança num mundo melhor e churrasco pra comemorar."},
            { "right" , "De Jesus", "DeJesus_1", "Cavaleiro do Espetinho, você nos faz acreditar que um novo dia vai raiar e que a nossa hora vai chegar, você tem quer trazer de volta o sonho da humanidade, beber uma cerveja bem gelada e dar aquela paquerada junto com a rapaziada."},
            { "", "", "", ""},
        },
        new string[,] { // 27 - Interação com qualquer um com o Encapuzado no HUB
            { "right" , "Paulo", "Paulo_1", "Esse maluco de capuz aí parece que passou a vida inteira andando debaixo de escada, tá me dando um mau presságio."},
            { "right" , "Gomes", "Gomes_1", "Eu não confio nesse tal encapuzado, me passa uma energia esquisita."},
            { "right" , "De Jesus", "DeJesus_1", "O ditado é \"então não julgue pra depois não ser julgado\", mas esta pessoa de capuz está estragando o clima no santuário."},
            { "", "", "", ""},
        },
        new string[,] { // 28 - Encapuzado, primeira interação
            { "right" , "Encapuzado", "Encapuzado_1", "Opa, bom dia cavaleiro."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Encapuzado", "Encapuzado_1", "Oh bem, tem razão, bom dia, boa tarde, boa noite, essas frases meio que perderam o significado desde o Eclipse. Mas, me diga, o que veio fazer aqui?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Encapuzado", "Encapuzado_1", "Entendo, bem, boa sorte em sua jornada."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Encapuzado", "Encapuzado_1", "Oh não, não, eu estou bem por essas bandas, mas tenho certeza que nos veremos novamente, até logo."},
            { "", "", "", ""},
        },
        new string[,] { // 29 - Encapuzado, interação repetida no meio da fase
            { "right" , "Encapuzado", "Encapuzado_1", "Oh, não se preocupe comigo, sério, siga sua jornada, se o destino mandar, nossos caminhos passarão um pelo outro novamente."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "", "", "", ""},
        },
        new string[,] { // 30 - Encapuzado, primeira interação no HUB
            { "right" , "Encapuzado", "Encapuzado_1", "Bom dia nobre cavaleiro, ou seria boa noite? Bem, fico aliviado de ver que você retornou salvo."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Encapuzado", "Encapuzado_1", "Ah, eu estava apenas caminhando quando me deparei com este santuário e as pessoas que vivem aqui, muito simpáticas por sinal."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Encapuzado", "Encapuzado_1", "Um churrasco? Ha, que ideia genial, espero que consiga o que deseja."},
            { "", "", "", ""},
        },
        new string[,] { // 31 - Encapuzado, segunda Interação no HUB
            { "right" , "Encapuzado", "Encapuzado_1", "Olá Cavaleiro, o que lhe traz a mim? Quer jogar conversa fora?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Encapuzado", "Encapuzado_1", "Ah, sim, a masmorra do Eclipse, se não se incomoda com a pergunta, por que quer ir lá?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Encapuzado", "Encapuzado_1", "Verdade, verdade, churrasco não é a mesma coisa sem o sol..."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Encapuzado", "Encapuzado_1", "Ah sim, claro, eu sei como entrar. Bem, eu não tenho a chave, mas na cidade eu ouvi dizer que há uma passagem secreta, uma espécie de atalho para a masmorra, é bem protegida, mas creio que um cavaleiro com suas capacidades consiga entrar sem problemas."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Encapuzado", "Encapuzado_1", "Disponha, você me salvou de morrer de tédio, é o mínimo que eu posso fazer."},
            { "", "", "", ""},
        },
        new string[,] { // 32 - Encapuzado, interação no meio da fase 3.5
            { "right" , "Encapuzado", "Encapuzado_1", "Nobre cavaleiro! O atalho para a masmorra é logo ali, venha!"},
            { "", "", "", ""},
        },
        new string[,] { // 33 - Encapuzado, interação repetida no meio da fase 3.5
            { "right" , "Encapuzado", "Encapuzado_1", "Vamos vamos, não queremos que o atalho seja fechado não é?"},
            { "", "", "", ""},
        },
        new string[,] { // 34 - Encapuzado, quando você encontra ele na sala do Boss Final
            { "right" , "Encapuzado", "Encapuzado_1", "Olá Nobre Cavaleiro, eu estava te esperando."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Encapuzado", "Encapuzado_1", "Devo admitir, você é mais forte do que eu esperava, quando te trouxe de volta não esperava que fosse ser tão divertido te acompanhar, digo, um churrasco? Ha, é uma ideia estúpida demais com o jeito que o mundo está, mas lá vai você, se jogar de cabeça no perigo iminente pra achar companheiros pro seu joguinho."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Encapuzado", "Encapuzado_1", "Você não entendeu ainda? Oh bem, monologar não vai nos levar a lugar nenhum, se você quer o sol de volta pra fazer seu churrasco, você vai ter que me derrotar primeiro."},
            { "", "", "", ""},
        },
        new string[,] { // 35 - Encapuzado, quando é derrotado, mas você não completou todas as side quests
            { "right" , "Encapuzado", "Encapuzado_1", "Ha ha.... ha ha ha... seu imbecil... você vai deixar de existir... junto com tudo que o Eclipse trouxe... você realmente... foi cegado pelo desejo por churrasco..."},
            { "", "", "", ""},
        },
        new string[,] { // 36 - Encapuzado, quando você encontra ele na sala do Boss Final
            { "right" , "Encapuzado", "Encapuzado_1", "Ha ha... ha ha ha... seu imbecil... você vai deixar de existir... junto com tudo que o Eclipse trouxe..."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Encapuzado", "Encapuzado_1", "O que... como... ha ha ha... você realmente não me deixa entediado... viva o resto da sua vida miserável... Sabet..."},
            { "", "", "", ""},
        },
        new string[,] { // 37 - VAZIA
            { "left", "???", "Sabet_1", "Dialogo não implementado ainda."},
            { "", "", "", ""},
        },
    };

    public static string[][,] spanish_dialog_bank_new = new string[][,]
    {
        new string[,] { // 0 - Ferreiro, primeiro encontro
            { "right", "Smith", "Ferreira_1", "¡¡¡Ayuda!!! Por favor, ¿alguien? ¡Alguien, por favor! ¡Este monstruo me va a matar!"},
            { "", "", "", ""}
        },
        new string[,] { // 1 - Ferreiro, depois do boss
            { "right", "Smith", "Ferreira_1", "Oh, cielos, gracias..."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "¿Un santuario? ¿En este desastre de mundo?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right", "Smith", "Ferreira_1", "¿UNA ASADO? No... tienes razón, cualquier cosa es mejor que estar abandonado en este bosque con estos monstruos... tú no eres uno de ellos... ¿verdad?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "Bueno, nos vemos allí."},
            { "", "", "", ""},
        },
        new string[,] { // 2 - Ferreiro, hub
            { "right", "Smith", "Ferreira_1", "Hola amigo, me alegro de verte por aquí, y gracias de nuevo por rescatarme. Creo que no me he presentado, mi nombre es Ferreira, soy un Herrero de primera, o al menos... lo era... antes de todo este desastre."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "Estaré encantado de ayudarte en lo que necesites."},
            { "", "", "", ""},
        },
        new string[,] { // 3 - Ferreiro, hub 2
            { "right" , "Smith", "Ferreira_1", "Vaya, mi jefe, ¿en qué puedo ayudarle hoy?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "¿El acceso a la cueva de hielo? Puedo fabricar una llave para ella sí, pero debo advertirte, es un lugar bastante peligroso, he oído rumores de que hay bestias venenosas gigantes allí, y odiaría ver morir tan joven al tipo que me salvó."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "Está bien, está bien, tú eres el jefe..."},
            { "", "", "", ""},
        },
        new string[,] { // 4 - Ferreiro, hub 3
            { "right" , "Smith", "Ferreira_1", "¡Mira, justo a quien quería ver!"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "Bueno, tengo buenas y malas noticias."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "La buena es que he conseguido recordar una vieja técnica para mejorar armaduras como la tuya, aunque el cuero no sea mi especialidad."},
            { "left" , "Sabet", "Sabet_1", "...?"},
            { "right" , "Smith", "Ferreira_1", "Bueno, la mala es que las herramientas que necesito están en el Bosque del que me rescataste, y por lo que parece, ese bicho grande y feo que iba a matarme ha vuelto más grande y más feo."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "Bueno, no quiero que te pongas en peligro, pero si puedes traerme las herramientas, ¡te mejoraré la armadura en un santiamén!"},
            { "", "", "", ""},
        },
        new string[,] { // 5 - Ferreiro, hub 4, interação quando a quest ainda não foi concluída
            { "right" , "Smith", "Ferreira_1", "Hey jefe, ¿qué pasa? ¿Trabajando duro?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "Si, ojala pudiera decir lo mismo, pero mis herramientas se quedaron en el bosque, que pena."},
            { "", "", "", ""},
        },
        new string[,] { // 6 - Ferreiro, hub 4, interação quando conclui a quest
            { "right" , "Smith", "Ferreira_1", "Hey jefe, ¿qué pasa? ¿Trabajando duro?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "¡Oh! ¡Mis herramientas! ¡Ven con papá!"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "¡Ah, sí, claro, como te prometí, déjame ver tu armadura un segundo y te la devuelvo directamente!"},
            { "right" , "Smith", "Ferreira_1", "Ahí la tienes jefe, toda de rosa usando los mejores materiales que tengo, perfecta para un héroe como tú."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "Jaja, adelante."},
            { "", "", "", ""},
        },
        new string[,] { // 7 - Ferreiro, hub 5, interações entre quests
            { "right" , "Smith", "Ferreira_1", "Buenos días jefe, ¿te pasas a ver cómo estoy?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Smith", "Ferreira_1", "Jaja, sí, una locura."},
            { "", "", "", ""},
        },
        new string[,] { // 8 - Ferreiro, hub, quando o encapuzado está presente
            { "right" , "Smith", "Ferreira_1", "Hola jefe... mira, no soy quien para juzgar por las apariencias, pero ese tipo nuevo no es un filete para comérselo, por favor ten cuidado."},
            { "", "", "", ""},
        },
        new string[,] { // 9 - Bruna, primeiro encontro
            { "right" , "Bruna", "Bruna_1", "¡Aaaaaaaaaaaaaah! ¿Estás ahí, una ayudita?"},
            { "", "", "", ""},
        },
        new string[,] { // 10 - Bruna, depois do resgate
            { "right" , "Bruna", "Bruna_1", "Uf, gracias, por un segundo pensé que me iba a comer una araña, y peor, una araña fea."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "¿Qué? ¿Un santuario? ¿Una asado? ¿El Eclipse también te volvió loco? Ah, pero cualquier sitio es mejor que esta maldita cueva, odio el frío, bueno, nos vemos allí guapo."},
            { "", "", "", ""},
        },
        new string[,] { // 11 - Bruna, hub
            { "right" , "Bruna", "Bruna_1", "¡Hola, Pequeño Jinete! Supongo que la situación no era la mejor para una primera cita, así que empecemos de nuevo. Un placer, soy Bruna la Bruja y te debo una ya que me salvaste la vida y eso."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "Entendido, eres del tipo tranquilo, agradable... agradable..."},
            { "", "", "", ""},
        },
        new string[,] { // 12 - Bruna, hub 2
            { "right" , "Bruna", "Bruna_1", "Hey hermano asado, ¿qué puedo hacer por ti este... día? noche?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "¿El sello mágico para la ciudad? Bueno, probablemente fue esa asquerosa araña la que lo puso ahí, parece bastante sencillo para mí romperlo, ¿por qué?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "¿Quieres ir a la ciudad? ¿La ciudad infestada de mutantes y cultistas? ¿La ciudad que está destrozada desde que pasó lo del apocaeclipse?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "Vale. Buena suerte, asegúrate de no morir."},
            { "", "", "", ""},
        },
        new string[,] { // 13 - Bruna, hub, quest
            { "right" , "Bruna", "Bruna_1", "Whoa, Asado, ¿puedo hablar contigo un minuto?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "Sé que ya te debo una por, ya sabes, salvarme la vida y bla bla bla, pero si puedo pedirte un pequeño favor puedo ayudarte en tu aventura allí, ¿qué dices?"},
            { "left" , "Sabet", "Sabet_1", "...?"},
            { "right" , "Bruna", "Bruna_1", "Sabía que lo entenderías. Básicamente, a mí no me arrastró a esa cueva el bicho feo, yo estaba allí para estudiar una nueva variante de estos salvajes que se han extendido desde el Eclipse para ver si podía encontrar una fuente común de magia y tal vez revertir el Eclipse."},
            { "left" , "Sabet", "Sabet_1", "...?"},
            { "right" , "Bruna", "Bruna_1", "Sé que he dicho que odio el frío, pero hay que sacrificar cosas por un bien mayor. Como te decía, fui allí a investigar hasta que me capturó el baranga de ocho patas, y cuando me rescataste como que olvidé mi cuaderno allí..."},
            { "left" , "Sabet", "Sabet_1", "...?"},
            { "right" , "Bruna", "Bruna_1", "¡Cállate! De todos modos, si vas a buscarlo te daré una copia del libro, tal vez te sea más fácil enfrentarte a esas feas bestias si sabes a lo que te enfrentas."},
            { "left" , "Sabet", "Sabet_1", "...?"},
            { "right" , "Bruna", "Bruna_1", "Sabía que me entenderías, ahora vete, te espero."},
            { "", "", "", ""},
        },
        new string[,] { // 14 - Bruna, hub, quest incompleta
            { "right" , "Bruna", "Bruna_1", "Hola Asado, ¿cómo va la aventura?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "¿Cómo que aún no tienes tu cuaderno? Hay cosas importantes ahí, ¿sabes? ¡Acelera hijo mío!"},
            { "", "", "", ""},
        },
        new string[,] { // 15 - Bruna, hub, quest completa
            { "right" , "Bruna", "Bruna_1", "Hola Asado, ¿cómo te va?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "¿EL LIBRO ESTABA ABIERTO? ¿Lo... lo leíste? "},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "¡Es bueno de verdad! Bueno, como prometí, aquí tienes una copia con los detalles importantes sobre las aberraciones."},
            { "", "", "", ""},
        },
        new string[,] { // 16 - Bruna, hub, interação entre quests
            { "right" , "Bruna", "Bruna_1", "Oye Asado, ¿cómo te va en el combate?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Bruna", "Bruna_1", "¡NO ES UN DIARIO! Es un PAPEL DE NOTAS MÁGICAS IMPORTANTE! ... en el que escribo mis sentimientos y acontecimientos diarios..."},
            { "", "", "", ""},
        },
        new string[,] { // 17 - Bruna, hub, interação quando o encapuzado está no HUB
            { "right" , "Bruna", "Bruna_1", "Hey chico Churras, soy la última persona que juzgaría por las apariencias, pero no sé si confiar en este chico nuevo no, tiene lunas literales en los ojos, como, ¿hola? ¡El apocalipsis vino por un eclipse! Bueno, duerme con un ojo abierto mientras esté aquí, ¿vale?"},
            { "", "", "", ""},
        },
        new string[,] { // 18 - Pagodeiros, primeiro encontro
            { "right" , "Paulo", "Paulo_1", "¡Dios mío! No era un pájaro, ¡era una trampa!"},
            { "right" , "Gomes", "Gomes_1", "¡Quizá sea demasiado tarde, pero la realidad es que no quiero morir!"},
            { "right" , "De Jesus", "DeJesus_1", "¡Eh, caballero del escupitajo! Deja a un lado ese bajo atral, levanta la cabeza, ¡enfréntate al mal!"},
            { "", "", "", ""},
        },
        new string[,] { // 19 - Pagodeiros, após o resgate
            { "right" , "Paulo", "Paulo_1", "Mi niño, pensé que íbamos a morir."},
            { "right" , "Gomes", "Gomes_1", "Te dije que no es bueno seguir a un pajarito en el fin del mundo."},
            { "right" , "De Jesus", "DeJesus_1", "Hola, caballero del kebab, muchas gracias."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Gomes", "Gomes_1", "¿Qué? ¿Un lugar seguro? ¿Una asado? Nos hemos vuelto locos y no nos hemos dado cuenta."},
            { "right" , "De Jesus", "DeJesus_1", "No sé, mirando a este tío sus ojos te dicen cosas que no ves."},
            { "right" , "Paulo", "Paulo_1", "Yo confío en el chico del pincho, cuida de mí y yo cuidaré de ti."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Paulo", "Paulo_1", "Vale, nos vemos allí, ¡vamos chicos!"},
            { "", "", "", ""},
        },
        new string[,] { // 20 - Paulo, primeira interação
            { "right" , "Paulo", "Paulo_1", "¡Chico Kaboom! Encantado de verte aquí a salvo. Un placer, me llamo Paulo, soy miembro de este trío de pagodeiros que ahora os deben la vida."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Paulo", "Paulo_1", "Tranquilo, encontraremos la forma de devolverte este favor."},
            { "", "", "", ""},
        },
        new string[,] { // 21 - Gomes, primeira interação
            { "right" , "Gomes", "Gomes_1", "Hola, chico de la asado, me gustaría darte las gracias por salvarnos de esa cosa, déjame que me presente, me llamo Gomes, soy miembro de este trío de pagodeiros que te dice."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Gomes", "Gomes_1", "Créeme, te ayudaremos, después de todo, la asado con pagode es muy buena, y la asado sin pagode no es nada."},
            { "", "", "", ""},
        },
        new string[,] { // 22 - De Jesus, primeira interação
            { "right" , "De Jesus", "DeJesus_1", "Caballero del pincho, mi más sincera gratitud. Permíteme presentarme, llámame De Jesús, soy parte de esta tríada de pagodas que ahora están en tu santuario."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "De Jesus", "DeJesus_1", "Ja, nosotros estamos pensando en pagode y tú en churrasco, pero recuerda siempre, caballero del pincho, audacia para ganar, alegría para vivir."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "De Jesus", "DeJesus_1", "No es nada, sólo un viejo refrán."},
            { "", "", "", ""},
        },
        new string[,] { // 23 - Paulo. interação repetida com qualquer um antes de ter interagido ao menos uma vez com todos
            { "right" , "Paulo", "Paulo_1", "Camarada, gracias de nuevo por salvarnos."},
            { "", "", "", ""},
        },
        new string[,] { // 24 - Gomes. interação Repetida com qualquer um antes de ter interagido ao menos uma vez com todos
            { "right" , "Gomes", "Gomes_1", "Camarada, gracias de nuevo por salvarnos."},
            { "", "", "", ""},
        },
        new string[,] { // 25 - De Jesus, interação Repetida com qualquer um antes de ter interagido ao menos uma vez com todos
            { "right" , "De Jesus", "DeJesus_1", "Camarada, gracias de nuevo por salvarnos."},
            { "", "", "", ""},
        },
        new string[,] { // 26 - Segunda interação com qualquer um dos pagodeiros depois de já ter interagido com todos
            { "right" , "Paulo", "Paulo_1", "No puedo prescindir del pagode, sólo quiero vivir en paz y ser feliz, pase lo que pase."},
            { "right" , "Gomes", "Gomes_1", "Este lugar está rodeado de lucha y sudor, pero más aún, está lleno de esperanza por un mundo mejor y una asado para celebrarlo."},
            { "right" , "De Jesus", "DeJesus_1", "Nos haces creer que amanecerá un nuevo día y llegará nuestro momento, tienes que traer de vuelta el sueño de la humanidad, beber una cerveza fría y ligar con los chicos."},
            { "", "", "", ""},
        },
        new string[,] { // 27 - Interação com qualquer um com o Encapuzado no HUB
            { "right" , "Paulo", "Paulo_1", "Este engendro encapuchado parece que se ha pasado la vida caminando bajo escaleras, me está dando un mal presagio."},
            { "right" , "Gomes", "Gomes_1", "No me fío de este encapuchado, me da una energía rara."},
            { "right" , "De Jesus", "DeJesus_1", "El refrán dice \"no juzguéis, para que no seáis juzgados\", pero este encapuchado está estropeando el ambiente del santuario."},
            { "", "", "", ""},
        },
        new string[,] { // 28 - Encapuzado, primeira interação
            { "right" , "Encapuchado", "Encapuzado_1", "Vaya, buenos días caballero."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Encapuchado", "Encapuzado_1", "Oh bueno, tienes razón, buenos días, buenas tardes, buenas noches, esas frases como que han perdido su significado desde el Eclipse. Pero dime, ¿qué has venido a hacer aquí?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Encapuchado", "Encapuzado_1", "Ya veo, bueno, buena suerte en tu viaje."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Encapuchado", "Encapuzado_1", "Oh no, no, estoy bien por allí, pero seguro que nos volveremos a ver, hasta luego."},
            { "", "", "", ""},
        },
        new string[,] { // 29 - Encapuzado, interação repetida no meio da fase
            { "right" , "Encapuchado", "Encapuzado_1", "Oh, no te preocupes por mí, de verdad, sigue tu viaje, si el destino lo dicta, nuestros caminos volverán a cruzarse."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "", "", "", ""},
        },
        new string[,] { // 30 - Encapuzado, primeira interação no HUB
            { "right" , "Encapuchado", "Encapuzado_1", "Buenos días noble caballero, ¿o serían buenas tardes? Bueno, me alivia ver que has vuelto sano y salvo."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Encapuchado", "Encapuzado_1", "Ah, estaba paseando cuando me encontré con este santuario y la gente que vive aquí, gente muy agradable por cierto."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Encapuchado", "Encapuzado_1", "¿Una asado? Ja, qué gran idea, espero que consigas lo que quieres."},
            { "", "", "", ""},
        },
        new string[,] { // 31 - Encapuzado, segunda Interação no HUB
            { "right" , "Encapuchado", "Encapuzado_1", "Hola Caballero, ¿qué te trae por aquí? ¿Te gustaría tener una pequeña charla?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Encapuchado", "Encapuzado_1", "Ah sí, la mazmorra de Eclipse, si no te importa la pregunta, ¿por qué quieres ir allí?"},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Encapuchado", "Encapuzado_1", "Cierto, cierto, la asado no es lo mismo sin el sol..."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Encapuchado", "Encapuzado_1", "Ah sí, claro, ya sé cómo entrar. Bueno, no tengo la llave, pero en la ciudad he oído que hay un pasadizo secreto, una especie de atajo a la mazmorra, está bien protegido, pero creo que un caballero con tus habilidades puede entrar sin problemas."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Encapuchado", "Encapuzado_1", "De nada, me has salvado de morir de aburrimiento, es lo menos que puedo hacer."},
            { "", "", "", ""},
        },
        new string[,] { // 32 - Encapuzado, interação no meio da fase 3.5
            { "right" , "Encapuchado", "Encapuzado_1", "¡Noble caballero! El atajo a la mazmorra está por allí, ¡ven!"},
            { "", "", "", ""},
        },
        new string[,] { // 33 - Encapuzado, interação repetida no meio da fase 3.5
            { "right" , "Encapuchado", "Encapuzado_1", "Vamos vamos, no queremos que se cierre el atajo ¿verdad?"},
            { "", "", "", ""},
        },
        new string[,] { // 34 - Encapuzado, quando você encontra ele na sala do Boss Final
            { "right" , "Encapuchado", "Encapuzado_1", "Hola Noble Caballero, te estaba esperando."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Encapuchado", "Encapuzado_1", "Debo admitir que eres más fuerte de lo que esperaba, cuando te traje de vuelta no esperaba que fuera tan divertido acompañarte, quiero decir, ¿una asado? Ja, es una idea demasiado estúpida tal y como está el mundo, pero ahí vas, lanzándote de cabeza a un peligro inminente para encontrar compañeros para tu jueguecito."},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Encapuchado", "Encapuzado_1", "¿Aún no lo entiendes? Oh bueno, monologar no nos llevará a ninguna parte, si quieres que vuelva el sol para hacer tu asado, tendrás que derrotarme primero."},
            { "", "", "", ""},
        },
        new string[,] { // 35 - Encapuzado, quando é derrotado, mas você não completou todas as side quests
            { "right" , "Encapuchado", "Encapuzado_1", "Ja ja .... ja ja ja ja... imbécil... dejarás de existir... junto con todo lo que trajo el Eclipse... Realmente has sido cegado por tu deseo de asado."},
            { "", "", "", ""},
        },
        new string[,] { // 36 - Encapuzado, quando você encontra ele na sala do Boss Final
            { "right" , "Encapuchado", "Encapuzado_1", "Ja ja... ja ja ja... imbécil... dejarás de existir... junto con todo lo que trajo el Eclipse.... "},
            { "left" , "Sabet", "Sabet_1", "..."},
            { "right" , "Encapuchado", "Encapuzado_1", "Que... como... ja ja ja... de verdad no me aburres... vive el resto de tu miserable vida.... Sabet..."},
            { "", "", "", ""},
        },
        new string[,] { // 37 - VAZIA
            { "left", "???", "Sabet_1", "Cuadro de diálogo aún no implementado"},
            { "", "", "", ""},
        },
    };


}