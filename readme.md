
![Poster](/Assests/posters/GST%20API.png)

# Open Source SDK for GST Returns  

&emsp;&emsp; [GSP API](/GSTAPI) is a friendly class library which you can use to build your own GST software. It is based on .Net framework 4.6.1 just to help those millions of software developers to develop software on GST returns with better customization and better statical solutions. This is a software management tool used to file, save, submit as well as fetch GST details for any taxpayer.<br />
&emsp;&emsp; This c# class library has the capability of versatile software development and would make software development for GST returns simple and convinient. If used the sdk correctly can harness any big GST software and can be improvised since its open source. 

# Tables of Content

- [Services](#services)
- [Helpers](#helpers)
- [Models](#models)
- [Contribution](#contribution)
- [FAQ](#faq)


## Services 
&emsp;&emsp; The class library provides a number of services which includes authentication of taxpayers, authentication token on OTP from GST portal, public api services, and all the GST returns api as provided by gst portal. These services internally make their own web client and required encryption to send the api call to gst portal in order to fetch encrypted response which is decrypted and return to the user.<br />
&emsp;&emsp; The services incorporates authentication services, common or public services, document services, gstr1 services, gstr2a services, gstr3 services, gstr3b services, gstr4 services, gstr4a services, gstr7 services, gstr9c services, gstr9 services, cmp08 services, itc04 services and other services which includes methods like get details, save, submit as well as file details. For more information, go visit: https://developer.gst.gov.in/apiportal.

## Helpers
&emsp;&emsp; As the name suggests these class files help the service classes to achieve its goals of persuing processed GST api respone. These are done in three class files includes as follows:

#### 1. Request Handler
&emsp;&emsp; From creation of a web client to sending the request with the encrypted payload as well as the task of validating the request are the main roles of this class file. Firstly, It validates all the info given in the model then creating the web client request considering all the security protocols. The payload which is taken from user is then encrypted to finally send the request to GST portal.

#### 2. Cipher Handler
&emsp;&emsp; It works with encryption of request payload and decryption of response payload recieved from the GST poratl. This involves encryption with public GSTN Key, the app key provided by the user at the time of authentication and session key provided by the GST portal at the time of authentication.<br />
For encryption using GSTN Key, RSA algorithm is used for encryption as shown below:
```
RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)certificate.PublicKey.Key;
byte[] bytesEncrypted = rsa.Encrypt(bytesToBeEncrypted, false);
```
For HMAC decryption, SHA256 hash algorithm is used as shown below:
```
using (var hmacsha256 = new HMACSHA256(sessionKeyBytes))
{
    byte[] data = Encoding.UTF8.GetBytes(base64String);
    byte[] hashmessage = hmacsha256.ComputeHash(data);
    return Convert.ToBase64String(hashmessage);
}
```
And the rest of encryption and decryption is done by Advanced Encryption Standard (AES) symmetric algorithm. For more security related information, go visit: https://developer.gst.gov.in.

#### 3. Url handler
&emsp;&emsp; A simple class file which provides the url for the web client request in order to maintain the access name, version and mod name of the GST API urls. This class file is made independent from the rest of the helpers since, any change in url would not cause any kind of change in creating request. 

## Models
&emsp;&emsp; Every service method requires a model that forms the request header for every GST API calls that mainly comprises of  properties like _Username_, _StateCode_, _GSTIN_, _ReturnPeriod_, _GSTNAppKey_ etc. And in case of requests like save, submit or file, would require data for actions like 'RETSAVE', 'RETSUBMIT' or 'RETFILE' in json string format, namely _JsonData_. For more payload related information on specific return type, go visit: https://developer.gst.gov.in/apiportal/taxpayer/returns/apilist.

## Contribution
&emsp;&emsp; Among the immediate updates comprises of method description, building models for every save, submit, file and other request payload, etc. This sdk needes lot of conributions which would complete my vision of unified GST software and in turn change the lifes of many taxpayer as well as software developers trying to develop GST returns software.  

## FAQ

#### How to run the project?
As sdk user, a [Demo App](/DemoApp) is made to elastrate dll's services.<br />
As contributor, [GST API visual studio solution](../../blob/master/GSTAPI.sln) in your visual studio and Press 'F5'.

#### What security protocol is used for any request?
```
SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls
```
#### Any plans for production?
Needed to work on it a lot.

#### From where does it fetches data?
From GST Developers Portal's sandbox environment.

#### What's the minimum framework needed?
.Net Framework 4.6.1 & C# 6.0

#### How to test the project?
Email me for a demo GSTIN and Username.

