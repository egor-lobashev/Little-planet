using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Translator : MonoBehaviour
{
    public GameObject[] texts;
    public static string language;

    private Dictionary<string, string[]> languages = new Dictionary<string, string[]>();

    public void Translate()
    {
        language = language == "english" ? "russian" : "english";
        PlayerPrefs.SetString("language", language);
        
        string[] strings_list;
        if (SceneManager.GetActiveScene().buildIndex == 0)
            strings_list = languages[language];
        else
            strings_list = languages[language + "_pause"];

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].GetComponent<UnityEngine.UI.Text>().text = strings_list[i];
        }
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("language"))
            PlayerPrefs.SetString("language", "english");
        language = PlayerPrefs.GetString("language");

        languages.Add("english",@"

Play

Records

Quit

Reset
stars

Русский

Mercury

Too close to the Sun

Mars

The planet of fire

Neptune

The planet of ice

Venus

Beware of acid clouds

Saturn

Hide under the rings

Pluto

Your first planet

Back

Back

Records

All

Pluto

Saturn

Venus

Mars

Neptune

Mercury

you will lose all stars.
are you shure?

I know what
i'm doing

cancel

".Split(new string[] {"\n\n"}, StringSplitOptions.RemoveEmptyEntries));

        languages.Add("russian",@"

Играть

Рекорды

Выход

Сбросить
звёзды

English

Меркурий

Слишком близко к Солнцу

Марс

Планета огня

Нептун

Планета льда

Венера

Остерегайтесь кислотных облаков

Сатурн

Можно прятаться под кольцами

Плутон

Первая планета

Назад

Назад

Рекорды

Все

Плутон

Сатурн

Венера

Марс

Нептун

Меркурий

Вы уверены, что хотите
потерять все звёзды?

Я знаю,
что делаю

Отмена

".Split(new string[] {"\n\n"}, StringSplitOptions.RemoveEmptyEntries));

        languages.Add("english_pause",@"

Pause

Continue

Quit

Retry

Game Over

Result

Quit

print here...

Name:

Retry

Good result!

New record!!!

".Split(new string[] {"\n\n"}, StringSplitOptions.RemoveEmptyEntries));

languages.Add("russian_pause",@"

Пауза

Продолжить

Выход

Заново

Конец игры

Результат

Выход

имя рекорда...

Имя:

Заново

Отличный результат!

Новый рекорд!!!

".Split(new string[] {"\n\n"}, StringSplitOptions.RemoveEmptyEntries));

        if (language != "english")
        {
            language = "english";
            Translate();
        }
    }
}
