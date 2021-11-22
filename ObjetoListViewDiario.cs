/// <summary>
/// Ángel Picado Cuadrado -- 70926454C 
/// Grupo PB1 -- IGU 2021/2022
/// angel.piccua@usal.es - GII USAL
/// </summary>
/// 
namespace TrabajoFinal_IGU_70926454C
{
    class ObjetoListViewDiario
    {
        public string TipoComida { get; set; }
        public int Calorias { get; set; }
        public ObjetoListViewDiario(string tipoComida, int calorias)
        {
            TipoComida = tipoComida;
            Calorias = calorias;
        }
    }
}
