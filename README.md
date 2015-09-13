[logo]: https://s3.amazonaws.com/emailhippo/bizbranding/co.logos/eh-horiz-695x161.png "Email Hippo"
[Email Hippo]: https://www.emailhippo.com
[SLAB]: https://msdn.microsoft.com/en-us/library/dn440729(v=pandp.60).aspx
[Docs]: http://api-docs.emailhippo.com

![alt text][logo]

# Email Verification Client

## About
This is a .NET package built for easy integration with [Email Hippo] RESTful API services. For
further information on the RESTful server side implementation, please see the [Docs].

## How to Get The Package
From [Nuget](http://nuget.org).
```
install-package EmailHippo.EmailVerify.Api.Client
```

## Who is the package for?
 * __.NET__ developers and system integrators needing a fast start to using [Email Hippo] technology.

## What this package can do
If you're working in the .NET environment, this package can save you __hours of work__ writing your own JSON parsers, message pumping logic, threading and logging code.

## Prerequisites
 * __Visual Studio__ 2012 or later
 * __.NET 4.5__ or later
 * __API license key__ from [Email Hippo]

## Features
 * Built for __high performance__ throughput
 * __Sync__ and __async__ methods
 * __Parallel__ batch processing available
 * __Progress reporting__ built in
 * __Extensive Logging__ built in using async [SLAB]
  
## How to use the package
Please note that full code for all of the snippets below are available in the "EmailHippo.EmailVerify.Api.Client.Tests" 
project which can be found in the GitHub repository for this project.

### Step1 - Create and Configure
The main client object is created using a static factory as follows:

__Example 1 - Creating The Client__
```c#
/*Visit https://www.emailhippo.com to get a license key*/
const string LicenseKey = "{YourLicenseKey}"; 
var myClient = ApiClientFactoryV2.Create(LicenseKey);
```

### Step 2 - Use
Once you have a reference to the client object, go ahead and use it.

__Example 2__ - Checking One or More Email Address Synchronously
```c#
var responses = myClient.Process(new VerificationRequest{Emails = new List<string>{"me@here.com"});

/*Process responses*/
/*..responses*/
```

__Example 3__ - Checking More Than Email Address Asynchronously
```c#
var responses = myClient.ProcessAsync(new VerificationRequest{Emails = new List<string>{"me@here.com"}, CancellationToken.None).Result;

/*Process responses*/
/*..responses*/
```

__Example 4__ - Progress Reporting
Progress can be captured using the built in event delegate "ProgressChanged" as follows
```c#
myClient.ProgressChanged += (o, args) => Console.WriteLine(JsonConvert.SerializeObject(args));
```

__Example 5__ - Logging
High performance, Azure compatible exception and application logging is provided using [SLAB].

Enable logging using standard [SLAB] listeners.
```c#
var ObservableEventListener listener1;
var ObservableEventListener listener2;

listener1 = new ObservableEventListener();
listener1.EnableEvents(ExceptionLoggingEventSource.Log, EventLevel.Error);

listener1.LogToConsole();

listener2 = new ObservableEventListener();
listener2.EnableEvents(ActivityLoggingEventSource.Log, EventLevel.Error, Keywords.All);

listener2.LogToConsole();
```

For full details of logging options see the "EmailHippo.EmailVerify.Api.Client.Diagnostics" namespace in the source code.

