using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Lógica de interacción para TablaDatos.xaml
    /// </summary>
    public class ObjetoListViewDiario
    {
        public string TipoComida { get; set; }
        public int Calorias { get; set; }
        public ObjetoListViewDiario (string tipoComida, int calorias)
        {
            TipoComida = tipoComida;
            Calorias = calorias;
        }
    }
    public partial class TablaDatos : Window
    {
        AddDatos addDatos;
        ObservableCollection<Comidas> listaComidas;


        public TablaDatos()
        {
            InitializeComponent();
            listaComidas = new ObservableCollection<Comidas>();
            listViewGeneral.ItemsSource = listaComidas;
        }

        private void BtnFecha_Click(object sender, RoutedEventArgs e)
        {
            
             addDatos = new AddDatos();
            
            addDatos.ShowDialog();
            addDatos.Owner = this;
            if (addDatos.DialogResult == true)
            {
                DateTime fecha = addDatos.fechaDato.SelectedDate.Value;
                Comidas comida = new Comidas(fecha  ,addDatos.desayunoDato.IntValue, addDatos.almuerzoDato.IntValue, addDatos.comidaDato.IntValue, addDatos.meriendaDato.IntValue, addDatos.cenaDato.IntValue, addDatos.otrosDato.IntValue);
                listaComidas.Add(comida);

            }

        }
        private void BtnFechaMod_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void BtnFechaElim_Click(object sender, RoutedEventArgs e)
        {
            Comidas comidaSelecionada = (Comidas)listViewGeneral.SelectedItem;
            string msg = "Seguro que deseas eliminar el registro del " + comidaSelecionada.Fecha;
            string titulo = "¿Quieres eliminar?";
            MessageBoxButton btn = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(msg, titulo, btn, icon);
            if(result == MessageBoxResult.Yes)
            {
                listaComidas.Remove((Comidas)listViewGeneral.SelectedItem);
            }
        }

        private void ListViewGeneral_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listViewDiario.Items.Clear();
            Comidas comidaSelecionada = (Comidas)listViewGeneral.SelectedItem;
            if (comidaSelecionada == null) //No hay fecha selecionada 
            {
                listViewDiario.Items.Clear();
                btnFechaMod.IsEnabled = false;
                btnFechaElim.IsEnabled = false;
                fechaSelec.Content = "Selecciona un día para ver en detalle";
            }
            else
            {
                btnFechaMod.IsEnabled = true;
                btnFechaElim.IsEnabled = true;
                fechaSelec.Content = "Se muestran los datos del " + comidaSelecionada.Fecha;
                listViewDiario.Items.Add(new ObjetoListViewDiario("Desayuno", comidaSelecionada.Desayuno));
                listViewDiario.Items.Add(new ObjetoListViewDiario("Almuerzo", comidaSelecionada.Almuerzo));
                listViewDiario.Items.Add(new ObjetoListViewDiario("Comida", comidaSelecionada.Comida));
                listViewDiario.Items.Add(new ObjetoListViewDiario("Merienda", comidaSelecionada.Merienda));
                listViewDiario.Items.Add(new ObjetoListViewDiario("Cena", comidaSelecionada.Cena));
                listViewDiario.Items.Add(new ObjetoListViewDiario("Otros", comidaSelecionada.Otros));
            }


        }

        
    }
}
