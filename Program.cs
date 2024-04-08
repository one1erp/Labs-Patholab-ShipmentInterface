using Microsoft.Win32;
using Oracle.ManagedDataAccess.Client;
//using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ShipmentInterface;
using ShipmentInterfaceHelpers;
using Patholab_DAL_V1;

namespace ShipmentInterface
{
    public class Program
    {

        private static DataLayer _dal;
     public   static string NautConStr, InputPath, OuputPath;

        static void Main(string[] args)
        {

            //Setup
            SetAppSettings();

            //Create Connection
            _dal = new DataLayer();
            _dal.MockConnect(NautConStr);
            XML2Nautilus xml2nautilus = new XML2Nautilus(_dal);
            xml2nautilus.Run();
           
            _dal = new DataLayer();
            _dal.MockConnect(NautConStr);
            Nautilus2Container na2con=new Nautilus2Container(_dal);
            na2con.Run();

            _dal.Close();
            _dal = null;
            return;
        }

    

     
               
  

       

        private static void SetAppSettings()
        {
            NautConStr = ConfigurationManager.ConnectionStrings["NautConnectionString"].ConnectionString;
            InputPath = ConfigurationManager.AppSettings["InputPath"];
            OuputPath = ConfigurationManager.AppSettings["OuputPath"];

        }

        private static void Exit(string p)
        {
            Console.WriteLine("Exit Program");
            Console.WriteLine(p);
            Console.WriteLine("Press any key to continue");
            Console.Read();
            //CloseDBConnection();
        }

    





     


    

      
        private static void OPTION_B_USING_POOL()
        {
            //Option b (POOL)
            //newContainer = _dal.FindBy<U_CONTAINER_USER>
            //   (c => c.U_STATUS == "U" && c.U_RECEIVE_NUMBER == null).OrderBy(x => x.U_CONTAINER_ID).FirstOrDefault();
            //if (newContainer == null)
            //{
            //    Console.WriteLine("Pool Is Empty");
            //    Exit("Pool Is Empty");
            //}
            //newContainer.U_CONTAINER.DESCRIPTION = item.U_MSG_NAME;
            //newContainer.U_RECEIVE_NUMBER = item.U_CONTAINER_NBR.ToString();
            //newContainer.U_CLINIC = item.U_CLINIC_ID;
            //newContainer.U_NUMBER_OF_SAMPLES = item.U_CONTAINER_MSG.U_CONTAINER_MSG_ROW_USER.Count;
            //newContainer.U_DRIVER_ID = item.U_DRIVER_ID;
            //newContainer.U_SEND_ON = item.U_PACKED_ON;
            //newContainer.U_RECEIVED_ON = DateTime.Now;
            //newContainer.U_REQUESTS = GetRequests(item);
            //newContainer.U_STATUS = "V";
            //item.U_RECEIVING_STATUS = "R";

            //    _dal.SaveChanges();
        }
    }

}
