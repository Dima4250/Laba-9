using Laba_9.Loggers;
using Laba_9.Presenters;
using Laba_9.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba_9
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Создаем логгер (можно выбрать XmlLogger или JsonLogger)

            //ILogger logger = new JsonLogger("sync_log.json");

            ILogger logger = new XmlLogger("sync_log.xml");

            var view = new MainForm();
            var presenter = new MainPresenter(view);
            view.Tag = presenter;

            Application.Run(view);
        }
    }
}
