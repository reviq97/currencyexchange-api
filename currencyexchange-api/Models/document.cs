namespace currencyexchange_api.Models
{

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
    public class document
    {

        private List<documentData> dataField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("data")]
        public List<documentData> data
        {
            get
            {
                return dataField;
            }
            init
            {
                dataField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public class documentData
    {

        private DateTime dateField;

        private string codeField;

        private decimal rateField;

        private string baseField;

        private DateTime start_dateField;

        private DateTime end_dateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "date")]
        public DateTime date
        {
            get
            {
                return dateField;
            }
            init
            {
                dateField = value;
            }
        }

        /// <remarks/>
        public string code
        {
            get
            {
                return codeField;
            }
            init
            {
                codeField = value;
            }
        }

        /// <remarks/>
        public decimal rate
        {
            get
            {
                return rateField;
            }
            init
            {
                rateField = value;
            }
        }

        /// <remarks/>
        public string @base
        {
            get
            {
                return baseField;
            }
            init
            {
                baseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "date")]
        public DateTime start_date
        {
            get
            {
                return start_dateField;
            }
            init
            {
                start_dateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement(DataType = "date")]
        public DateTime end_date
        {
            get
            {
                return end_dateField;
            }
            init
            {
                end_dateField = value;
            }
        }
    }


}
