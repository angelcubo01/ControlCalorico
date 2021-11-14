﻿using System;
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

namespace TrabajoFinal_IGU_70926454C
{
    /// <summary>
    /// Lógica de interacción para TablaDatos.xaml
    /// </summary>
    public partial class TablaDatos : Window
    {
        AddDatos addDatos;
        public TablaDatos()
        {
            InitializeComponent();
        }

        private void btnFecha_Click(object sender, RoutedEventArgs e)
        {
            if (addDatos == null)
            {
                addDatos = new AddDatos();
            }
            addDatos.ShowDialog();
        }
    }
}
