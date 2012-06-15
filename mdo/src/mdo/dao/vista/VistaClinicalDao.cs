using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using System.Text;
using System.IO;
using gov.va.medora.mdo.dao;
using gov.va.medora.mdo.dao.vista;
using gov.va.medora.utils;
using gov.va.medora.mdo.exceptions;
using gov.va.medora.mdo.src.mdo;
using System.Configuration;
using System.Xml;
using gov.va.medora.mdo.src.utils;
using gov.va.medora.mdo.domain.ccd;

namespace gov.va.medora.mdo.dao.vista
{
    public class VistaClinicalDao : IClinicalDao
    {
         AbstractConnection cxn = null;

         public VistaClinicalDao(AbstractConnection cxn)
        {
            this.cxn = cxn;
        }

        #region Health Summaries

        public AdHocHealthSummary[] getAdHocHealthSummaries()
        {
            MdoQuery request = buildGetAdHocHealthSummariesRequest();
            string response = (string)cxn.query(request);
            return toAdHocHealthSummaries(response);
        }

        internal MdoQuery buildGetAdHocHealthSummariesRequest()
        {
            VistaQuery vq = new VistaQuery("ORWRP2 HS COMPONENTS");
            return vq;
        }

        internal AdHocHealthSummary[] toAdHocHealthSummaries(string response)
        {
            if (response == "")
            {
                return null;
            }
            string[] lines = StringUtils.split(response, StringUtils.CRLF);
            lines = StringUtils.trimArray(lines);
            AdHocHealthSummary[] result = new AdHocHealthSummary[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] flds = StringUtils.split(lines[i], StringUtils.CARET);
                result[i] = new AdHocHealthSummary();
                result[i].Id = flds[0];
                result[i].Title = flds[4];
            }
            return result;
        }

        public string getAdHocHealthSummaryByDisplayName(string displayName)
        {
            return getAdHocHealthSummaryByDisplayName(cxn.Pid, displayName);
        }

        public string getAdHocHealthSummaryByDisplayName(string dfn, string displayName)
        {
            AdHocHealthSummary hs = getAdHocHealthSummaryDataByDisplayName(displayName);
            if (hs == null)
            {
                return null;
            }
            return getAdHocHealthSummary(dfn, hs.Id, hs.Title);
        }

        public AdHocHealthSummary getAdHocHealthSummaryDataByDisplayName(string displayName)
        {
            MdoQuery request = buildGetAdHocHealthSummariesRequest();
            string response = (string)cxn.query(request, new MenuOption(VistaConstants.CPRS_CONTEXT));
            return toAdHocHealthSummary(displayName, response);
        }

        internal AdHocHealthSummary toAdHocHealthSummary(string displayName, string response)
        {
            if (String.IsNullOrEmpty(response))
            {
                return null;
            }
            string[] lines = StringUtils.split(response, StringUtils.CRLF);
            lines = StringUtils.trimArray(lines);
            for (int i = 0; i < lines.Length; i++)
            {
                string[] flds = StringUtils.split(lines[i], StringUtils.CARET);
                if (String.Equals(flds[1], displayName, StringComparison.CurrentCultureIgnoreCase))
                {
                    AdHocHealthSummary result = new AdHocHealthSummary();
                    result.Id = flds[0];
                    result.Title = flds[4];
                    return result;
                }
            }
            return null;
        }

        public string getAdHocHealthSummary(string IEN, string title)
        {
            return getAdHocHealthSummary(cxn.Pid, IEN, title);
        }

        public string getAdHocHealthSummary(string DFN, string IEN, string title)
        {
            MdoQuery request = buildGetAdHocHealthSummaryRequest(DFN, IEN, title);
            string response = (string)cxn.query(request);
            return response;
        }

        internal MdoQuery buildGetAdHocHealthSummaryRequest(string DFN, string IEN, string title)
        {
            VistaQuery vq = new VistaQuery("ORWRP2 HS REPORT TEXT");
            DictionaryHashList lst = new DictionaryHashList();
            lst.Add("1", IEN + "^^^" + title + "^^^");
            vq.addParameter(vq.LIST, lst);
            vq.addParameter(vq.LITERAL, DFN);
            return vq;
        }

        public MdoDocument[] getHealthSummaryList()
        {
            MdoQuery request = buildGetHealthSummaryListRequest();
            string response = (string)cxn.query(request);
            return toMdoDocuments(response);
        }

        internal MdoQuery buildGetHealthSummaryListRequest()
        {
            VistaQuery vq = new VistaQuery("ORWRP REPORT LISTS");
            return vq;
        }

        /// <summary>Parses ^-delim into MdoDocuments for Health Summary Types
        /// </summary>
        /// <remarks>
        /// Throws Exception if no summary types are returned.
        /// </remarks>
        /// <param name="response">response string from VistA</param>
        /// <returns></returns>
        internal MdoDocument[] toMdoDocuments(string response)
        {
            if (response == "")
            {
                throw new Exception("No summary types returned");
            }
            string[] rex = StringUtils.split(response, StringUtils.CRLF);
            ArrayList lst = new ArrayList();
            int i = 0;
            while (rex[i] != "[HEALTH SUMMARY TYPES]")
            {
                i++;
            }
            while (rex[++i] != "$$END")
            {
                string[] parts = StringUtils.split(rex[i], StringUtils.CARET);
                MdoDocument hs = new MdoDocument(parts[0].Substring(1), parts[1]);
                lst.Add(hs);
            }
            return (MdoDocument[])lst.ToArray(typeof(MdoDocument));
        }

        public string getHealthSummaryTitle(string IEN)
        {
            string arg = "$P(^GMT(142," + IEN + ",0),\"^\",1)";
            string response = VistaUtils.getVariableValue(cxn,arg);
            return toHealthSummaryTitle(response);
        }

        internal string toHealthSummaryTitle(string response)
        {
            if (response == "")
            {
                return "";
            }
            int index = response.IndexOf('^');
            if (index > -1)
            {
                return response.Substring(0, index);
            }
            else
            {
                return response;
            }
        }

	    public string getHealthSummaryText(string mpiPid, MdoDocument hs, string sourceSiteId)
        {
            MdoQuery request = buildGetHealthSummaryTextRequest(mpiPid, hs, sourceSiteId);
            string response = (string)cxn.query(request);
            return response;
        }

        internal MdoQuery buildGetHealthSummaryTextRequest(string mpiPid, MdoDocument hs, string sourceSiteId)
        {
		    VistaQuery vq = new VistaQuery("ORWRP REPORT TEXT");
		    vq.addParameter(vq.LITERAL,"0;" + mpiPid);
		    vq.addParameter(vq.LITERAL,"1;1~");
		    if (sourceSiteId == cxn.DataSource.SiteId.Id) 
            {
                vq.addParameter(vq.LITERAL,hs.Id + ";" + getHealthSummaryTitle(hs.Id));
		    }
		    else 
            {
			    vq.addParameter(vq.LITERAL,hs.Id + ";" + hs.Title.ToUpper());
		    }
		    vq.addParameter(vq.LITERAL,"");
		    vq.addParameter(vq.LITERAL,"");
		    vq.addParameter(vq.LITERAL,"0");
		    vq.addParameter(vq.LITERAL,"0");
            return vq;
	    }

        /// <summary>Gets a local health summary
        /// </summary>
        /// <param name="dfn">The patient DFN</param>
        /// <param name="hs">MdoDocument with the health summary name and/or health summary ien.</param>
        /// <returns>requested health summary or null if not found.</returns>
        public HealthSummary getHealthSummary(string dfn, MdoDocument hs)
        {
            MdoQuery request = buildGetHealthSummaryRequest(dfn, hs);
            string response = (string)cxn.query(request);
            return toHealthSummary(hs, response);
        }

        public HealthSummary getHealthSummary(MdoDocument hs)
        {
            return getHealthSummary(cxn.Pid,hs);
        }

        /// <summary> Gets health summary IEN based on displayName
        /// </summary>
        /// <remarks>
        /// VAN 2012-05-14: this could be useful for other report types but is currently limited by the getHealthSummaryList() call. 
        /// Perhaps consider extending at a later date.
        /// 
        /// Also, there has to be a better way to do this (like a different xref based on health summary title), so this should be temporary.
        /// </remarks>
        /// <param name="displayName"></param>
        /// <returns></returns>
        internal string getHealthSummaryIdByDisplayName(string displayName)
        {
            if (string.IsNullOrEmpty(displayName))
            {
                return null;
            }
            try
            {
                //throws an Exception if no health summary list was returned
                MdoDocument[] summaries = getHealthSummaryList();
                for (int i = 0; i < summaries.Length; i++)
                {
                    if (summaries[i].Title.ToUpper() == displayName.ToUpper())
                    {
                        return summaries[i].Id;
                    }
                }

            }
            catch (Exception e)
            {
                // TBD van 2012-05-14 need to decide what I want to do with this.
                // throw e;
            }
            return null;
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// Health Summaries are pulled by their IEN, so if we don't have one for a title we need to get it based on the display name.
        /// </remarks>
        /// <param name="dfn"></param>
        /// <param name="hs">MdoDocument containing the local display name and the local ien of the summary</param>
        /// <returns>MdoQuery or MdoException if can't get a valid Health Summary IEN.</returns>
        internal MdoQuery buildGetHealthSummaryRequest(string dfn, MdoDocument hs)
        {
            // TBD VAN 2012-05-15 consider refactoring to encapsulate
            if (string.IsNullOrEmpty(hs.Id))
            {
                hs.Id = getHealthSummaryIdByDisplayName(hs.Title);
                if (string.IsNullOrEmpty(hs.Id))
                {
                    throw new MdoException(MdoExceptionCode.ARGUMENT_INVALID, "Missing Health Summary identification. Please provide a valid health summary IEN or Name.");
                }
            }
            else if (string.IsNullOrEmpty(hs.Title))
            {
                hs.Title = getHealthSummaryTitle(hs.Id);
            }
            VistaQuery vq = new VistaQuery("ORWRP REPORT TEXT");
            vq.addParameter(vq.LITERAL, dfn);
            vq.addParameter(vq.LITERAL, "1");
            vq.addParameter(vq.LITERAL, hs.Id);
            vq.addParameter(vq.LITERAL, "");
            vq.addParameter(vq.LITERAL, "");
            vq.addParameter(vq.LITERAL, "0");
            vq.addParameter(vq.LITERAL, "0");
            return vq;
        }

        internal HealthSummary toHealthSummary(MdoDocument md, string response)
        {
            HealthSummary hs = new HealthSummary();
            hs.Id = md.Id;
            hs.Title = md.Title;
            hs.Text = response;
            return hs;
        }


        #endregion

        #region Allergies

        public Allergy[] getAllergies()
        {
            return getAllergies(cxn.Pid);
        }

        public Allergy[] getAllergies(string dfn)
        {
            MdoQuery request = buildGetAllergiesRequest(dfn);
            string response = (string)cxn.query(request);

            MdoQuery coverRequest = buildGetCoverSheetAllergiesRequest(dfn);
            string coverResponse = (string)cxn.query(coverRequest);

            Allergy[] rdvAllergies = toAllergies(response);
            Allergy[] coverAllergies = toAllergiesFromCover(coverResponse);

            return supplementAllergies(coverAllergies, rdvAllergies);
        }

        //Added a toDate call to the interface, to allow automated tests to run 
        //succesfully.
        public Allergy[] getAllergies(string dfn,string toDate)
        {
            MdoQuery request = buildGetAllergiesRequest(dfn,toDate);
            string response = (string)cxn.query(request);

            MdoQuery coverRequest = buildGetCoverSheetAllergiesRequest(dfn);
            string coverResponse = (string)cxn.query(coverRequest);

            Allergy[] rdvAllergies = toAllergies(response);
            Allergy[] coverAllergies = toAllergiesFromCover(coverResponse);

            return supplementAllergies(coverAllergies, rdvAllergies);
        }

        internal MdoQuery buildGetAllergiesRequest(string dfn)
        {
            return VistaUtils.buildReportTextRequest(dfn, "", "", 0, "OR_BADR:ALLERGIES~ADR;ORDV01;73;");
        }
        
        //Added a toDate call to the interface, to allow automated tests to run 
        //succesfully.
        internal MdoQuery buildGetAllergiesRequest(string dfn, String toDate)
        {
            return VistaUtils.buildReportTextRequest(dfn, "", toDate, 0, "OR_BADR:ALLERGIES~ADR;ORDV01;73;");
        }

        //ORQQAL LIST
        //Params ------------------------------------------------------------------
        //literal	100848
        //Results -----------------------------------------------------------------
        //973^NONSTEROIDAL ANTI-INFLAMMATORY^^ABDOMINAL PAIN
        //985^PENICILLIN^
        internal MdoQuery buildGetCoverSheetAllergiesRequest(string dfn)
        {
            VistaQuery vq = new VistaQuery("ORQQAL LIST");
            vq.addParameter(vq.LITERAL, dfn);
            return vq;
        }

        internal Allergy[] supplementAllergies(Allergy[] coverSheetAllergies, Allergy[] rdvAllergies)
        {
            if (coverSheetAllergies == null || coverSheetAllergies.Length == 0)
            {
                return rdvAllergies;
            }
            if (rdvAllergies == null || rdvAllergies.Length == 0)
            {
                return coverSheetAllergies;
            }

            foreach (Allergy allergy in rdvAllergies)
            {
                for (int i = 0; i < coverSheetAllergies.Length; i++)
                {
                    if (String.Equals(coverSheetAllergies[i].AllergenId, allergy.AllergenId))
                    {
                        //allergy.AllergenId = coverSheetAllergies[i].AllergenId;
                        allergy.Reactions = coverSheetAllergies[i].Reactions;
                        break;
                    }
                }
            }

            return rdvAllergies;
        }

        internal Allergy[] toAllergiesFromCover(string response)
        {
            if (String.IsNullOrEmpty(response))
            {
                return new Allergy[0];
            }

            string[] lines = response.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            if (lines == null || lines.Length == 0)
            {
                return new Allergy[0];
            }

            IList<Allergy> allergies = new List<Allergy>();

            foreach (string line in lines)
            {
                string[] flds = line.Split(new char[] { '^' });

                if (flds == null || flds.Length == 0)
                {
                    continue;
                }

                Allergy newAllergy = new Allergy();
                newAllergy.AllergenId = flds[0];
                if (flds.Length > 1)
                {
                    newAllergy.AllergenName = flds[1];
                }
                if (flds.Length > 2)
                {
                    // what is in field 2???
                }
                if (flds.Length > 3)
                {
                    newAllergy.Reactions = new List<Symptom>() { new Symptom() { Name = flds[3] } };
                }

                allergies.Add(newAllergy);
            }

            Allergy[] result = new Allergy[allergies.Count];
            allergies.CopyTo(result, 0);
            return result;
        }

        //        ORWRP REPORT TEXT
 
        //Params ------------------------------------------------------------------
        //literal	100848
        //literal	OR_BADR:ALLERGIES~ADR;ORDV01;73;10
        //literal	
        //literal	
        //literal	
        //literal	3120319
        //literal	3120326
 
        //Results -----------------------------------------------------------------
        //1^CAMP MASTER;500
        //2^NONSTEROIDAL ANTI-INFLAMMATORY
        //3^DRUG
        //4^04/03/2011 08:25
        //5^HISTORICAL
        //7^973
        //1^CAMP MASTER;500
        //2^PENICILLIN
        //3^DRUG
        //4^11/28/2011 17:54
        //5^HISTORICAL
        //7^985

        internal Allergy[] toAllergies(string response)
        {
            if (response == "")
            {
                return null;
            }
            string[] lines = StringUtils.split(response, StringUtils.CRLF);
            ArrayList lst = new ArrayList();
            Allergy rec = null;
            string comment = "";
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "")
                {
                    continue;
                }
                string[] flds = StringUtils.split(lines[i], StringUtils.CARET);
                int fldnum = Convert.ToInt16(flds[0]);
                switch (fldnum)
                {
                    case 1:
                        if (rec != null)
                        {
                            rec.Comment = comment;
                            lst.Add(rec);
                        }
                        rec = new Allergy();
                        comment = "";
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
                            rec.AllergenName = flds[1];
                        }
                        break;
                    case 3:
                        if (flds.Length == 2)
                        {
                            rec.AllergenType = flds[1];
                        }
                        break;
                    case 4:
                        if (flds.Length == 2)
                        {
                            rec.Timestamp = VistaTimestamp.toUtcFromRdv(flds[1]);
                        }
                        break;
                    case 5:
                        if (flds.Length == 2)
                        {
                            rec.Type = new ObservationType("","Allergies and Adverse Reactions", flds[1]);
                        }
                        break;
                    case 6:
                        if (flds.Length == 2)
                        {
                            comment += flds[1] + '\n';
                        }
                        break;
                    case 7:
                        if (flds.Length == 2)
                        {
                            rec.AllergenId = flds[1];
                        }
                        break;
                }
            }
            if (rec != null)
            {
                lst.Add(rec);
            }
            return (Allergy[])lst.ToArray(typeof(Allergy));
        }

        #endregion

        #region Problem List

        public Problem[] getProblemList(string type)
        {
            return getProblemList(cxn.Pid, type);
        }

        public Problem[] getProblemList(string dfn, string type)
        {
            MdoQuery request = buildGetProblemListRequest(dfn, type);
            string response = (string)cxn.query(request);
            return toProblemList(response);
        }

        internal MdoQuery buildGetProblemListRequest(string dfn, string type)
        {
            if (!VistaUtils.isWellFormedIen(dfn))
            {
                throw new MdoException(MdoExceptionCode.ARGUMENT_INVALID, "Invalid DFN: " + dfn);
            }
            if (String.IsNullOrEmpty(type))
            {
                type = "ALL"; // get all problems if type was not specified
            }
            type = type.ToUpper();
            string arg = "";
            if (type == "ACTIVE" || type == "A")
            {
                arg = "OR_PLA:ACTIVE PROBLEMS~PLAILA;ORDV04;59;";
            }
            else if (type == "INACTIVE" || type == "I")
            {
                arg = "OR_PLI:INACTIVE PROBLEMS~PLAILI;ORDV04;60;";
            }
            else if (type == "ALL")
            {
                arg = "OR_DODPLL:ALL PROBLEM LIST~PLAILALL;ORDV04;61;";
            }
            else
            {
                throw new MdoException(MdoExceptionCode.ARGUMENT_INVALID, "Invalid type request: " + type + ".  Must be 'ACTIVE', 'INACTIVE', or 'ALL'.");
            }
            VistaQuery vq = new VistaQuery("ORWRP REPORT TEXT");
            vq.addParameter(vq.LITERAL, dfn);
            vq.addParameter(vq.LITERAL, arg + '0');
            vq.addParameter(vq.LITERAL, "");
            vq.addParameter(vq.LITERAL, "");
            vq.addParameter(vq.LITERAL, "");

            // Time frame has been removed since VistA doesn't use them...
            //vq.addParameter(vq.LITERAL, VistaTimestamp.fromUtcFromDate(fromDate));
            //vq.addParameter(vq.LITERAL, VistaTimestamp.fromUtcToDate(toDate));

            return vq;
        }

        internal Problem[] toProblemList(string response)
        {
            if (response == "")
            {
                return null;
            }
            string[] lines = StringUtils.split(response, StringUtils.CRLF);
            ArrayList lst = new ArrayList();
            Problem rec = null;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "")
                {
                    continue;
                }
                string[] flds = StringUtils.split(lines[i], StringUtils.CARET);
                int fldnum = Convert.ToInt32(flds[0]);
                switch (fldnum)
                {
                    case 1:
                        if (rec != null)
                        {
                            if (rec.ProviderNarrative != null)
                            {
                                rec.ProviderNarrative = rec.ProviderNarrative.Substring(0, rec.ProviderNarrative.Length - 1);
                            }
                            if (rec.NoteNarrative != null)
                            {
                                rec.NoteNarrative = rec.NoteNarrative.Substring(0, rec.NoteNarrative.Length - 1);
                            }
                            if (rec.Exposures != null)
                            {
                                rec.Exposures = rec.Exposures.Substring(0, rec.Exposures.Length - 1);
                            }
                            lst.Add(rec);
                        }
                        rec = new Problem();
                        rec.Type = new ObservationType("", "Problems and Diagnoses", "Problem");
                        string[] parts = StringUtils.split(flds[1], StringUtils.SEMICOLON);
                        if (parts.Length == 2)
                        {
                            rec.Facility = new SiteId(parts[0], parts[1]);
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
                            rec.Status = flds[1];
                        }
                        break;
                    case 3:
                        if (flds.Length == 2)
                        {
                            rec.ProviderNarrative += flds[1] + '\n';
                        }
                        break;
                    case 4:
                        if (flds.Length == 2)
                        {
                            rec.OnsetDate = VistaTimestamp.toUtcFromRdv(flds[1]);
                        }
                        break;
                    case 5:
                        if (flds.Length == 2)
                        {
                            rec.ModifiedDate = VistaTimestamp.toUtcFromRdv(flds[1]);
                        }
                        break;
                    case 6:
                        if (flds.Length == 2)
                        {
                            rec.Observer = new Author("",flds[1],"");
                        }
                        break;
                    case 7:
                        if (flds.Length == 2)
                        {
                            rec.NoteNarrative += flds[1] + '\n';
                        }
                        break;
                    case 8:
                        if (flds.Length == 2)
                        {
                            rec.Exposures += flds[1] + '\n';
                        }
                        break;
                }
            }
            if (rec != null)
            {
                if (rec.ProviderNarrative != null)
                {
                    rec.ProviderNarrative = rec.ProviderNarrative.Substring(0, rec.ProviderNarrative.Length - 1);
                }
                if (rec.NoteNarrative != null)
                {
                    rec.NoteNarrative = rec.NoteNarrative.Substring(0, rec.NoteNarrative.Length - 1);
                }
                if (rec.Exposures != null)
                {
                    rec.Exposures = rec.Exposures.Substring(0, rec.Exposures.Length - 1);
                }
                lst.Add(rec);
            }
            Problem[] result = (Problem[])lst.ToArray(typeof(Problem));
            //annotateAcuteProblems(rex, type, dfn);
            return result;
        }

        public string[] getProblems()
        {
            return getProblems(cxn.Pid);
        }

        public string[] getProblems(string dfn)
        {
            MdoQuery request = buildGetProblemsRequest(dfn);
            string response = (string)cxn.query(request);
            return toProblems(response);
        }

        internal MdoQuery buildGetProblemsRequest(string dfn)
        {
            VistaQuery vq = new VistaQuery("ORQQPL PROBLEM LIST");
            vq.addParameter(vq.LITERAL, dfn);
            return vq;
        }

        internal string[] toProblems(string response)
        {
            if (response == "")
            {
                return null;
            }
            string[] rex = StringUtils.split(response, StringUtils.CRLF);
            rex = StringUtils.trimArray(rex);
            string[] result = new string[rex.Length - 1];
            Array.Copy(rex,1,result,0,rex.Length-1);
            return result;
        }

        #endregion

        #region Surgery

        public SurgeryReport[] getSurgeryReports(bool fWithText)
        {
            return getSurgeryReports(cxn.Pid, fWithText);
        }

        public SurgeryReport[] getSurgeryReports(string dfn, bool fWithText)
        {
            MdoQuery request = buildGetSurgeryReportsRequest(dfn);
            string response = (string)cxn.query(request);
            return toSurgeryReports(response, fWithText);
        }

        internal MdoQuery buildGetSurgeryReportsRequest(string dfn)
        {
            VistaQuery vq = new VistaQuery("ORWSR RPTLIST");
            vq.addParameter(vq.LITERAL, dfn);
            return vq;
        }

        internal SurgeryReport[] toSurgeryReports(string response, bool fWithText)
        {
            if (response == "")
            {
                return null;
            }
            ArrayList lst = new ArrayList();
            SurgeryReport rec = null;
            string[] lines = StringUtils.split(response, StringUtils.CRLF);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "")
                {
                    continue;
                }
			    string[] flds = StringUtils.split(lines[i],StringUtils.CARET);
			    rec = new SurgeryReport();
                string[] parts = StringUtils.split(flds[0], StringUtils.SEMICOLON);
                rec.Facility = new SiteId(parts[0], parts[1]);
			    rec.Id = flds[1];
			    rec.Timestamp = flds[2];
			    rec.Title = flds[3];
			    rec.Author = new Author("",flds[4],"");
                if (fWithText)
                {
                    rec.Text = getSurgeryReportText(cxn.Pid, rec.Id);
                }
			    lst.Add(rec);
		    }
		    return (SurgeryReport[])lst.ToArray(typeof(SurgeryReport));
        }

        public string getSurgeryReportText(string ien)
        {
            return getSurgeryReportText(cxn.Pid, ien);
        }

        public string getSurgeryReportText(string dfn, string ien)
        {
            MdoQuery request = buildGetSurgeryReportTextRequest(dfn, ien);
            string response = (string)cxn.query(request);
            return response;
        }

        public MdoQuery buildGetSurgeryReportTextRequest(string dfn, string ien)
        {
            VistaQuery vq = new VistaQuery("ORWRP REPORT TEXT");
            vq.addParameter(vq.LITERAL, dfn);
            vq.addParameter(vq.LITERAL, "28:SURGERY (LOCAL ONLY)~");
            vq.addParameter(vq.LITERAL, "");
            vq.addParameter(vq.LITERAL, "");
            vq.addParameter(vq.LITERAL, ien);
            vq.addParameter(vq.LITERAL, "0");
            vq.addParameter(vq.LITERAL, "0");
            return vq;
        }

        #endregion

        #region NHIN/AViVA Call
        /// <summary>
        /// Get NHIN data
        /// </summary>
        /// <param name="types">The RPC argument for data types</param>
        /// <returns>A string of NHIN data in XML format</returns>
        public string getNhinData(string types)
        {
            return getNhinData(cxn.Pid, types, null);
        }

        /// <summary>
        /// Get NHIN data
        /// </summary>
        /// <param name="types">The RPC argument for data types</param>
        /// <param name="validTypesString">Comma delimited valid types string</param>
        /// <returns>A string of NHIN data in XML format</returns>
        public string getNhinData(string types, string validTypesString)
        {
            return getNhinData(cxn.Pid, types, types.Split(new char[] { ',' }));
        }

        /// <summary>
        /// Get NHIN data
        /// </summary>
        /// <param name="types">The RPC argument for data types</param>
        /// <param name="validTypes">A string array of valid types</param>
        /// <returns>A string of NHIN data in XML format</returns>
        public string getNhinData(string types, string[] validTypes)
        {
            return getNhinData(cxn.Pid, types, validTypes);
        }

        //Added the menu option back in for the nhin context.
        internal string getNhinData(string dfn, string types, string[] validTypes)
        {
            MdoQuery request = buildGetNhinData(dfn, types, validTypes);
            string response = (string)cxn.query(request , new MenuOption(VistaConstants.VPR_CONTEXT) );
            return response;
        }

        internal MdoQuery buildGetNhinData(string dfn, string types, string[] validTypes)
        {
            VistaUtils.CheckRpcParams(dfn);
            if (validTypes != null && !isValidTypesString(types, validTypes))
            {
                throw new MdoException(MdoExceptionCode.ARGUMENT_INVALID, "Invalid types string");
            }
            VistaQuery vq = new VistaQuery("VPR GET PATIENT DATA");
            vq.addParameter(vq.LITERAL, dfn);
            vq.addParameter(vq.LITERAL, types);
            return vq;
        }

        internal bool isValidTypesString(string types, string[] validTypes)
        {
            if (String.IsNullOrEmpty(types))
            {
                return true;
            }
            // found in app.config / web.config for dynamic reloading, 
            // if adding a type, add it here and to each app.config and the web.config
            /* string[] validTypeArray = 
            { 
                "accession",
                "allergy", 
                "appointment",
                "document",
                "immunization",
                "lab", 
                "med",
                "panel",
                "patient", 
                "problem",
                "procedure",
                "radiology",
                "rx",
                "surgery",
                "visit",
                "vital"
            };
             * */

            HashSet<string> validTypesSet = new HashSet<string>(validTypes); 

            string requestedTypes = types.ToLower().Replace(" ", "");
            HashSet<string> requestedTypesSet = new HashSet<string>(requestedTypes.ToLower().Split(','));

            return requestedTypesSet.IsSubsetOf(validTypesSet);            
        }

        public Hashtable getPatientRecord(string validTypes)
        {
            return getPatientRecord(cxn.Pid, validTypes);
        }

        public Hashtable getPatientRecord(string patientID, string validTypes)
        {
            if (string.IsNullOrEmpty(patientID))
            {
                throw new MdoException(MdoExceptionCode.ARGUMENT_NULL_PATIENT_ID);
            }
            MdoQuery request = buildGetNhinData(patientID, "", null); // get all types
            string response = (string)cxn.query(request, new MenuOption(VistaConstants.VPR_CONTEXT));
            return parseVprXml(response, validTypes.Split(new char[1] { ',' })); // TODO - function that parses results and returns as object
        }

        // Temporary note - the types passed in here do not correspond to the valid types collection. Demographics, reactions, problems, etc are
        // the types we will expect at the top level - those will contain collections of the valid types we are familiar with
        internal Hashtable parseVprXml(string xml, string[] validTypes)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            Patient patient = new Patient();
            IList<Allergy> allergies = new List<Allergy>();
            IList<Appointment> appointments = new List<Appointment>();
            IList<Note> notes = new List<Note>();
            IList<LabReport> labs = new List<LabReport>();
            IList<Medication> meds = new List<Medication>();
            IList<PathologyReport> pathReports = new List<PathologyReport>();
            IList<Problem> problems = new List<Problem>();
            IList<RadiologyReport> radiologyReports = new List<RadiologyReport>();
            IList<SurgeryReport> surgeryReports = new List<SurgeryReport>();
            IList<Visit> visits = new List<Visit>();
            IList<VitalSignSet> vitals = new List<VitalSignSet>();
            IList<Consult> consults = new List<Consult>();
            IList<HealthSummary> healthSummaries = new List<HealthSummary>();
            IList<PatientRecordFlag> flags = new List<PatientRecordFlag>();

            foreach (string type in validTypes)
            {
                XmlNodeList nodes = xmlDoc.GetElementsByTagName(type);
                if (nodes == null || nodes.Count != 1)
                {
                    continue;
                }
                XmlNode node = nodes[0]; // should only be one for each of the expected types
                if (node == null || node.Attributes == null || node.Attributes.Count == 0 ||
                    node.Attributes.GetNamedItem("total") == null)
                {
                    continue;
                }
                Int32 count = 0;
                if (!Int32.TryParse(node.Attributes.GetNamedItem("total").Value, out count) || count == 0)
                {
                    continue;
                }

                // finally we know we have some data to parse
                switch (type)
                {
                    case "demographics" :
                        patient = toPatientFromXmlNode(node);
                        break;
                    case "reactions" :
                        allergies = toAllergiesFromXmlNode(node);
                        break;
                    case "problems" :
                        problems = toProblemsFromXmlNode(node);
                        break;
                    case "vitals" :
                        vitals = toVitalsFromXmlNode(node);
                        break;
                    case "labs" :
                        labs = toLabsFromXmlNode(node);
                        break;
                    case "meds" :
                        meds = toMedsFromXmlNode(node);
                        break;
                    case "immunizations" :
                        // TODO - need to implement
                        break;
                    case "appointments" :
                        appointments = toAppointmentsFromXmlNode(node);
                        break;
                    case "visits" :
                        visits = toVisitsFromXmlNode(node);
                        break;
                    case "documents" :
                        notes = toNotesFromXmlNode(node);
                        break;
                    case "procedures" :
                        // TODO - need to implement
                        break;
                    case "consults" :
                        consults = toConsultsFromXmlNode(node);
                        break;
                    case "flags" :
                        flags = toFlagsFromXmlNode(node);
                        break;
                    case "healthFactors" :
                        healthSummaries = toHealthSummariesFromXmlNode(node);
                        break;
                }
            }

            Hashtable results = new Hashtable();
            results.Add("demographics", patient);
            results.Add("reactions", allergies);
            results.Add("healthFactors", healthSummaries);
            results.Add("flags", flags);
            results.Add("consults", consults);
            results.Add("procedures", null);
            results.Add("documents", notes);
            results.Add("visits", visits);
            results.Add("appointments", appointments);
            results.Add("problems", problems);
            results.Add("vitals", vitals);
            results.Add("labs", labs);
            results.Add("meds", meds);
            results.Add("immunizations", null);

            return results;
        }

        #region VPR XML Parsing

        int verifyTopLevelNode(XmlNode node)
        {
            if (node == null || node.Attributes == null || node.Attributes.Count == 0 || node.Attributes["total"] == null)
            {
                return 0;
            }
            string strTotal = node.Attributes["total"].Value;
            int total = 0;
            if (!Int32.TryParse(strTotal, out total))
            {
                return 0;
            }
            return total;
        }

        // TODO - finish and write tests
        internal IList<LabReport> toLabsFromXmlNode(XmlNode node)
        {
            IList<LabReport> labs = new List<LabReport>();

            int total = verifyTopLevelNode(node);
            if (total == 0)
            {
                return labs;
            }

            XmlNodeList labNodes = node.SelectNodes("/lab");
            if (labNodes == null || labNodes.Count == 0)
            {
                return labs;
            }

            CCRHelper helper = new CCRHelper();
            Dictionary<string, IList<LabResult>> results = new Dictionary<string, IList<LabResult>>();

            foreach (XmlNode labNode in labNodes)
            {
                LabResult result = new LabResult();
                string specimentType = result.SpecimenType = XmlUtils.getXmlAttributeValue(labNode, "specimen", "name");
                string resultValue = result.Value = XmlUtils.getXmlAttributeValue(labNode, "result", "value");

                XmlNode commentNode = labNode.SelectSingleNode("comment");
                if (commentNode != null)
                {
                    string resultComment = result.Comment = commentNode.InnerXml;
                }

                result.Test = new LabTest();
                string testDataType = result.Test.DataType = XmlUtils.getXmlAttributeValue(labNode, "type", "value");
                string testHighRef = result.Test.HiRef = XmlUtils.getXmlAttributeValue(labNode, "high", "value");
                string testId = result.Test.Id = XmlUtils.getXmlAttributeValue(labNode, "id", "value");
                string testName = result.Test.Name = XmlUtils.getXmlAttributeValue(labNode, "test", "value");
                string testLoinc = result.Test.Loinc = XmlUtils.getXmlAttributeValue(labNode, "loinc", "value");
                string testLowRef = result.Test.LowRef = XmlUtils.getXmlAttributeValue(labNode, "low", "value");
                string testShortName = result.Test.ShortName = XmlUtils.getXmlAttributeValue(labNode, "localName", "value");
                string testUnits = result.Test.Units = XmlUtils.getXmlAttributeValue(labNode, "units", "value");

                string id = XmlUtils.getXmlAttributeValue(labNode, "id", "value");
                string accessionNumber = XmlUtils.getXmlAttributeValue(labNode, "groupName", "value");

                //helper.buildLabObject(id, accessionNumber, testDataType, specimentType, 

                if (!results.ContainsKey(accessionNumber))
                {
                    results.Add(accessionNumber, new List<LabResult>());
                }
                results[accessionNumber].Add(result);
            }


            foreach (string key in results.Keys)
            {
                if (key.StartsWith("CH"))
                {
                }
                else if (key.StartsWith("HE"))
                {

                }
                else if (key.StartsWith("MI"))
                {

                }
                else if (key.StartsWith("COAG"))
                {
                    
                }
                else if (key.StartsWith("RIA"))
                {

                }
            }
            return labs;
        }

        internal IList<Visit> toVisitsFromXmlNode(XmlNode node)
        {
            IList<Visit> visits = new List<Visit>();
            
            int total = verifyTopLevelNode(node);
            if (total == 0)
            {
                return visits;
            }

            XmlNodeList visitNodes = node.SelectNodes("/visit");
            if (visitNodes == null || visitNodes.Count == 0)
            {
                return visits;
            }

            foreach (XmlNode visitNode in visitNodes)
            {
                Visit visit = new Visit();
                visit.Timestamp = XmlUtils.getXmlAttributeValue(visitNode, "dateTime", "value");
                visit.Id = XmlUtils.getXmlAttributeValue(visitNode, "id", "value");
                visit.Location = new HospitalLocation() { Name = XmlUtils.getXmlAttributeValue(visitNode, "location", "value") };
                visit.PatientType = XmlUtils.getXmlAttributeValue(visitNode, "patientClass", "value");
                visit.Service = XmlUtils.getXmlAttributeValue(visitNode, "service", "value");
                visit.VisitId = XmlUtils.getXmlAttributeValue(visitNode, "visitString", "value");
                visit.Type = XmlUtils.getXmlAttributeValue(visitNode, "type", "name");

                visits.Add(visit);
            }


            return visits;
        }

        internal IList<PatientRecordFlag> toFlagsFromXmlNode(XmlNode node)
        {
            IList<PatientRecordFlag> flags = new List<PatientRecordFlag>();

            return flags;
        }

        internal IList<Consult> toConsultsFromXmlNode(XmlNode node)
        {
            IList<Consult> consults = new List<Consult>();

            int total = verifyTopLevelNode(node);
            if (total == 0)
            {
                return consults;
            }

            XmlNodeList consultNodes = node.SelectNodes("/consult");
            if (consultNodes == null || consultNodes.Count == 0)
            {
                return consults;
            }

            foreach (XmlNode consultNode in consultNodes)
            {
                Consult consult = new Consult();
                consult.Id = XmlUtils.getXmlAttributeValue(consultNode, "id", "value");
                consult.Title = XmlUtils.getXmlAttributeValue(consultNode, "name", "value");
                consult.Status = XmlUtils.getXmlAttributeValue(consultNode, "status", "value");
                consult.Type = new OrderType() { Id = XmlUtils.getXmlAttributeValue(consultNode, "orderID", "value"), Name1 = XmlUtils.getXmlAttributeValue(consultNode, "type", "value") };

                consults.Add(consult);
            }

            return consults;
        }

        internal IList<HealthSummary> toHealthSummariesFromXmlNode(XmlNode node)
        {
            IList<HealthSummary> summaries = new List<HealthSummary>();

            int total = verifyTopLevelNode(node);
            if (total == 0)
            {
                return summaries;
            }

            XmlNodeList summaryNodes = node.SelectNodes("/factor");
            if (summaryNodes == null || summaryNodes.Count == 0)
            {
                return summaries;
            }

            foreach (XmlNode summaryNode in summaryNodes)
            {
                HealthSummary summary = new HealthSummary();
                summary.Id = XmlUtils.getXmlAttributeValue(summaryNode, "id", "value");
                summary.Text = XmlUtils.getXmlAttributeValue(summaryNode, "comment", "value");
                summary.Title = XmlUtils.getXmlAttributeValue(summaryNode, "name", "value");

                summaries.Add(summary);
            }

            return summaries;
        }

        internal IList<Note> toNotesFromXmlNode(XmlNode node)
        {
            IList<Note> notes = new List<Note>();

            int total = verifyTopLevelNode(node);
            if (total == 0)
            {
                return notes;
            }

            XmlNodeList noteNodes = node.SelectNodes("/document");
            if (noteNodes == null || noteNodes.Count == 0)
            {
                return notes;
            }

            foreach (XmlNode noteNode in noteNodes)
            {
                Note note = new Note();
                note.ConsultId = XmlUtils.getXmlAttributeValue(noteNode, "encounter", "value");
                note.SiteId = new SiteId(XmlUtils.getXmlAttributeValue(noteNode, "facility", "code"), XmlUtils.getXmlAttributeValue(noteNode, "facility", "name"));
                note.Id = XmlUtils.getXmlAttributeValue(noteNode, "id", "value");
                note.LocalTitle = XmlUtils.getXmlAttributeValue(noteNode, "localTitle", "value");
                note.StandardTitle = XmlUtils.getXmlAttributeValue(noteNode, "nationalTitle", "name");
                note.Timestamp = XmlUtils.getXmlAttributeValue(noteNode, "referenceDateTime", "value");
                note.Status = XmlUtils.getXmlAttributeValue(noteNode, "status", "value");

                XmlNodeList authorNodes = noteNode.SelectNodes("clinicians/clinician");
                if (authorNodes != null && authorNodes.Count > 0)
                {
                    foreach (XmlNode authorNode in authorNodes)
                    {
                        string authorRole = XmlUtils.getXmlAttributeValue(authorNode, "/", "role");
                        if (!String.IsNullOrEmpty(authorRole))
                        {
                            Author author = new Author(XmlUtils.getXmlAttributeValue(authorNode, "/", "code"), XmlUtils.getXmlAttributeValue(authorNode, "/", "name"), "");
                            if (String.Equals(authorRole, "A"))
                            {
                                note.Author = author;
                            }
                            else if (String.Equals(authorRole, "S"))
                            {
                                author.Signature = XmlUtils.getXmlAttributeValue(authorNode, "/", "signature");
                                note.ProcTimestamp = XmlUtils.getXmlAttributeValue(authorNode, "/", "dateTime");
                                note.ApprovedBy = author;
                            }
                            else if (String.Equals(authorRole, "C"))
                            {
                                author.Signature = XmlUtils.getXmlAttributeValue(authorNode, "/", "signature");
                                note.Cosigner = author;
                            }
                        }
                    }
                }

                notes.Add(note);
            }

            return notes;
        }

        internal IList<Appointment> toAppointmentsFromXmlNode(XmlNode node)
        {
            IList<Appointment> appointments = new List<Appointment>();

            int total = verifyTopLevelNode(node);
            if (total == 0)
            {
                return appointments;
            }

            XmlNodeList appointmentNodes = node.SelectNodes("/appointment");
            if (appointmentNodes == null || appointmentNodes.Count == 0)
            {
                return appointments;
            }

            foreach (XmlNode appointmentNode in appointmentNodes)
            {
                Appointment appointment = new Appointment();
                appointment.Clinic = new HospitalLocation(XmlUtils.getXmlAttributeValue(appointmentNode, "clinicStop", "code"), XmlUtils.getXmlAttributeValue(appointmentNode, "clinicStop", "name"));
                appointment.Facility = new SiteId(XmlUtils.getXmlAttributeValue(appointmentNode, "facility", "code"), XmlUtils.getXmlAttributeValue(appointmentNode, "facility", "name"));
                appointment.Id = XmlUtils.getXmlAttributeValue(appointmentNode, "id", "value");
                appointment.Status = XmlUtils.getXmlAttributeValue(appointmentNode, "apptStatus", "value");
                appointment.Timestamp = XmlUtils.getXmlAttributeValue(appointmentNode, "dateTime", "value");
                appointment.Type = XmlUtils.getXmlAttributeValue(appointmentNode, "type", "name");

                appointments.Add(appointment);
            }
            return appointments;
        }

        internal IList<Medication> toMedsFromXmlNode(XmlNode node)
        {
            IList<Medication> meds = new List<Medication>();

            int total = verifyTopLevelNode(node);
            if (total == 0)
            {
                return meds;
            }
            XmlNodeList medNodes = node.SelectNodes("/med");
            if (medNodes == null || medNodes.Count == 0)
            {
                return meds;
            }

            foreach (XmlNode medNode in medNodes)
            {
                Medication med = new Medication();
                med.Cost = XmlUtils.getXmlAttributeValue(medNode, "fillCost", "value");
                med.DaysSupply = XmlUtils.getXmlAttributeValue(medNode, "daysSupply", "value");
                med.Id = XmlUtils.getXmlAttributeValue(medNode, "id", "value");
                med.Hospital = new KeyValuePair<string, string>(XmlUtils.getXmlAttributeValue(medNode, "location", "code"), XmlUtils.getXmlAttributeValue(medNode, "location", "name"));
                med.Name = XmlUtils.getXmlAttributeValue(medNode, "name", "value");
                
                med.PharmacyId = XmlUtils.getXmlAttributeValue(medNode, "medID", "value");
                if (!String.IsNullOrEmpty(med.PharmacyId) && med.PharmacyId.Contains(";")) // check outpatient by this ID - e.g. 40494;O
                {
                    string[] pieces = med.PharmacyId.Split(new char[1] { ';' });
                    if (pieces != null && pieces.Length == 2 && !String.IsNullOrEmpty(pieces[1]))
                    {
                        med.IsOutpatient = String.Equals(pieces[1], "O");
                        med.IsInpatient = String.Equals(pieces[1], "I");
                        med.IsUnitDose = pieces[0].EndsWith("U");
                        med.IsNonVA = pieces[0].EndsWith("N");
                        med.IsIV = pieces[0].EndsWith("V");
                        // found a "P" code - what's that??? e.g.: 771P;I
                    }
                }

                med.OrderId = XmlUtils.getXmlAttributeValue(medNode, "orderID", "value");
                med.Refills = XmlUtils.getXmlAttributeValue(medNode, "fillsAllowed", "value");
                med.Remaining = XmlUtils.getXmlAttributeValue(medNode, "fillsRemaining", "value");
                med.Facility = new SiteId(XmlUtils.getXmlAttributeValue(medNode, "facility", "code"), XmlUtils.getXmlAttributeValue(medNode, "facility", "name"));
                med.Provider = new Author(XmlUtils.getXmlAttributeValue(medNode, "orderingProvider", "code"), XmlUtils.getXmlAttributeValue(medNode, "orderingProvider", "name"), "");
                med.RxNumber = XmlUtils.getXmlAttributeValue(medNode, "prescription", "value");
                med.Quantity = XmlUtils.getXmlAttributeValue(medNode, "quantity", "value");

                XmlNode sigNode = medNode.SelectSingleNode("sig");
                if (sigNode != null)
                {
                    med.Sig = sigNode.InnerXml;
                }
                med.Status = XmlUtils.getXmlAttributeValue(medNode, "vaStatus", "value");
                med.Type = XmlUtils.getXmlAttributeValue(medNode, "vaType", "value");
                med.Dose = XmlUtils.getXmlAttributeValue(medNode, "doses/dose", "dose");
                med.Route = XmlUtils.getXmlAttributeValue(medNode, "doses/dose", "route");
                med.Schedule = XmlUtils.getXmlAttributeValue(medNode, "doses/dose", "schedule");
                med.Rate = XmlUtils.getXmlAttributeValue(medNode, "rate", "value");

                med.ExpirationDate = XmlUtils.getXmlAttributeValue(medNode, "expires", "value");
                med.IssueDate = XmlUtils.getXmlAttributeValue(medNode, "ordered", "value");
                med.LastFillDate = XmlUtils.getXmlAttributeValue(medNode, "lastFilled", "value");
                med.StartDate = XmlUtils.getXmlAttributeValue(medNode, "start", "value");
                med.StopDate = XmlUtils.getXmlAttributeValue(medNode, "stop", "value");

                meds.Add(med);
            }
            return meds;
        }

        internal Medications toCCRMedsFromXmlNode(XmlNode node)
        {
            Medications meds = new Medications();

            int total = verifyTopLevelNode(node);
            if (total == 0)
            {
                return meds;
            }
            XmlNodeList medNodes = node.SelectNodes("/med");
            if (medNodes == null || medNodes.Count == 0)
            {
                return meds;
            }

            meds.Medication = new List<StructuredProductType>();// new List<StructuredProductType>();
            CCRHelper ccrHelper = new CCRHelper();

            foreach (XmlNode medNode in medNodes)
            {
                string cost = XmlUtils.getXmlAttributeValue(medNode, "fillCost", "value");
                string daysSupply = XmlUtils.getXmlAttributeValue(medNode, "daysSupply", "value");
                string medId = XmlUtils.getXmlAttributeValue(medNode, "id", "value");
                KeyValuePair<string, string> hospitalLocation = 
                    new KeyValuePair<string, string>(XmlUtils.getXmlAttributeValue(medNode, "location", "code"), XmlUtils.getXmlAttributeValue(medNode, "location", "name"));
                string medName = XmlUtils.getXmlAttributeValue(medNode, "name", "value");

                string pharmId = XmlUtils.getXmlAttributeValue(medNode, "medID", "value");
                string orderId = XmlUtils.getXmlAttributeValue(medNode, "orderID", "value");
                string refills = XmlUtils.getXmlAttributeValue(medNode, "fillsAllowed", "value");
                string refillsRemaining = XmlUtils.getXmlAttributeValue(medNode, "fillsRemaining", "value");
                SiteId facility = new SiteId(XmlUtils.getXmlAttributeValue(medNode, "facility", "code"), XmlUtils.getXmlAttributeValue(medNode, "facility", "name"));
                Author provider = new Author(XmlUtils.getXmlAttributeValue(medNode, "orderingProvider", "code"), XmlUtils.getXmlAttributeValue(medNode, "orderingProvider", "name"), "");
                string rxNumber = XmlUtils.getXmlAttributeValue(medNode, "prescription", "value");
                string quantity = XmlUtils.getXmlAttributeValue(medNode, "quantity", "value");

                string sig = "";
                XmlNode sigNode = medNode.SelectSingleNode("sig");
                if (sigNode != null)
                {
                    sig = sigNode.InnerXml;
                }
                string status = XmlUtils.getXmlAttributeValue(medNode, "vaStatus", "value");
                string medType = XmlUtils.getXmlAttributeValue(medNode, "vaType", "value");
                string dose = XmlUtils.getXmlAttributeValue(medNode, "doses/dose", "dose");
                string route = XmlUtils.getXmlAttributeValue(medNode, "doses/dose", "route");
                string schedule = XmlUtils.getXmlAttributeValue(medNode, "doses/dose", "schedule");
                string rate = XmlUtils.getXmlAttributeValue(medNode, "rate", "value");

                // this should be a dictionary - can have more than 1 orderable item associate with a med (e.g. two components of a solution for IV med)
                // these should be the <products> nodes
                //med.PharmacyOrderableItem 

                string expires = XmlUtils.getXmlAttributeValue(medNode, "expires", "value");
                string issued = XmlUtils.getXmlAttributeValue(medNode, "ordered", "value");
                string lastFilled = XmlUtils.getXmlAttributeValue(medNode, "lastFilled", "value");
                string startDate = XmlUtils.getXmlAttributeValue(medNode, "start", "value");
                string stopDate = XmlUtils.getXmlAttributeValue(medNode, "stop", "value");

                // extra data in CCR med only
                string unitsPerDose = XmlUtils.getXmlAttributeValue(medNode, "doses/dose", "unitsPerDose");
                string units = XmlUtils.getXmlAttributeValue(medNode, "doses/dose", "units");
                string form = XmlUtils.getXmlAttributeValue(medNode, "doses/dose", "noun");

                StructuredProductType ccrMed = ccrHelper.buildMedObject(medName, medId, startDate, stopDate, issued, lastFilled, expires,
                    sig, dose, units, form, unitsPerDose, schedule, route, refills, refillsRemaining, quantity, 
                    provider.Name, provider.Id, status, medType);

                meds.Medication.Add(ccrMed);
            }
            return meds;
        }


        // not finished - need to determin in domain objects are correct/consider migrating to standard
        internal IList<VitalSignSet> toVitalsFromXmlNode(XmlNode node)
        {
            IList<VitalSignSet> vitals = new List<VitalSignSet>();

            int total = verifyTopLevelNode(node);
            if (total == 0)
            {
                return vitals;
            }

            XmlNodeList vitalsNodes = node.SelectNodes("/vital");
            if (vitalsNodes == null || vitalsNodes.Count == 0)
            {
                return vitals;
            }

            foreach (XmlNode vitalNode in vitalsNodes)
            {
                XmlNodeList measurementNodes = vitalNode.SelectNodes("/measurements/measurement");
                if (measurementNodes == null || measurementNodes.Count == 0)
                {
                    continue;
                }
                VitalSignSet vitalsSet = new VitalSignSet();
                IList<VitalSign> vitalsSetVitals = new List<VitalSign>();

                foreach (XmlNode measurementNode in measurementNodes)
                {
                    VitalSign vital = new VitalSign();
                }
            }

            return vitals;
        }

        internal IList<Problem> toProblemsFromXmlNode(XmlNode node)
        {
            IList<Problem> problems = new List<Problem>();

            int total = verifyTopLevelNode(node);
            if (total == 0)
            {
                return problems;
            }

            XmlNodeList problemNodes = node.SelectNodes("/problem");
            if (problemNodes == null || problemNodes.Count == 0)
            {
                return problems;
            }

            foreach (XmlNode problemNode in problemNodes)
            {
                Problem problem = new Problem();

                problem.Timestamp = XmlUtils.getXmlAttributeValue(problemNode, "entered", "value");
                problem.Facility = new SiteId(XmlUtils.getXmlAttributeValue(problemNode, "facility", "code"), XmlUtils.getXmlAttributeValue(problemNode, "facility", "name"));
                problem.Icd = XmlUtils.getXmlAttributeValue(problemNode, "icd", "value");
                problem.Id = XmlUtils.getXmlAttributeValue(problemNode, "id", "value");
                problem.IsServiceConnected = (XmlUtils.getXmlAttributeValue(problemNode, "sc", "value") == "1");
                problem.Location = new HospitalLocation(XmlUtils.getXmlAttributeValue(problemNode, "location", "code"), XmlUtils.getXmlAttributeValue(problemNode, "location", "value"));
                problem.ModifiedDate = XmlUtils.getXmlAttributeValue(problemNode, "updated", "value");
                problem.Observer = new Author(XmlUtils.getXmlAttributeValue(problemNode, "provider", "code"), XmlUtils.getXmlAttributeValue(problemNode, "provider", "name"), "");
                problem.OnsetDate = XmlUtils.getXmlAttributeValue(problemNode, "onset", "value");
                problem.Status = XmlUtils.getXmlAttributeValue(problemNode, "status", "name");
                problem.Type = new ObservationType("", "problem", XmlUtils.getXmlAttributeValue(problemNode, "name", "value"));

                problems.Add(problem);
            }

            return problems;
        }

        internal IList<Allergy> toAllergiesFromXmlNode(XmlNode node)
        {
            IList<Allergy> allergies = new List<Allergy>();

            int total = verifyTopLevelNode(node);
            if (total == 0)
            {
                return allergies;
            }

            XmlNodeList allergyNodes = node.SelectNodes("/allergy");
            if (allergyNodes == null || allergyNodes.Count == 0)
            {
                return allergies;
            }

            foreach (XmlNode allergyNode in allergyNodes)
            {
                Allergy allergy = new Allergy();

                XmlNodeList drugClassesNodes = allergyNode.SelectNodes("drugClasses/drugClass");
                if (drugClassesNodes != null && drugClassesNodes.Count > 0)
                {
                    allergy.DrugClasses = new StringDictionary();
                    foreach (XmlNode drugClassNode in drugClassesNodes)
                    {
                        string vuid = XmlUtils.getXmlAttributeValue(drugClassNode, "/", "vuid");
                        string name = XmlUtils.getXmlAttributeValue(drugClassNode, "/", "name");
                        if (!String.IsNullOrEmpty(vuid))
                        {
                            allergy.DrugClasses.Add(vuid, name);
                        }
                    }
                }

                XmlNodeList drugIngredientsNodes = allergyNode.SelectNodes("drugIngredients/drugIngredient");
                if (drugIngredientsNodes != null && drugIngredientsNodes.Count > 0)
                {
                    allergy.DrugIngredients = new StringDictionary();
                    foreach (XmlNode drugIngredientNode in drugIngredientsNodes)
                    {
                        string vuid = XmlUtils.getXmlAttributeValue(drugIngredientNode, "/", "vuid");
                        string name = XmlUtils.getXmlAttributeValue(drugIngredientNode, "/", "name");
                        if (!String.IsNullOrEmpty(vuid))
                        {
                            allergy.DrugIngredients.Add(vuid, name);
                        }
                    }
                }

                XmlNodeList reactionsNodes = allergyNode.SelectNodes("reactions/reaction");
                if (reactionsNodes != null && reactionsNodes.Count > 0)
                {
                    allergy.Reactions = new List<Symptom>();
                    foreach (XmlNode reactionNode in reactionsNodes)
                    {
                        Symptom symptom = new Symptom();
                        symptom.Name = XmlUtils.getXmlAttributeValue(reactionNode, "/", "name");
                        symptom.Id = XmlUtils.getXmlAttributeValue(reactionNode, "/", "vuid");
                        allergy.Reactions.Add(symptom);
                    }
                }

                allergy.Timestamp = XmlUtils.getXmlAttributeValue(allergyNode, "entered", "value");
                allergy.AllergenId = XmlUtils.getXmlAttributeValue(allergyNode, "id", "value");
                allergy.AllergenName = XmlUtils.getXmlAttributeValue(allergyNode, "name", "value");
                allergy.AllergenType = XmlUtils.getXmlAttributeValue(allergyNode, "type", "name");
                allergy.Comment = XmlUtils.getXmlAttributeValue(allergyNode, "assessment", "value");
                allergy.Facility = new SiteId(XmlUtils.getXmlAttributeValue(allergyNode, "facility", "code"), XmlUtils.getXmlAttributeValue(allergyNode, "facility", "name"));

                allergies.Add(allergy);
            }

            return allergies;
        }

        internal Patient toPatientFromXmlNode(XmlNode node)
        {
            Patient patient = new Patient();

            if (node == null || node.Attributes == null || node.Attributes.Count == 0 || node.Attributes["total"] == null)
            {
                return patient;
            }
            string strTotal = node.Attributes["total"].Value;
            int total = 0;
            if (!Int32.TryParse(strTotal, out total))
            {
                return patient;
            }
            
            try
            {
                patient.DOB = XmlUtils.getXmlAttributeValue(node, "/patient/dob", "value");
                patient.LocalPid = XmlUtils.getXmlAttributeValue(node, "/patient/id", "value");
                patient.IsServiceConnected = (XmlUtils.getXmlAttributeValue(node, "/patient/sc", "value") == "1");
                if (patient.IsServiceConnected)
                {
                    Int32 scPercent = 0;
                    if (Int32.TryParse(XmlUtils.getXmlAttributeValue(node, "/patient/scPercent", "value"), out scPercent))
                    {
                        patient.ScPercent = scPercent;
                    }
                }
                patient.SSN = new SocSecNum(XmlUtils.getXmlAttributeValue(node, "/patient/ssn", "value"));
                patient.MpiPid = XmlUtils.getXmlAttributeValue(node, "/patient/icn", "value");
                patient.MaritalStatus = XmlUtils.getXmlAttributeValue(node, "/patient/maritalStatus", "value");
                patient.Ethnicity = XmlUtils.getXmlAttributeValue(node, "/patient/ethnicities/ethnicity", "value");
                patient.Name = new PersonName(XmlUtils.getXmlAttributeValue(node, "/patient/fullName", "value"));
                patient.Gender = XmlUtils.getXmlAttributeValue(node, "/patient/gender", "value");

                patient.SitePids = new StringDictionary();
                XmlNode facilitiesNode = node.SelectSingleNode("/patient/facilities");
                if (facilitiesNode != null && facilitiesNode.ChildNodes != null && facilitiesNode.ChildNodes.Count > 0)
                {
                    foreach (XmlNode childNode in facilitiesNode.ChildNodes)
                    {
                        string sitecode = childNode.Attributes["code"].Value;
                        string siteName = childNode.Attributes["name"].Value;
                        if (!patient.SitePids.ContainsKey(sitecode))
                        {
                            patient.SitePids.Add(sitecode, siteName);
                        }
                        if (childNode.Attributes["homeSite"] != null || facilitiesNode.ChildNodes.Count == 1)
                        {
                            patient.LocalSiteId = patient.CmorSiteId = sitecode;
                        }
                    }
                }
                patient.Demographics = new Dictionary<string, DemographicSet>();
                patient.Demographics.Add(patient.LocalSiteId, new DemographicSet());
                Address address = new Address();
                address.Street1 = XmlUtils.getXmlAttributeValue(node, "/patient/address", "streetLine1");
                address.Street2 = XmlUtils.getXmlAttributeValue(node, "/patient/address", "streetLine2");
                address.Street3 = XmlUtils.getXmlAttributeValue(node, "/patient/address", "streetLine3");
                address.City = XmlUtils.getXmlAttributeValue(node, "/patient/address", "city");
                address.State = XmlUtils.getXmlAttributeValue(node, "/patient/address", "stateProvince");
                address.Zipcode = XmlUtils.getXmlAttributeValue(node, "/patient/address", "postalCode");
                patient.Demographics[patient.LocalSiteId].StreetAddresses = new List<Address>();
                patient.Demographics[patient.LocalSiteId].StreetAddresses.Add(address);

                patient.IsRestricted = (XmlUtils.getXmlAttributeValue(node, "/patient/sensitive", "value") == "1");
            }
            catch (Exception)
            {
                // how to handle... allow missing data?
            }
            return patient;
        }

        // end VPR XML PARSING
        #endregion

        #endregion

        #region Mental Health

        public List<MentalHealthInstrumentAdministration> getMentalHealthInstrumentsForPatient()
        {
            return getMentalHealthInstrumentsForPatient(cxn.Pid);
        }

        public List<MentalHealthInstrumentAdministration> getMentalHealthInstrumentsForPatient(string dfn)
        {
            DdrLister query = buildGetMentalHealthInstrumentsForPatientQuery(dfn);
            string[] response = query.execute();
            return toMentalHealthInstrumentAdministrations(response);
        }

        internal DdrLister buildGetMentalHealthInstrumentsForPatientQuery(string dfn)
        {
            DdrLister query = new DdrLister(cxn);
            query.File = VistaConstants.MH_ADMINISTRATIONS;
            query.Fields = "1;1E;2;3;4;5;5E;6;6E;7;8;9;11;12;13";
            query.Xref = "C";
            query.Flags = "IP";
            query.From = VistaUtils.adjustForNumericSearch(dfn);
            query.Part = dfn;

            // This takes care of the hospital location name.
            query.Id = "S X=$P(^(0),U,11) I X'=\"\",$D(^SC(X,0)) S X=$P($G(^SC(X,0)),U) D EN^DDIOL(X)";
            return query;
        }

        internal List<MentalHealthInstrumentAdministration> toMentalHealthInstrumentAdministrations(string[] response)
        {
            if (response == null || response.Length == 0)
            {
                return null;
            }

            StringDictionary instruments = cxn.SystemFileHandler.getLookupTable(VistaConstants.MH_TESTS_AND_SURVEYS);

            List<MentalHealthInstrumentAdministration> result = new List<MentalHealthInstrumentAdministration>(response.Length);
            for (int i = 0; i < response.Length; i++)
            {
                MentalHealthInstrumentAdministration mhia = toMentalHealthInstrumentAdministration(response[i], instruments);
                if (mhia != null)
                {
                    result.Add(mhia);
                }
            }
            return result;
        }

        internal MentalHealthInstrumentAdministration toMentalHealthInstrumentAdministration(string response, StringDictionary instruments)
        {
            if (String.IsNullOrEmpty(response))
            {
                return null;
            }
            MentalHealthInstrumentAdministration result = new MentalHealthInstrumentAdministration();
            string[] flds = response.Split(new char[] { '^' });
            result.Id = flds[0];
            result.Patient = new KeyValuePair<string, string>(flds[1], flds[2]);
            string instrumentName = "";
            if (instruments.ContainsKey(flds[3]))
            {
                instrumentName = instruments[flds[3]];
            }
            result.Instrument = new KeyValuePair<string, string>(flds[3], instrumentName);
            result.DateAdministered = VistaTimestamp.toUtcString(flds[4]);
            result.DateSaved = VistaTimestamp.toUtcString(flds[5]);
            result.OrderedBy = new KeyValuePair<string, string>(flds[6], flds[7]);
            result.AdministeredBy = new KeyValuePair<string, string>(flds[8], flds[9]);
            result.IsSigned = flds[10] == "Y";
            result.IsComplete = flds[11] == "Y";
            result.NumberOfQuestionsAnswered = flds[12];
            result.TransmissionStatus = decodeMentalHealthInstrumentTransimissionStatus(flds[13]);
            result.TransmissionTime = VistaTimestamp.toUtcString(flds[14]);
            result.HospitalLocation = new KeyValuePair<string, string>(flds[15], flds[16]);
            return result;
        }

        internal string decodeMentalHealthInstrumentTransimissionStatus(string code)
        {
            if (code == "S")
            {
                return "Successfully added to db";
            }
            if (code == "T")
            {
                return "Transmitted, not yet added";
            }
            if (code == "E")
            {
                return "Error";
            }
            return "Invalid code: " + code;
        }

        public MentalHealthInstrumentResultSet getMentalHealthInstrumentResultSet(string administrationId)
        {
            DdrLister query = buildGetMentalHealthInstrumentResultSetQuery(administrationId);
            string[] response = query.execute();
            return toMentalHealthAdministrationResultSet(response);
        }

        public void addMentalHealthInstrumentResultSet(MentalHealthInstrumentAdministration administration)
        {
            DdrLister query = buildGetMentalHealthInstrumentResultSetQuery(administration.Id);
            string[] response = query.execute();
            administration.ResultSet = toMentalHealthAdministrationResultSet(response);
            administration.ResultSet.Instrument = administration.Instrument;
            administration.ResultSet.AdministrationId = administration.Id;
        }

        internal DdrLister buildGetMentalHealthInstrumentResultSetQuery(string ien)
        {
            DdrLister query = new DdrLister(cxn);
            query.File = VistaConstants.MH_RESULTS;
            query.Fields = "2;3;4;5;6";
            query.Flags = "IP";
            query.Xref = "AC";
            query.From = VistaUtils.adjustForNumericSearch(ien);
            query.Part = ien;
            return query;
        }

        internal MentalHealthInstrumentResultSet toMentalHealthAdministrationResultSet(string[] response)
        {
            if (response == null || response.Length == 0)
            {
                return null;
            }
            MentalHealthInstrumentResultSet result = new MentalHealthInstrumentResultSet();
            string[] flds = response[0].Split(new char[] { '^' });
            result.Id = flds[0];

            // Note that we are getting the name of the Scale, not the pointer as the VistA
            // documentation claims.
            result.Scale = new KeyValuePair<string, string>("", flds[1]);

            result.RawScore = flds[2];
            result.TransformedScores.Add("1",flds[3]);
            result.TransformedScores.Add("2", flds[4]);
            result.TransformedScores.Add("3", flds[5]);
            return result;
        }

        // Changed my mind. There are a whole bunch of fields in this and the immediate need is just for
        // the IEN and name so we'll do the StringD
        //public List<MentalHealthInstrument> getMentalHealthInstruments()
        //{
        //    DdrLister query = buildGetMentalHealthInstrumentsQuery();
        //    string[] response = query.execute();
        //    return toMentalHealthInstruments(response);
        //}

        //internal DdrLister buildGetMentalHealthInstrumentsQuery()
        //{
        //    DdrLister query = new DdrLister(cxn);
        //    query.File = VistaConstants.MH_TESTS_AND_SURVEYS;
        //    query.Fields = ".01;1;1E;2";
        //    query.Xref = "#";
        //    query.Flags = "IP";
        //    return query;
        //}

        #endregion


        public string getAllergiesAsXML()
        {
            throw new NotImplementedException();
        }
    }
}
