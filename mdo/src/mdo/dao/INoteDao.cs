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

namespace gov.va.medora.mdo.dao
{
    public interface INoteDao
    {
        Dictionary<string, ArrayList> getNoteTitles(string target, string direction);
        Note[] getSignedNotes(string fromDate, string toDate, int nNotes);
        Note[] getUnsignedNotes(string fromDate, string toDate, int nNotes);
        Note[] getUncosignedNotes(string fromDate, string toDate, int nNotes);
        Note[] getNotes(string fromDate, string toDate, int nNotes);
        Note[] getDischargeSummaries(string fromDate, string toDate, int nNotes);
        string getNoteText(String noteId);
        bool isOneVisitNote(string docDefId, string pid, Encounter encounter);
        bool isOneVisitNote(string docDefId, string pid, string visitStr);
        bool isSurgeryNote(string noteId);
        bool isConsultNote(string noteId);
        NoteResult writeNote(
            string titleId,
            Encounter encounter,
            string text,
            string authorId,
            string cosignerId,
            string consultId,
            string prfId);
        string getCrisisNotes(string fromDate, string toDate, int nrpts);
        bool isCosignerRequired(string userId, string noteId, string authorId = null);
        string getNoteEncounterString(string noteId);
        bool isPrfNote(string noteId);
        PatientRecordFlag[] getPrfNoteActions(string noteId);
        string signNote(string noteId, string userId, string esig);
        string closeNote(string noteId, string consultId);
        string getClinicalWarnings(string fromDate, string toDate, int nrpts);
        string getAdvanceDirectives(string fromDate, string toDate, int nrpts);
    }
}
