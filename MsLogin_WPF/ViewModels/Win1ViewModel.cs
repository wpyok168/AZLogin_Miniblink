using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;
using MB;

namespace MsLogin_WPF.ViewModels
{
    public class Win1ViewModel:BindableBase
    {
        private WebView wv;
        private DelegateCommand<object> winformsLoad;

        public Win1ViewModel()
        {
            wv = new WebView();
            winformsLoad = new DelegateCommand<object>(this.WVInit);
        }

        public DelegateCommand<object> WinformsLoad { get => winformsLoad; set => winformsLoad = value; }
        private void WVInit(object obj)
        {
            if (obj==null)
            {
                return;
            }
            System.Windows.Forms.Panel panel = obj as System.Windows.Forms.Panel;
            wv.Bind(panel);
            wv.Load("https://www.baidu.com");
        }
    }
}
