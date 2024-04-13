using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab05
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string connectionString = "Data Source=LAB1504-28\\SQLEXPRESS;Initial Catalog=lab05;User Id=reynoso;Password=123";
        public List<Clientes> ListaClientes { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ListaClientes = new List<Clientes>(); 
            dataClientes.ItemsSource = ListaClientes;
        }

        private void Button_Insertar(object sender, RoutedEventArgs e)
        {
            string idCliente = txtIdCliente.Text;
            string nombreCompañia = txtNombreCompañia.Text;
            string nombreContacto = txtNombreContacto.Text;
            string cargoContacto = txtCargoContacto.Text;
            string direccion = txtDireccion.Text;
            string ciudad = txtCiudad.Text;
            string pais = txtPais.Text;
            string telefono = txtTelefono.Text;

            Clientes nuevoCliente = new Clientes(idCliente, nombreCompañia, nombreContacto, cargoContacto, direccion, ciudad, pais, telefono);
            ListaClientes.Add(nuevoCliente);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("USP_InsertarClientes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@idCliente", idCliente);
                    command.Parameters.AddWithValue("@NombreCompañia", nombreCompañia);
                    command.Parameters.AddWithValue("@NombreContacto", nombreContacto);
                    command.Parameters.AddWithValue("@CargoContacto", cargoContacto);
                    command.Parameters.AddWithValue("@Direccion", direccion);
                    command.Parameters.AddWithValue("@Ciudad", ciudad);
                    command.Parameters.AddWithValue("@Pais", pais);
                    command.Parameters.AddWithValue("@Telefono", telefono);

                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }


        private void Button_Listar(object sender, RoutedEventArgs e)
        {
            List<Clientes> clientes = new List<Clientes>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("USP_ListarClientes", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string idCliente = reader.GetString(reader.GetOrdinal("idCliente"));
                                string nombreCompañia = reader.GetString(reader.GetOrdinal("NombreCompañia"));
                                string nombreContacto = reader.GetString(reader.GetOrdinal("NombreContacto"));
                                string cargoContacto = reader.GetString(reader.GetOrdinal("CargoContacto"));
                                string direccion = reader.GetString(reader.GetOrdinal("Direccion"));
                                string ciudad = reader.GetString(reader.GetOrdinal("Ciudad"));
                                string pais = reader.GetString(reader.GetOrdinal("Pais"));
                                string telefono = reader.GetString(reader.GetOrdinal("Telefono"));

                                clientes.Add(new Clientes(idCliente, nombreCompañia, nombreContacto, cargoContacto, direccion, ciudad, pais, telefono));
                            }
                        }
                    }
                }

                dataClientes.ItemsSource = clientes;
                dataClientes.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



    }
}