﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CCFlow_Comm_Sys_FuncLib : BP.Web.WebPageAdmin
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string expFileName = "all-wcprops,dir-prop-base,entries";
        string expDirName = ".svn";

        string pathDir = BP.Sys.SystemConfig.PathOfData + "\\JSLib\\";


        this.Pub1.AddFieldSet(" System-defined functions .  Location :"+pathDir);
        DirectoryInfo dir = new DirectoryInfo(pathDir);
        DirectoryInfo[] dirs = dir.GetDirectories();
        foreach (DirectoryInfo mydir in dirs)
        {
            if (expDirName.Contains(mydir.Name))
                continue;


            this.Pub1.Add(" Event Name :" + mydir.Name);

            this.Pub1.AddUL();
            FileInfo[] fls = mydir.GetFiles();
            foreach (FileInfo fl in fls)
            {
                if (expFileName.Contains(fl.Name))
                    continue;

                this.Pub1.AddLi(fl.Name);
            }

            this.Pub1.AddULEnd();
        }
        this.Pub1.AddFieldSetEnd();


        pathDir = BP.Sys.SystemConfig.PathOfDataUser + "\\JSLib\\";
        this.Pub1.AddFieldSet(" User-defined functions .  Location :" + pathDir);
        dir = new DirectoryInfo(pathDir);
        dirs = dir.GetDirectories();
        foreach (DirectoryInfo mydir in dirs)
        {
            if (expDirName.Contains(mydir.Name))
                continue;

            this.Pub1.Add(" Event Name :" + mydir.Name);
            this.Pub1.AddUL();
            FileInfo[] fls = mydir.GetFiles();
            foreach (FileInfo fl in fls)
            {
                if (expFileName.Contains(fl.Name))
                    continue;
                this.Pub1.AddLi(fl.Name);
            }
            this.Pub1.AddULEnd();
        }
        this.Pub1.AddFieldSetEnd();
    }
}