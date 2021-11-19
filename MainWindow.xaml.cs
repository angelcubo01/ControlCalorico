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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TrabajoFinal_IGU_70926454C
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //COLORES PARA LOS RECTANGULOS
        Brush brochaDesayuno = new SolidColorBrush(Color.FromRgb(0x68, 0xb9, 0xde));
        Brush brochaAlmuerzo = new SolidColorBrush(Color.FromRgb(0x21, 0x9e, 0xbc));
        Brush brochaComida = new SolidColorBrush(Color.FromRgb(0x02, 0x30, 0x47));
        Brush brochaMerienda = new SolidColorBrush(Color.FromRgb(0xfe, 0xb7, 0x03));
        Brush brochaCena = new SolidColorBrush(Color.FromRgb(0xfb, 0x85, 0x00));
        Brush brochaOtros = new SolidColorBrush(Color.FromRgb(0x8d, 0x4c, 0x02));
        //

        Comidas comidaSelec;
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TablaDatos td = new TablaDatos();

            td.Show();
            td.Owner = this;
            td.NuevaSelecionComida += Td_nuevaSelecionComida;
        }

        private void Td_nuevaSelecionComida(object sender, ComidaSelecionadaEventArgs e)
        {
            //SE HA SELECIONADO UN ELEMENTO EN LA OTRA VENTANA
            if (e.Lacomida != null)//SE HA SELECIONADO
            {
                canvasTablaDiaria.Children.Clear();
                DibujarIndivdual(e.Lacomida);
                comidaSelec = e.Lacomida;
            }

        }

        private void DibujarIndivdual(Comidas lacomida)
        {
            if (tablaDiaria.IsSelected)
            {
                int anchoRectangulos = DibujarEjesIndividual();

                //Etiqueta para el maximo
                int maximo = lacomida.Mayor;
                double altoRectangulo ;
                Label maximoLabel = new Label
                {
                    Content = maximo.ToString()+" cal"
                };
                Canvas.SetTop(maximoLabel, 35);
                Canvas.SetLeft(maximoLabel, 2);
                canvasTablaDiaria.Children.Add(maximoLabel);
                


                Rectangle desayunoRec = new Rectangle();
                desayunoRec.Fill = brochaDesayuno;
                desayunoRec.Width = anchoRectangulos;
                altoRectangulo = (lacomida.Desayuno * (canvasTablaDiaria.ActualHeight - 100) ) / maximo;
                desayunoRec.Height = altoRectangulo;
                Canvas.SetTop(desayunoRec, canvasTablaDiaria.ActualHeight - 50 - altoRectangulo);
                Canvas.SetLeft(desayunoRec, 50);
                canvasTablaDiaria.Children.Add(desayunoRec);

                Rectangle almuerzoRec = new Rectangle();
                almuerzoRec.Fill = brochaAlmuerzo;
                almuerzoRec.Width = anchoRectangulos;
                altoRectangulo = (lacomida.Almuerzo * (canvasTablaDiaria.ActualHeight-100)) / maximo;
                almuerzoRec.Height = Math.Round(altoRectangulo);
                Canvas.SetTop(almuerzoRec, canvasTablaDiaria.ActualHeight - 50 - altoRectangulo);
                Canvas.SetLeft(almuerzoRec, 50 + anchoRectangulos);
                canvasTablaDiaria.Children.Add(almuerzoRec);

                Rectangle comidaRec = new Rectangle();
                comidaRec.Fill = brochaComida;
                comidaRec.Width = anchoRectangulos;
                altoRectangulo = (lacomida.Comida * (canvasTablaDiaria.ActualHeight - 100)) / maximo;
                comidaRec.Height = Math.Round(altoRectangulo);
                Canvas.SetTop(comidaRec, canvasTablaDiaria.ActualHeight - 50 - altoRectangulo);
                Canvas.SetLeft(comidaRec, 50 + anchoRectangulos*2);
                canvasTablaDiaria.Children.Add(comidaRec);

                Rectangle meriendaRec = new Rectangle();
                meriendaRec.Fill = brochaMerienda;
                meriendaRec.Width = anchoRectangulos;
                altoRectangulo = (lacomida.Merienda * (canvasTablaDiaria.ActualHeight - 100)) / maximo;
                meriendaRec.Height = Math.Round(altoRectangulo);
                Canvas.SetTop(meriendaRec, canvasTablaDiaria.ActualHeight - 50 - altoRectangulo);
                Canvas.SetLeft(meriendaRec, 50 + anchoRectangulos * 3);
                canvasTablaDiaria.Children.Add(meriendaRec);

                Rectangle cenaRec = new Rectangle();
                cenaRec.Fill = brochaCena;
                cenaRec.Width = anchoRectangulos;
                altoRectangulo = (lacomida.Cena * (canvasTablaDiaria.ActualHeight - 100)) / maximo;
                cenaRec.Height = Math.Round(altoRectangulo);
                Canvas.SetTop(cenaRec, canvasTablaDiaria.ActualHeight - 50 - altoRectangulo);
                Canvas.SetLeft(cenaRec, 50 + anchoRectangulos * 4);
                canvasTablaDiaria.Children.Add(cenaRec);

                Rectangle otrosRec = new Rectangle();
                otrosRec.Fill = brochaOtros;
                otrosRec.Width = anchoRectangulos;
                altoRectangulo = (lacomida.Otros * (canvasTablaDiaria.ActualHeight - 100)) / maximo;
                otrosRec.Height = Math.Round(altoRectangulo);
                Canvas.SetTop(otrosRec, canvasTablaDiaria.ActualHeight - 50 - altoRectangulo);
                Canvas.SetLeft(otrosRec, 50 + anchoRectangulos * 5);
                canvasTablaDiaria.Children.Add(otrosRec);
            }
            

        }

        private int DibujarEjesIndividual()
        {
            Line ejeX = new Line
            {
                X1 = 50,
                Y1 = 50, //PUNTO 1
                X2 = 50,
                Y2 = canvasTablaDiaria.ActualHeight - 50,
                Stroke = Brushes.Black
            };
            canvasTablaDiaria.Children.Add(ejeX);
            Line ejeY = new Line
            {
                X1 = 50,
                Y1 = canvasTablaDiaria.ActualHeight - 50, //PUNTO 2
                X2 = canvasTablaDiaria.ActualWidth - 50,
                Y2 = canvasTablaDiaria.ActualHeight - 50,// PUNTO 3
                Stroke = Brushes.Black
            };
            canvasTablaDiaria.Children.Add(ejeY);
            int tamanoEtiquetaVertical = (int)(canvasTablaDiaria.ActualWidth - 100) / 6; //X3 - X2 y 6 ingestas
            Label desayunoLabel = new Label
            {
                Content = "Desayuno"
            };
            Canvas.SetTop(desayunoLabel, canvasTablaDiaria.ActualHeight -50);
            Canvas.SetLeft(desayunoLabel,  50 );
            canvasTablaDiaria.Children.Add(desayunoLabel);
            Label almuerzoLabel = new Label
            {
                Content = "Almuerzo"
            };
            Canvas.SetTop(almuerzoLabel, canvasTablaDiaria.ActualHeight - 50);
            Canvas.SetLeft(almuerzoLabel, 50 + tamanoEtiquetaVertical);
            canvasTablaDiaria.Children.Add(almuerzoLabel);
            Label comidaLabel = new Label
            {
                Content = "Comida"
            };
            Canvas.SetTop(comidaLabel, canvasTablaDiaria.ActualHeight - 50);
            Canvas.SetLeft(comidaLabel, 50 + tamanoEtiquetaVertical*2);
            canvasTablaDiaria.Children.Add(comidaLabel);
            Label meriendaLabel = new Label
            {
                Content = "Merienda"
            }; 
            Canvas.SetTop(meriendaLabel, canvasTablaDiaria.ActualHeight - 50);
            Canvas.SetLeft(meriendaLabel, 50 + tamanoEtiquetaVertical*3);
            canvasTablaDiaria.Children.Add(meriendaLabel);
            Label cenaLabel = new Label
            {
                Content = "Cena"
            };
            Canvas.SetTop(cenaLabel, canvasTablaDiaria.ActualHeight - 50);
            Canvas.SetLeft(cenaLabel, 50 + tamanoEtiquetaVertical*4);
            canvasTablaDiaria.Children.Add(cenaLabel);
            Label otrosLabel = new Label
            {
                Content = "Otros"
            };
            Canvas.SetTop(otrosLabel, canvasTablaDiaria.ActualHeight - 50);
            Canvas.SetLeft(otrosLabel, 50 + tamanoEtiquetaVertical*5);
            canvasTablaDiaria.Children.Add(otrosLabel);

            return tamanoEtiquetaVertical;
        }

        private void canvasTablaDiaria_Loaded(object sender, RoutedEventArgs e)
        {
            canvasTablaDiaria.Children.Clear();
            DibujarEjesIndividual();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (tablaDiaria.IsSelected)
            {
                canvasTablaDiaria.Children.Clear();
                DibujarIndivdual(comidaSelec);
            }
        }
    }
}
