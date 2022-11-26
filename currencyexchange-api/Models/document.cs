namespace currencyexchange_api.Models
{

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class document
    {

        private List<documentData> dataField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("data")]
        public List<documentData> data
        {
            get
            {
                return this.dataField;
            }
            init
            {
                this.dataField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class documentData
    {

        private System.DateTime dateField;

        private string codeField;

        private decimal rateField;

        private string baseField;

        private System.DateTime start_dateField;

        private System.DateTime end_dateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime date
        {
            get
            {
                return this.dateField;
            }
            init
            {
                this.dateField = value;
            }
        }

        /// <remarks/>
        public string code
        {
            get
            {
                return this.codeField;
            }
            init
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        public decimal rate
        {
            get
            {
                return this.rateField;
            }
            init
            {
                this.rateField = value;
            }
        }

        /// <remarks/>
        public string @base
        {
            get
            {
                return this.baseField;
            }
            init
            {
                this.baseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime start_date
        {
            get
            {
                return this.start_dateField;
            }
            init
            {
                this.start_dateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime end_date
        {
            get
            {
                return this.end_dateField;
            }
            init
            {
                this.end_dateField = value;
            }
        }
    }


}
