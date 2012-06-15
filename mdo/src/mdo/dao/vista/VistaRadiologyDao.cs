using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using System.Text;
using gov.va.medora.utils;
using gov.va.medora.mdo.exceptions;
using gov.va.medora.mdo.src.mdo;

namespace gov.va.medora.mdo.dao.vista
{
    public class VistaRadiologyDao : IRadiologyDao
    {
        AbstractConnection cxn = null;

        public VistaRadiologyDao(AbstractConnection cxn)
        {
            this.cxn = cxn;
        }

        #region Radiology Reports

        public RadiologyReport[] getRadiologyReports(string fromDate, string toDate, int nrpts)
        {
            return getRadiologyReports(cxn.Pid, fromDate, toDate, nrpts);
        }

        public RadiologyReport[] getRadiologyReports(string dfn, string fromDate, string toDate, int nrpts)
        {
            MdoQuery request = VistaUtils.buildReportTextRequest(dfn, fromDate, toDate, nrpts, "OR_R18:IMAGING~RIM;ORDV08;0;");
            string response = (string)cxn.query(request);
            return toRadiologyReports(response);
        }

        internal RadiologyReport[] toRadiologyReports(string response)
        {
            if (response == "")
            {
                return null;
            }
            string[] lines = StringUtils.split(response, StringUtils.CRLF);
            ArrayList lst = new ArrayList();
            RadiologyReport rec = null;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "")
                {
                    continue;
                }
                string[] flds = StringUtils.split(lines[i], StringUtils.CARET);
                if (flds[1] == "[+]")
                {
                    lst.Add(rec);
                    continue;
                }
                int fldnum = Convert.ToInt32(flds[0]);
                switch (fldnum)
                {
                    // TODO
                    // need to consider the following case:
                    //1^NH CAMP PENDLETON;NH CAMP PENDLETON <--- not a recognized site code, should add to site 200
                    //2^05/24/2010 12:26
                    //3^L-SPINE, SERIES (3)
                    // lots more stuff in between
                    //10^[+]
                    case 1:
                        //if (rec != null)
                        //{
                        //    lst.Add(rec);
                        //}
                        rec = new RadiologyReport();
                        string[] parts = StringUtils.split(flds[1], StringUtils.SEMICOLON);
                        if (parts.Length == 2)
                        {
                            rec.Facility = new SiteId(parts[1], parts[0]);
                        }
                        else if (flds[1] != "")
                        {
                            rec.Facility = new SiteId(cxn.DataSource.SiteId.Id, flds[1]);
                        }
                        else
                        {
                            rec.Facility = cxn.DataSource.SiteId;
                        }
                        break;
                    case 2:
                        if (flds.Length == 2)
                        {
                            rec.Timestamp = VistaTimestamp.toUtcFromRdv(flds[1]);
                        }
                        break;
                    case 3:
                        if (flds.Length == 2)
                        {
                            rec.Title = flds[1];
                        }
                        break;
                    case 4:
                        if (flds.Length == 2)
                        {
                            rec.Status = flds[1];
                        }
                        break;
                    case 5:
                        if (flds.Length == 2)
                        {
                            rec.CaseNumber = flds[1];
                            rec.AccessionNumber = rec.getAccessionNumber(rec.Timestamp, rec.CaseNumber, DateFormat.ISO);
                        }
                        break;
                    case 6:
                        if (flds.Length == 2)
                        {
                            rec.Text += flds[1] + '\n';
                        }
                        break;
                        //case 7:
                        //    if (flds.Length == 2)
                        //    {
                        //        rec.Impression += flds[1] + '\n';
                        //    }
                        //    break;
                        //case 8:
                        //    if (flds.Length == 2)
                        //    {
                        //        rec.Text += flds[1] + '\n';
                        //    }
                        //    break;
                }
            }
            //if (rec != null)
            //{
            //    lst.Add(rec);
            //}

            return (RadiologyReport[])lst.ToArray(typeof(RadiologyReport));
        }

        public RadiologyReport getImagingReport(string dfn, string accessionNumber)
        {
            return getReport(dfn, accessionNumber);
        }

        // TODO - need to return a RadiologyReport object here with correctly parsed data
        public RadiologyReport getReport(string dfn, string accessionNumber)
        {
            Dictionary<string, RadiologyReport> exams = getExamList(dfn);
            if (null == exams || !exams.ContainsKey(accessionNumber))
            {
                return null;
            }
            MdoQuery request = null;
            string response = "";
            try
            {
                string caseNumber = accessionNumber.Substring(accessionNumber.IndexOf('-') + 1);
                request = buildGetReportRequest(dfn, exams[accessionNumber].Id, caseNumber);
                response = (string)cxn.query(request);
                return toRadiologyReport(exams[accessionNumber], response);
            }
            catch (Exception exc)
            {
                throw new exceptions.MdoException(request, response, exc);
            }
        }

        internal MdoQuery buildGetReportRequest(string dfn, string id, string caseNumber)
        {
            VistaQuery vq = new VistaQuery("ORWRP REPORT TEXT");
            vq.addParameter(vq.LITERAL, dfn);
            vq.addParameter(vq.LITERAL, "18:IMAGING (LOCAL ONLY)~");
            vq.addParameter(vq.LITERAL, "");
            vq.addParameter(vq.LITERAL, "");
            vq.addParameter(vq.LITERAL, id + '#' + caseNumber);
            vq.addParameter(vq.LITERAL, "0");
            vq.addParameter(vq.LITERAL, "0");
            return vq;
        }

        public RadiologyReport toRadiologyReport(RadiologyReport report, string reportText)
        {
            report.Text = reportText;
            return report;
        }

        #endregion

        #region Get Exams
        /// <summary>
        /// Currently this dao method is not in use.  Unit test written for this DAO method.
        /// </summary>
        /// <param name="dfn"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        internal ArrayList getExams(string dfn, string timestamp)
        {
            DdrLister query = buildGetExamsQuery(dfn, timestamp);
            string[] response = query.execute();
            return toExamArrayList(response);
        }

        internal DdrLister buildGetExamsQuery(string dfn, string timestamp)
        {
            DdrLister result = new DdrLister(cxn);
            result.File = "70.03";
            result.Iens = "," + timestamp + "," + dfn + ",";
            result.Fields = ".01;2;3;17";
            result.Flags = "IP";
            result.Xref = "";
            return result;
        }

        internal ArrayList toExamArrayList(string[] response)
        {
            return null;
        }

        #endregion

        // GetExamList
        /// <summary>
        /// Get a Dictionary of RadiologyReport objects with the accession number as the key
        /// </summary>
        /// <param name="dfn">The patient's local ID</param>
        /// <returns>Dictionary of RadiologyReport objects with accession number as the key</returns>
        public Dictionary<string, RadiologyReport> getExamList(string dfn)
        {
            MdoQuery request = null;
            string response = "";
            try
            {
                request = buildGetExamListRequest(dfn);
                response = (string)cxn.query(request);
                return toImagingExamIds(response);
            }
            catch (Exception exc)
            {
                throw new exceptions.MdoException(request, response, exc);
            }
        }

        internal MdoQuery buildGetExamListRequest(string dfn)
        {
            VistaUtils.CheckRpcParams(dfn);
            VistaQuery vq = new VistaQuery("ORWRA IMAGING EXAMS1");
            vq.addParameter(vq.LITERAL, dfn);
            return vq;
        }

        internal Dictionary<string, RadiologyReport> toImagingExamIds(string response)
        {
            if (String.IsNullOrEmpty(response))
            {
                return null;
            }
            Dictionary<string, RadiologyReport> result = new Dictionary<string, RadiologyReport>();
            string[] rex = StringUtils.split(response, StringUtils.CRLF);
            rex = StringUtils.trimArray(rex);
            for (int i = 0; i < rex.Length; i++)
            {
                RadiologyReport newReport = new RadiologyReport();
                string[] flds = StringUtils.split(rex[i], StringUtils.CARET);
                newReport.AccessionNumber = newReport.getAccessionNumber(flds[2], flds[4], DateFormat.VISTA);
                newReport.Id = flds[1];
                newReport.Timestamp = flds[2];
                newReport.Title = flds[3];
                newReport.CaseNumber = flds[4];
                newReport.Status = flds[5];
                newReport.Exam = new ImagingExam();
                newReport.Exam.Status = (flds[8].Split(new char[] { '~' }))[1]; // e.g. 9~COMPLETE
                // maybe map some more fields? lot's of stuff being returned by RPC. For example:
                //ANN ARBOR VAMC;506
                //^6899085.8548-1
                //^3100914.1451
                //^TIBIA & FIBULA 2 VIEWS
                //^794
                //^Verified
                //^
                //^878633
                //^9~COMPLETE
                //^AA RADIOLOGY,AA
                //^RAD~GENERAL RADIOLOGY
                //^
                //^73590
                //^20323714
                //^N
                //^
                //^
                result.Add(newReport.AccessionNumber, newReport); 
            }
            return result;
        }

    }
}
