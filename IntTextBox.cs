using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace TrabajoFinal_IGU_70926454C
{
    class IntTextBox : TextBox
    {
        public IntTextBox()
        {
            this.PreviewTextInput += new TextCompositionEventHandler(IntTextBox_PreviewTextInput);

        }

        public int IntValue
        {
            get
            {
                return Int32.Parse(this.Text);
            }
        }
        public double DoubleValue
        {
            get
            {
                return Double.Parse(this.Text);
            }
        }
        void IntTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
       
            string caracter = e.Text;
            if (Char.IsDigit(e.Text[0]))
            {
                // No hacemos nada porque aceptamos los dígitos
            }
            else if (caracter == "\b")
            {
                // No hacemos nada porque aceptamos el Backspace
            }
            else
            {
                // Nos saltamos el carácter deteniendo el enrutamiento
                e.Handled = true;
            }
        }
    }
}
