
![Poster](/Assests/posters/GST%20API.png)

## Open Source SDK for GST Returns  

&emsp;&emsp; GST DLL is a friendly class library which you can use to build your own GST software. It runs on .Net framework 4.6.1 and c# verion 6.0 just to help those millions of software developers to develop software on GST returns with better customization and better statical solutions. This is a software management tool used to file, save, submit as well as fetch GST details for any taxpayer.<br />
&emsp;&emsp; This c# class library has the capability of versatile software development and more over its open source which allows us to add more features as we discover it more. If used this dll as predicted can surely replace gaint head MNC's GST software and indeed with less resouce and low budget.

## Tables of Content

- [Services](#services)
- [Helpers](#helpers)
- [Models](#models)
- [Contribution](#contribution)
- [FAQ](#faq)


### Services 
&emsp;&emsp; The class library provides a number of services which includes authentication of taxpayers, authentication token on OTP from GST portal, public api services, and all the GST returns api as provided by gst portal. These services internally make their own web client and required encryption to send the api call to gst portal in order to fetch encrypted response which is decrypted and return to the user.<br />
&emsp;&emsp; The services incorporates authentication services, common or public services, document services, gstr1 services, gstr2a services, gstr3 services, gstr3b services, gstr4 services, gstr4a services, gstr7 services, gstr9c services, gstr9 services, cmp08 services, itc04 services and other services which includes methods like get details, save, submit as well as file details. For more information, go visit: https://developer.gst.gov.in/apiportal.

### Helpers
&emsp;&emsp; As the name suggests these class files help the service classes to achieve its goals of persuing processed GST api respone. These are done in three class files includes as follows:
#### 1. Request Handler
&emsp;&emsp; From creation of a web client to sending the request with the encrypted payload as well as the task of validating the request are the main roles of this class file. Firstly, It validates all the info given in the model (object) then creating the web client request with considering all the security protocols. The payload which is taken from user is now encrypted for the created request to finally send the request to GST portal.<br/>
#### 2. Cipher Handler
&emsp;&emsp; It works with encryption of the payload when a request is created and decryption of the response payload recieved from the GST poratl. This involves encryption with public GSTN Key, the app key provided by the user at the time of authentication and session key provided by the GST portal at the time of authentication. For moree security query, go visit: https://developer.gst.gov.in.
#### 3. Url handler
&emsp;&emsp; A simple class file which provides the url for the web client request in order to maintain the access name, version and mod name of the GST portal api urls. This class file is made independent from the rest of the helpers since, any change in url would not cause any kind of change in creating request. 

### Models
&emsp;&emsp; A object (model) that forms the request header for every api call (or service method) that is which comprises properties like __Username__, __StateCode__, __GSTIN__, __ReturnPeriod__, __GSTNAppKey__ etc. And request (or service methods) like save, submit or file, would require data for actions like 'RETSAVE', 'RETSUBMIT' or 'RETFILE' in json string format(_JsonData_). For more information on specific return type please visit: https://developer.gst.gov.in/apiportal

### Contribution



### FAQ

#### How to run the project?
- Made a DemoApp to elastrate DLL's services
- Open the DemoApp.sln in your VS & Press 'F5'

#### From where does it fetches data?
From GST Developers Portal's sandbox environment.

#### What's the minimum framework needed?
.Net Framework 4.6.1 & C# 6.0

#### How to test the project?
Email me for a demo GSTIN and Username.

