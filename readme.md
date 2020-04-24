### Class Library which saves, submits, files as well as fetches GST details

#### What is the project about?
- Provides GSTR services like GSTR-1, GSTR-2A, GSTR-3B, etc.
- Uses web api to get its response from GST Developers Portal
- Hnadles all the encrption and decryption at its end

#### How it works?
- Takes raw data and encrypts it with the session key and app key given at the time of auth token
- Prepares web client request and gets the response
- Encrypted response id decrypted by the hmac and session key and app key given at the time of auth token
- Returns a model of the response

#### What do I needed to process a request?
- A object(_Request_) that has the following properties:
  - __UserName__ 
  - __StaeCode__
  - __GSTIN__ 
  - __ReturnPeriod__ 
  - __GSTNAppKey__
- To save, submit or file, it needs a json string(_JsonData_) which is to be saved, submited or filed
- For more information on specific return type please visit: https://developer.gst.gov.in/apiportal

#### How to run the project?
- Made a DemoApp to elastrate DLL's services
- Open the DemoApp.sln in your VS & Press 'F5'

#### From where does it fetches data?
From GST Developers Portal's sandbox environment

#### What's the minimum framework needed?
.Net Framework 4.6.1 & C# 6.0

#### How to test the project?
Email me for a demo GSTIN and Username

