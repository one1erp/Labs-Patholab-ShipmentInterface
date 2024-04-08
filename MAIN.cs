using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentInterface
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class MAIN
    {

        private string xMLNAMEField;

        private string xMLDATEField;

        private string xMLHRField;

        private string tRCONTNUMField;

        private string tRCONTSTSField;

        private string tRUPDTNUMField;

        private string tRRPDTRSNField;

        private string tRHOSCODEField;

        private string tRLABNUMField;

        private string tRDRIVERField;

        private string tRDATEPICKField;

        private string tRTIMEPICKField;

        private MAINTRANSPORT[] tRREQNUMField;

        /// <remarks/>
        public string XMLNAME
        {
            get
            {
                return this.xMLNAMEField;
            }
            set
            {
                this.xMLNAMEField = value;
            }
        }

        /// <remarks/>
        public string XMLDATE
        {
            get
            {
                return this.xMLDATEField;
            }
            set
            {
                this.xMLDATEField = value;
            }
        }

        /// <remarks/>
        public string XMLHR
        {
            get
            {
                return this.xMLHRField;
            }
            set
            {
                this.xMLHRField = value;
            }
        }

        /// <remarks/>
        public string TRCONTNUM
        {
            get
            {
                return this.tRCONTNUMField;
            }
            set
            {
                this.tRCONTNUMField = value;
            }
        }

        /// <remarks/>
        public string TRCONTSTS
        {
            get
            {
                return this.tRCONTSTSField;
            }
            set
            {
                this.tRCONTSTSField = value;
            }
        }

        /// <remarks/>
        public string TRUPDTNUM
        {
            get
            {
                return this.tRUPDTNUMField;
            }
            set
            {
                this.tRUPDTNUMField = value;
            }
        }

        /// <remarks/>
        public string TRRPDTRSN
        {
            get
            {
                return this.tRRPDTRSNField;
            }
            set
            {
                this.tRRPDTRSNField = value;
            }
        }

        /// <remarks/>
        public string TRHOSCODE
        {
            get
            {
                return this.tRHOSCODEField;
            }
            set
            {
                this.tRHOSCODEField = value;
            }
        }

        /// <remarks/>
        public string TRLABNUM
        {
            get
            {
                return this.tRLABNUMField;
            }
            set
            {
                this.tRLABNUMField = value;
            }
        }

        /// <remarks/>
        public string TRDRIVER
        {
            get
            {
                return this.tRDRIVERField;
            }
            set
            {
                this.tRDRIVERField = value;
            }
        }

        /// <remarks/>
        public string TRDATEPICK
        {
            get
            {
                return this.tRDATEPICKField;
            }
            set
            {
                this.tRDATEPICKField = value;
            }
        }

        /// <remarks/>
        public string TRTIMEPICK
        {
            get
            {
                return this.tRTIMEPICKField;
            }
            set
            {
                this.tRTIMEPICKField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("TRANSPORT", IsNullable = false)]
        public MAINTRANSPORT[] TRREQNUM
        {
            get
            {
                return this.tRREQNUMField;
            }
            set
            {
                this.tRREQNUMField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MAINTRANSPORT
    {

        private ulong tRREQUESTField;

        private string tRXMLDATEField;

        private string tRXMLTIMEField;

        private ulong tRSMPLNUMField;

        private object tRRECEIVEDField;

        /// <remarks/>
        public ulong TRREQUEST
        {
            get
            {
                return this.tRREQUESTField;
            }
            set
            {
                this.tRREQUESTField = value;
            }
        }

        /// <remarks/>
        public string TRXMLDATE
        {
            get
            {
                return this.tRXMLDATEField;
            }
            set
            {
                this.tRXMLDATEField = value;
            }
        }

        /// <remarks/>
        public string TRXMLTIME
        {
            get
            {
                return this.tRXMLTIMEField;
            }
            set
            {
                this.tRXMLTIMEField = value;
            }
        }

        /// <remarks/>
        public ulong TRSMPLNUM
        {
            get
            {
                return this.tRSMPLNUMField;
            }
            set
            {
                this.tRSMPLNUMField = value;
            }
        }

        /// <remarks/>
        public object TRRECEIVED
        {
            get
            {
                return this.tRRECEIVEDField;
            }
            set
            {
                this.tRRECEIVEDField = value;
            }
        }
    }
}
