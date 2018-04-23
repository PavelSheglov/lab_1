using System;
using System.Text;

namespace task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowRepeatingWords(FindRepeatingWords(InputText()));
        }

        static string InputText()
        {
            string text=string.Empty;
            bool isOk = false;
            char[] symbols = { '.', '!', '?' };
            char[] symbols2 = { ' ', ',', ';', ':', '-' };
            while (!isOk)
            {
                Console.WriteLine($"Введите текст, состоящий из предложений, разделенных символами ({symbols[0]})/({symbols[1]})/({symbols[2]}))");
                Console.WriteLine($"В качестве разделителей для слов используйте ({symbols2[0]})/({symbols2[1]})/({symbols2[2]})/({symbols2[3]})/({symbols2[4]})");
                Console.Write("Ваш текст:");
                text = Console.ReadLine();
                if (text.IndexOfAny(symbols) > 0 && text.LastIndexOfAny(symbols) == text.Length - 1)
                    isOk = true;
                else
                    Console.WriteLine("Ошибки при вводе текста");
            }
            return text;
        }

        static string[] GetPhrasesFromText(string text)
        {
            string[] phrases = null;
            if(!string.IsNullOrEmpty(text) && !string.IsNullOrWhiteSpace(text))
            {
                phrases = text.Split('.', '!', '?');
                var temp = new StringBuilder(string.Empty, text.Length * 2);
                for (int i = 0; i < phrases.Length; i++)
                    phrases[i]=phrases[i].Trim();
                for (int i = 0; i < phrases.Length; i++)
                    if (!string.IsNullOrEmpty(phrases[i]))
                        temp.Append(phrases[i]+".");
                if (!string.IsNullOrEmpty(temp.ToString()))
                {
                    string result = temp.ToString().Remove(temp.ToString().Length - 1);
                    phrases = result.Split('.');
                }
                else
                    phrases = null;
            }
            return phrases;
        }

        static string[] GetWordsFromPhrase(string phrase)
        {
            string[] words = null;
            if (!string.IsNullOrEmpty(phrase) && !string.IsNullOrWhiteSpace(phrase))
            {
                words = phrase.Split(' ', ',', ';', ':', '-');
                for (int i = 0; i < words.Length; i++)
                    words[i]=words[i].Trim();
                var temp = new StringBuilder(string.Empty, phrase.Length * 2);
                for (int i = 0; i < words.Length; i++)
                    if (!string.IsNullOrEmpty(words[i]))
                        temp.Append(words[i] + " ");
                if (!string.IsNullOrEmpty(temp.ToString()))
                {
                    string result=temp.ToString().Trim();
                    words = result.Split(' ');
                }
                else
                    words = null;
            }
            return words;
        }

        static bool PhraseContainsWord(string phrase, string word)
        {
            bool isFounded = false;
            if(!string.IsNullOrEmpty(phrase) && !string.IsNullOrEmpty(word))
            {
                string[] phraseWords = GetWordsFromPhrase(phrase);
                if (phraseWords != null)
                {
                    for (int i = 0; i < phraseWords.Length; i++)
                        if (string.Compare(phraseWords[i], word, true) == 0)
                        {
                            isFounded = true;
                            break;
                        }
                }
            }
            return isFounded;
        }

        static string[] FindRepeatingWords(string text)
        {
            string[] repatingWords = null;
            if(!string.IsNullOrEmpty(text) && !string.IsNullOrWhiteSpace(text))
            {
                var temp = new StringBuilder(string.Empty, text.Length * 2);
                string[] phrases = GetPhrasesFromText(text);
                if (phrases != null)
                {
                    int countOfPhrasesWithWords = 0;
                    foreach (var phrase in phrases)
                        if (GetWordsFromPhrase(phrase) != null)
                            countOfPhrasesWithWords++;
                    if (countOfPhrasesWithWords > 0)
                    {
                        string[] words = null;
                        foreach (var phrase in phrases)
                            if (GetWordsFromPhrase(phrase) != null)
                            {
                                words = GetWordsFromPhrase(phrase);
                                break;
                            }
                        foreach (var word in words)
                        {
                            int countOfRepetitions = 0;
                            foreach(var phrase in phrases)
                            {
                                if (PhraseContainsWord(phrase, word))
                                    countOfRepetitions++;
                            }
                            if (countOfRepetitions == countOfPhrasesWithWords)
                                temp.Append(word.ToLower() + " ");
                        }
                        repatingWords = GetWordsFromPhrase(temp.ToString());
                    }
                }
            }
            return repatingWords;
        }

        static void ShowRepeatingWords(string[] words)
        {
             if (words != null)
             {
                 Console.WriteLine("Слова, которые присутствуют в каждом предложении:");
                 foreach (var word in words)
                      Console.WriteLine(word);
             }
             else
                 Console.WriteLine("Слов, которые присутствуют в каждом предложении, нет. Либо пустая строка.");
        }
    }
}
