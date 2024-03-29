﻿using System;
using System.Windows.Input;

namespace Projet_File_Rouge.Commands
{
    /// <summary>
    /// Class for Commands to launch to switch page
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter) => true;

        public abstract void Execute(object parameter);

        protected void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
