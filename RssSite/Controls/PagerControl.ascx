<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PagerControl.ascx.cs" Inherits="Controls_PagerControl" %>
<span runat="server" id="PagerHolder">
<a href="#<%=PrevLink %>" class="PagingLink<%=(PrevLink==""?"_Off":"") %>" id="<%=ID %>_PrevLink" onclick="return PagingLinkClick(this)">הקודם</a>

<asp:DropDownList runat="server" ID="PostPagingDdl" CssClass="Centered"></asp:DropDownList>
<script type="text/javascript">$('#<%=PostPagingDdl.ClientID %>').change(PagingDdlChanged); </script> 
            
<a href="#<%=NextLink %>" class="PagingLink<%=(NextLink==""?"_Off":"") %>" id="<%=ID %>_NextLink" onclick="return PagingLinkClick(this)">הבא</a>
</span>