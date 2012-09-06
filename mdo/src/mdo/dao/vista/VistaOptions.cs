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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using System.Text;
using gov.va.medora.mdo.utils;

namespace gov.va.medora.mdo.dao.vista
{
    public class VistaOptions
    {
        public static KeyValuePair<string,ArrayList> parse(String siteId, String[] vistaRtn)
        {
            ArrayList options = new ArrayList();
            for (int i=0; i<vistaRtn.Length; i++)
            {
                String[] flds = StringUtils.split(vistaRtn[i], StringUtils.CARET);
                options.Add(new UserOption(flds[0],flds[1],flds[2]));
            }
            return new KeyValuePair<string,ArrayList>(siteId, options);
        }

        public static String getNumberByIen(KeyValuePair<string, ArrayList> siteOptions, String target)
        {
            for (int i = 0; i < siteOptions.Value.Count; i++)
            {
                VistaOption opt = (VistaOption)siteOptions.Value[i];
                if (opt.Id == target)
                {
                    return opt.Number;
                }
            }
            return "";
        }

        public static String getNumberByName(VistaOption[] siteOptions, String target)
        {
            for (int i = 0; i < siteOptions.Length; i++)
            {
                if (siteOptions[i].Name == target)
                {
                    return siteOptions[i].Number;
                }
            }
            return "";
        }

        public static String getIenByNumber(KeyValuePair<string, ArrayList> siteOptions, String target)
        {
            for (int i = 0; i < siteOptions.Value.Count; i++)
            {
                VistaOption opt = (VistaOption)siteOptions.Value[i];
                if (opt.Number == target)
                {
                    return opt.Id;
                }
            }
            return "";
        }

        public static String getIenByName(KeyValuePair<string, ArrayList> siteOptions, String target)
        {
            for (int i = 0; i < siteOptions.Value.Count; i++)
            {
                VistaOption opt = (VistaOption)siteOptions.Value[i];
                if (opt.Name == target)
                {
                    return opt.Id;
                }
            }
            return "";
        }

        public static void add(KeyValuePair<string, ArrayList> siteOptions, String optNum, String optIen, String optName)
        {
            ((ArrayList)siteOptions.Value).Add(new UserOption(optNum, optIen, optName));
        }

        public static void remove(KeyValuePair<string, ArrayList> siteOptions, String optionNum)
        {
            for (int i = 0; i < siteOptions.Value.Count; i++)
            {
                VistaOption opt = (VistaOption)siteOptions.Value[i];
                if (opt.Number == optionNum)
                {
                    siteOptions.Value.RemoveAt(i);
                    return;
                }
            }
        }

        public static bool hasOption(VistaOption[] siteOptions, String option)
        {
            for (int i = 0; i < siteOptions.Length; i++)
            {
                if (siteOptions[i].Name == option)
                {
                    return true;
                }
            }
            return false;

        }
    }
}
