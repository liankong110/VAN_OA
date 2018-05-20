using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Collections.Generic;
using VAN_OA.Model.OA;

namespace VAN_OA.Model.OA
{
    public class ExploreFile
    {
      static int dirCounter=1;
      static int indentLevel=-1;
      static int fileCounter=0;
      public List<tb_File> Files = new List<tb_File>();
      public List<Directorys> direLis = new List<Directorys>();

      public List<Directorys> getALLDires()
      {
          try
          {
              string theDirectory = ConfigurationManager.AppSettings["FileDire"].ToString();// @"E:\资料库";        //这里我访问的是网上邻居中的一台计算机
              Directorys dire = new Directorys();
              dire.DireName= "资料库";
              dire.DireUrl = theDirectory;
              dire.Id = 1;
              dire.ParentID = 0;
              direLis.Add(dire);
              
              DirectoryInfo dir = new DirectoryInfo(theDirectory);
              ExploreDirectory_1(dir, dire.Id);

             
          }
          catch (Exception)
          {


          }
          return direLis; 
      }
      public List<tb_File> MAIN()
      {

          try
          {
              string theDirectory = ConfigurationManager.AppSettings["FileDire"].ToString();// @"E:\资料库";        //这里我访问的是网上邻居中的一台计算机
              DirectoryInfo dir = new DirectoryInfo(theDirectory);
              ExploreDirectory(dir);

              for (int i = 0; i < Files.Count; i++)
              {
                  Files[i].id = i;
              }
          }
          catch (Exception)
          {
              
               
          }
              return Files;
      }

      public List<tb_File> MAIN(string url)
      {

          try
          {
              string theDirectory = url;// @"E:\资料库";        //这里我访问的是网上邻居中的一台计算机
              DirectoryInfo dir = new DirectoryInfo(theDirectory);
              ExploreDirectory_2(dir);

              for (int i = 0; i < Files.Count; i++)
              {
                  Files[i].id = i;
              }
          }
          catch (Exception)
          {


          }
          return Files;
      }
    /// <summary>
    /// 查询文件夹 包括子文件夹下的所有文件
    /// </summary>
    /// <param name="dir"></param>
      public void ExploreDirectory(DirectoryInfo dir)
      {
           for(int i=0;i<indentLevel;i++)
           {
            Console.WriteLine(" ");
           }
           Console.WriteLine("[{0}][{1}][{2}]\n",indentLevel,dir.Name,dir.LastAccessTime);
           FileInfo[] filesInDir=dir.GetFiles();
        
           foreach(FileInfo file in filesInDir)
           {
            for(int i=0;i<indentLevel+1;i++)
               Console.Write(" ");
             tb_File file1 = new tb_File();
               file1.fileURL = file.FullName;
               file1.fileName = file.Name;
               Files.Add(file1);
            fileCounter++;
           }
           DirectoryInfo[] directories=dir.GetDirectories();
           foreach(DirectoryInfo newDir in directories)
           {
            dirCounter++;
            ExploreDirectory(newDir);
           }
           indentLevel--;
      }

      /// <summary>
      /// 查询所有文件夹
      /// </summary>
      /// <param name="dir"></param>
      /// <param name="parentId"></param>
      public void ExploreDirectory_1(DirectoryInfo dir,int parentId)
      {       
          
        
          DirectoryInfo[] directories = dir.GetDirectories();
          foreach (DirectoryInfo newDir in directories)
          {
              Directorys dire = new Directorys();
              dire.DireName = newDir.Name;
              dire.DireUrl = newDir.FullName;
              dire.Id = direLis.Count+1;
              dire.ParentID = parentId;
              direLis.Add(dire);
              ExploreDirectory_1(newDir, dire.Id);
          }
          
      }


        /// <summary>
        /// 只查询指定文件夹下面的文件
        /// </summary>
        /// <param name="dir"></param>
      public void ExploreDirectory_2(DirectoryInfo dir)
      {
           FileInfo[] filesInDir = dir.GetFiles();

          foreach (FileInfo file in filesInDir)
          {
              
              tb_File file1 = new tb_File();
              file1.fileURL = file.FullName;
              file1.fileName = file.Name;
              Files.Add(file1);
              
          }
          
      }

 

    }
}
