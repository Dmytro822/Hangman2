using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Hangman2.ViewModels.Base;

namespace Hangman2.Models
{
    public class CharModel : ViewModel
    {
        private string text;

        public string Text
        {
            get => text;
            set => Set(ref text, value);
        }

        private bool enable;

        public bool Enable
        {
            get => enable;
            set
            {
                Set(ref enable, value);
                if (enable)
                {
                    Color = "gray";
                }
                else
                {
                    Color = "white";
                }
            }
        }

        private string color = "gray";

        public string Color
        {
            get => color;
            set => Set(ref color, value);
        }

    }
}
