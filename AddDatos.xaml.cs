using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace TrabajoFinal_IGU_70926454C
{
    /// <summary>
    /// Lógica de interacción para AddDatos.xaml
    /// </summary>
    public partial class AddDatos : Window
    {
        public AddDatos()
        {
            InitializeComponent();
        }

        private void GuardarBtn_Click(object sender, RoutedEventArgs e)
        {
             
            if (fechaDato.SelectedDate.HasValue == false || desayunoDato.Text.Length == 0 || almuerzoDato.Text.Length == 0 || comidaDato.Text.Length == 0 || meriendaDato.Text.Length == 0 || cenaDato.Text.Length == 0 || otrosDato.Text.Length == 0)
            {
                string msg = "Te falta por completar algún dato, asegúrate que has introduccido la fecha y todas las ingestas";
                string titulo = "Corrije los datos";
                MessageBoxButton btn = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show(msg, titulo, btn, icon);
            }
            else
            {
                DialogResult = true;
            }
        }

        private void DescartarBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            
        }
    }
}
