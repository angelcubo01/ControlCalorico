using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;

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
            addDatos = new AddDatos();
            Comidas comidaSelec = (Comidas)listViewGeneral.SelectedItem;
            addDatos.fechaLabel.Content = "Modifica las ingestas del día " + comidaSelec.Fecha;
            DateTime fecha =;//TODO MAL HECHO
            addDatos.fechaDato.SelectedDate = fecha.ToString("d");
            addDatos.desayunoDato.Text = comidaSelec.Desayuno.ToString();
            addDatos.almuerzoDato.Text = comidaSelec.Almuerzo.ToString();
            addDatos.comidaDato.Text = comidaSelec.Comida.ToString();
            addDatos.meriendaDato.Text = comidaSelec.Merienda.ToString();
            addDatos.cenaDato.Text = comidaSelec.Cena.ToString();
            addDatos.otrosDato.Text = comidaSelec.Otros.ToString();
            addDatos.ShowDialog();
            addDatos.Owner = this;
            if (addDatos.DialogResult == true)
            {
                comidaSelec.Desayuno = addDatos.desayunoDato.IntValue;
                comidaSelec.Almuerzo = addDatos.almuerzoDato.IntValue;
                comidaSelec.Comida = addDatos.comidaDato.IntValue;
                comidaSelec.Merienda = addDatos.meriendaDato.IntValue;
                comidaSelec.Cena = addDatos.cenaDato.IntValue;
                comidaSelec.Otros = addDatos.otrosDato.IntValue;
            }
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(sender == exportarDatos)
            {
                SaveFileDialog exportDialog = new SaveFileDialog()
                {
                    Title = "Exportar datos de ingestas",
                    FileName = "Ingestas",
                    DefaultExt = ".contCal",
                    Filter = "Archivo de Control de Calorias (*.contCal)|*.contCal",
                    AddExtension = true
                };


                if ((bool)exportDialog.ShowDialog())
                {
                    string jsonString = JsonConvert.SerializeObject(listaComidas);
                    File.WriteAllText(exportDialog.FileName, jsonString);


                }
            }else if (sender == importarDatos)
            {
                OpenFileDialog importDialog = new OpenFileDialog()
                {
                    Title = "Importar datos de ingestas",
                    FileName = "Ingestas",
                    DefaultExt = ".contCal",
                    Filter = "Archivo de Control de Calorias (*.contCal)|*.contCal",
                    AddExtension = true
                };


                if ((bool)importDialog.ShowDialog())
                {
                    if (importDialog.FileName.EndsWith(".contCal")) //El archivo es del tipo del programa
                    {
                        listaComidas.Clear();
                        List <Comidas> listaTempComidas;
                        string file = File.ReadAllText(importDialog.FileName);
                        listaTempComidas = JsonConvert.DeserializeObject<List<Comidas>>(file);
                        foreach(Comidas comida in listaTempComidas)
                        {
                            listaComidas.Add(comida);
                        }
                    }
                }
            }else if(sender == vaciarDatos)
            {
                string msg = "¿Estás seguro de eliminar todos los registros?";
                string titulo = "¿Quieres eliminar?";
                MessageBoxButton btn = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Question;
                MessageBoxResult result = MessageBox.Show(msg, titulo, btn, icon);
                if (result == MessageBoxResult.Yes)
                {
                    listaComidas.Clear();
                }
            }
        }

    }
}
