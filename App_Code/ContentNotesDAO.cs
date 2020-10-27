using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Collections;

/// <summary>
/// Summary description for ContentNotesDAO
/// </summary>
namespace dao
{
	public class ContentNotesDAO
	{
        public ContentNotesDAO()
        {
 
        }

        private int iContentID;

        public int IContentID
        {
            get { return iContentID; }
            set { iContentID = value; }
        }
        private string scompanyid;

        public string Scompanyid
        {
            get { return scompanyid; }
            set { scompanyid = value; }
        }

        


        private string sTitle;

        public string STitle
        {
            get { return sTitle; }
            set { sTitle = value; }
        }
        private string sProcess;

        public string SProcess
        {
            get { return sProcess; }
            set { sProcess = value; }
        }

        private string sDescription;

        public string SDescription
        {
            get { return sDescription; }
            set { sDescription = value; }
        }

        private string sFileName;
        public string SFileName
        {
            get { return sFileName; }
            set { sFileName = value; }
        }

		private string sFilePath;
        public string SFilePath
        {
            get { return sFilePath; }
            set { sFilePath = value; }
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

        private ArrayList arrSubNotes = null;
        public void AddContent(dao.ContentSubNotesDAO pDao)
        {
            if (arrSubNotes == null)
            {
                arrSubNotes = new ArrayList();
            }

            arrSubNotes.Add(pDao);
        }
        public ArrayList GetContain()
        {
            return arrSubNotes;
        }

        

	}
}