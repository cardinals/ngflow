﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BP;
using BP.Port;
using BP.Web;
using BP.En;
using BP.WF;
using BP.DA;
using System.Threading;
namespace SMSServices
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //if (Glo.IsExitProcess("CCFlowServices.exe"))
            //{
            //    MessageBox.Show(" Gallop workflow design application has been started , You can not start two operating window .", " Operation Tips ",
            //        MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            Glo.LoadConfigByFile();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Emp emp = new Emp("zhanghaicheng");
            //BP.Web.WebUser.SignInOfGener(emp);
            //WorkNode wn = new WorkNode(499, 1301);
            //wn.AfterNodeSave();

            bool initiallyOwned = true;
            bool isCreated;
            Mutex m = new Mutex(initiallyOwned, "CCFlowServices", out isCreated);
            if (!(initiallyOwned && isCreated))
            {
                MessageBox.Show(" Gallop workflow design application has been started , You can not start two operating window .", " Operation Tips ",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
            else
            {
                Application.Run(new FrmMain());
            }

         
        }
    }
}
