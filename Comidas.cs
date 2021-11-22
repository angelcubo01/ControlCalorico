using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Linq;

/// <summary>
/// Ángel Picado Cuadrado -- 70926454C 
/// Grupo PB1 -- IGU 2021/2022
/// angel.piccua@usal.es - GII USAL
/// </summary>


namespace TrabajoFinal_IGU_70926454C
{
    public class Comidas : INotifyPropertyChanged
    {
        private int desayunoPriv, almuerzoPriv, comidaPriv, meriendaPriv, cenaPriv, otrosPriv;
        private DateTime fechaPriv;
        private int total;
        private int mayorIngesta; //PARA DIBUJO DIARIO
        private bool dibujar;
        public event PropertyChangedEventHandler PropertyChanged;

        public Comidas(DateTime fecha, int desayuno, int almuerzo, int comida, int merienda, int cena, int otros)
        {
            fechaPriv = fecha;
            desayunoPriv = desayuno;
            almuerzoPriv = almuerzo;
            comidaPriv = comida;
            meriendaPriv = merienda;
            cenaPriv = cena;
            otrosPriv = otros;
            total = desayuno + almuerzo + comida + merienda + cena + otros;
            mayorIngesta = CalcularMayor();
        }
        [JsonConstructor]
        public Comidas(string fecha, int desayuno, int almuerzo, int comida, int merienda, int cena, int otros)
        {
            fechaPriv = DateTime.Parse(fecha);
            desayunoPriv = desayuno;
            almuerzoPriv = almuerzo;
            comidaPriv = comida;
            meriendaPriv = merienda;
            cenaPriv = cena;
            otrosPriv = otros;
            total = desayuno + almuerzo + comida + merienda + cena + otros;
            mayorIngesta = CalcularMayor();
        }

        private int CalcularMayor()
        {
            int [] listIngestas = new int[] { desayunoPriv, almuerzoPriv, comidaPriv, meriendaPriv, cenaPriv,otrosPriv };
            return listIngestas.Max();
        }

        //Propiedades
        public string Fecha
        {
            get { return fechaPriv.ToString("d"); }
            set { fechaPriv = DateTime.Parse(value); OnPropertyChanged("Fecha"); }
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
        public int Mayor
        {
            get { return mayorIngesta; }
        }
        void OnPropertyChanged(String propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
            total = desayunoPriv + almuerzoPriv + comidaPriv + meriendaPriv + cenaPriv + otrosPriv;
            mayorIngesta = CalcularMayor();
        }
    }
}
