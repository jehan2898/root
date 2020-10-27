using System;
using System.Collections.Generic;
using System.Text;


    //class DrugDAO
    //{
    //}

//public class drugrs
public class DrugDAO
    {

    //public DrugDAO()
    //{ }

        private string _sz_company_name= "DRUGS R US PHARMACY,INC";
        public string sz_company_name
        {

            get
            {
                return this._sz_company_name;
            }
            set 
            {
                this._sz_company_name = value; 
            }
        }
        private string _sz_company_name_address= "1963,GRAND CONCOURSE";
        public string sz_company_name_address
        {
            get
            {
                return _sz_company_name_address;
            }
            set
            {
                _sz_company_name_address = value;
            }
        }
        private string _sz_city= "BRONX,";
        public string sz_city
        {
            get
            {
                return _sz_city;
            }
            set
            {
                _sz_city = value;
            }
        }
        private string _sz_state= "NY";
        public string sz_state
        {
            get
            {
                return _sz_state;
            }
            set
            {
                _sz_state = value;
            }
        }
        private string _sz_zip= "10453";
        public string sz_zip
        {
            get
            {
                return _sz_zip;
            }
            set
            {
                _sz_zip = value;
            }
        }
        private string _sz_tel= "718 299 4400";
        public string sz_tel
        {
            get
            {
                return _sz_tel;
            }
            set
            {
                _sz_tel = value;
            }
        }
        private string _sz_fax= "718 299 4700";
        public string sz_fax
        {
            get
            {
                return _sz_fax;
            }
            set
            {
                _sz_fax = value;
            }
        }
        private string _sz_patient_name;
        //= "WESTCOTT NICOLE";
        public string sz_patient_name
        {
            get
            {
                return _sz_patient_name;
            }
            set
            {
                _sz_patient_name = value;
            }
        }
        private string _date_of_accident;
        //= "10/12/2012";
        public string date_of_accident
        {
            get
            {
                return _date_of_accident;
            }
            set
            {
                _date_of_accident = value;
            }
        }

        private string _sz_patient_address;
        //= "167-04 81 Avenue Samical Hill,Ny,11452";
        public string sz_patient_address
        {
            get
            {
                return _sz_patient_address;
            }
            set
            {
                _sz_patient_address = value;
            }
        }
        private string _sz_Ins_Co;
        //= "Geico";
        public string sz_Ins_Co
        {
            get
            {
                return _sz_Ins_Co;
            }
            set
            {
                _sz_Ins_Co = value;
            }
        }
    }

    public class drugorderlist
    {
        private string sordernumber;
        private string sordereddrug;
        //= "DRUGS R US PHARMACY,INC";
        public string ordernumber
        {

            get
            {
                return this.sordernumber;
            }
            set
            {
                this.sordernumber = value;
            }
        }

        public string ordereddrug
        {

            get
            {
                return this.sordereddrug;
            }
            set
            {
                this.sordereddrug = value;
            }
        }
    
    }
public class print_delivery_receipt
{
    private string _quantity;
    private string _szProcCodeID;
    private string _Date;

    public string quantity
    {

        get
        {
            return this._quantity;
        }
        set
        {
            this._quantity = value;
        }
    }
    public string szProcCodeID
    {

        get
        {
            return this._szProcCodeID;
        }
        set
        {
            this._szProcCodeID = value;
        }
    }
    public string Date
    {

        get
        {
            return this._Date;
        }
        set
        {
            this._Date = value;
        }
    }
}



