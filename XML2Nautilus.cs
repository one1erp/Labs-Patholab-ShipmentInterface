using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ShipmentInterfaceHelpers;
using System.Threading.Tasks;
using Patholab_DAL_V1;

namespace ShipmentInterface
{
    public class XML2Nautilus
    {

        private DataLayer _dal;

        public XML2Nautilus(Patholab_DAL_V1.DataLayer _dal)
        {
            // TODO: Complete member initialization
            this._dal = _dal;
        }

        internal void Run()
        {


            string xmlPath = Program.InputPath;
            //Get XML from folder
            var files = Directory.GetFiles(xmlPath, "*.xml");
            foreach (var item in files)
            {
                try
                {
                    string xml = File.ReadAllText(item);

                    var xmlObj = xml.ParseXML<MAIN>();

                    //Populates nautilus from XML
                    var NewIdNbr = InsertContainerMSGFF(xmlObj);

                    var newDest = MoveFile(Program.OuputPath, item);

                    UpdateAsSuccess(NewIdNbr, newDest);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error on Parse XML path " + item);
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Press any key to continue");
                    Console.Read();
                }

            }



        }
        #region Add to DB
        private long InsertContainerMSGFF(MAIN msg)
        {
            var id = _dal.GetNewId("SQ_U_CONTAINER_MSG");
            long NewId = Convert.ToInt64(id);
            U_CONTAINER_MSG req = new U_CONTAINER_MSG()
            {
                NAME = NewId.ToString(),
                U_CONTAINER_MSG_ID = NewId,
                VERSION = "1",
                VERSION_STATUS = "A"
            };
            decimal temp;
            // typecast either 'temp' or 'null'
            decimal? numericValue =
              decimal.TryParse(msg.TRCONTNUM, out temp) ? temp : (decimal?)null;
            string status = msg.TRCONTSTS.ToString();
            long? sender = GetSenderClinic(msg.TRHOSCODE);
            DateTime? date = GetDate(msg.XMLDATE, msg.XMLHR);
            DateTime? pckdate = GetDate(msg.TRDATEPICK, msg.TRTIMEPICK, "ddMMyyyyHmm");

            long? driver = GetDriver(msg.TRDRIVER);


            string errors = GetErrors(sender, date, driver);

            U_CONTAINER_MSG_USER reqUser = new U_CONTAINER_MSG_USER()
            {
                U_CONTAINER_MSG_ID = NewId,
                U_DATE = date,//Rename to msg_date in db
                U_CONTAINER_NBR = temp,
                U_STATUS = status,
                //U_SENDER = sender,//todo delete from db
                U_CLINIC_ID = sender,
                U_MSG_NAME = msg.XMLNAME,
                U_DRIVER_ID = driver,
                U_PACKED_ON = pckdate,
                U_CREATED_ON = DateTime.Now,
                U_ERRORS = errors

            };
            if (!string.IsNullOrEmpty(reqUser.U_ERRORS))
            {
                reqUser.U_RECEIVING_STATUS = "H";
            }

            _dal.Add(req);
            _dal.Add(reqUser);
            foreach (var item in msg.TRREQNUM)
            {
                InsertContainerMSG_EntryFF(item, NewId);
            }

            _dal.SaveChanges();

            return NewId;
        }
        private void InsertContainerMSG_EntryFF(MAINTRANSPORT xmlRow, long headerId)
        {





            var rowId = _dal.GetNewId("SQ_U_CONTAINER_MSG_ROW");
            long NewrowId = Convert.ToInt64(rowId);
            U_CONTAINER_MSG_ROW NautRow = new U_CONTAINER_MSG_ROW()
            {
                NAME = NewrowId.ToString(),
                U_CONTAINER_MSG_ROW_ID = NewrowId,
                VERSION = "1",
                VERSION_STATUS = "A"
            };
            DateTime? date = GetDate(xmlRow.TRXMLDATE, xmlRow.TRXMLTIME);

            U_CONTAINER_MSG_ROW_USER NautRowUser = new U_CONTAINER_MSG_ROW_USER()
            {
                U_CONTAINER_MSG_ROW_ID = NewrowId,
                U_REQUEST = xmlRow.TRREQUEST.ToString(),
                U_MSG_ID = headerId,
                U_PACKED_ON = date

            };


            _dal.Add(NautRow);
            _dal.Add(NautRowUser);
        }
        #endregion
        private void UpdateAsSuccess(long NewIdNbr, string newDest)
        {

            U_CONTAINER_MSG_USER msg = _dal.FindBy<U_CONTAINER_MSG_USER>(x => x.U_CONTAINER_MSG_ID == NewIdNbr).FirstOrDefault();
            if (msg != null)
            {
                msg.U_PATH = newDest;
            }
            _dal.SaveChanges();
        }
        private string GetErrors(long? sender, DateTime? date, long? driver)
        {
            string errors = "";
            if (!sender.HasValue)
            {
                errors += "No Clinic;";
            }
            if (!date.HasValue)
            {
                errors += "No Date;";
            }
            if (!driver.HasValue)
            {
                errors += "No driver;";
            }
            return errors;
        }
        public DirectoryInfo GetCreateMyFolder(string baseFolder)
        {
            var now = DateTime.Now;
            var yearName = now.ToString("yyyy");
            var monthName = now.ToString("MM");
            var dayName = now.ToString("dd-MM-yyyy");

            var folder = Path.Combine(baseFolder, Path.Combine(yearName, monthName));

            return Directory.CreateDirectory(folder);
        }
        private string MoveFile(string xmlDir, string file)
        {

            string NameWithoutExtension = Path.GetFileNameWithoutExtension(file);
            FileInfo f = new FileInfo(file);
            var bb = GetCreateMyFolder(xmlDir);
            var newDest = Path.Combine(bb.FullName, NameWithoutExtension + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt");
            File.Move(file, newDest);
            return newDest;
        }
        #region Conversoins
          DateTime? GetDate(string date, string time, string format = "ddMMyyyyHmmss")
        {


            try
            {

                var dttimefull = DateTime.ParseExact(date + time, format, null);


                Console.WriteLine(dttimefull.ToString());

                return dttimefull;

            }
            catch (Exception)
            {

                return null;
            }
        }
         long? GetDriver(string driverCode)
        {


            var DRIVER = _dal.GetAll<U_DRIVER_USER>().FirstOrDefault(x => x.U_CODE == driverCode);
            if (DRIVER != null)
            {
                return DRIVER.U_DRIVER_ID;

            }
            return null;
        }
         long? GetSenderClinic(string msgCode)
        {


            var clinic = _dal.GetAll<U_CLINIC_USER>().FirstOrDefault(x => x.U_ASSUTA_DIVISION_CODE == msgCode);
            if (clinic != null)
            {
                return clinic.U_CLINIC_ID;

            }
            return null;
        }
        #endregion

    }
}
