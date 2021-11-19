using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

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
    public class ComidaSelecionadaEventArgs : EventArgs
    {
        public Comidas Lacomida { get; set; }
        public ComidaSelecionadaEventArgs(Comidas c) { Lacomida = c; }
        
    }
    public delegate void ComidaSelecionadaEventHandler(Object sender, ComidaSelecionadaEventArgs e);
    public partial class TablaDatos : Window
    {
        AddDatos addDatos;
        ObservableCollection<Comidas> listaComidas;
        public event ComidaSelecionadaEventHandler NuevaSelecionComida;
        
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
            addDatos.fechaDato.SelectedDate = DateTime.Parse(comidaSelec.Fecha);
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
                comidaSelec.Fecha = addDatos.fechaDato.SelectedDate.ToString();
                comidaSelec.Desayuno = addDatos.desayunoDato.IntValue;
                comidaSelec.Almuerzo = addDatos.almuerzoDato.IntValue;
                comidaSelec.Comida = addDatos.comidaDato.IntValue;
                comidaSelec.Merienda = addDatos.meriendaDato.IntValue;
                comidaSelec.Cena = addDatos.cenaDato.IntValue;
                comidaSelec.Otros = addDatos.otrosDato.IntValue;
                listViewGeneral.Items.Refresh();
                AgregarListViewDiario();
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
            
            Comidas comidaSelecionada = (Comidas)listViewGeneral.SelectedItem;
            
            if (comidaSelecionada == null) //No hay fecha selecionada 
            {
                listViewDiario.Items.Clear();
                btnFechaMod.IsEnabled = false;
                btnFechaElim.IsEnabled = false;
            }
            else
            {
                btnFechaMod.IsEnabled = true;
                btnFechaElim.IsEnabled = true;
                AgregarListViewDiario();
                
            }

            NuevaSelecionComida(this, new ComidaSelecionadaEventArgs(comidaSelecionada)); //MANDA AL MAINWINDOWS
        }

        private void AgregarListViewDiario()
        {
            listViewDiario.Items.Clear();
            Comidas comidaSelecionada = (Comidas)listViewGeneral.SelectedItem;
            listViewDiario.Items.Add(new ObjetoListViewDiario("Desayuno", comidaSelecionada.Desayuno));
            listViewDiario.Items.Add(new ObjetoListViewDiario("Almuerzo", comidaSelecionada.Almuerzo));
            listViewDiario.Items.Add(new ObjetoListViewDiario("Comida", comidaSelecionada.Comida));
            listViewDiario.Items.Add(new ObjetoListViewDiario("Merienda", comidaSelecionada.Merienda));
            listViewDiario.Items.Add(new ObjetoListViewDiario("Cena", comidaSelecionada.Cena));
            listViewDiario.Items.Add(new ObjetoListViewDiario("Otros", comidaSelecionada.Otros));
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(sender == exportarDatos)
            {
                SaveFileDialog exportDialog = new SaveFileDialog()
                {
                    Title = "Exportar datos de ingestas",
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
                    DefaultExt = ".contCal",
                    Filter = "Archivo de Control de Calorias (*.contCal)|*.contCal",
                    AddExtension = true
                };


                if ((bool)importDialog.ShowDialog())
                {
                    if (importDialog.FileName.EndsWith(".contCal")) //El archivo es del tipo del programa
                    {
                        listaComidas.Clear();
                        string linea = File.ReadAllText(importDialog.FileName);
                        List<Comidas> comidas = JsonConvert.DeserializeObject<List<Comidas>>(linea); 
                        foreach (Comidas comida in comidas)
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
