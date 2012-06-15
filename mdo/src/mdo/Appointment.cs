using System;
using System.Collections.Generic;
using System.Text;

namespace gov.va.medora.mdo
{
    public class Appointment
    {
        string id;
        string timestamp;
        string title;
        string status;
        string text;
        SiteId facility;
        HospitalLocation clinic;
        string labDateTime;
        string xrayDateTime;
        string ekgDateTime;
        string purpose;
        string type;
        string currentStatus;

        public Appointment() { }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public SiteId Facility
        {
            get { return facility; }
            set { facility = value; }
        }

        public HospitalLocation Clinic
        {
            get { return clinic; }
            set { clinic = value; }
        }

        public string LabDateTime
        {
            get { return labDateTime; }
            set { labDateTime = value; }
        }

        public string XrayDateTime
        {
            get { return xrayDateTime; }
            set { xrayDateTime = value; }
        }

        public string EkgDateTime
        {
            get { return ekgDateTime; }
            set { ekgDateTime = value; }
        }

        public string Purpose
        {
            get { return purpose; }
            set { purpose = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string CurrentStatus
        {
            get { return currentStatus; }
            set { currentStatus = value; }
        }
    }
}
