using System;
using System.Collections.Generic;

[Serializable]
public class Word
{
    public string word;
    public List<string> categories;
    public List<string> connections;
}

[Serializable]
public class WordList
{
    public List<Word> words;
}