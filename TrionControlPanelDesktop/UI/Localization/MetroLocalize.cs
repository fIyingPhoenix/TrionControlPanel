/**
 * MetroFramework - Modern UI for WinForms
 * 
 * The MIT License (MIT)
 * Copyright (c) 2011 Sven Walter, http://github.com/viperneo
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of 
 * this software and associated documentation files (the "Software"), to deal in the 
 * Software without restriction, including without limitation the rights to use, copy, 
 * modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
 * and to permit persons to whom the Software is furnished to do so, subject to the 
 * following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace MetroFramework.Localization
{
    internal class MetroLocalize
    {
        private DataSet languageDataset;

        public static string DefaultLanguage()
        {
            return "en";
        }

        public static string CurrentLanguage()
        {
            string language = Application.CurrentCulture.TwoLetterISOLanguageName;
            if (language.Length == 0)
            {
                language = DefaultLanguage();
            }

            return language.ToLower();
        }

        public MetroLocalize(string ctrlName)
        {
            ImportManifestResource(ctrlName);
        }

        public MetroLocalize(Control ctrl)
        {
            ImportManifestResource(ctrl.Name);            
        }

        private void ImportManifestResource(string ctrlName)
        {
            Assembly callingAssembly = Assembly.GetCallingAssembly();

            string localizationFilename = callingAssembly.GetName().Name + ".Localization." + CurrentLanguage()  + "." + ctrlName + ".xml";
            Stream xmlStream = callingAssembly.GetManifestResourceStream(localizationFilename)!;

            if (xmlStream == null)
            {
                localizationFilename = callingAssembly.GetName().Name + ".Localization." + DefaultLanguage() + "." + ctrlName + ".xml";
                xmlStream = callingAssembly.GetManifestResourceStream(localizationFilename)!;
            }


                languageDataset ??= new DataSet();

            if (xmlStream != null)
            {
                DataSet importDataset = new();
                importDataset.ReadXml(xmlStream);

                languageDataset.Merge(importDataset);
                xmlStream.Close();
            }
        }

        private static string ConvertVar(object var)
        {
            if (var == null)
                return "";

            return var.ToString()!;
        }

        public string Translate(string key)
        {
            if ((string.IsNullOrEmpty(key))) {
                return "";
            }

            if (languageDataset == null) {
                return "~" + key;
            }

            if (languageDataset.Tables["Localization"] == null) {
                return "~" + key;
            }

            DataRow[] languageRows = languageDataset.Tables["Localization"]!.Select("Key='" + key + "'");
            if (languageRows.Length <= 0)
            {
                return "~" + key;
            }

            return languageRows[0]["Value"].ToString()!;
        }

        public string Translate(string key, object var1)
        {
            string str = Translate(key);
            return str.Replace("#1", ConvertVar(var1));
        }

        public string Translate(string key, object var1, object var2)
        {
            string str = Translate(key);
            str = str.Replace("#1", ConvertVar(var1));
            return str.Replace("#2", ConvertVar(var2));
        }
        public string GetValue(string key, object var1, object var2, object var3)
        {
            string str = Translate(key);
            str = str.Replace("#1", ConvertVar(var1));
            str = str.Replace("#2", ConvertVar(var2));
            return str.Replace("#3", ConvertVar(var3));
        }
        public string GetValue(string key, object var1, object var2, object var3, object var4)
        {
            string str = Translate(key);
            str = str.Replace("#1", ConvertVar(var1));
            str = str.Replace("#2", ConvertVar(var2));
            str = str.Replace("#3", ConvertVar(var3));
            return str.Replace("#4", ConvertVar(var4));
        }
        public string GetValue(string key, object var1, object var2, object var3, object var4, object var5)
        {
            string str = Translate(key);
            str = str.Replace("#1", ConvertVar(var1));
            str = str.Replace("#2", ConvertVar(var2));
            str = str.Replace("#3", ConvertVar(var3));
            str = str.Replace("#4", ConvertVar(var4));
            return str.Replace("#5", ConvertVar(var5));
        }
    }
}
