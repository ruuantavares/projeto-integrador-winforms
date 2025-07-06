
using View;

namespace ProjetoIntegrador
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ViewLogin()); // Inicia pelo login
        }
    }
}
