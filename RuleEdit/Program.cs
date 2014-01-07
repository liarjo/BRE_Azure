using System;
using System.IO;

using HelperBRE;
using HelperBRE.SampleSubject;

namespace RuleEdit
{
    class Program
    {
        static string conn = "DefaultEndpointsProtocol=https;AccountName=rulestore;AccountKey=MmiI2ZhCoNosjLE9+8Vr8yYB1ZsL5X3oK/PZAR23repDB5+aVDJ0WiI2HcNAvqNXyjoPkHv5oM1Uon4XK+MA+g==";
        static string RuleContainer = "demo";
        static string RuleXmlFile = "PersonCredit.xml";
        static void EDitar()
        {
            //conn is Azure Storage´s connection string 
            BREClient RuleClient = new BREClient(conn);
            //Target Class to use in Rules
            Person mySubjectEval = new Person();
            string RuleContainer = "demo";
            string RuleXmlFile = "PersonCredit.xml";
            RuleClient.EditRuleDialog(typeof(Person), RuleContainer, RuleXmlFile);
        }
        static void Test()
        {
            Person subjectX= new Person();
            BREClient RuleClient = new BREClient(conn);
            DateTime timezero = DateTime.Now;
            for (int i = 0; i < 10; i++)
            {
                subjectX.Age = i;
                RuleClient.RuleExecute(RuleContainer, RuleXmlFile, subjectX,false);
                Console.WriteLine(string.Format("Age= {0} rule response {1}", i, subjectX.RuleEval));
            }
            DateTime timeOne = DateTime.Now;
            Console.WriteLine("Total time: " + (timeOne - timezero).TotalSeconds.ToString());
            Console.WriteLine("");
        }
        static void TestCache()
        {
            Person subjectX = new Person();
            BREClient RuleClient = new BREClient(conn);
            DateTime timezero = DateTime.Now;
            for (int i = 0; i < 10; i++)
            {
                subjectX.Age = i;
              RuleClient.RuleExecute(RuleContainer, RuleXmlFile, subjectX,true);
              Console.WriteLine(string.Format("Age= {0} rule response {1}", i, subjectX.RuleEval));
            }
            DateTime timeOne = DateTime.Now;
            Console.WriteLine("Total time: " + (timeOne - timezero).TotalSeconds.ToString());
            Console.WriteLine("");
        }
       
        static void TestWF4WFC()
        {
            Person subjectX = new Person();
            srvEvalPersona.ServiceClient proxy = new srvEvalPersona.ServiceClient();
            DateTime timezero = DateTime.Now;
            for (int i = 0; i < 10; i++)
            {
                subjectX.Age = i;
                subjectX.Salary = 100;
                proxy.EvalPersona(ref subjectX);
                Console.WriteLine(string.Format("Age= {0} rule response {1} and Salary={2}", i,subjectX.RuleEval,subjectX.Salary));
            }
            DateTime timeOne = DateTime.Now;
            Console.WriteLine("Total time: " + (timeOne - timezero).TotalSeconds.ToString());
            Console.WriteLine("");
        }
        static void TestWF4WFC_Bacth()
        {
            Person[] subjectXs = new Person[10];
            for (int i = 0; i < 10; i++)
            {
                subjectXs[i] = new Person();
                subjectXs[i].Age = i;
                subjectXs[i].Salary = 100;
            }
            srvEvalPersonaBatch.ServiceClient proxy = new srvEvalPersonaBatch.ServiceClient();
            DateTime timezero = DateTime.Now;
            //execute
            proxy.EvalPersonaBatch(ref subjectXs);
            foreach (var subject in subjectXs)
            {
                Console.WriteLine(string.Format("Age= {0} rule response {1} and Salary={2}", subject.Age, subject.RuleEval, subject.Salary));
            }
            DateTime timeOne = DateTime.Now;
            Console.WriteLine("Total time: " + (timeOne - timezero).TotalSeconds.ToString());
            Console.WriteLine("");
        }
        static void Main()
        {
            bool sw = true;
            do
            {
                Console.WriteLine("1. Editar Regla");
                Console.WriteLine("2. Probar Regla");
                Console.WriteLine("3. Probar Regla con Cache");
                Console.WriteLine("4. Probar Worflow como servicio llamadasIndividuales");
                Console.WriteLine("5. Probar Worflow como servicio llamadas Batch");
                Console.WriteLine("q. Salir");
                string menu = Console.ReadLine();
                switch (menu)
                {
                    case "1":
                        EDitar();
                        break;
                    case "2":
                        Test();
                        break;
                    case "3":
                        TestCache();
                        break;
                    case "4":
                        TestWF4WFC();
                        break;
                    case "5":
                        TestWF4WFC_Bacth();
                        break;
                    case "q":
                        sw = false;
                        break;
                    default:
                        break;
                }
            } while (sw);
        }
    }
}
