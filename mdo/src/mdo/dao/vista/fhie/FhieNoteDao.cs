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
using System.Text;

namespace gov.va.medora.mdo.dao.vista.fhie
{
    public class FhieNoteDao : INoteDao
    {
        VistaNoteDao vistaDao = null;

        public FhieNoteDao(AbstractConnection cxn)
        {
            vistaDao = new VistaNoteDao(cxn);
        }

        public Dictionary<string, ArrayList> getNoteTitles(string target, string direction)
        {
            return null;
        }

        public Note[] getSignedNotes(string fromDate, string toDate, int nNotes)
        {
            return vistaDao.getNotes(fromDate, toDate, nNotes);
        }

        public Note[] getUnsignedNotes(string fromDate, string toDate, int nNotes)
        {
            return null;
        }

        public Note[] getUncosignedNotes(string fromDate, string toDate, int nNotes)
        {
            return null;
        }

        public Note[] getNotes(string fromDate, string toDate, int nNotes)
        {
            return vistaDao.getNotes(fromDate, toDate, nNotes);
        }

        public Note[] getDischargeSummaries(string fromDate, string toDate, int nNotes)
        {
            return vistaDao.getDischargeSummaries(fromDate, toDate, nNotes);
        }

        public string getNoteText(String noteId)
        {
            return null;
        }

        public bool isSurgeryNote(string noteId)
        {
            return false;
        }

        public bool isOneVisitNote(string docDefId, string dfn, Encounter encounter)
        {
            return false;
        }

        public bool isOneVisitNote(string docDefId, string dfn, string visitStr)
        {
            return false;
        }

        public bool isConsultNote(string noteId)
        {
            return false;
        }

        public NoteResult writeNote(
            string titleId,
            Encounter encounter,
            string text,
            string authorId,
            string cosignerId,
            string consultId,
            string prfId)
        {
            return null;
        }

        public string getCrisisNotes(string fromDate, string toDate, int nrpts)
        {
            return null;
        }

        public bool isCosignerRequired(string userId, string noteId)
        {
            return false;
        }

        public bool isCosignerRequired(string userId, string authorId, string noteId)
        {
            return false;
        }

        public string getNoteEncounterString(string noteId)
        {
            return null;
        }

        public bool isPrfNote(string noteId)
        {
            return false;
        }

        public PatientRecordFlag[] getPrfNoteActions(string noteId)
        {
            return null;
        }

        public string signNote(string noteId, string userId, string esig)
        {
            return null;
        }

        public string closeNote(string noteIEN, string consultIEN)
        {
            return null;
        }

        public string getClinicalWarnings(string fromDate, string toDate, int nrpts)
        {
            return null;
        }

        public string getAdvanceDirectives(string fromDate, string toDate, int nrpts)
        {
            return null;
        }

    }
}
