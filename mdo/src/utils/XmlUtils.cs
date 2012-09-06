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
using System.Linq;
using System.Text;
using System.Xml;

namespace gov.va.medora.mdo.src.utils
{
    public static class XmlUtils
    {
        /// <summary>
        /// Get an attribute from an XmlNode
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public static string getXmlAttributeValue(XmlNode node, string xPath, string attributeName)
        {
            if (node == null || node.SelectSingleNode(xPath) == null)
            {
                return null;
            }

            XmlNode selectedNode = null;
            if (String.IsNullOrEmpty(xPath) || String.Equals(xPath, "/"))
            {
                selectedNode = node;
            }
            else
            {
                selectedNode = node.SelectSingleNode(xPath);
            }
            if (selectedNode.Attributes == null || selectedNode.Attributes.Count == 0 || selectedNode.Attributes[attributeName] == null)
            {
                return null;
            }
            return selectedNode.Attributes[attributeName].Value;
        }
    }
}
