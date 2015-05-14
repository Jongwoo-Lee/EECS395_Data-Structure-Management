using System;
using System.Text;
using System.Windows.Forms;

namespace Interpreter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private readonly Dictionary environment = new ListDictionary();

        private void RunButtonClick(object sender, EventArgs e)
        {
            try
            {
                Output.Text = "Result: "+Interpreter.Run(command.Text, this.environment).ToString();
                UpdateVariables();
            }
            catch (Exception exception)
            {
                Output.Text = "Exception:\n"+exception.Message;
                variablesValuesArea.Text = "Stack trace:\n"+exception.StackTrace;
            }
        }

        private void UpdateVariables()
        {
            var b= new StringBuilder();
            b.Append("Variable values:\n");
            bool gotOne = false;
            foreach (var binding in environment)
            {
                b.AppendFormat("{0}={1}\n", binding.Key, binding.Value);
                gotOne = true;
            }
            if (!gotOne)
                variablesValuesArea.Text = "No variables defined.";
            else
            {
                variablesValuesArea.Text = b.ToString();
            }
        }
    }
}
