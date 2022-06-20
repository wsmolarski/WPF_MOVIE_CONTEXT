using System;
using System.Data.SqlClient;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static WPF_BOXING_01.MoviesContext;
using System.Data;

namespace WPF_BOXING_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Program.MoviesContext;Integrated Security=True");
        SqlDataAdapter adpt;
        DataTable dt;
        public MainWindow()
        {
            InitializeComponent();
            ShowData();
        }
        

        private void ShowData()
        {
            adpt = new SqlDataAdapter("select Title, Genre, Director from Movies", con);
            
            dt = new DataTable();
            adpt.Fill(dt);
            dataGridView.ItemsSource = dt.DefaultView;
        }

        private void btnReserve_Click(object sender, RoutedEventArgs e)
        {
            
            
                con.Open();
            adpt = new SqlDataAdapter("update Title, Genre, Director from Movies", con);

            dt = new DataTable();
            adpt.Update(dt);
            dataGridView.ItemsSource = dt.DefaultView;

            MessageBox.Show("Film został zarezerwowany");
            
           

        }
    }
}
