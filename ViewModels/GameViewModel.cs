using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using System.Collections.ObjectModel;
using System.Web;
using System.Threading.Tasks.Dataflow;
using System.Windows;
using Hangman2.Constants;
using System.Windows.Controls;
using System.Xaml;
using System.Windows.Markup.Localizer;
using System.IO;
using System.Windows.Documents;
using Hangman2.Models;
using Hangman2.ViewModels.Base;
using Hangman2.Views.Pages;

namespace Hangman2.ViewModels
{
    public class  GameViewModel : ViewModel
    {
        #region Word Text
        private string? wordText;

        public string WordText
        {
            get => wordText!; set => Set(ref wordText, value); }
        #endregion
        private string? imagePath;

        public string ImagePath
        {
            get => imagePath!;
            set => Set(ref imagePath, value);
        }
        private ObservableCollection<CharModel> word = new();

        public ObservableCollection<CharModel> Word
        {
            get => word;
            set => Set(ref word, value);
        }

        private ObservableCollection<ObservableCollection<CharModel>> alphabet = new();

        public ObservableCollection<ObservableCollection<CharModel>> Alphabet
        {
            get => alphabet;
            set => Set(ref alphabet, value);
        }

        private int step;

        public int Step
        {
            get => step;
            set => Set(ref step, value);
        }

        private bool gameEnable;

        public bool GameEnable
        {
            get => gameEnable;
            set => Set(ref gameEnable, value);
        }
        public GameViewModel()
        {
            StartGame();
        }

        public void GenerateAlphabet()
        {
            string alphabet_set = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            Alphabet.Clear();

            for (int i = 0; i < 3; i++)
            {
                ObservableCollection<CharModel> row = new();
                row.Clear();

                for (int j = 0; j < 11; j++)
                {
                    row.Add(new CharModel() { Text = alphabet_set[i * 11 + j].ToString(), Enable = true, Color = "gray" });
                }
                Alphabet.Add(row);
            }
        }

        public void ShowWord()
        {
            for (int i = 0; i < WordText.Length; i++)
            {
                Word[i].Text = wordText![i].ToString();
            }
        }

        public void UpdateTexturePath()
        {
            ImagePath = $"../../Images/{step}.png";
        }

        public void Win()
        {
            MessageBox.Show("Перемога", "Ура");
            RestartGame();
        }

        public void Lose()
        {
            MessageBox.Show("Ви програли", "Ви програли");
            RestartGame();
        }

        public void RestartGame()
        {
            MessageBoxResult res = MessageBox.Show("Грати знову?", "Грати", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                StartGame();
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        public void StartGame()
        {
            Step = 0;

            string[] Words = ReadWordsFromFile("Words.txt");

            Random rnd = new();
            int index = rnd.Next(0, Words.Length);

            WordText = Words[index].ToUpper();

            GenerateWord(wordText!);

            UpdateTexturePath();

            GenerateAlphabet();

            GameEnable = true;
        }

        public static string[] ReadWordsFromFile(string path)
        {
            try
            {
                using FileStream fstream = File.OpenRead(path);

                byte[] buffer = new byte[fstream.Length];

                fstream.Read(buffer, 0, buffer.Length);

                string textFromFile = Encoding.Default.GetString(buffer);
                string[] array = textFromFile.Split("\n");

                for (int i = 0; i < array.Length - 1; i++)
                {
                    array[i] = array[i][..(array[i].Length - 1)];
                }
                return array;
            }
            catch (FileNotFoundException)
            {
                return ReserveWords.reserve;
            }
        }

        public void GenerateWord(string str)
        {
            Word.Clear();
            for (int i = 0; i < str.Length; i++)
            {
                Word.Add(new CharModel() { Text = "___", Enable = true, Color = "gray" });
            }
        }

        public void AlphabetClick(CharModel chr)
        {
            if (!chr.Enable) return;
            chr.Enable = false;
            if (OpenChar(chr.Text))
            {
                int free = 0;
                for (int i = 0; i < Word.Count; i++)
                {
                    if (Word[i].Text == "___")
                    {
                        free++;
                    }
                }
                if (free < 1)
                {
                    Win();
                }
            }
            else
            {
                step++;
                UpdateTexturePath();
                if (step == 9) ;
                {
                    ShowWord();
                    Lose();
                }
            }
        }

        public bool OpenChar(string text)
        {
            bool inWord = false;
            for (int i = 0; i < WordText.Length; i++)
            {
                if (WordText[i].ToString() == text)
                {
                    Word[i].Text = text;
                    inWord = true;
                    //break;
                }
            }
            return inWord;
        }
    }
}

