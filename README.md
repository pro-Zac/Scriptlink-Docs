# ScriptLink Documentation

ScriptLink allows additional functionality, external data processing and validation within MyAvatar EHR through the use of SOAP web services

## Resources

Here are some resources to help you get started with creating and maintaining your first ScriptLink. Make sure you have access to Netsmart Cares and Sharepoint.

- [Scott Olsons Blog - Creating Your First myAvatar ScriptLink API Using C#](https://rarelysimpleblog.wordpress.com/2020/02/04/creating-your-first-myavatar-scriptlink-api-using-c/)

- [RarelySimple.AvatarScriptLink](https://github.com/rarelysimple/RarelySimple.AvatarScriptLink?tab=MIT-1-ov-file#readme) Scott Olson's git repo for ScriptLink resources.

- [Sharepoint - Creating Your First myAvatar ScriptLink API Using C#]

- Get a tut on how to make a scriptlink in there. Can't really open up our sharepoint to the world. These two ^ and v ain't staying.

- [Sharepoint - ScriptLink & Web Services Video Introduction]

- [NTST Wiki - ScriptLink Settings](https://wikihelp.ntst.com/myAvatar/Avatar_RADplus/RADplus_User_Guide/0B0Modeling/0N0Form_Designer/NI0Form_Layout_Screen/ScriptLink_Settings_screen)


- dead link v
- [NTST Developers Resource Group - MyAvatar Development Resource Group - ScriptLinks, Widgets, Web Services](https://netsmartcares.force.com/s/group/0F970000000XeyJCAS/developers-resource-group)

- [NLog Wiki - This package is helpful in implementing logs within your ScriptLink](https://github.com/nlog/nlog/wiki)

## Tools needed for ScriptLink development

Several programs are needed to create and deploy Scriptlink APIs in your environment. These programs are free of charge or Open Source, and should not require admin/financial overhead for approval, but of course, your org's software policy may vary.

- [Microsoft Visual Stuido](https://visualstudio.microsoft.com/) Runs on Windows only. Popular IDE for C# and .NET development. Recommended as it will automatically scaffold your web service upon build/publish.
    - ScriptLink is technically language agnostic, and can be programmed in pretty much any language you want, though official documentation and most tutorials are in C# via MS Visual Studio (not VS Code) for brevity's sake.
    - You can use any IDE or text editor to develop.
    - Most devs will recommend you use VS and C# for Scriptlink, as this is the officially supported flavor.

- [Microsoft Internet Information Services (IIS)](https://www.iis.net/) Comes with Windows Server natively. This is generally where you will deploy your ScriptLink APIs.

- [Microsoft Remote Desktop](https://support.microsoft.com/en-us/windows/how-to-use-remote-desktop-5fe128d5-8fb1-7a23-3b8a-41e636865e8c) Native RDP (remote desktop protocol) application to locally access a remote server. Your ScriptLink APIs will be deployed on a remote server, and this is how you will access it.



## Netsmart Hosted ScriptLink Server Development and Deployment

Our ScriptLink Server is hosted by Netsmart. The following will show you how to access this server. This server hosts our UAT and LIVE environments. Make sure you netsmart has given you credentials and access to RDP into this server.

- Our ScriptLinks are hosted using IIS
    - IIS is inlcluded in Windows Server, it shoudld come preinstalled on your system
    - IIS has 2 sites for applications. 1. UAT and 2. PROD
    - UAT hosts all of our UAT ScriptLink applications. PROD hosts all of our LIVE ScriptLink applications
    - UAT=UAT PROD=LIVE

- Your Scriptlink server will be either self hosted on prem, or hosted by Netsmart. Access is through RDP into your server, which will typically be accessible only through an on prem connection or VPN.

- Your IP address will be provided by Netsmart for hosted servers. On prem servers will be your local 198.xx.xx.xx address that you set up for the remote desktop.

- Directories for UAT and PROD ScriptLinks are defined in your IIS (or other web server app, like Apache or whatever). For example, ours on our server are under `C:/inetpub/UAT/ICWebservices` and `C:/inetpub/PROD/ICWebservices` respectively.

- The UAT and PROD ScriptLinks are held within the `C:/inetpub/UAT/ICWebservices` and `C:/inetpub/PROD/ICWebservices` directories respectively.

- Deploying ScriptLinks
    1. Confirm ScriptLink is developed and built within respective directory `C:/inetpub/UAT/ICWebservices` or `C:/inetpub/PROD/ICWebservices` (Checkout Resources on how to build a ScriptLink)
    2. Select site in IIS for whichever environment you are working with. UAT or PROD
    3. Select application under ICWebservices directory drop down 
    4. Right Click on your .Soap project and select "Convert to Application" then press "OK"
    5. Your ScriptLink has now been deployed! Your wsdl endpoints to be imported into MyAvatar's form designer are the following: 
        * UAT: https://xx.xxx.xx.01/ICWebservices/name_of_project/name_of_soap_project.Soap/api/v3/name_of_scriptlink_controller.asmx?WSDL
        * PROD: https://xx.xxx.xx.02/ICWebservices/name_of_project/name_of_soap_project.Soap/api/v3/name_of_scriptlink_controller.asmx?WSDL
        * Note xx.xxx.xx.02 is our UAT IP Address and xx.xxx.xx.01 is our LIVE/PROD IP Address.

## ScriptLink OptionObject return message types

These message numbers allow you to customize the type of response that staff see within MyAvatar

- 1 - returns an error message and stops further processing of scripts
- 2 - returns a message with ok/cancel buttons
- 3 - returns a message with an ok button
- 4 - returns a message with yes/no buttons
- 5 - returns a url to be opened in a new browser window
- 6 - returns a list of formids to launch avatar forms
- 0 - process optionobject and show no message

## Setting fields

You may see in quite a few repositories with fields being set to required/un-required/disabled/enabled in the following matter:

```
OptionObject2015 returnObject = CopyObject(inputObject);
RowObject row = inputObject.Forms[0].CurrentRow;

FieldObject fieldObj1 = new FieldObject();
RowObject rowObj1 = new RowObject();
FormObject formObj1 = new FormObject();

FieldObject fieldObj2 = new FieldObject();
RowObject rowOb2 = new RowObject();
FormObject formObj2 = new FormObject();

foreach (FieldObject field in row.Fields)
{
    switch (field.FieldNumber)
    {
        case "fieldnumber1":
            fieldObj1 = field;
            rowObj1.RowId = row.RowId;
            formObj1.FormId = inputObject.Forms[0].FormId;
            formObj1.MultipleIteration = inputObject.Forms[0].MultipleIteration;
            break;
        case "fieldnumber2":
            fieldObj1 = field;
            rowObj1.RowId = row.RowId;
            formObj1.FormId = inputObject.Forms[0].FormId;
            formObj1.MultipleIteration = inputObject.Forms[0].MultipleIteration;
            break;
    }
}

fieldObj1.Enabled = "1";
fieldObj1.Required = "0";
fieldObj1.FieldValue = fieldObj1.FieldValue;
rowObj1.Fields = new List<FieldObject>() { fieldObj1 };
rowObj1.RowAction = "EDIT";
formObj1.CurrentRow = rowObj1;
formObj1.CurrentRow.ParentRowId = "0";

fieldObj2.Enabled = "1";
fieldObj2.Required = "0";
fieldObj2.FieldValue = fieldObj2.FieldValue;
rowObj2.Fields = new List<FieldObject>() { fieldObj2 };
rowObj2.RowAction = "EDIT";
formObj2.CurrentRow = rowObj2;
formObj2.CurrentRow.ParentRowId = "0";

returnObject.Forms = new List<FormObject>() { formObj1, formObj2 };
return returnObject;
```

Although the previous example is perfectly acceptable in setting the values and states of fields within myAvatar, and may be more beneficial in certain instances; Please use the following method if you are requiring, unrequiring, disabling, or enabling in a bulk fashion ( this reduces the code tremendously ):

```   
OptionObject2015 returnObject = CopyObject(inputObject);
RowObject row = inputObject.Forms[0].CurrentRow;

IDictionary<string, FormObject> formObjects = new Dictionary<string, FormObject>();
List<string> fieldNums = new List<string>() { "add your list of field numbers here" };

foreach (FieldObject field in row.Fields)
{
    FieldObject fieldObj = new FieldObject();
    RowObject rowObj = new RowObject();
    FormObject formObj = new FormObject();

    //fields set in the fieldNums list won't be accounted for within the setting
    if(!fieldNums.Contains(field.FieldNumber))
    {
        // all of these fields will be included in the formObjects
        fieldObj = field;
        rowObj.RowId = row.RowId;
        formObj.FormId = inputObject.Forms[0].FormId;
        formObj.MultipleIteration = inputObject.Forms[0].MultipleIteration;

        // set enabled and/or required to your desired values
        fieldObj.Enabled = "0";
        fieldObj.Required = "0";
        fieldObj.FieldValue = fieldObj.FieldValue;
        rowObj.Fields = new List<FieldObject>() { fieldObj };
        rowObj.RowAction = "EDIT";
        formObj.CurrentRow = rowObj;
        formObj.CurrentRow.ParentRowId = "0";

        formObjects.Add(field.FieldNumber, formObj);
    }
}

List<FormObject> forms = formObjects.Values.ToList();
returnObject.Forms = forms;

return returnObject;
```

- feel free to refactor the current repositories with the second method above

## SQL Connections
- Please see [ScriptLink Database Connections] on how to create database connections from ScriptLink to MyAvatar Database

## Web Services
- Please see [ScriptLink Web Service Connections] on how to consume MyAvatar web services through ScriptLink

## Other Resources and helpful information
- [Microsoft - C# tuts](https://dotnet.microsoft.com/learn/csharp)

## Credits
- h/t to avenson, you beautiful bastard.
