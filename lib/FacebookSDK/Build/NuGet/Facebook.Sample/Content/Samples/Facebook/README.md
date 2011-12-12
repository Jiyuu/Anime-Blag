# Facebook C# SDK Sample

    Install-Package Facebook.Sample

## Overview
This sample contains simple usage on how to make requests to Facebook 
using the [Facebook C# SDK](http://facebooksdk.codeplex.com) in C#.
(For VB equivalent please checkout "Facebook.Sample.VB" nuget package instead.)

Facebook.Sample nuget package contains variety of codes on helping you 
get started with the new graph api, old legacy rest api, fql and batch
requests.

## Getting Started
There are several source files included in this sample. They have been
categoried according to the type of api.

Each sample file contains a public static method called **RunSamples** 
which allows you to easily start the samples in a particular file. 
You can continue to run individual sample by calling any particular methods.

Note: 
* Most of the samples requires an access_token. If you would like to get
  an access_token you can try installing Facebook.Sample.Winforms.Login nuget
  package.
* You need user_about_me and publish_stream to run most of the samples.
* Certain samples writes on your facebook wall.

	Samples.Facebook.GraphApiSamples.RunSamples(accessToken);
	Samples.Facebook.LegacyRestApiSamples.RunSamples(accessToken);
	Samples.Facebook.FQLSamples.RunSamples(accessToken);
	Samples.Facebook.BatchRequestsSamples.RunSamples(accessToken);

You can also run the samples individually by calling the approprite methods.

	Samples.Facebook.GraphApiSamples.GetSampleWithoutAccessToken();
	Samples.Facebook.GraphApiSamples.GetSampleWithAccessToken(accessToken);
	Samples.Facebook.LegacyRestApiSamples.PostToMyWall(accessToken, "hello world");

Incase your are using .net 4.0 or any compiler that supports *dynamic* keyword,
you might want to try out *Facebook.Sample.Dynamic* nuget package instead.

## Facebook C# SDK related nuget sample packages

	Install-Package Facebook.Sample.Dynamic
	Install-Package Facebook.Sample.Winforms.Login

## License
All source codes included in this sample are distributed under 
[Microsoft Public License (Ms-PL)](http://facebooksdk.codeplex.com/license).
