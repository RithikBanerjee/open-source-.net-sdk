using GSTAPI.Models;
using GSTAPI.Services;
using System;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var demo = new DemoClass();
            //first step get authentication token
            if(!demo.AuthenticateRequest(out string message))
                Console.WriteLine($"\n{message}. Try Again");
            else
                demo.MainMenu();
            Console.ReadLine();
        }
    }
    public class DemoClass
    {
        private Request MyRequest;
        private bool PrepareRequest()
        {
            MyRequest = new Request()
            {
                Header = new Header(),
                Keys = new CipherKeys()
            };
            Console.WriteLine("\nUsername:");
            MyRequest.Header.Username = Console.ReadLine();
            Console.WriteLine("\nStateCode:");
            MyRequest.Header.StateCode = Console.ReadLine();
            Console.WriteLine("\nReturnPeriod:");
            MyRequest.Header.ReturnPeriod = Console.ReadLine();
            Console.WriteLine("\nGSTN:");
            MyRequest.Header.GSTN = Console.ReadLine();
            Console.WriteLine("\nGenerate App Key?(0/1)");
            if (!int.TryParse(Console.ReadLine(), out var generateKey))
                return false;
            if(Equals(generateKey, 0))
            {
                Console.WriteLine("\nGSTN App Key(32 Charecters):");
                MyRequest.Keys.GSTNAppKey = Console.ReadLine();
                if (Equals(MyRequest.Keys.GSTNAppKey.Length, 32))
                    return true;
            }
            MyRequest.Keys.GSTNAppKey = "124HJs0927Yhst682JSB654FSYIJB733";
            return true;
        }
        public bool AuthenticateRequest(out string message)
        {
            message = "Invalid input";
            //preparing request header
            if (!PrepareRequest())
                return false;

            Console.WriteLine("\nGenerate Auth Token?(0/1)");
            if(!int.TryParse(Console.ReadLine(), out var generateAuth))
                return false;

            if (Equals(generateAuth, 0))
            {
                Console.WriteLine("\nAuth Token:");
                MyRequest.Header.AuthToken = Console.ReadLine();
                Console.WriteLine("\nSession Key:");
                MyRequest.Keys.SessionKey = Console.ReadLine();
                return true;
            }

            Console.WriteLine("\nGenerate OTP?(0/1)");
            if (!int.TryParse(Console.ReadLine(), out var generateOtp))
                return false;
            if(Equals(generateOtp, 0))
            {
                var response = AuthenticateService.RequestForOtp(MyRequest);
                message = response.OutcomeTransaction;
                if (!response.Status)
                    return false;
            }

            Console.WriteLine("\nOTP:");
            var otp = Console.ReadLine();
            var authResponse = AuthenticateService.RequestForAuthToken(MyRequest, otp);
            message = authResponse.OutcomeTransaction;
            if (!authResponse.Status)
                return false;

            MyRequest.Header.AuthToken = authResponse.AuthToken;
            MyRequest.Keys.SessionKey = authResponse.SessionKey;
            return true;
        }
        //second step get, save, submit or file gstr data
        public void MainMenu()
        {
            int option = 100;
            while (Equals(option, 0))
            {
                Console.WriteLine("\nChoose Return Type:\n1. GSTR-1\n2. GSTR-2A\n3. GSTR-3B\n4. GSTR-4A\n5. GSTR-4\n6. GSTR-7\n7. GSTR-9C\n8. GSTR-9\n9. ITC-04\n10. CMP-08\n11. Ledger\n0. Exit");
                if (!int.TryParse(Console.ReadLine(), out option))
                    Console.WriteLine("\nInvalid input.");
                switch (option)
                {
                    //only implemented gstr1, similarly implement other returns
                    case 1: GSTR1(); break;
                }
            }
        }
        private void GSTR1()
        {
            Console.WriteLine("\nChoose Action Type:\n1. Get\n2. Save\n3. Submit\n4. File");
            if (!int.TryParse(Console.ReadLine(), out var action))
            {
                Console.WriteLine("\nInvalid input.");
                return;
            }
            var response = "Invalid input";
            switch(action)
            {
                case 1:
                    response = GetRequest();
                    break;
                case 2:
                    response = SaveRequest();
                    break;
                case 3:
                    response = SubmitRequest();
                    break;
                case 4:
                    response = FileRequest();
                    break;
            }
            Console.WriteLine($"\nResponse:\n{response}");
        }
        private string SaveRequest()
        {
            Console.WriteLine("\nJson To Save GSTR1:");
            var response = GSTR1Service.Save(MyRequest, Console.ReadLine());
            if (!response.Status)
                return response.OutcomeTransaction;
            return response.Data;
        }
        private string SubmitRequest()
        {
            Console.WriteLine("\nJson To Submit GSTR1:");
            var response = GSTR1Service.Submit(MyRequest, Console.ReadLine());
            if (!response.Status)
                return response.OutcomeTransaction;
            return response.Data;
        }
        private string FileRequest()
        {
            Console.WriteLine("\nFile using DSC or EVC?(0/1)");
            if (!int.TryParse(Console.ReadLine(), out var file))
                Console.WriteLine("\nInvalid input.");

            Console.WriteLine("\nJson To File GSTR1:");
            var json = Console.ReadLine();
            Console.WriteLine("\nPAN Number:");
            var pan = Console.ReadLine();
            Response response = null;
            switch (file)
            {
                case 0:
                    Console.WriteLine("\nDSC Signature:");
                    var sign = Console.ReadLine();
                    response = GSTR1Service.FileWithDSC(MyRequest, json, sign, pan);
                    break;
                case 1:
                    Console.WriteLine("\nOTP For EVC:");
                    var otp = Console.ReadLine();
                    response = GSTR1Service.FileWithEVC(MyRequest, json, pan ,otp);
                    break;
            };
            if (response == null)
                return "Invalid input";
            if (!response.Status)
                return response.OutcomeTransaction;
            return response.Data;
        }
        private string GetRequest()
        {
            Console.WriteLine("\nChoose Action Type:\n1. B2B\n2. B2CS\n3. B2CL\n4. CDNR\n5. CDNUR\n6. EXP\n7. TXP\n8. HSNSummary\n9. Summary");
            if (!int.TryParse(Console.ReadLine(), out var section))
                return "Invalid input";

            Console.WriteLine("\nFinancial Year(Blank if same):");
            var fp = Console.ReadLine();
            if(string.IsNullOrEmpty(fp))
                fp = MyRequest.Header.ReturnPeriod;

            Console.WriteLine("\nGSTIN(Blank if same):");
            var gstin = Console.ReadLine();
            if (string.IsNullOrEmpty(gstin))
                gstin = MyRequest.Header.GSTN;

            Response response = null;
            switch (section)
            {
                case 1:
                    response = GSTR1Service.GetB2BInvoices(MyRequest, fp, gstin);
                    break;
                case 2:
                    response = GSTR1Service.GetB2CSInvoices(MyRequest, fp, gstin);
                    break;
                case 3:
                    Console.WriteLine("\nState Code(Blank if same):");
                    var stateCode = Console.ReadLine();
                    if (string.IsNullOrEmpty(stateCode))
                        stateCode = MyRequest.Header.StateCode;

                    response = GSTR1Service.GetB2CLInvoices(MyRequest, fp, gstin, stateCode);
                    break;
                case 4:
                    response = GSTR1Service.GetCDNRInvoices(MyRequest, fp, gstin);
                    break;
                case 5:
                    response = GSTR1Service.GetCDNURInvoices(MyRequest, fp, gstin);
                    break;
                case 6:
                    response = GSTR1Service.GetEXP(MyRequest, fp, gstin);
                    break;
                case 7:
                    response = GSTR1Service.GetTXP(MyRequest, fp, gstin);
                    break;
                case 8:
                    response = GSTR1Service.GetHSNSummary(MyRequest, fp, gstin);
                    break;
                case 9:
                    response = GSTR1Service.GetSummary(MyRequest, fp, gstin);
                    break;
            }
            if (response == null)
                return "Invalid input.";
            if (!response.Status)
                return response.OutcomeTransaction;
            return response.Data;
        }
        string demoJson = "{\"gstin\":\"33GSPTN1331G1ZJ\",\"fp\":\"122018\",\"gt\":3782969.01,\"cur_gt\":3782969.01,\"b2b\":[{\"ctin\":\"01AABCE2207R1Z5\",\"inv\":[{\"inum\":\"S008400\",\"idt\":\"24-11-2016\",\"val\":729248.16,\"pos\":\"06\",\"rchrg\":\"N\",\"etin\":\"01AABCE5507R1C4\",\"inv_typ\":\"R\",\"diff_percent\":0.65,\"itms\":[{\"num\":1,\"itm_det\":{\"rt\":5,\"txval\":10000,\"iamt\":325,\"csamt\":500}}]}]}],\"b2ba\":[{\"ctin\":\"01AABCE2207R1Z5\",\"inv\":[{\"oinum\":\"S008400\",\"oidt\":\"24-11-2016\",\"inum\":\"S008400\",\"idt\":\"24-11-2016\",\"val\":729248.16,\"pos\":\"06\",\"rchrg\":\"N\",\"etin\":\"01AABCE5507R1C4\",\"inv_typ\":\"R\",\"diff_percent\":0.65,\"itms\":[{\"num\":1,\"itm_det\":{\"rt\":5,\"txval\":10000,\"iamt\":325,\"camt\":0,\"samt\":0,\"csamt\":500}}]}]}],\"b2cl\":[{\"pos\":\"05\",\"inv\":[{\"inum\":\"92661\",\"idt\":\"10-01-2016\",\"val\":784586.33,\"inv_typ\":\"CBW\",\"etin\":\"27AHQPA8875L1CU\",\"diff_percent\":0.65,\"itms\":[{\"num\":1,\"itm_det\":{\"rt\":5,\"txval\":10000,\"iamt\":325,\"csamt\":500}}]}]}],\"b2cla\":[{\"pos\":\"06\",\"inv\":[{\"oinum\":\"9266\",\"oidt\":\"10-02-2016\",\"inv_typ\":\"CBW\",\"diff_percent\":0.65,\"inum\":\"92661\",\"idt\":\"10-01-2016\",\"val\":784586.33,\"etin\":\"01AABCE5507R1C4\",\"itms\":[{\"num\":1,\"itm_det\":{\"rt\":5,\"txval\":10000,\"iamt\":833.33}}]}]}],\"cdnr\":[{\"ctin\":\"01AAAAP1208Q1ZS\",\"nt\":[{\"ntty\":\"C\",\"nt_num\":\"533515\",\"nt_dt\":\"23-09-2016\",\"p_gst\":\"N\",\"inum\":\"915914\",\"idt\":\"23-09-2016\",\"val\":123123,\"diff_percent\":0.65,\"itms\":[{\"num\":1,\"itm_det\":{\"rt\":10,\"txval\":5225.28,\"iamt\":339.64,\"csamt\":789.52}}]}]}],\"cdnra\":[{\"ctin\":\"01AAAAP1208Q1ZS\",\"nt\":[{\"ntty\":\"C\",\"ont_num\":\"533515\",\"ont_dt\":\"23-09-2016\",\"nt_num\":\"533515\",\"nt_dt\":\"23-09-2016\",\"p_gst\":\"N\",\"inum\":\"915914\",\"diff_percent\":0.65,\"idt\":\"23-09-2016\",\"val\":123123,\"itms\":[{\"num\":1,\"itm_det\":{\"rt\":10,\"txval\":5225.28,\"iamt\":845.22,\"camt\":0,\"samt\":0,\"csamt\":789.52}}]}]}],\"b2cs\":[{\"sply_ty\":\"INTER\",\"diff_percent\":0.65,\"rt\":5,\"typ\":\"E\",\"etin\":\"01AABCE5507R1C4\",\"pos\":\"05\",\"txval\":110,\"iamt\":10,\"csamt\":10},{\"rt\":5,\"sply_ty\":\"INTER\",\"diff_percent\":0.65,\"typ\":\"OE\",\"txval\":100,\"iamt\":10,\"csamt\":10,\"pos\":\"05\"}],\"b2csa\":[{\"omon\":\"122016\",\"sply_ty\":\"INTER\",\"diff_percent\":0.65,\"typ\":\"E\",\"etin\":\"01AABCE5507R1C4\",\"pos\":\"05\",\"itms\":[{\"rt\":5,\"txval\":110,\"iamt\":10,\"camt\":0,\"samt\":0,\"csamt\":10},{\"rt\":12,\"txval\":110,\"iamt\":10,\"camt\":0,\"samt\":0,\"csamt\":10}]}],\"exp\":[{\"exp_typ\":\"WPAY\",\"inv\":[{\"inum\":\"81542\",\"idt\":\"12-02-2016\",\"val\":995048.36,\"diff_percent\":0.65,\"sbpcode\":\"ASB991\",\"sbnum\":\"7896542\",\"sbdt\":\"04-10-2016\",\"itms\":[{\"txval\":10000,\"rt\":5,\"iamt\":833.33,\"csamt\":100}]}]},{\"exp_typ\":\"WOPAY\",\"inv\":[{\"inum\":\"81542\",\"idt\":\"12-02-2016\",\"val\":995048.36,\"sbpcode\":\"ASB981\",\"sbnum\":\"7896542\",\"sbdt\":\"04-10-2016\",\"diff_percent\":0.65,\"itms\":[{\"txval\":10000,\"rt\":0,\"iamt\":0,\"csamt\":100}]}]}],\"expa\":[{\"exp_typ\":\"WPAY\",\"inv\":[{\"oinum\":\"81542\",\"oidt\":\"12-02-2016\",\"inum\":\"81542\",\"idt\":\"12-02-2016\",\"diff_percent\":0.65,\"val\":995048.36,\"sbpcode\":\"ASB995\",\"sbnum\":\"1234567\",\"sbdt\":\"04-10-2016\",\"itms\":[{\"txval\":10000,\"rt\":5,\"iamt\":833.33,\"csamt\":100}]}]}],\"hsn\":{\"data\":[{\"num\":1,\"hsn_sc\":\"1009\",\"desc\":\"GoodsDescription\",\"uqc\":\"kg\",\"qty\":2.05,\"val\":995048.36,\"txval\":10.23,\"iamt\":14.52,\"csamt\":500}]},\"nil\":{\"inv\":[{\"sply_ty\":\"INTRB2B\",\"expt_amt\":123.45,\"nil_amt\":1470.85,\"ngsup_amt\":1258.5},{\"sply_ty\":\"INTRB2C\",\"expt_amt\":123.45,\"nil_amt\":1470.85,\"ngsup_amt\":1258.5}]},\"txpd\":[{\"pos\":\"05\",\"sply_ty\":\"INTER\",\"diff_percent\":0.65,\"itms\":[{\"rt\":5,\"ad_amt\":100,\"iamt\":9400,\"csamt\":500}]}],\"txpda\":[{\"omon\":\"122016\",\"pos\":\"05\",\"sply_ty\":\"INTER\",\"diff_percent\":0.65,\"itms\":[{\"rt\":5,\"ad_amt\":100,\"iamt\":9400,\"csamt\":500}]}],\"at\":[{\"pos\":\"05\",\"sply_ty\":\"INTER\",\"diff_percent\":0.65,\"itms\":[{\"rt\":5,\"ad_amt\":100,\"iamt\":9400,\"csamt\":500}]}],\"ata\":[{\"omon\":\"022017\",\"pos\":\"05\",\"sply_ty\":\"INTER\",\"diff_percent\":0.65,\"itms\":[{\"rt\":5,\"ad_amt\":100,\"iamt\":9400,\"csamt\":500}]}],\"doc_issue\":{\"doc_det\":[{\"doc_num\":1,\"docs\":[{\"num\":1,\"from\":\"20\",\"to\":\"29\",\"totnum\":20,\"cancel\":3,\"net_issue\":17}]}]},\"cdnur\":[{\"typ\":\"B2CL\",\"ntty\":\"C\",\"nt_num\":\"533515\",\"nt_dt\":\"23-09-2016\",\"p_gst\":\"N\",\"inum\":\"915914\",\"idt\":\"23-09-2016\",\"val\":64646,\"diff_percent\":0.65,\"itms\":[{\"num\":1,\"itm_det\":{\"rt\":10,\"txval\":5225.28,\"iamt\":339.64,\"csamt\":789.52}}]}],\"cdnura\":[{\"ont_num\":\"533515\",\"ont_dt\":\"23-09-2016\",\"nt_num\":\"533515\",\"nt_dt\":\"23-09-2016\",\"ntty\":\"C\",\"typ\":\"B2CL\",\"p_gst\":\"N\",\"inum\":\"915914\",\"val\":123123,\"idt\":\"23-09-2016\",\"diff_percent\":0.65,\"itms\":[{\"num\":1,\"itm_det\":{\"rt\":10,\"txval\":5225.28,\"iamt\":845.22,\"csamt\":789.52}}]}]}";
    }
}
