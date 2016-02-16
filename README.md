[logo]: https://s3.amazonaws.com/emailhippo/bizbranding/co.logos/eh-horiz-695x161.png "Email Hippo"
[Email Hippo]: https://www.emailhippo.com
[SLAB]: https://msdn.microsoft.com/en-us/library/dn440729(v=pandp.60).aspx
[Docs]: http://api-docs.emailhippo.com

![alt text][logo]

# Email Verification API Client

[![email-hippo-public MyGet Build Status](https://www.myget.org/BuildSource/Badge/email-hippo-public?identifier=2486ec88-c07c-4d26-8460-ce2a74093473)](https://www.myget.org/)

## About
This is a .NET package built for easy integration with [Email Hippo] RESTful API services. For
further information on the RESTful server side implementation, please see the [Docs].

## How to get the package
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
 * Built for __high performance__ throughput. Will scale for concurrency and performance based on your hardware configuration (i.e. more CPU cores = more throughput).
 * __Sync__ and __async__ methods.
 * __Parallel__ batch processing available.
 * __Progress reporting__ via event callbacks built in.
 * __Extensive Logging__ built in using async [SLAB].

## Performance
Fast throughput can be achieved by sending lists (IList<string>) of emails for processing. Speed of overall processing depends on your hardware configuration (i.e. number of effective CPU cores and available RAM).

Processing for lists of email is executed in parallel using multiple threads.

### Typical Processing Speed Results

#### Test Hardware Configuration
* __CPU__ : Intel 2700k (4 core + HT = 8 effective cores)
* __RAM__ : 32GB
* __Network (WAN)__ : Fibre (40Mb/sec)

__notes on tests__ :
 * tests run on sequence of randomized @gmail email addresses
 * caching not a test factor (as using random email addresses)

| # Emails | Run Time to Completion (ms)  | Processing Rate  (emails /sec) |
|---------:|-----------------------------:|-------------------------------:|
|       20 |                        7,803 |                           2.56 |
|       50 |                       1,4755 |                           3.38 |
|      100 |                       27,128 |                           3.69 |

## How to use the package
Please note that full example code for all of the snippets below are available in the "EmailHippo.EmailVerify.Api.Client.Tests" 
project which can be found in the GitHub repository for this project.

### Step 1 - license and initialize
This software must be initialized before use. Initializaton is only needed once per app domain. The best palce to do this in in the hosting process bootstrap code. For example, a web app use global.asax, a console app use Main() method.

Supply license configuration to the software by either:

__XML configuration__
In app.config or web.config
```XML
<appSettings>
    <add key="Hippo.EmailVerifyApiKey" value="{your license key}"/>
</appSettings>
```
and then call
```C#
ApiClientFactoryV2.Initialize();
```
or:

__in code as part of initialization__

Invoke static method ApiClientFactoryV2.Initialize(string licenseKey = null)... as follows if supplying the license in code:
```C#
/*Visit https://www.emailhippo.com to get a license key*/
ApiClientFactoryV2.Initialize("{your license key}");
```


### Step 2 - create
The main client object is created using a static factory as follows:

__Example 2__ - creating the client
```c#
var myService = ApiClientFactoryV2.Create();
```

### Step 3 - use
Once you have a reference to the client object, go ahead and use it.

__Example 3__ - checking one or more email address synchronously
```c#
var responses = myService.Process(new VerificationRequest{Emails = new List<string>{"me@here.com"}});

/*Process responses*/
/*..responses*/
```

__Example 4__ - checking more than one email address asynchronously
```c#
var responses = myService.ProcessAsync(new VerificationRequest{Emails = new List<string>{"me@here.com","me2@here.com"}, CancellationToken.None}).Result;

/*Process responses*/
/*..responses*/
```

__Example 5__ - progress reporting

Progress can be captured using the built in event delegate "ProgressChanged" as follows
```c#
myService.ProgressChanged += (o, args) => Console.WriteLine(JsonConvert.SerializeObject(args));
```

__Example 6__ - logging

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
