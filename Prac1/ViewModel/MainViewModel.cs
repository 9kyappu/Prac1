using Prac1.Model;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace Prac1.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _inputText;
        public string InputText
        {
            get { return _inputText; }
            set
            {
                if (_inputText != value)
                {
                    _inputText = value;
                    OnPropertyChanged(nameof(InputText));
                    OnPropertyChanged(nameof(UpperCaseText));
                    OnPropertyChanged(nameof(LowerCaseText));
                }
            }
        }

        public string UpperCaseText => InputText?.ToUpper();
        public string LowerCaseText => InputText?.ToLower();

        private ICommand _openImageCommand;
        public ICommand OpenImageCommand
        {
            get
            {
                if (_openImageCommand == null)
                {
                    _openImageCommand = new DelegateCommand(OpenImage);
                }
                return _openImageCommand;
            }
        }

        private void OpenImage()
        {
            // Открываем диалоговое окно для выбора изображения
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg;*.gif)|*.png;*.jpeg;*.jpg;*.gif|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                // Получаем путь к выбранному изображению
                string imagePath = openFileDialog.FileName;

                // Создаем новое окно для отображения изображения
                Window imageWindow = new Window();
                imageWindow.Title = "Selected Image";
                imageWindow.Width = 400;
                imageWindow.Height = 400;

                // Создаем элемент Image для отображения выбранного изображения
                Image imageControl = new Image();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath);
                bitmap.EndInit();
                imageControl.Source = bitmap;

                // Устанавливаем содержимое окна как изображение
                imageWindow.Content = imageControl;

                // Отображаем окно
                imageWindow.ShowDialog();
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
