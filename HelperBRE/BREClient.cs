using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

using System.Workflow.Activities.Rules;
using System.Workflow.Activities.Rules.Design;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;

using System.Windows.Forms;
using System.Xml;

namespace HelperBRE
{
    public class BREClient
    {
        private CloudStorageAccount storageAccount;
        private CloudBlobClient blobClient;
        private System.Collections.Hashtable RuleSetCache = new System.Collections.Hashtable();

        public Object Target { get; set; }
        public BREClient(string RuleStoreConn)
        {
            storageAccount = CloudStorageAccount.Parse(RuleStoreConn);
            blobClient = storageAccount.CreateCloudBlobClient();
        }
        private static bool BlobExist( CloudBlockBlob  blob)
        {
            try
            {
                blob.FetchAttributes();
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }
        /// <summary>
        /// Load or create RulseSet from XMl file RuleSetXmlFile stoarge in RuleSetContainer 
        /// </summary>
        /// <param name="RuleSetContainer">Container´s name</param>
        /// <param name="RuleSetXmlFile">Xml File´s name</param>
        /// <returns></returns>
        private RuleSet LoadRuleSet(string RuleSetContainer,string RuleSetXmlFile)
        {
            RuleSet ruleset;
            XmlTextReader reader;
            try
            {
                CloudBlobContainer container = blobClient.GetContainerReference(RuleSetContainer);
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(RuleSetXmlFile);
                
                if (BlobExist(blockBlob))
                {
                    //Load RuleSet From XML
                    using (var memoryStream = new MemoryStream())
                    {
                        blockBlob.DownloadToStream(memoryStream);
                        memoryStream.Position = 0;
                        reader = new XmlTextReader(memoryStream);
                        WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
                        object results = serializer.Deserialize(reader);
                        ruleset = (RuleSet)results;
                    }
                }
                else
                {
                    //Create New RuleSet
                    ruleset = new RuleSet();
                }
            }
            catch (Exception X)
            {
                //TODO: manage exception
                throw X;
            }
            return ruleset;
        }
        /// <summary>
        /// Save RuleSet to XML file in Blob Storage
        /// </summary>
        /// <param name="RuleSetContainer">Container´s Name</param>
        /// <param name="RuleSetXmlFile">XMl File´s name</param>
        /// <param name="ruleset">Rulset to persist</param>
        private void SaveRuleSet(string RuleSetContainer,string RuleSetXmlFile, RuleSet ruleset)
        {
            MemoryStream memoryStream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(memoryStream, null);
            WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
            serializer.Serialize(writer, ruleset);
            try
            {
                CloudBlobContainer container = blobClient.GetContainerReference(RuleSetContainer);
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(RuleSetXmlFile);
                memoryStream.Position = 0;
                blockBlob.UploadFromStream(memoryStream);
            }
            catch (Exception)
            {
                //TODO: manage exception
                throw;
            }
            memoryStream = null;
        }
        /// <summary>
        /// Execute Rule avaluation
        /// </summary>
        /// <param name="Target">Object used in rules</param>
        /// <param name="ruleset">RuleSet</param>
        private void RuleExecute(Object Target, RuleSet ruleset)
        {
            try
            {
                RuleValidation validation = new RuleValidation(Target.GetType(), null);
                RuleExecution engine = new RuleExecution(validation, Target);
                ruleset.Execute(engine);
            }
            catch (Exception X)
            {
                //System.Diagnostics.Trace.WriteLine(X.Message, "Error");
                throw X;
            }
        }
        /// <summary>
        /// Load or create Ruleset, Edit with Rule Set Editor and save the changes in XML file.
        /// </summary>
        /// <param name="TargetType">Type Class used in Rules</param>
        /// <param name="RuleSetContainer">Xml File Blob Container </param>
        /// <param name="RuleSetXmlFile">XML File´s name</param>
        public void EditRuleDialog(Type TargetType, string RuleSetContainer, string RuleSetXmlFile)
        {
            RuleSet ruleset = null;
            //load or creat if not exits
            ruleset = this.LoadRuleSet(RuleSetContainer, RuleSetXmlFile);
            RuleSetDialog dialog = new RuleSetDialog(TargetType, null, ruleset);
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                SaveRuleSet(RuleSetContainer, RuleSetXmlFile, dialog.RuleSet);
            }
        }
        /// <summary>
        /// Execute rule evaluation with or without rulset´s cache
        /// </summary>
        /// <param name="RuleSetContainer">Rulset´s blob container</param>
        /// <param name="RuleSetXmlFile">Rulset´s Xml file</param>
        /// <param name="Target">Object used in rules</param>
        /// <param name="UseRuleCache">Use Rulset Cache</param>
        public void RuleExecute(string RuleSetContainer, string RuleSetXmlFile, Object Target, bool UseRuleCache)
        {
            RuleSet ruleset = null;
            if (UseRuleCache)
            {
                string key = string.Format("{0}/{1}", RuleSetContainer, RuleSetXmlFile);
                if (!RuleSetCache.ContainsKey(key))
                {
                    ruleset = LoadRuleSet(RuleSetContainer, RuleSetXmlFile);
                    RuleSetCache.Add(key, ruleset);
                }
                ruleset = (RuleSet)RuleSetCache[key];
               this.RuleExecute(Target, ruleset);
            }
            else
            {
                ruleset = LoadRuleSet(RuleSetContainer, RuleSetXmlFile);
                this.RuleExecute(Target, ruleset);
            }
        }
       
    }
}
