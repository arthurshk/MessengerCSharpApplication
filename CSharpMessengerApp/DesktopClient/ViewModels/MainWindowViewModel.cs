using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommonLibrary;
using My.BaseViewModels;

namespace DesktopClient.ViewModels;

public class MainWindowViewModel : NotifyPropertyChangedBase
{
    private Client Me { get; set; }

    private Service _service;

    public MainWindowViewModel()
    {
        _service = new Service("127.0.0.1", 5000);
        Me = new Client { Login = "", Password = "" };
        
        // Auth
        var loginWindow = new LoginWindow(new ClientViewModel(Me));
        loginWindow.ShowDialog();
        try
        {
            _service.GetMessageCount(Me);
        } catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            throw ex;
        }

        NewMessage = new MessageViewModel(new Message { Text = "", From = Me, CreatedAt = DateTime.Now, To = new Client { Login = "" } });
        OnPropertyChanged(nameof(NewMessage));


        Task.Run(() => {
            while (true)
            {
                UpdateMessages();
                Thread.Sleep(5000);
            }
            
        });
        
    }

    private List<Message> _messages = new List<Message>();

    public ObservableCollection<MessageViewModel> Messages
    {
        get
        {
            var collection = new ObservableCollection<MessageViewModel>();
            _messages.ForEach(m => collection.Add(new MessageViewModel(m)));
            return collection;
        }
    }
    private MessageViewModel _selectedMessage;
    public MessageViewModel SelectedMessage
    {
        get => _selectedMessage; set
        {
            _selectedMessage = value;
            OnPropertyChanged(nameof(SelectedMessage));
        }
    }

    private void UpdateMessages()
    {
        _messages.AddRange(_service.GetMessages(Me));
        //_messages.Add(new Message
        //{
        //    From = new Client { Login = "test2" },
        //    To = new Client { Login = "test" },
        //    Text = "test message 1",
        //    CreatedAt = DateTime.Now
        //});
        //_messages.Add(new Message
        //{
        //    From = new Client { Login = "test3" },
        //    To = new Client { Login = "test" },
        //    Text = "test message 2",
        //    CreatedAt = DateTime.Now
        //});
        //_messages.Add(new Message
        //{
        //    From = new Client { Login = "test4" },
        //    To = new Client { Login = "test" },
        //    Text = "test message 3",
        //    CreatedAt = DateTime.Now
        //});

        OnPropertyChanged(nameof(Messages));
    }

    public MessageViewModel NewMessage { get; set; }

    public ICommand Send => new RelayCommand(x =>
    {
        _service.SendMessage(NewMessage.Model);
        MessageBox.Show("Message send");
        NewMessage = new MessageViewModel(new Message { Text = "", From = Me, CreatedAt = DateTime.Now, To = new Client { Login = "" } });
        OnPropertyChanged(nameof(NewMessage));

    }, x => true);

}
