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
    public class VistaOrdersDao : IOrdersDao
    {
        AbstractConnection cxn;

         public VistaOrdersDao(AbstractConnection cxn)
        {
            this.cxn = cxn;
        }

        #region Order Dialogs

        public OrderDialog getOrderDialog(string dlgIen)
        {
            MdoQuery request = buildGetOrderDialogRequest(dlgIen);
            string response = (string)cxn.query(request);
            return toOrderDialog(response);
        }

        internal MdoQuery buildGetOrderDialogRequest(string dlgIen)
        {
            VistaUtils.CheckRpcParams(dlgIen);
            VistaQuery vq = new VistaQuery("ORWDXM MENU");
            vq.addParameter(vq.LITERAL, dlgIen);
            return vq;
        }

        internal OrderDialog toOrderDialog(string response)
        {
            if (response == "")
            {
                return null;
            }
            string[] lines = StringUtils.split(response, StringUtils.CRLF);
            lines = StringUtils.trimArray(lines);
            OrderDialog result = new OrderDialog();
            string[] flds = StringUtils.split(lines[0], StringUtils.CARET);
            result.Name = flds[0];
            result.Columns = new IndexedHashtable();
            for (int linenum = 1; linenum < lines.Length; linenum++)
            {
                flds = StringUtils.split(lines[linenum], StringUtils.CARET);
                string colnum = flds[0];
                string rownum = flds[1];
                if (!result.Exists(colnum))
                {
                    result.AddColumn(colnum);
                }
                OrderDialogColumn column = result.GetColumn(colnum);
                if (!column.Exists(rownum))
                {
                    column.AddRow(rownum);
                }
                OrderDialogRow row = column.GetRow(rownum);
                row.Type = flds[2];
                row.MenuId = flds[3];
                row.FormId = flds[4];
                row.AutoAccept = (flds[5] == "1");
                row.DisplayText = flds[6];
                row.Mnemonic = flds[7];
                row.DisplayOnly = (flds[8] == "1");
            }
            return result;
        }

        internal string getNewDlgIen(string type, string locationIen)
        {
            MdoQuery request = buildGetNewDlgIenRequest(type, locationIen);
            string response = (string)cxn.query(request);
            return toNewDlgIen(response);
        }

        internal MdoQuery buildGetNewDlgIenRequest(string type, string locationIen)
        {
            VistaQuery vq = new VistaQuery("ORWDCN32 NEWDLG");
            vq.addParameter(vq.LITERAL, type);
            vq.addParameter(vq.LITERAL, locationIen);
            return vq;
        }

        internal string toNewDlgIen(string response)
        {
            if (response == "")
            {
                return "";
            }
            string[] flds = StringUtils.split(response, StringUtils.SEMICOLON);
            return VistaUtils.errMsgOrIen(flds[0]);
        }

        internal string getDlgIen(string dlgName)
        {
            if (String.IsNullOrEmpty(dlgName))
            {
                throw new NullOrEmptyParamException("dlgName");
            }
            string arg = "$O(^ORD(101.41,\"B\",\"" + dlgName + "\",0))";
            string response = VistaUtils.getVariableValue(cxn,arg);
            return VistaUtils.errMsgOrIen(response);
        }

        internal string getDisabledMsg(string dlgIen)
        {
            MdoQuery request = buildGetDisabledMsgRequest(dlgIen);
            string response = (string)cxn.query(request);
            return response;
        }

        internal MdoQuery buildGetDisabledMsgRequest(string dlgIen)
        {
            VistaQuery vq = new VistaQuery("ORWDX DISMSG");
            vq.addParameter(vq.LITERAL, dlgIen);
            return vq;
        }

        internal string getOrderDialogNode0(string dlgIen)
        {
            string arg = "$G(^ORD(101.41," + dlgIen + ",0))";
            string response = VistaUtils.getVariableValue(cxn, arg);
            return response;
        }

        internal OrderDialogName getOrderDialogName(string orderIen)
        {
            MdoQuery request = buildGetOrderDialogNameRequest(orderIen);
            string response = (string)cxn.query(request);
            return toOrderDialogName(response);
        }

        internal MdoQuery buildGetOrderDialogNameRequest(string orderIen)
        {
            VistaQuery vq = new VistaQuery("ORWDXM DLGNAME");
            vq.addParameter(vq.LITERAL, orderIen);
            return vq;
        }

        internal OrderDialogName toOrderDialogName(string response)
        {
            if (response == "")
            {
                return null;
            }
            string[] flds = StringUtils.split(response, StringUtils.CARET);
            OrderDialogName result = new OrderDialogName();
            result.Id = flds[0];
            result.DisplayName = flds[1];
            result.BaseId = flds[2];
            result.BaseName = flds[3];
            return result;
        }

        internal OrderResponse[] getOrderResponses(string responseIen)
        {
            MdoQuery request = buildGetOrderResponsesRequest(responseIen);
            string response = (string)cxn.query(request);
            return toOrderResponses(response);
        }

        internal MdoQuery buildGetOrderResponsesRequest(string responseIen)
        {
            VistaQuery vq = new VistaQuery("ORWDX LOADRSP");
            vq.addParameter(vq.LITERAL, responseIen);
            return vq;
        }

        internal OrderResponse[] toOrderResponses(string response)
        {
            if (response == "")
            {
                return null;
            }
            ArrayList lst = new ArrayList();
            OrderResponse rsp = null;
            string[] lines = StringUtils.split(response, StringUtils.CRLF);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "")
                {
                    continue;
                }
                switch (lines[i][0])
                {
                    case '~':
                        if (rsp != null)
                        {
                            lst.Add(rsp);
                        }
                        rsp = new OrderResponse();
                        string[] flds = StringUtils.split(lines[i].Substring(1), StringUtils.CARET);
                        rsp.PromptId = flds[0];
                        rsp.Instance = Convert.ToInt32(flds[1]);
                        rsp.Name = flds[2];
                        rsp.Ivalue = "";
                        rsp.Evalue = "";
                        rsp.Tvalue = "";
                        break;
                    case 'i':
                        rsp.Ivalue += lines[i].Substring(1);
                        break;
                    case 'e':
                        rsp.Evalue += lines[i].Substring(1);
                        break;
                    case 't':
                        rsp.Tvalue += lines[i].Substring(1);
                        break;
                }
            }
            if (rsp != null)
            {
                lst.Add(rsp);
            }
            return (OrderResponse[])lst.ToArray(typeof(OrderResponse));
        }

        internal OrderDialog bldQrsp(
            Patient patient,
            string duz,
            string locationIen,
            string dlgIen,
            string eventStr,
            string keyVars)
        {
            MdoQuery request = buildBldQrspRequest(patient, duz, locationIen, dlgIen, eventStr, keyVars);
            string response = (string)cxn.query(request);
            return toQrsp(response);
        }

        internal MdoQuery buildBldQrspRequest(
            Patient patient, string duz, string locationIen, string dlgIen, string eventStr, string keyVars)
        {
            VistaQuery vq = new VistaQuery("ORWDXM1 BLDQRSP");
            vq.addParameter(vq.LITERAL, dlgIen);
            string s = patient.LocalPid + '^' + locationIen + '^' + duz + '^' +
                (patient.IsInpatient ? "1" : "0") + '^' + patient.Gender + '^' +
                patient.Age + '^' + eventStr + '^' + patient.ScPercent + "^^^" + keyVars;
            vq.addParameter(vq.LITERAL, s);
            return vq;
        }

        internal OrderDialog toQrsp(string response)
        {
            if (response == "")
            {
                return null;
            }
            if (response.Substring(0, 3) == "8^0")
            {
                string[] lines = StringUtils.split(response, StringUtils.CRLF);
                throw new Exception(lines[1]);
            }
            string[] flds = StringUtils.split(response, StringUtils.CARET);
            OrderDialog result = new OrderDialog();
            result.QuickLevel = Convert.ToInt32(flds[0]);
            result.ResponseId = flds[1];
            result.DialogId = flds[2];
            result.Type = flds[3];
            result.FormId = flds[4];
            result.DisplayGrp = flds[5];
            return result;
        }

        public OrderedDictionary getOrderDialogsForDisplayGroup(string displayGroupId)
        {
            MdoQuery request = buildGetOrderDialogsForDisplayGroupRequest(displayGroupId);
            string response = (string)cxn.query(request, new MenuOption("AMOJ VL CPGPI"));
            return orderDialogsToMdo(response);
        }

        internal MdoQuery buildGetOrderDialogsForDisplayGroupRequest(string displayGroupId)
        {
            VistaUtils.CheckRpcParams(displayGroupId);
            VistaQuery vq = new VistaQuery("AMOJVL CPGPI GDIADG");
            vq.addParameter(vq.LITERAL, displayGroupId);
            return vq;
        }

        internal OrderedDictionary orderDialogsToMdo(string response)
        {
            if (String.IsNullOrEmpty(response))
            {
                return null;
            }
            string[] lines = StringUtils.split(response, StringUtils.CRLF);
            OrderedDictionary result = new OrderedDictionary();
            for (int i = 0; i < lines.Length; i++)
            {
                if (String.IsNullOrEmpty(lines[i]))
                {
                    continue;
                }
                string[] flds = StringUtils.split(lines[i], StringUtils.CARET);
                result.Add(flds[0], flds[1]);
            }
            return result;
        }

        public List<OrderDialogItem> getOrderDialogItems(string dialogId)
        {
            MdoQuery request = buildGetOrderDialogItemsRequest(dialogId);
            string response = (string)cxn.query(request, new MenuOption("AMOJ VL CPGPI"));
            return dialogItemsToMdo(response);
        }

        internal MdoQuery buildGetOrderDialogItemsRequest(string dialogId)
        {
            VistaUtils.CheckRpcParams(dialogId);
            VistaQuery vq = new VistaQuery("AMOJVL CPGPI GDG");
            vq.addParameter(vq.LITERAL, dialogId);
            return vq;
        }

        internal List<OrderDialogItem> dialogItemsToMdo(string response)
        {
            if (String.IsNullOrEmpty(response))
            {
                return null;
            }
            string[] lines = StringUtils.split(response, StringUtils.CRLF);
            List<OrderDialogItem> result = new List<OrderDialogItem>();
            for (int i = 0; i < lines.Length; i++)
            {
                if (String.IsNullOrEmpty(lines[i]))
                {
                    continue;
                }
                string[] flds = StringUtils.split(lines[i], StringUtils.CARET);
                OrderDialogItem item = new OrderDialogItem();

                // check for integer sequence #
                item.SequenceNumber = Convert.ToInt16(flds[0]);
                item.DataType = flds[1][0];
                item.Domain = flds[1].Substring(2);
                item.DisplayText = flds[2];
                item.OrderableItemId = flds[3];
                result.Add(item);
            }
            return result;
        }

        #endregion

        #region Write Orders

        internal bool isUnitDoseQuickOrder(string orderIen)
        {
            MdoQuery request = buildIsUnitDoseQuickOrderRequest(orderIen);
            string response = (string)cxn.query(request);
            return (response == "1");
        }

        internal MdoQuery buildIsUnitDoseQuickOrderRequest(string orderIen)
        {
            VistaQuery vq = new VistaQuery("ORWDXM3 ISUDQO");
            vq.addParameter(vq.LITERAL, orderIen);
            return vq;
        }

        internal bool isIvQuickOrder(string orderIen)
        {
            MdoQuery request = buildIsIvQuickOrderRequest(orderIen);
            string response = (string)cxn.query(request);
            return (response == "1");
        }

        internal MdoQuery buildIsIvQuickOrderRequest(string orderIen)
        {
            VistaQuery vq = new VistaQuery("ORIMO ISIVQO");
            vq.addParameter(vq.LITERAL, orderIen);
            return vq;
        }

        internal bool isQuickOrderGrp(string orderIen)
        {
            MdoQuery request = buildIsQuickOrderGrpRequest(orderIen);
            string response = (string)cxn.query(request);
            return (response == "1");
        }

        internal MdoQuery buildIsQuickOrderGrpRequest(string orderIen)
        {
            VistaQuery vq = new VistaQuery("ORWDPS2 QOGRP");
            vq.addParameter(vq.LITERAL, orderIen);
            return vq;
        }

        internal MdoQuery buildSaveOrderRequest(
            string pid,
            string duz,
            string locationIEN,
            string dlgBaseName,
            OrderDialog dlg,
            string orderIEN,
            string startDate)
        {
            VistaQuery vq = new VistaQuery("ORWDX SAVE");
            vq.addParameter(vq.LITERAL, pid);
            vq.addParameter(vq.LITERAL, duz);
            vq.addParameter(vq.LITERAL, locationIEN);
            vq.addParameter(vq.LITERAL, dlgBaseName);
            vq.addParameter(vq.LITERAL, dlg.DisplayGrp);
            vq.addParameter(vq.LITERAL, orderIEN);
            vq.addParameter(vq.LITERAL, "");
            DictionaryHashList lst = new DictionaryHashList();
            OrderResponse[] responses = dlg.Responses;
            for (int i = 0; i < responses.Length; i++)
            {
                lst.Add(responses[i].Index, responses[i].Ivalue);
            }
            vq.addParameter(vq.LIST, lst);
            vq.addParameter(vq.LITERAL, "");
            vq.addParameter(vq.LITERAL, "");
            vq.addParameter(vq.LITERAL, "");
            vq.addParameter(vq.LITERAL, "0");
            return vq;
        }

        internal Order saveOrder(
            string pid,
            string duz,
            string locationIEN,
            string dlgBaseName,
            OrderDialog dlg,
            string orderIEN,
            string startDate)
        {
            MdoQuery request = buildSaveOrderRequest(pid, duz, locationIEN, dlgBaseName, dlg, orderIEN, startDate);
            string response = (string)cxn.query(request);
            return toOrder(response);
        }

        internal Order toOrder(string response)
        {
            if (response == "")
            {
                return null;
            }
            string[] flds = StringUtils.split(response, StringUtils.CARET);
            Order order = new Order();
            order.Id = flds[0].Substring(1);
            order.Timestamp = VistaTimestamp.toDateTime(flds[2]);
            order.StartDate = VistaTimestamp.toDateTime(flds[3]);
            order.StopDate = VistaTimestamp.toDateTime(flds[4]);
            order.Status = flds[5];
            order.SigStatus = flds[6];
            order.VerifyingNurse = flds[7];
            order.VerifyingClerk = flds[8];
            User provider = new User();
            provider.Uid = flds[9];
            provider.Name = new PersonName(flds[10]);
            order.Provider = provider;
            return order;
        }

        internal string validateOrderActionNature(string ien, string action, string encounterProviderIen, string nature)
        {
            VistaQuery vq = new VistaQuery("ORWDXA VALID");
            vq.addParameter(vq.LITERAL, ien);
            vq.addParameter(vq.LITERAL, action);
            vq.addParameter(vq.LITERAL, encounterProviderIen);
            vq.addParameter(vq.LITERAL, nature);
            string rtn = (string)cxn.query(vq);
            if (rtn != "")
            {
                return rtn;
            }
            return "OK";
        }

        internal string checkReleaseOrder(Order order)
        {
            VistaQuery vq = new VistaQuery("ORWDXC SESSION");
            vq.addParameter(vq.LITERAL, cxn.Pid);
            DictionaryHashList lst = new DictionaryHashList();
            lst.Add("1", order.Id + "^^1");
            vq.addParameter(vq.LIST, lst);
            return (string)cxn.query(vq);
        }

        internal string getComplexOrderMsg(string ien)
        {
            VistaQuery vq = new VistaQuery("ORWDXA OFCPLX");
            vq.addParameter(vq.LITERAL, ien);
            return (string)cxn.query(vq);
        }

        internal bool unlockOrder(string ien)
        {
            VistaQuery vq = new VistaQuery("ORWDX UNLOCK ORDER");
            vq.addParameter(vq.LITERAL, ien);
            string rtn = (string)cxn.query(vq);
            return (rtn == "1");
        }

        internal string lockOrder(string ien)
        {
            VistaQuery vq = new VistaQuery("ORWDX LOCK ORDER");
            vq.addParameter(vq.LITERAL, ien);
            string rtn = (string)cxn.query(vq);
            string[] flds = StringUtils.split(rtn, StringUtils.CARET);
            if (flds[0] != "1")
            {
                return flds[1];
            }
            return "OK";
        }

        internal bool lockOrdersForPatient()
        {
            return lockOrdersForPatient(cxn.Pid);
        }

        internal bool lockOrdersForPatient(string dfn)
        {
            VistaQuery vq = new VistaQuery("ORWDX LOCK");
            vq.addParameter(vq.LITERAL, dfn);
            string rtn = (string)cxn.query(vq);
            return (rtn == "1");
        }

        internal bool unlockOrdersForPatient()
        {
            VistaQuery vq = new VistaQuery("ORWDX UNLOCK");
            vq.addParameter(vq.LITERAL, cxn.Pid);
            string rtn = (string)cxn.query(vq);
            return (rtn == "1");
        }

        internal void sendOrders(String providerId, String locIen, String esig, Order[] orders)
        {
            VistaQuery vq = new VistaQuery("ORWDX SEND");
            vq.addParameter(vq.LITERAL, cxn.Pid);
            vq.addParameter(vq.LITERAL, providerId);
            vq.addParameter(vq.LITERAL, locIen);
            vq.addParameter(vq.LITERAL, ' ' + esig);	//' ' apparently needed to avoid a bug in Vista?  See CPRS.
            DictionaryHashList lst = new DictionaryHashList();
            for (int i = 0; i < orders.Length; i++)
            {
                string value = orders[i].Id + '^' +
                    VistaConstants.SS_ESIGNED + '^' +
                    VistaConstants.RS_RELEASE + '^' +
                    VistaConstants.NO_POLICY;
                lst.Add(Convert.ToString(i), value);
            }
            string rtn = (string)cxn.query(vq);
            string[] rex = StringUtils.split(rtn, StringUtils.CRLF);
            for (int i = 0; i < rex.Length; i++)
            {
                string[] flds = StringUtils.split(rex[i], StringUtils.CARET);
                if (orders[i].Id != flds[0])
                {
                    orders[i].ErrMsg = "ID mismatch returning from send: expected " + orders[i].Id + ", found " + flds[0];
                    continue;
                }
                if (flds[1] == "RS")
                {
                    orders[i].Status = "Released";
                }
                else
                {
                    orders[i].Status = "Error: " + flds[3];
                }
            }
        }

        internal void sendOrder(String locIen, String esig, Order order)
        {
            VistaQuery vq = new VistaQuery("ORWDX SEND");
            vq.addParameter(vq.LITERAL, cxn.Pid);
            vq.addParameter(vq.LITERAL, "0");
            vq.addParameter(vq.LITERAL, locIen);
            vq.addEncryptedParameter(vq.LITERAL, ' ' + esig);	//' ' apparently needed to avoid a bug in Vista?  See CPRS.
            DictionaryHashList lst = new DictionaryHashList();
            string value = order.Id + '^' +
                VistaConstants.SS_ESIGNED + '^' +
                VistaConstants.RS_RELEASE + '^' +
                VistaConstants.NO_POLICY;
            lst.Add("1", value);
            vq.addParameter(vq.LIST, lst);
            string rtn = (string)cxn.query(vq);
            string[] flds = StringUtils.split(rtn, StringUtils.CARET);
            flds[1] = flds[1].TrimEnd(null);
            if (flds[1] == "RS")
            {
                order.Status = "Released";
            }
            else
            {
                order.Status = "Error: " + flds[3];
                throw new MdoException(MdoExceptionCode.ARGUMENT_INVALID, "Error: This order has been signed!"); 
               
            }
        }

        internal void releaseOrder(String duz, String locIen, String esig, Order order)
        {
            if (!lockOrdersForPatient())
            {
                //			order.setErrMsg("Unable to lock patient's orders");
                //			return;
            }
            string rtn = lockOrder(order.Id);
            if (rtn != "OK")
            {
                order.ErrMsg = "Unable to lock order: " + rtn;
                unlockOrdersForPatient();
                return;
            }
            rtn = getComplexOrderMsg(order.Id);
            if (rtn != "")
            {
                order.ErrMsg = "Complex order: " + rtn;
                unlockOrdersForPatient();
                unlockOrder(order.Id);
                return;
            }
            rtn = checkReleaseOrder(order);
            if (rtn != "")
            {
                order.ErrMsg = "Release order error: " + rtn;
                unlockOrdersForPatient();
                unlockOrder(order.Id);
                return;
            }
            //			rtn = validateOrderActionNature(ien);
            //			if (!rtn.equals("OK"))
            //			{
            //				orders[i].setErrMsg("Unable to validate order: " + rtn);
            //				continue;
            //			}
            sendOrder(locIen, esig, order);
            if (!unlockOrder(order.Id))
            {
                order.ErrMsg = "Unable to unlock order: " + rtn;
            }
            if (!unlockOrdersForPatient())
            {
                order.ErrMsg = "Unable to unlock patient's orders: " + rtn;
            }
        }

        internal void releaseOrders(String duz, String locIen, String esig, Order[] orders)
        {
            IUserDao userDao = new VistaUserDao(cxn);
            if (!userDao.hasPermission(duz, new SecurityKey("","PERMISSION")))
            {
                throw new Exception("Order is not being made for a provider");
            }
            if (!lockOrdersForPatient())
            {
                throw new Exception("Unable to lock patient's orders");
            }
            for (int i = 0; i < orders.Length; i++)
            {
                string ien = orders[i].Id;
                string rtn = getComplexOrderMsg(ien);
                orders[i].ErrMsg = "";
                if (rtn != "")
                {
                    orders[i].ErrMsg = "Complex order message: " + rtn;
                    continue;
                }
                rtn = lockOrder(ien);
                if (rtn != "OK")
                {
                    orders[i].ErrMsg = "Unable to lock order: " + rtn;
                    continue;
                }
                rtn = checkReleaseOrder(orders[i]);
                if (rtn != "")
                {
                    orders[i].ErrMsg = "Release order error: " + rtn;
                    continue;
                }
                //			rtn = validateOrderActionNature(ien);
                //			if (!rtn.equals("OK"))
                //			{
                //				orders[i].setErrMsg("Unable to validate order: " + rtn);
                //				continue;
                //			}
            }
            sendOrders(duz, locIen, esig, orders);
            for (int i = 0; i < orders.Length; i++)
            {
                if (!unlockOrder(orders[i].Id))
                {
                    orders[i].ErrMsg = "Unable to unlock order";
                    continue;
                }
            }
            unlockOrdersForPatient();
        }

        internal string[] getOrderChecks(string dfn, string dlgType)
        {
            VistaQuery vq = new VistaQuery("ORWDXC DISPLAY");
            vq.addParameter(vq.LITERAL, dfn);
            vq.addParameter(vq.LITERAL, dlgType);
            string rtn = (string)cxn.query(vq);
            if (StringUtils.isEmpty(rtn))
            {
                return new string[0];
            }
            return StringUtils.split(rtn, StringUtils.CRLF);
        }

        internal bool isOrderCheckingEnabled()
        {
            VistaQuery vq = new VistaQuery("ORWDXC ON");
            return ((string)cxn.query(vq) == "E");
        }

        internal string killUnsignedOrdersAlert()
        {
            VistaQuery vq = new VistaQuery("ORWORB KILL UNSIG ORDERS ALERT");
            vq.addParameter(vq.LITERAL, cxn.Pid);
            string rtn = (string)cxn.query(vq);
            if (rtn != "")
            {
                return rtn;
            }
            return "OK";
        }

        internal string killExpiredMedsAlert()
        {
            VistaQuery vq = new VistaQuery("ORWORB KILL EXPIR MEDS ALERT");
            vq.addParameter(vq.LITERAL, cxn.Pid);
            string rtn = (string)cxn.query(vq);
            if (rtn != "")
            {
                return rtn;
            }
            return "OK";
        }

        internal string killUnverifiedMedsAlert()
        {
            VistaQuery vq = new VistaQuery("ORWORB KILL UNVER MEDS ALERT");
            vq.addParameter(vq.LITERAL, cxn.Pid);
            string rtn = (string)cxn.query(vq);
            if (rtn != "")
            {
                return rtn;
            }
            return "OK";
        }

        internal string killUnverifiedOrdersAlert()
        {
            VistaQuery vq = new VistaQuery("ORWORB KILL UNVER ORDERS ALERT");
            vq.addParameter(vq.LITERAL, cxn.Pid);
            string rtn = (string)cxn.query(vq);
            if (rtn != "")
            {
                return rtn;
            }
            return "OK";
        }

        #endregion

        #region Old order writing from VistaClinicalDao

        public Order writeSimpleOrderByPolicy(
            Patient patient,
            String duz,
            String esig,
            String locationIen,
            String orderIen,
            DateTime startDate)
        {
            return writeSimpleOrderByPolicy(patient, duz, esig, locationIen, orderIen, startDate, "");
        }

        public Order writeSimpleOrderByPolicy(
            Patient patient,
            String duz,
            String esig,
            String locationIen,
            String orderIen,
            DateTime startDate,
            string reason)
        {
            OrderDialog dlg = bldQRSP(patient, duz, locationIen, orderIen, "0;C;0;0", "", "0", "0");
            dlg.Responses = getOrderResponses(dlg.ResponseId);
            OrderDialogName dlgName = getOrderDialogName(orderIen);
            VistaQuery vq = new VistaQuery("ORWDX SAVE");
            vq.addParameter(vq.LITERAL, patient.LocalPid);
            vq.addParameter(vq.LITERAL, duz);
            vq.addParameter(vq.LITERAL, locationIen);
            vq.addParameter(vq.LITERAL, dlgName.BaseName);
            vq.addParameter(vq.LITERAL, dlg.DisplayGrp);
            vq.addParameter(vq.LITERAL, orderIen);
            vq.addParameter(vq.LITERAL, "");
            DictionaryHashList lst = new DictionaryHashList();
            OrderResponse[] responses = dlg.Responses;
            for (int i = 0; i < responses.Length; i++)
            {
                if (responses[i].PromptId == "6")
                {
                    String vistaTS = VistaTimestamp.fromDateTime(startDate);
                    lst.Add(responses[i].PromptId + ',' + responses[i].Instance, vistaTS);
                }
                else
                {
                    lst.Add(responses[i].PromptId + ',' + responses[i].Instance, responses[i].Ivalue);
                }
            }
            lst.Add("\"ORCHECK\"", "0");
            lst.Add("\"ORTS\"", "0");
            vq.addParameter(vq.LIST, lst);
            vq.addParameter(vq.LITERAL, "");
            vq.addParameter(vq.LITERAL, "");
            vq.addParameter(vq.LITERAL, "");
            vq.addParameter(vq.LITERAL, "0");
            String rtn = (String)cxn.query(vq);
            if (StringUtils.isEmpty(rtn))
            {
                return null;
            }
            String[] flds = StringUtils.split(rtn, StringUtils.CARET);
            Order order = new Order();
            order.Id = flds[0].Substring(1);
            order.Timestamp = VistaTimestamp.toDateTime(flds[2]);
            order.StartDate = VistaTimestamp.toDateTime(flds[3]);
            order.StopDate = VistaTimestamp.toDateTime(flds[4]);
            order.Status = flds[5];
            order.SigStatus = flds[6];
            order.VerifyingNurse = flds[7];
            order.VerifyingClerk = flds[8];
            User provider = new User();
            provider.Uid = flds[9];
            provider.Name = new PersonName(flds[10]);
            order.Provider = provider;
            releaseOrderByPolicy(patient.LocalPid, duz, locationIen, esig, order);
            return order;
        }

        internal OrderDialog bldQRSP(
            Patient patient,
            string duz,
            String locationIen,
            String orderIen,
            String eventStr,
            String keyVars,
            string isimo,
            string encloc)
        {
            MdoQuery request = buildBldQrspRequest(patient, duz, locationIen, orderIen, eventStr, keyVars, isimo, encloc);
            string response = (string)cxn.query(request);
            return toOrderDialog(response);
        }

        internal MdoQuery buildBldQrspRequest(
            Patient patient,
            String duz,
            String locationIen,
            String orderIen,
            String eventStr,
            String keyVars,
            string isimo,
            string encloc)
        {
            VistaQuery vq = new VistaQuery("ORWDXM1 BLDQRSP");
            vq.addParameter(vq.LITERAL, orderIen);
            String s = patient.LocalPid + '^' + locationIen + '^' + duz + '^' +
                (patient.IsInpatient ? "1" : "0") + '^' + patient.Gender + '^' +
                patient.Age + '^' + eventStr + '^' + patient.ScPercent + "^^^" + keyVars;
            vq.addParameter(vq.LITERAL, s);
            vq.addParameter(vq.LITERAL, isimo);
            vq.addParameter(vq.LITERAL, encloc);
            return vq;
        }

        public void releaseOrderByPolicy(string dfn, String duz, String locIen, String esig, Order order)
        {
            if (!lockOrdersForPatient(dfn))
            {
                order.ErrMsg = "Unable to lock patient's orders";
                return;
            }
            String rtn = lockOrder(order.Id);
            if (rtn != "OK")
            {
                order.ErrMsg = "Unable to lock order: " + rtn;
                unlockOrdersForPatient(dfn);
                return;
            }
            rtn = getComplexOrderMsg(order.Id);
            if (rtn != "")
            {
                order.ErrMsg = "Complex order: " + rtn;
                unlockOrdersForPatient(dfn);
                unlockOrder(order.Id);
                return;
            }
            rtn = checkReleaseOrder(order, dfn);
            if (rtn != "")
            {
                order.ErrMsg = "Release order error: " + rtn;
                unlockOrdersForPatient(dfn);
                unlockOrder(order.Id);
                return;
            }
            //			rtn = validateOrderActionNature(ien);
            //			if (!rtn.equals("OK"))
            //			{
            //				orders[i].setErrMsg("Unable to validate order: " + rtn);
            //				continue;
            //			}
            sendOrderByPolicy(dfn, locIen, esig, order);
            if (!unlockOrder(order.Id))
            {
                order.ErrMsg = "Unable to unlock order: " + rtn;
            }
            if (!unlockOrdersForPatient(dfn))
            {
                order.ErrMsg = "Unable to unlock patient's orders: " + rtn;
            }
        }

        internal bool unlockOrdersForPatient(string dfn)
        {
            MdoQuery request = buildUnlockOrdersForPatientRequest(dfn);
            string response = (string)cxn.query(request);
            return (response == "1");
        }

        internal MdoQuery buildUnlockOrdersForPatientRequest(string dfn)
        {
            VistaQuery vq = new VistaQuery("ORWDX UNLOCK");
            vq.addParameter(vq.LITERAL, dfn);
            return vq;
        }

        public void sendOrderByPolicy(string dfn, string locIen, string esig, Order order)
        {
            MdoQuery request = buildSendOrderByPolicyRequest(dfn, locIen, esig, order);
            string response = (string)cxn.query(request);
            setPropertiesForReleasedOrder(order, response);
        }

        internal MdoQuery buildSendOrderByPolicyRequest(string dfn, string locIen, string esig, Order order)
        {
            VistaQuery vq = new VistaQuery("ORWDX SEND");
            vq.addParameter(vq.LITERAL, dfn);
            vq.addParameter(vq.LITERAL, "0");
            vq.addParameter(vq.LITERAL, locIen);
            vq.addEncryptedParameter(vq.LITERAL, ' ' + esig);	//' ' apparently needed to avoid a bug in Vista?  See CPRS.
            DictionaryHashList lst = new DictionaryHashList();
            String value = order.Id + '^' +
                VistaConstants.SS_ESIGNED + '^' +
                VistaConstants.RS_RELEASE + '^' +
                VistaConstants.NO_POLICY;
            lst.Add("1", value);
            vq.addParameter(vq.LIST, lst);
            return vq;
        }

        internal void setPropertiesForReleasedOrder(Order order, string response)
        {
            string[] flds = StringUtils.split(response, StringUtils.CARET);
            if (order.Id != flds[0])
            {
                order.ErrMsg = "ID mismatch returning from send: expected " + order.Id + ", found " + flds[0];
                return;
            }
            if (flds[1].Contains("RS"))
            {
                order.Status = "Released";
            }
            else
            {
                order.Status = "Error: " + flds[3];
            }
        }

        internal string checkReleaseOrder(Order order, string dfn)
        {
            MdoQuery request = buildCheckReleaseOrderRequest(order, dfn);
            string response = (string)cxn.query(request);
            return response;
        }

        internal MdoQuery buildCheckReleaseOrderRequest(Order order, string dfn)
        {
            VistaQuery vq = new VistaQuery("ORWDXC SESSION");
            vq.addParameter(vq.LITERAL, dfn);
            DictionaryHashList lst = new DictionaryHashList();
            lst.Add("1", order.Id + "^^1");
            vq.addParameter(vq.LIST, lst);
            return vq;
        }

        #endregion

        #region Orderable Items

        public OrderedDictionary getOrderableItemsByName(string name)
        {
            MdoQuery request = buildGetOrderableItemsByNameRequest(name);
            string response = (string)cxn.query(request, new MenuOption("AMOJ VL CPGPI"));
            return orderableItemsToMdo(response);
        }

        internal MdoQuery buildGetOrderableItemsByNameRequest(string name)
        {
            VistaQuery vq = new VistaQuery("AMOJVL CPGPI OILOOK");
            vq.addParameter(vq.LITERAL, name);
            return vq;
        }

        internal OrderedDictionary orderableItemsToMdo(string response)
        {
            if (String.IsNullOrEmpty(response))
            {
                return null;
            }
            string[] lines = StringUtils.split(response, StringUtils.CRLF);
            OrderedDictionary result = new OrderedDictionary();
            for (int i = 0; i < lines.Length; i++)
            {
                if (String.IsNullOrEmpty(lines[i]))
                {
                    continue;
                }
                string[] flds = StringUtils.split(lines[i], StringUtils.CARET);
                result.Add(flds[1], flds[0]);
            }
            return result;
        }

        public string[] getOrderNamesForPrefix(string prefix)
        {
            DdrLister query = new DdrLister(cxn);
            query.File = "101.41";
            query.Fields = ".01;2";
            query.Flags = "IP";
            query.From = prefix;
            query.Xref = "B";
            query.Max = "20";
            string[] rtn = query.execute();
            return rtn;
        }

        #endregion

        #region Read Orders

        public string getOrderDetail(string orderIen, string dfn)
        {
            if (String.IsNullOrEmpty(orderIen))
                throw new NullOrEmptyParamException("orderIen");

            MdoQuery request = buildGetOrderDetailRequest(orderIen, dfn);
            string response = (string)cxn.query(request);
            return response;
        }

        internal MdoQuery buildGetOrderDetailRequest(string orderIen, string dfn)
        {
            VistaUtils.CheckRpcParams(dfn);
            VistaQuery vq = new VistaQuery("ORQOR DETAIL");
            vq.addParameter(vq.LITERAL, orderIen);
            vq.addParameter(vq.LITERAL, dfn);
            return vq;
        }

        public Order getOrder(string orderIen)
        {
            MdoQuery request = buildGetOrderRequest(orderIen);
            string response = (string)cxn.query(request);
            return toOrderByIfn(response);
        }

        internal MdoQuery buildGetOrderRequest(string orderIen)
        {
            VistaUtils.CheckRpcParams(orderIen);
            VistaQuery vq = new VistaQuery("ORWORR GETBYIFN");
            vq.addParameter(vq.LITERAL, orderIen);
            return vq;
        }

        internal Order toOrderByIfn(string response)
        {
            if (response == "")
            {
                return null;
            }
            StringDictionary statuses = cxn.SystemFileHandler.getLookupTable(VistaConstants.ORDER_STATUS);
            Order result = new Order();
            string[] lines = StringUtils.split(response, StringUtils.CRLF);
            lines = StringUtils.trimArray(lines);
            string[] flds = StringUtils.split(lines[0], StringUtils.CARET);
            result.Id = flds[0].Substring(1);
            // flds[1] == Grp
            result.Timestamp = VistaTimestamp.toDateTime(flds[2]);
            result.StartDate = VistaTimestamp.toDateTime(flds[3]);
            result.StopDate = VistaTimestamp.toDateTime(flds[4]);
            if (statuses.ContainsKey(flds[5]))
            {
                result.Status = statuses[flds[5]];
            }
            else
            {
                result.Status = flds[5];
            }
            result.SigStatus = flds[6];
            result.VerifyingNurse = flds[7];
            result.VerifyingClerk = flds[8];
            result.Provider = new User();
            result.Provider.Uid = flds[9];
            result.Provider.Name = new PersonName(flds[10]);
            // flds[11] == ActDA
            result.Flag = flds[12] == "1";
            // flds[13] == DCType
            result.ChartReviewer = flds[14];
            // flds[15] == DEA#
            // flds[16] == VA#
            flds = StringUtils.split(flds[18], StringUtils.COLON);
            result.Location = new HospitalLocation(flds[1],flds[0]);
            result.Text = "";
            for (int i = 1; i < lines.Length; i++)
            {
                result.Text += lines[i].Substring(1) + StringUtils.CRLF;
            }
            return result;
        }

        public Order[] getOrdersForPatient()
        {
            return getOrdersForPatient(cxn.Pid);
        }

        public Order[] getOrdersForPatient(string dfn)
        {
            IndexedHashtable orders = getOrderIdsForPatient(dfn);
            MdoQuery request = null;
            string response = "";

            try
            {
                request = buildGetOrdersForPatientRequest(orders);
                response = (string)cxn.query(request);
                return completeOrders(orders, response);
            }
            catch (Exception exc)
            {
                throw new MdoException(request, response, exc);
            }
        }

        public IndexedHashtable getOrderIdsForPatient(string dfn)
        {
            MdoQuery request = null;
            string response = "";

            try
            {
                request = buildGetOrderIdsForPatientRequest(dfn);
                response = (string)cxn.query(request);
                return toOrderHashtable(response);
            }
            catch (Exception exc)
            {
                throw new MdoException(request, response, exc);
            }
        }

        internal MdoQuery buildGetOrderIdsForPatientRequest(string dfn)
        {
            VistaUtils.CheckRpcParams(dfn);
            VistaQuery vq = new VistaQuery("ORWORR AGET");
            vq.addParameter(vq.LITERAL, dfn);
            vq.addParameter(vq.LITERAL, "2^0");
            vq.addParameter(vq.LITERAL, "1");
            vq.addParameter(vq.LITERAL, "-1");
            vq.addParameter(vq.LITERAL, "0");
            vq.addParameter(vq.LITERAL, "");
            return vq;
        }

        internal MdoQuery buildGetOrdersForPatientRequest(IndexedHashtable orders)
        {
            VistaQuery vq = new VistaQuery("ORWORR GET4LST");
            vq.addParameter(vq.LITERAL, "2");
            vq.addParameter(vq.LITERAL, VistaTimestamp.fromDateTime(DateTime.Now));
            DictionaryHashList lst = new DictionaryHashList();
            for (int i = 0; i < orders.Count; i++)
            {
                lst.Add(Convert.ToString(i + 1), ((Order)orders.GetValue(i)).Id);
            }
            vq.addParameter(vq.LIST, lst);
            return vq;
        }

        internal IndexedHashtable toOrderHashtable(string response)
        {
            if (StringUtils.isEmpty(response))
            {
                return null;
            }
            string[] lines = StringUtils.split(response, StringUtils.CRLF);
            IndexedHashtable t = new IndexedHashtable(lines.Length - 1);
            for (int i = 1; i < lines.Length; i++)
            {
                if (StringUtils.isEmpty(lines[i]))
                {
                    continue;
                }
                string[] flds = StringUtils.split(lines[i], StringUtils.CARET);
                Order order = new Order();
                order.Id = flds[0];
                order.Timestamp = VistaTimestamp.toDateTime(flds[2]);
                t.Add(order.Id, order);
            }
            return t;
        }

        // From CPRS:
        //{           1   2    3     4      5     6   7   8   9    10    11    12    13    14     15     16  17    18
        //{ Pieces: ~IFN^Grp^ActTm^StrtTm^StopTm^Sts^Sig^Nrs^Clk^PrvID^PrvNam^ActDA^Flag^DCType^ChrtRev^DEA#^VA#^DigSig}
        internal Order[] completeOrders(IndexedHashtable t, string response)
        {
            if (StringUtils.isEmpty(response))
            {
                return new Order[0];
            }

            Dictionary<string, OrderType> orderTypes = getOrderTypes(); 

            string[] lines = StringUtils.split(response, StringUtils.CRLF);
            ArrayList lst = new ArrayList();
            lines = StringUtils.trimArray(lines);
            Order currentOrder = null;
            for (int lineIdx = 0; lineIdx < lines.Length; lineIdx++)
            {
                if (String.IsNullOrEmpty(lines[lineIdx])) 
                { 
                    continue; 
                }
                if (lines[lineIdx].StartsWith("~"))
                {
                    if (currentOrder != null) 
                    { 
                        lst.Add(currentOrder); 
                    } 
                    currentOrder = new Order();
                    string[] flds = StringUtils.split(lines[lineIdx], StringUtils.CARET);
                    if (flds == null || flds.Length < 15)
                    {
                        continue; // TBD - should throw exception? data not in expected format
                    }
                    currentOrder.Id = flds[0].Substring(1);
                    if(orderTypes.ContainsKey(flds[1]))
                    {
                        currentOrder.Type = orderTypes[flds[1]];
                    }
                    currentOrder.Timestamp = VistaTimestamp.toDateTime(flds[2]);
                    currentOrder.StartDate = VistaTimestamp.toDateTime(flds[3]);
                    if (flds[4] != "")
                    {
                        currentOrder.StopDate = VistaTimestamp.toDateTime(flds[4]);
                    }
                    currentOrder.Status = decodeOrderStatus(flds[5]);
                    currentOrder.SigStatus = decodeSignatureStatus(flds[6]);
                    currentOrder.VerifyingNurse = flds[7];
                    currentOrder.VerifyingClerk = flds[8];
                    currentOrder.Provider = new User();
                    currentOrder.Provider.Uid = flds[9];
                    currentOrder.Provider.Name = new PersonName(flds[10]);
                    currentOrder.Flag = flds[12] == "1";
                    currentOrder.ChartReviewer = flds[14];
                    if (flds.Length > 17)
                    {
                        //currentOrder.
                    }
                }
                else if (lines[lineIdx].StartsWith("t")) 
                { 
                    currentOrder.Text += (lines[lineIdx].Substring(1) + StringUtils.CRLF); 
                } 
            } 
            if (currentOrder != null) 
            { 
                lst.Add(currentOrder); 
            } 
 		 
            return (Order[])lst.ToArray(typeof(Order)); 
        }

        internal void supplementOrders(Order[] orders)
        {
            StringDictionary urgencyCodes = cxn.SystemFileHandler.getLookupTable(VistaConstants.URGENCY_CODES);
            Dictionary<string, object> collectionSamples = cxn.SystemFileHandler.getFile(VistaConstants.COLLECTION_SAMPLE);
            for (int i = 0; i < orders.Length; i++)
            {
                string[] parts = orders[i].Id.Split(new char[] { ';' });
                DdrLister query = buildSupplementOrdersQuery(parts[0]);
                string[] response = query.execute();
                for (int j=0; j < response.Length; j++)
                {
                    string[] flds = response[j].Split(new char[] {'^'});
                    if (flds[1] == "180" || flds[1] == "7")
                    {
                        if (urgencyCodes.ContainsKey(flds[2]))
                        {
                            orders[i].Urgency = urgencyCodes[flds[2]];
                        }
                    }
                    else if (flds[1] == "126")
                    {
                        if (collectionSamples.ContainsKey(flds[2]))
                        {
                            orders[i].CollectionSample = (CollectionSample)collectionSamples[flds[2]];
                        }
                    }
                }
            }
        }

        internal DdrLister buildSupplementOrdersQuery(string orderId)
        {
            VistaUtils.CheckRpcParams(orderId);
            DdrLister query = new DdrLister(cxn);
            query.File = "100.045";
            query.Iens = "," + orderId + ",";
            query.Fields = ".02;1";
            query.Flags = "IP";
            query.Xref = "#";
            return query;
        }

        public static string decodeOrderStatus(string statusCode)
        {
            Order.OrderStatus status = Order.OrderStatus.valueOf(statusCode);
            if (null != status)
                return status.Name;

            return "Invalid Order Status: " + statusCode;
        }

        internal string decodeSignatureStatus(string statusCode)
        {
            if (statusCode == "0")
            {
                return "ON CHART w/written orders";
            }
            if (statusCode == "1")
            {
                return "ELECTRONIC";
            }
            if (statusCode == "2")
            {
                return "NOT SIGNED";
            }
            if (statusCode == "3")
            {
                return "NOT REQUIRED";
            }
            if (statusCode == "4")
            {
                return "ON CHART w/printed orders";
            }
            if (statusCode == "5")
            {
                return "NOT REQUIRED due to cancel/lapse";
            }
            if (statusCode == "6")
            {
                return "SERVICE CORRECTION to signed order";
            }
            if (statusCode == "7")
            {
                return "DIGITALLY SIGNED";
            }
            if (statusCode == "8")
            {
                return "ON PARENT order";
            }
            return "Invalid Signature Status: " + statusCode;
        }

        public Order[] getLatestOrders(string fromOrderIEN)
        {
            DdrLister query = buildGetLatestOrdersQuery(fromOrderIEN);
            string[] response = query.execute();
            return toOrders(response);
        }

        internal DdrLister buildGetLatestOrdersQuery(string fromOrderIEN)
        {
            VistaUtils.CheckRpcParams(fromOrderIEN);
            DdrLister query = new DdrLister(cxn);
            query.File = "100";
            query.Fields = ".02;1;3;4;5;6;7;8.1;10;11;21;22;23;35";
            query.Flags = "IP";
            query.From = VistaUtils.adjustForNumericSearch(fromOrderIEN);
            query.Xref = "#";
            query.Max = "20";
            return query;
        }

        internal Order[] toOrders(string[] response)
        {
            if (response == null || response.Length == 0)
            {
                return null;
            }
            Order[] result = new Order[response.Length];
            for (int i = 0; i < response.Length; i++)
            {
                string[] flds = StringUtils.split(response[i], StringUtils.CARET);
                Order order = new Order();
                order.Id = flds[0];
                order.PatientId = flds[1];
                order.Provider = new User();
                order.Provider.Uid = flds[2];
                order.WhoEnteredId = flds[3];
                order.Timestamp = VistaTimestamp.toDateTime(flds[4]);
                order.Status = flds[5];
                order.PatientLocationId = flds[6];
                result[i] = order;
            }
            return result;
        }

        public IndexedHashtable getOrders1(string dfn)
        {
            MdoQuery request = null;
            string response = "";
            try
            {
                request = buildGetOrdersRequest1(dfn);
                response = (string)cxn.query(request);
                IndexedHashtable t = toOrderHashtable(response);
                return t;
            }
            catch (Exception exc)
            {
                throw new MdoException(request, response, exc);
            }
        }

        internal MdoQuery buildGetOrdersRequest1(string dfn)
        {
            VistaQuery vq = new VistaQuery("ORWORR AGET");
            vq.addParameter(vq.LITERAL, dfn);
            vq.addParameter(vq.LITERAL, "2^0");
            vq.addParameter(vq.LITERAL, "1");
            vq.addParameter(vq.LITERAL, "0");
            vq.addParameter(vq.LITERAL, "0");
            vq.addParameter(vq.LITERAL, "");
            vq.addParameter(vq.LITERAL, "0");
            return vq;
        }

        public Order[] getOrders(string dfn)
        {
            IndexedHashtable t = getOrders1(dfn);
            MdoQuery request = null;
            string response = "";
            try
            {
                request = buildGetOrdersRequest(t);
                response = (string)cxn.query(request);
                return completeOrders(t, response);
            }
            catch (Exception exc)
            {
                throw new MdoException(request, response, exc);
            }
        }

        internal MdoQuery buildGetOrdersRequest(IndexedHashtable t)
        {
            VistaQuery vq = new VistaQuery("ORWORR GET4LST");
            vq.addParameter(vq.LITERAL, "2");
            vq.addParameter(vq.LITERAL, VistaTimestamp.fromDateTime(DateTime.Now));
            DictionaryHashList lst = new DictionaryHashList();
            for (int i = 0; i < t.Count; i++)
            {
                lst.Add(Convert.ToString(i + 1), ((Order)t.GetValue(i)).Id);
            }
            vq.addParameter(vq.LIST, lst);
            return vq;
        }

        #endregion

        #region Miscellaneous

        public StringDictionary getDiscontinueReasons()
        {
            MdoQuery request = buildGetDiscontinueReasonsRequest();
            string response = (string)cxn.query(request);
            return toDcReasons(response);
        }

        internal MdoQuery buildGetDiscontinueReasonsRequest()
        {
            VistaQuery vq = new VistaQuery("ORWDX2 DCREASON");
            return vq;
        }

        internal StringDictionary toDcReasons(string response)
        {
            if (response == "")
            {
                return null;
            }
            StringDictionary result = new StringDictionary();
            string[] lines = StringUtils.split(response, StringUtils.CRLF);
            lines = StringUtils.trimArray(lines);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] flds = StringUtils.split(lines[i], StringUtils.CARET);
                result.Add(flds[0].Substring(1), flds[1]);
            }
            return result;
        }

        public string[] getDisplayGroups()
        {
            DdrLister query = buildGetDisplayGroupsQuery();
            string[] response = query.execute();
            return response;
        }

        internal DdrLister buildGetDisplayGroupsQuery()
        {
            DdrLister query = new DdrLister(cxn);
            query.File = "100.98";
            query.Fields = ".01;2;3;4";
            query.Flags = "IP";
            query.Xref = "#";
            return query;
        }

        internal string[] getDialogList(string loc)
        {
            VistaUtils.CheckRpcParams(loc);
            VistaQuery vq = new VistaQuery("ORWDX WRLST");
            vq.addParameter(vq.LITERAL, loc);
           
            string response = (string)cxn.query(vq);
            return StringUtils.split(response, StringUtils.CRLF);
        }

        internal string[] getMenu(string ien)
        {
            VistaUtils.CheckRpcParams(ien);
            VistaQuery vq = new VistaQuery("ORWDXM MENU");
            vq.addParameter(vq.LITERAL, ien);

            string response = (string)cxn.query(vq);
            return StringUtils.split(response, StringUtils.CRLF);
        }

        internal Dictionary<string, OrderType> getOrderTypes()
        {
            MdoQuery request = null;
            string response = "";
            try
            {
                request = buildGetOrderTypeRequest();
                response = (string)cxn.query(request);
                Dictionary<string, OrderType> result = toOrderTypes(response);
                return result;
            }
            catch (Exception exc)
            {
                throw new MdoException(request, response, exc);
            }
        }

        private MdoQuery buildGetOrderTypeRequest()
        {
            VistaQuery vq = new VistaQuery("ORWORDG MAPSEQ");
            return vq;
        }

        private Dictionary<string, OrderType> toOrderTypes(string response)
        {
            if (String.IsNullOrEmpty(response))
            {
                return null;
            }
            
            string[] lines = StringUtils.split(response, StringUtils.CRLF);
            if (lines == null || lines.Length == 0)
            {
                return null;
            }
            Dictionary<string, OrderType> orderTypes = new Dictionary<string, OrderType>();
            foreach (string s in lines)
            {
                if (String.IsNullOrEmpty(s))
                {
                    continue;
                }
                // e.g. 3=65^Inpt. Meds^Inpt. Meds
                string[] firstSplit = StringUtils.split(s, StringUtils.EQUALS);
                if (firstSplit == null || firstSplit.Length != 2)
                {
                    continue;
                }
                string[] secondSplit = StringUtils.split(firstSplit[1], StringUtils.CARET);
                if (secondSplit == null || secondSplit.Length != 3)
                {
                    continue;
                }
                // build OrderType object with ID from first split and text parts from second split
                orderTypes.Add(firstSplit[0], new OrderType(firstSplit[0], secondSplit[1], secondSplit[2]));
            }
            if (orderTypes.Count == 0)
            {
                return null;
            }
            else
            {
                return orderTypes;
            }
        }

        public string getOrderStatusForPatient(string dfn, string orderableItemId)
        {
            MdoQuery request = buildGetOrderStatusForPatient(dfn, orderableItemId);
            string response = (string)cxn.query(request, new MenuOption("AMOJ VL CPGPI"));
            return response;
        }

        internal MdoQuery buildGetOrderStatusForPatient(string dfn, string orderableItemId)
        {
            VistaUtils.CheckRpcParams(dfn);
            VistaUtils.CheckRpcParams(orderableItemId);
            VistaQuery vq = new VistaQuery("AMOJVL CPGPI OSCAN");
            vq.addParameter(vq.LITERAL, dfn);
            vq.addParameter(vq.LITERAL, orderableItemId);
            return vq;
        }

        #endregion

        /// <summary>
        /// Get order events for connection's selected patient
        /// <c>Executes: OREVNTX PAT</c>
        /// </summary>
        /// <returns></returns>
        public string getPatientOrderEvents()
        {
            return getPatientOrderEvents(cxn.Pid);
        }

        /// <summary>
        /// Get order events for patient
        /// <c>Executes: OREVNTX PAT</c>
        /// </summary>
        /// <param name="dfn">local patient ID</param>
        /// <returns></returns>
        public string getPatientOrderEvents(string dfn)
        {
            string request = "";
            string response = "";
            try
            {
                VistaQuery vq = new VistaQuery("OREVNTX PAT");
                vq.addParameter(vq.LITERAL, dfn);
                request = vq.buildMessage();
                response = (string)cxn.query(request);

                return buildPatientOrderEventsResponse(response);
            }
            catch (Exception e)
            {
                throw new MdoException(request, response, e);
            }
        }

        internal string buildPatientOrderEventsResponse(string response)
        {
            return response;
        }

        public string getOrderSet(string orderDlgIen)
        {
            throw new NotImplementedException();
        }
    }
}
