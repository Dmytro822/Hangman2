using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangman2.Commands.Base;
using Hangman2.Models;
using Hangman2.ViewModels;


namespace Hangman2.Commands
{
    class AlphabetClickCommands : Command
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }


        public override void Execute(object parameter)
        {
            object[] parameters = parameter as object[];
            CharModel chr = parameters[0] as CharModel;
            GameViewModel gvm = parameters[1] as GameViewModel;
            gvm.AlphabetClick(chr);
            //chr.Parent.AlphabetClick(chr);
        }
    }
}
