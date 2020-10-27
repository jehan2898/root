using System;
using System.Collections.Generic;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ContentSubNotesDAO
/// </summary>
namespace dao
{
    public class ContentSubNotesDAO
    {
        public ContentSubNotesDAO()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private string SCompanyId;

        public string SCompanyId1
        {
            get { return SCompanyId; }
            set { SCompanyId = value; }
        }

        private string sSubTitle;

        public string SSubTitle
        {
            get { return sSubTitle; }
            set { sSubTitle = value; }
        }
        private string sSubDescription;

        public string SSubDescription
        {
            get { return sSubDescription; }
            set { sSubDescription = value; }
        }

        private string sCreatedBy;
        public string SCreatedBy
        {
            get { return sCreatedBy; }
            set { sCreatedBy = value; }
        }

        private string sAddedOnS;
        public string SAddedOnS
        {
            get { return sAddedOnS; }
            set { sAddedOnS = value; }
        }

        private DateTime dtAddedOnD;
        public DateTime DtAddedOnD
        {
            get { return dtAddedOnD; }
            set { dtAddedOnD = value; }
        }

        private int iContentID;
        public int IContentID
        {
            get { return iContentID; }
            set { iContentID = value; }
        }

        private int iSubContentID;
        public int ISubContentID
        {
            get { return iSubContentID; }
            set { iSubContentID = value; }
        }
    }
}