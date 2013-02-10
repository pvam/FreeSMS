using System;
using System.IO.IsolatedStorage;
using System.Diagnostics;

namespace FreeSMS
{
    public class AppSettings
    {
        // Our isolated storage settings
        IsolatedStorageSettings isolatedStore;
        // The isolated storage key names of our settings

        const string way2smsuidkeyname = "way2smsuid";
        const string way2smspwdkeyname = "way2smspwd";
        const string onesixtyuidkeyname= "uid160by2";
        const string onesixtypwdkeyname ="pwd160by2";
        const string fullonsmsuidkeyname = "uidfullonsms";
        const string fullonsmspwdkeyname = "pwdfullonsms";
        const string site2smsuidkeyname = "uidsite2sms";
        const string site2smspwdkeyname = "pwdsite2sms";
        const string smsfiuidkeyname = "uidsmsfi";
        const string smsfipwdkeyname = "pwdsmsfi";
        const string freesms8uidkeyname = "uidfreesms8";
        const string freesms8pwdkeyname = "pwdfreesms8";
        const string sms440uidkeyname = "uidsms440";
        const string sms440pwdkeyname = "pwdsms400";
        const string bhokaliuidkeyname = "uidbhokali";
        const string bhokalipwdkeyname = "pwdbhokali";
        const string statusdata = "status";
        const string backgrnd = "background";
        const string sms = "smsservice";
        const string promokeyname = "adpromo";

        // The default value of our settings
        bool adpromo_default = false;
        const string way2smsuid_default = "";
        const string way2smspwd_default = "";
        const string onesixtyuid_default = "";
        const string onesixtypwd_default = "";
        const string fullonsmsuid_default = "";
        const string fullonsmspwd_default = "";
        const string site2smsuid_default = "";
        const string site2smspwd_default = "";
        const string smsfiuid_default = "";
        const string smsfipwd_default = "";
        const string freesms8uid_default = "";
        const string freesms8pwd_default = "";
        const string sms440uid_default = "";
        const string sms440pwd_default = "";
        const string bhokaliuid_default = "";
        const string bhokalipwd_default = "";
        const int smsservice_default = 1;
        bool[] statusdata_default = {false,false,false,false,false,false,false,false};
        const string signature_default = "";

        /// <summary>
        /// Constructor that gets the application settings.
        /// </summary>
        public AppSettings()
        {
            try
            {
                // Get the settings for this application.
                isolatedStore = IsolatedStorageSettings.ApplicationSettings;

            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception while using IsolatedStorageSettings: " + e.ToString());
            }
        }
        public bool adpromo
        {
            get
            {
                return GetValueOrDefault<bool>(promokeyname, adpromo_default);
            }
            set
            {
                AddOrUpdateValue(promokeyname, value);
                Save();
            }
        }

        /// <summary>
        /// Update a setting value for our application. If the setting does not
        /// exist, then add the setting.
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool AddOrUpdateValue(string Key, Object value)
        {
            bool valueChanged = false;

            // If the key exists
            if (isolatedStore.Contains(Key))
            {
                // If the value has changed
                if (isolatedStore[Key] != value)
                {
                    // Store the new value
                    isolatedStore[Key] = value;
                    valueChanged = true;
                }
            }
            // Otherwise create the key.
            else
            {
                isolatedStore.Add(Key, value);
                valueChanged = true;
            }

            return valueChanged;
        }


        /// <summary>
        /// Get the current value of the setting, or if it is not found, set the 
        /// setting to the default setting.
        /// </summary>
        /// <typeparam name="valueType"></typeparam>
        /// <param name="Key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public valueType GetValueOrDefault<valueType>(string Key, valueType defaultValue)
        {
            valueType value;

            // If the key exists, retrieve the value.
            if (isolatedStore.Contains(Key))
            {
                value = (valueType)isolatedStore[Key];
            }
            // Otherwise, use the default value.
            else
            {
                value = defaultValue;
            }

            return value;
        }


        /// <summary>
        /// Save the settings.
        /// </summary>
        public void Save()
        {
            isolatedStore.Save();
        }

        public bool[] status
        {
            get
            {
                return GetValueOrDefault<bool[]>(statusdata,statusdata_default);
            }
            set
            {
                AddOrUpdateValue(statusdata, value);
                Save();
            }
        }
        public int smsservice
        {
            get
            {
                return GetValueOrDefault<int>(sms,smsservice_default);
            }
            set
            {
                AddOrUpdateValue(sms, value);
                Save();
            }
        }
        public string way2smsuid
        {
            get
            {
                return GetValueOrDefault<string>(way2smsuidkeyname, way2smsuid_default).ToString();
            }
            set
            {
                AddOrUpdateValue(way2smsuidkeyname, value);
                Save();
            }
        }

        public string way2smspwd
        {
            get
            {
                return GetValueOrDefault<string>(way2smspwdkeyname, way2smspwd_default).ToString();
            }
            set
            {
                AddOrUpdateValue(way2smspwdkeyname, value);
                Save();
            }
        }

        public string uid160by2
        {
            get
            {
                return GetValueOrDefault<string>(onesixtyuidkeyname, onesixtyuid_default).ToString();
            }
            set
            {
                AddOrUpdateValue(onesixtyuidkeyname, value);
                Save();
            }

        }

        public string pwd160by2
        {
            get
            {
                return GetValueOrDefault<string>(onesixtypwdkeyname, onesixtypwd_default).ToString();
            }
            set
            {
                AddOrUpdateValue(onesixtypwdkeyname, value);
                Save();
            }

        }

        public string uidfullonsms
        {
            get
            {
                return GetValueOrDefault<string>(fullonsmsuidkeyname, fullonsmsuid_default).ToString();
            }
            set
            {
                AddOrUpdateValue(fullonsmsuidkeyname, value);
                Save();
            }

        }

        public string pwdfullonsms
        {
            get
            {
                return GetValueOrDefault<string>(fullonsmspwdkeyname, fullonsmspwd_default).ToString();
            }
            set
            {
                AddOrUpdateValue(fullonsmspwdkeyname, value);
                Save();
            }

        }

        public string uidsite2sms
        {
            get
            {
                return GetValueOrDefault<string>(site2smsuidkeyname, site2smsuid_default).ToString();
            }
            set
            {
                AddOrUpdateValue(site2smsuidkeyname, value);
                Save();
            }

        }

        public string pwdsite2sms
        {
            get
            {
                return GetValueOrDefault<string>(site2smspwdkeyname, site2smspwd_default).ToString();
            }
            set
            {
                AddOrUpdateValue(site2smspwdkeyname, value);
                Save();
            }

        }

        public string uidsmsfi
        {
            get
            {
                return GetValueOrDefault<string>(smsfiuidkeyname, smsfiuid_default).ToString();
            }
            set
            {
                AddOrUpdateValue(smsfiuidkeyname, value);
                Save();
            }

        }

        public string pwdsmsfi
        {
            get
            {
                return GetValueOrDefault<string>(smsfipwdkeyname, smsfipwd_default).ToString();
            }
            set
            {
                AddOrUpdateValue(smsfipwdkeyname, value);
                Save();
            }

        }

        public string uidfreesms8
        {
            get
            {
                return GetValueOrDefault<string>(freesms8uidkeyname, freesms8uid_default).ToString();
            }
            set
            {
                AddOrUpdateValue(freesms8uidkeyname, value);
                Save();
            }

        }

        public string pwdfreesms8
        {
            get
            {
                return GetValueOrDefault<string>(freesms8pwdkeyname,freesms8pwd_default ).ToString();
            }
            set
            {
                AddOrUpdateValue(freesms8pwdkeyname, value);
                Save();
            }

        }

        public string uidsms440
        {
            get
            {
                return GetValueOrDefault<string>(sms440uidkeyname, sms440uid_default).ToString();
            }
            set
            {
                AddOrUpdateValue(sms440uidkeyname, value);
                Save();
            }

        }

        public string pwdsms440
        {
            get
            {
                return GetValueOrDefault<string>(sms440pwdkeyname,sms440pwd_default).ToString();
            }
            set
            {
                AddOrUpdateValue(sms440pwdkeyname, value);
                Save();
            }

        }

        public string uidbhokali
        {
            get
            {
                return GetValueOrDefault<string>(bhokaliuidkeyname, bhokaliuid_default).ToString();
            }
            set
            {
                AddOrUpdateValue(bhokaliuidkeyname, value);
                Save();
            }

        }

        public string pwdbhokali
        {
            get
            {
                return GetValueOrDefault<string>(bhokalipwdkeyname, bhokalipwd_default).ToString();
            }
            set
            {
                AddOrUpdateValue(bhokalipwdkeyname, value);
                Save();
            }

        }


        public string signature {
            get
            {
                return GetValueOrDefault<string>(signature, signature_default).ToString();
            }
            set
            {
                AddOrUpdateValue(signature, value);
                Save();
            }
        }
    }
}
