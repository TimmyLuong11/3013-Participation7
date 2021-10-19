using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GOT_Quote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<GOTAPI> quotesList = new List<GOTAPI>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonQuote_Click(object sender, RoutedEventArgs e)
        {
            
            using (var client = new HttpClient())
            {
                string json = client.GetStringAsync("https://got-quotes.herokuapp.com/quotes").Result;
                GOTAPI quote = JsonConvert.DeserializeObject<GOTAPI>(json);
                quotesList.Add(quote);
                string words = $"{quote.character} said \"{quote.quote}\"";
                txtBlock.Text = words;
            }
        }

        private void buttonExport_Click_1(object sender, RoutedEventArgs e)
        {
            string json = JsonConvert.SerializeObject(quotesList);
            File.WriteAllText("GOT_Quotes.json", json);
        }
    }
}
