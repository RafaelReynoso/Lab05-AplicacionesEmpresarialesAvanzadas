using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lab05
{
    /// <summary>
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Button_Crear(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.ShowDialog();

        }

        private void Button_Actualizar(object sender, RoutedEventArgs e)
        {
            Actualizar actualizar = new Actualizar();
            actualizar.ShowDialog();
        }

        private void Button_Borrar(object sender, RoutedEventArgs e)
        {
            Borrar borrar = new Borrar();
            borrar.ShowDialog();

        }
    }
}
