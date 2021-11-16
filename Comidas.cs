using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoFinal_IGU_70926454C
{
    public class Comidas : INotifyPropertyChanged
    {
        private int desayunoPriv, almuerzoPriv, comidaPriv, meriendaPriv, cenaPriv, otrosPriv;
        private string fechaPriv;
        private int total;
        private bool dibujar;
        public event PropertyChangedEventHandler PropertyChanged;

        public Comidas(DateTime fecha, int desayuno, int almuerzo, int comida, int merienda, int cena, int otros)
        {
            fechaPriv = fecha.ToString("d");
            desayunoPriv = desayuno;
            almuerzoPriv = almuerzo;
            comidaPriv = comida;
            meriendaPriv = merienda;
            cenaPriv = cena;
            otrosPriv = otros;
            total = desayuno + almuerzo + comida + merienda + cena + otros;
        }
        //Propiedades
        public string Fecha
        {
            get { return fechaPriv; }
            set { fechaPriv = value; OnPropertyChanged("Fecha"); }
        }
        public int Desayuno
        {
            get { return desayunoPriv; }
            set { desayunoPriv = value; OnPropertyChanged("Desayuno"); }
        }
        public int Almuerzo
        {
            get { return almuerzoPriv; }
            set { almuerzoPriv = value; OnPropertyChanged("Almuerzo"); }
        }
        public int Comida
        {
            get { return comidaPriv; }
            set { comidaPriv = value; OnPropertyChanged("Comida"); }
        }
        public int Merienda
        {
            get { return meriendaPriv; }
            set { meriendaPriv = value; OnPropertyChanged("Merienda"); }
        }
        public int Cena
        {
            get { return cenaPriv; }
            set { cenaPriv = value; OnPropertyChanged("Cena"); }
        }
        public int Otros
        {
            get { return otrosPriv; }
            set { otrosPriv = value; OnPropertyChanged("Otros"); }
        }
        public int Total
        {
            
            get { return total; }
            set { total = value; OnPropertyChanged("Total"); }
        }
        public bool Dibujar {
            get { return dibujar; }
            set { dibujar = value; OnPropertyChanged("Dibujar"); }
        }

        void OnPropertyChanged(String propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
