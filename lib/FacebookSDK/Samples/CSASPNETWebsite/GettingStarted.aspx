﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="GettingStarted.aspx.cs" Inherits="CSASPNETWebsite.GettingStarted" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Getting Started</h2>
    <p>
        You must set up a new Facebook Application before you can use this sample. Go to
        <a href="http://www.facebook.com/developers/createapp.php">http://www.facebook.com/developers/createapp.php</a>
        and create a new applicatoin.</p>
    <p>
        You must set the 'Site URL' property of your Facebook Application to the url of
        this site. If you are running this sample locally, your url will be http://localhost:5000/.</p>
    <p>
        Read the <a href="http://facebooksdk.codeplex.com/wikipage?title=Getting%20Started&referringTitle=Documentation">
            Getting Started</a> guide on codeplex for more help with configuration.</p>
    <p>
        After you have setup your application, you must configure your web.config file with
        your App ID and Secret.</p>
    <p>
        After you save your web.config file and refresh or reload this sample, you will
        see a Facebook login button.</p>
</asp:Content>
