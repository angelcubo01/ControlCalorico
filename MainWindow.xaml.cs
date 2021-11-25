using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

/// <summary>
/// Ángel Picado Cuadrado -- 70926454C 
/// Grupo PB1 -- IGU 2021/2022
/// angel.piccua@usal.es - GII USAL
/// </summary>

namespace TrabajoFinal_IGU_70926454C
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //COLORES PARA LOS RECTANGULOS
        readonly Brush brochaDesayuno = new SolidColorBrush(Color.FromRgb(0x68, 0xb9, 0xde));
        readonly Brush brochaAlmuerzo = new SolidColorBrush(Color.FromRgb(0x21, 0x9e, 0xbc));
        readonly Brush brochaComida = new SolidColorBrush(Color.FromRgb(0x02, 0x30, 0x47));
        readonly Brush brochaMerienda = new SolidColorBrush(Color.FromRgb(0xfe, 0xb7, 0x03));
        readonly Brush brochaCena = new SolidColorBrush(Color.FromRgb(0xfb, 0x85, 0x00));
        readonly Brush brochaOtros = new SolidColorBrush(Color.FromRgb(0x8d, 0x4c, 0x02));
        //

        private Comidas comidaSelec;
        private List<Comidas> comidasADibujar;
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
            td.NuevaSelecionGeneral += Td_NuevaSelecionGeneral;
        }


        //Llega un evento con el List<Comidas> que hay que dibujar
        private void Td_NuevaSelecionGeneral(object sender, ComidaDibujarGeneralEventArgs e)
        {
            if(e.ComidasDibujables != null)
            {
                canvasTablaGeneral.Children.Clear();
                DibujarGeneral(e.ComidasDibujables);
                comidasADibujar = e.ComidasDibujables;
            }
        }
        //Se ha seleccionado o deselecionado una fecha en la tabla
        private void Td_nuevaSelecionComida(object sender, ComidaSelecionadaEventArgs e)
        {

            if (e.Lacomida != null)
            {
                canvasTablaDiaria.Children.Clear();
                DibujarIndivdual(e.Lacomida);
                comidaSelec = e.Lacomida;
            }
            else
            {
                canvasTablaDiaria.Children.Clear();
            }

        }

        //Dibuja la tablaGeneral con las comidas selecionadas
        private void DibujarGeneral(List<Comidas> comidasDibujables)
        {
            double altoRectangulo;
            int maximo = 0;
            
            int posicion = 0;
            int tamanioEjeX = (int)(canvasTablaGeneral.ActualHeight - 100);
            if (tablaGeneral.IsSelected && comidasDibujables != null && comidasDibujables.Count > 0)
            {
                int tam = comidasDibujables.Count();
                //DIBUJADO EJES
                Line ejeX = new Line
                {
                    X1 = 50,
                    Y1 = 50, //PUNTO 1
                    X2 = 50,
                    Y2 = canvasTablaGeneral.ActualHeight - 50,
                    Stroke = Brushes.Black
                };
                canvasTablaGeneral.Children.Add(ejeX);
                Line ejeY = new Line
                {
                    X1 = 50,
                    Y1 = canvasTablaGeneral.ActualHeight - 50, //PUNTO 2
                    X2 = canvasTablaGeneral.ActualWidth - 50,
                    Y2 = canvasTablaGeneral.ActualHeight - 50,// PUNTO 3
                    Stroke = Brushes.Black
                };
                canvasTablaGeneral.Children.Add(ejeY);
                
                //DIBUJADO ETIQUETAS
                foreach (Comidas c in comidasDibujables)
                {
                    if (c.Total > maximo) maximo = c.Total;
                }
                int tamEtiquetas = tamanioEjeX / 6;
                for (int i = 0; i < 7; i++)
                {
                    Label label = new Label();
                    label.Content = (maximo / 6)*(6-i);
                    Canvas.SetTop(label, 40+tamEtiquetas*i);
                    Canvas.SetLeft(label, 2);
                    Line line = new Line();
                    line.X1 = 0;
                    line.X2 = 9;
                    line.Y1 = 0;
                    line.Y2 = 0;
                    line.Stroke = Brushes.Black;
                    Canvas.SetTop(line, 51 + tamEtiquetas * i);
                    Canvas.SetLeft(line, 42);
                    canvasTablaGeneral.Children.Add(label);
                    canvasTablaGeneral.Children.Add(line);
                }

                //DIBUJADO RECTANGULOS
                int anchoRectangulos = (int)(canvasTablaGeneral.ActualWidth - 100) / comidasDibujables.Count();
                foreach (Comidas c in comidasDibujables)
                {
                    Rectangle desayunoRec = new Rectangle();
                    desayunoRec.Fill = brochaDesayuno;
                    desayunoRec.Width = anchoRectangulos;
                    altoRectangulo = (c.Desayuno * (canvasTablaGeneral.ActualHeight - 100)) / maximo;
                    desayunoRec.Height = altoRectangulo;
                    Canvas.SetTop(desayunoRec, canvasTablaGeneral.ActualHeight - 50 - altoRectangulo);
                    Canvas.SetLeft(desayunoRec, 50 + posicion*anchoRectangulos);
                    

                    Rectangle almuerzoRec = new Rectangle();
                    almuerzoRec.Fill = brochaAlmuerzo;
                    almuerzoRec.Width = anchoRectangulos;
                    altoRectangulo = ((c.Almuerzo * (canvasTablaGeneral.ActualHeight - 100) )/ maximo)+altoRectangulo;
                    almuerzoRec.Height = Math.Round(altoRectangulo);
                    Canvas.SetTop(almuerzoRec, canvasTablaGeneral.ActualHeight - 50 - altoRectangulo);
                    Canvas.SetLeft(almuerzoRec, 50 + posicion * anchoRectangulos);
                    

                    Rectangle comidaRec = new Rectangle();
                    comidaRec.Fill = brochaComida;
                    comidaRec.Width = anchoRectangulos;
                    altoRectangulo = ((c.Comida * (canvasTablaGeneral.ActualHeight - 100)) / maximo) + altoRectangulo;
                    comidaRec.Height = Math.Round(altoRectangulo);
                    Canvas.SetTop(comidaRec, canvasTablaGeneral.ActualHeight - 50 - altoRectangulo);
                    Canvas.SetLeft(comidaRec, 50 + posicion * anchoRectangulos);
                    

                    Rectangle meriendaRec = new Rectangle();
                    meriendaRec.Fill = brochaMerienda;
                    meriendaRec.Width = anchoRectangulos;
                    altoRectangulo = ((c.Merienda * (canvasTablaGeneral.ActualHeight - 100)) / maximo) + altoRectangulo;
                    meriendaRec.Height = Math.Round(altoRectangulo);
                    Canvas.SetTop(meriendaRec, canvasTablaGeneral.ActualHeight - 50 - altoRectangulo);
                    Canvas.SetLeft(meriendaRec, 50 + posicion * anchoRectangulos);
                    

                    Rectangle cenaRec = new Rectangle();
                    cenaRec.Fill = brochaCena;
                    cenaRec.Width = anchoRectangulos;
                    altoRectangulo = ((c.Cena * (canvasTablaGeneral.ActualHeight - 100)) / maximo) + altoRectangulo;
                    cenaRec.Height = Math.Round(altoRectangulo);
                    Canvas.SetTop(cenaRec, canvasTablaGeneral.ActualHeight - 50 - altoRectangulo);
                    Canvas.SetLeft(cenaRec, 50 + posicion * anchoRectangulos);
                    

                    Rectangle otrosRec = new Rectangle();
                    otrosRec.Fill = brochaOtros;
                    otrosRec.Width = anchoRectangulos;
                    altoRectangulo = ((c.Otros * (canvasTablaGeneral.ActualHeight - 100)) / maximo) + altoRectangulo;
                    otrosRec.Height = Math.Round(altoRectangulo);
                    Canvas.SetTop(otrosRec, canvasTablaGeneral.ActualHeight - 50 - altoRectangulo);
                    Canvas.SetLeft(otrosRec, 50 + posicion * anchoRectangulos);

                    canvasTablaGeneral.Children.Add(otrosRec);
                    canvasTablaGeneral.Children.Add(cenaRec);
                    canvasTablaGeneral.Children.Add(meriendaRec);
                    canvasTablaGeneral.Children.Add(comidaRec);
                    canvasTablaGeneral.Children.Add(almuerzoRec);
                    canvasTablaGeneral.Children.Add(desayunoRec);

                    //DIBUJADO FECHA
                    Label fechaLabel = new Label
                    {
                        Content = c.Fecha
                    };
                    Canvas.SetTop(fechaLabel, canvasTablaGeneral.ActualHeight - 50);
                    Canvas.SetLeft(fechaLabel, 55 + posicion * anchoRectangulos);
                    RotateTransform rt = new RotateTransform(30);
                    fechaLabel.RenderTransform = rt;
                    canvasTablaGeneral.Children.Add(fechaLabel);
                    posicion++;
                }
            }
        }

       

        //Dibujado del elemento selecionado
        private void DibujarIndivdual(Comidas lacomida)
        {
            if (tablaDiaria.IsSelected && lacomida !=null)
            {
                int anchoRectangulos = DibujarEjesIndividual();
                int maximo = lacomida.Mayor;
                double altoRectangulo;

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

        //Dibujado de ejes individuales y de sus etiquetas
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

        //Administra cambios que puedan suceder
        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            if (canvasTablaDiaria == sender)
            {
                canvasTablaDiaria.Children.Clear();
                DibujarIndivdual(comidaSelec);
            }
            else if (canvasTablaGeneral == sender)
            {
                canvasTablaGeneral.Children.Clear();
                DibujarGeneral(comidasADibujar);
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (tablaDiaria.IsSelected)
            {
                canvasTablaDiaria.Children.Clear();
                DibujarIndivdual(comidaSelec);
            } else if (tablaGeneral.IsSelected)
            {
                canvasTablaGeneral.Children.Clear();
                DibujarGeneral(comidasADibujar);
            }
        }
    }
}
