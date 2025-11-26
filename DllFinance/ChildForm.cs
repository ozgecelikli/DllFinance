using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DllFinance
{
    public partial class ChildForm : DevExpress.XtraEditors.XtraForm
    {
      
        public static void fnc_void_clearpanelcontrols(PanelControl pnlcntrl_temp)
        {
            foreach (Control item in pnlcntrl_temp.Controls)
            {
                pnlcntrl_temp.Controls.Remove(item);
                item.Parent = null;
                item.Dispose();
            }
        }
        public static Control fnc_usercntrl_getir(String str_p_classfullid, Object[] obj_p_parameters)
        {
            try
            {
                if (obj_p_parameters == null)
                {
                    //  return (Control)Assembly.GetExecutingAssembly().CreateInstance(str_p_classfullid);
                    Type myType1 = Type.GetType(str_p_classfullid);
                    return (Control)Activator.CreateInstance(myType1, null);
                }
                else if (obj_p_parameters.Length == 1)
                {
                    Type myType1 = Type.GetType(str_p_classfullid);
                    return (Control)Activator.CreateInstance(myType1, new object[] { obj_p_parameters[0] });
                }
                else if (obj_p_parameters.Length > 1)
                {
                    Type myType1 = Type.GetType(str_p_classfullid);
                    return (Control)Activator.CreateInstance(myType1, new object[] { obj_p_parameters as object[] });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "!!!" + ex.StackTrace); 
                return null;
            }
        }

        public ChildForm()
        {
            InitializeComponent();
        }

        public ChildForm(string formTitle, UserControl userControl)
        {
            InitializeComponent();
            this.Text = formTitle;

            // UserControl'ü direkt olarak panel'e ekle
            if (userControl != null)
            {
                fnc_void_clearpanelcontrols(PanelControl);
                PanelControl.Dock = DockStyle.Fill;
                userControl.Dock = DockStyle.Fill;
                PanelControl.Controls.Add(userControl);
            }
        }
    }
}