using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MB;
using MsLogin_NetF.Properties;

namespace MsLogin_NetF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private WebView m_wView = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            m_wView = new WebView();
            m_wView.Bind(panel1);
            m_wView.LoadURL("https://portal.azure.com/");
            //m_wView.LoadURL("https://ssl.captcha.qq.com/template/wireless_mqq_captcha.html?style=simple&aid=16&uin=2687905088&sid=413727440959041728&cap_cd=5ayvRzjYM4rMr47wi5mXBuUZpl945FZp9IymLZzKC0s8c4rzLw_n_Q**&clientype=1&apptype=2");
            //m_wView.OnLoadUrlBegin += M_wView_OnLoadUrlBegin; ;
            //m_wView.OnLoadUrlEnd += M_wView_OnLoadUrlEnd;

            m_wView.OnLoadingFinish += M_wView_OnLoadingFinish;

        }

        private void M_wView_OnLoadUrlBegin(object sender, LoadUrlBeginEventArgs e)
        {
            m_wView.NetHookRequest(e.Job);
        }

        private void M_wView_OnLoadUrlEnd(object sender, LoadUrlEndEventArgs e)
        {
            Debug.Print(e.URL);
            string url = e.URL;
            if (e.URL.Equals("https://t.captcha.qq.com/cap_union_new_verify"))
            {
                string strData = Encoding.UTF8.GetString(e.Data);
            }
            
        }

        private void M_wView_OnLoadingFinish(object sender, LoadingFinishEventArgs e)
        {
            string url = e.URL.ToString();
            if (e.URL.Equals("https://portal.azure.com/cobrand/") || e.URL.Contains("https://login.live.com/oauth20_authorize.srf?response_type=code&client_id="))
            {
                string acount = "222222@netajisubhash.org";
                string pwd = "88888888*";
                m_wView.RunJS(Resources.msloginjs.Replace("账号", acount).Replace("密码", pwd));
            }
            string pattern = @"https:\/\/login\.microsoftonline\.com\/(.)*\/login";
            if (Regex.IsMatch(e.URL, pattern))//https://login.microsoftonline.com/common/login
            {
                m_wView.RunJS(Resources.loginstatusjs);
            }
            if (e.URL.Contains("https://portal.azure.com/signin/index/"))
            {
                string cookie = m_wView.GetCookie();
                //从页面 m_wView.GetSource();中 提取 Authorization
                string cookie1 = m_wView.GetCookie();
                m_wView.ClearCookie();
                string cookie2 = m_wView.GetCookie();
            }
            string html = m_wView.GetSource();
        }
    }
}
