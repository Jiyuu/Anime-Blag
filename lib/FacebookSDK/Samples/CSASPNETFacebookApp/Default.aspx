﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<asp:Label runat="server" ID="a" />
    <a href="<%: this.ResolveCanvasPageUrl("~/") %>" target="_top">Home</a><br />
    <asp:Panel ID="pnlHello" runat="server" Visible="false">
        <h2>
            Hello
            <asp:Label ID="lblName" runat="server" />!
        </h2>
        
    </asp:Panel>
    <asp:Panel ID="pnlError" runat="server" Visible="false">
        <a href="Default.aspx">
            <asp:Label ID="lblError" runat="server" ForeColor="Red" /><br />
        </a>
    </asp:Panel>
</asp:Content>