using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace UBSTest
{
    internal class WordsCounterViewModel: INotifyPropertyChanged
    {
        public ICommand WordsCountCommand 
        {
            get
            {
                return new DelegateCommand(CountWords);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void CountWords()
        {
            if (string.IsNullOrWhiteSpace(UserInput)) return;

            WordsCounter counter = new WordsCounter();
           
            Dictionary<string, int> results = counter.CountWords(UserInput);

            WordsCount = new ObservableCollection<WordsCounterModel>(from result in results
                                                                     select new WordsCounterModel()
                                                                     {
                                                                         Word = result.Key,
                                                                         WordCount = result.Value
                                                                     });

        }

        private string _userInput;
        private ObservableCollection<WordsCounterModel> _wordsCount = new ObservableCollection<WordsCounterModel>();

        public string UserInput
        {
            get { return _userInput; }
            set 
            {
                _userInput = value;
                NotifyPropertyChanged("UserInput");
            }
        }

        public ObservableCollection<WordsCounterModel> WordsCount
        {
            get { return _wordsCount; }
            set
            {
                _wordsCount = value;
                NotifyPropertyChanged("WordsCount");
            }
        }


    }

    public class DelegateCommand : ICommand
    {
        private readonly Action _action;

        public DelegateCommand(Action action)
        {
            _action = action;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }


        public event EventHandler CanExecuteChanged;
    }
}
