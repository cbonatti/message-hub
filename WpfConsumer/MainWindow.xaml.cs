using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfConsumer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HubConnection _connection;
        private ObservableCollection<Client> _clients;

        public MainWindow()
        {
            InitializeComponent();
            _clients = new ObservableCollection<Client>();
            dataGridClients.ItemsSource = _clients;
            InitializeSignalR();
        }

        private async Task InitializeSignalR()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(ConfigurationManager.AppSettings["SignalRHubUrl"] ?? string.Empty)
                .Build();

            _connection.On<List<string>>("ClientList", clients =>
            {
                Dispatcher.Invoke(() =>
                {
                    UpdateClients(clients);
                });
            });

            await _connection.StartAsync();
        }

        private void UpdateClients(List<string> clients)
        {
            clients = clients
                            .Where(x => x != _connection.ConnectionId)
                            .ToList();

            foreach (var client in clients.Where(x => !_clients.Any(y => y.ConnectionId == x)).ToList())
            {
                _clients.Add(new Client(client, ""));
                _connection.On<string>(client, message => OnMessage(client, message));
            }
            foreach (var client in _clients.Where(x => !clients.Exists(y => y == x.ConnectionId)).ToList())
            {
                _clients.Remove(client);
            }
        }

        private void OnMessage(string client, string message)
        {
            Dispatcher.Invoke(() =>
            {
                var connectedClient = _clients.FirstOrDefault(x => x.ConnectionId == client);
                connectedClient?.SetInput(message);
                dataGridClients.Items.Refresh();
            });
        }

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            _connection.InvokeAsync("SendMessageTo", txtInput.Text, _connection.ConnectionId);
        }
    }
}
