#region CopyrightHeader
//
//  Copyright by Contributors
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//         http://www.apache.org/licenses/LICENSE-2.0.txt
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using gov.va.medora.mdo.dao;

namespace gov.va.medora.mdo.api
{
    public class NoteApi
    {
	    String DAO_NAME = "INoteDao";

        public NoteApi() { }

        public IndexedHashtable getNoteTitles(ConnectionSet cxns, String target, String direction)
        {
            return cxns.query(DAO_NAME, "getNoteTitles", new object[] { target, direction });
        }

        public Dictionary<string,ArrayList> getNoteTitles(AbstractConnection cxn, string target, string direction)
        {
            return ((INoteDao)cxn.getDao(DAO_NAME)).getNoteTitles(target, direction);
        }

        public IndexedHashtable getSignedNotes(ConnectionSet cxns, String fromDate, String toDate, int nNotes)
        {
            return cxns.query(DAO_NAME, "getSignedNotes", new object[] { fromDate, toDate, nNotes });
        }

        public IndexedHashtable getUnsignedNotes(ConnectionSet cxns, String fromDate, String toDate, int nNotes)
        {
            return cxns.query(DAO_NAME, "getUnsignedNotes", new object[] { fromDate, toDate, nNotes });
        }

        public IndexedHashtable getUncosignedNotes(ConnectionSet cxns, String fromDate, String toDate, int nNotes)
        {
            return cxns.query(DAO_NAME, "getUncosignedNotes", new object[] { fromDate, toDate, nNotes });
        }

        public IndexedHashtable getNotes(ConnectionSet cxns, String fromDate, String toDate, int nNotes)
        {
            return cxns.query(DAO_NAME, "getNotes", new object[] { fromDate, toDate, nNotes });
        }

        public bool isOneVisitNote(AbstractConnection cxn, string docDefId, string pid, Encounter encounter)
        {
            return ((INoteDao)cxn.getDao(DAO_NAME)).isOneVisitNote(docDefId, pid, encounter);
        }

        public bool isOneVisitNote(AbstractConnection cxn, string docDefId, string pid, string visitStr)
        {
            return ((INoteDao)cxn.getDao(DAO_NAME)).isOneVisitNote(docDefId, pid, visitStr);
        }

        public bool isSurgeryNote(AbstractConnection cxn, string noteId)
        {
            return ((INoteDao)cxn.getDao(DAO_NAME)).isSurgeryNote(noteId);
        }

        public bool isConsultNote(AbstractConnection cxn, string noteId)
        {
            return ((INoteDao)cxn.getDao(DAO_NAME)).isConsultNote(noteId);
        }

        public bool isPrfNote(AbstractConnection cxn, string noteId)
        {
            return ((INoteDao)cxn.getDao(DAO_NAME)).isPrfNote(noteId);
        }

        public IndexedHashtable getDischargeSummaries(ConnectionSet cxns, string fromDate, string toDate, int nNotes)
        {
            return cxns.query(DAO_NAME, "getDischargeSummaries", new object[] { fromDate, toDate, nNotes });
        }

        public String getNoteText(AbstractConnection cxn, String noteIEN)
        {
            return ((INoteDao)cxn.getDao(DAO_NAME)).getNoteText(noteIEN);
        }

        public NoteResult writeNote(
            AbstractConnection cxn,
            string titleId,
            Encounter encounter,
            string text,
            string authorId,
            string cosignerId,
            string consultId,
            string prfId)
        {
            return ((INoteDao)cxn.getDao(DAO_NAME)).writeNote(
                titleId, encounter, text, authorId, cosignerId, consultId, prfId);
        }

        public IndexedHashtable getCrisisNotes(ConnectionSet cxns, string fromDate, string toDate, int nrpts)
        {
            return cxns.query(DAO_NAME, "getCrisisNotes", new object[] { fromDate, toDate, nrpts });
        }

        public bool isCosignerRequired(AbstractConnection cxn, string userId, string noteId, string authorId = null)
        {
            return ((INoteDao)cxn.getDao(DAO_NAME)).isCosignerRequired(userId, noteId, authorId);
        }

        public string getNoteEncounterString(AbstractConnection cxn, string noteId)
        {
            return ((INoteDao)cxn.getDao(DAO_NAME)).getNoteEncounterString(noteId);
        }

        public PatientRecordFlag[] getPrfNoteActions(AbstractConnection cxn, string noteId)
        {
            return ((INoteDao)cxn.getDao(DAO_NAME)).getPrfNoteActions(noteId);
        }

        public string signNote(AbstractConnection cxn, string noteId, string userId, string esig)
        {
            return ((INoteDao)cxn.getDao(DAO_NAME)).signNote(noteId, userId, esig);
        }

        public string closeNote(AbstractConnection cxn, string noteId, string consultId)
        {
            return ((INoteDao)cxn.getDao(DAO_NAME)).closeNote(noteId, consultId);
        }

        public IndexedHashtable getClinicalWarnings(ConnectionSet cxns, string fromDate, string toDate, int nrpts)
        {
            return cxns.query(DAO_NAME, "getClinicalWarnings", new object[] { fromDate, toDate, nrpts });
        }

        public IndexedHashtable getAdvanceDirectives(ConnectionSet cxns, string fromDate, string toDate, int nrpts)
        {
            return cxns.query(DAO_NAME, "getAdvanceDirectives", new object[] { fromDate, toDate, nrpts });
        }

    }
}
