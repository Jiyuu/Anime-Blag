<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" MasterPageFile="~/DefaultMaster.master" %>
<%@ Register Src="~/Controls/PagerControl.ascx" TagName="Pager" TagPrefix="Jiyuu" %>        

        <asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder" ID="content1">
        <div id="contentHolder" class="span-24 last">
        <div class="span-5 colborder">
            <h3 class="alt">
                    חיפוש תגים</h3>
            <div class="box" style="background-color: #FFE6CC;" id="TagSearchDiv" >
            <input type="text" id="TagSearchInput" size="20" style="width:65%;" onkeypress="return SwitchEnterAction(submitSearchTag)" /><input type="button" value="חפש" style="width:28%;" onclick="return submitSearchTag();" />
            </div>
            
            <h3 class="alt">
                בלוגים</h3>
            
            <asp:Repeater runat="server" ID="BlogsRepeater">
                <ItemTemplate>
                <a href="?BlogID=<%#Eval("BlogID") %>"><span class="box" style="padding:0.5em 0.5em 0.5em 0.5em;margin-bottom:1em;display:block;"><%#Eval("BlogName") %></span></a>
                </ItemTemplate>
            </asp:Repeater>
            
            <h3 class="alt">
                    עזרים</h3>
            <div class="box" style="background-color: #FFE6CC;" id="ToolsBoxContainer" >
                <a href="?Feed=RSS2">
                    <img alt="RSS2" src="Images/rss-small.png" style="margin-bottom:-2px;" />
                    פיד סיכום</a>
                    <br />
                <a href="http://www.facebook.com/pages/Anime-Blag/182397635151560" target="_blank">
                    <img alt="FB" src="Images/facebook-small.png" style="margin-bottom:-4px;" />
                    עמוד הפייסבוק</a>
            </div>

            <h3 class="alt">
                דברים שנותרו לעשות</h3>
                <%--<div class="box quiet" style="background-color:#dcdcdc">--%>
                    <ul>
                        <li><span style="text-decoration:line-through;">מעבר נוח בין עמודים</span> בערך</li>
                        <li><span style="text-decoration:line-through;">להוסיף את כל הבלוגים הרלוונטים למערכת</span> פחות או יותר</li>
                        <li><span style="text-decoration:line-through;">פיד רסס מסכם</span></li>
                        <li><span style="text-decoration:line-through;">אינטגרציה לפייסבוק</span> חלקי</li>
                        <li><span style="text-decoration:line-through;">חיפוש פוסטים</span> לפי טאגים</li>
                        <li><span style="text-decoration:line-through;">טיפול בעריכת פוסטים</span> חוץ מפייסבוק</li>
                        <li><span style="text-decoration:line-through;">אווטאר לכל בלוג</span>כמעט לכולם</li>
                        <li><span style="text-decoration:line-through;">קישור לפוסט מהשלוש נקודות בסוף התקציר </span></li>
                        <li>להוסיף תיבת הצעות</li>
                        <li>מימוש קגטוריות ברמת הבלוג </li>
                        <li>לייקים לפוסטים ישירות מהאתר</li>
                        <li>להוסיף תיאור לכל בלוג </li>
                        <li>להוסיף פייביקון</li>
                    </ul>
                <%--</div>--%>
                
        </div>
        
        <div class="span-18  last" id="MainColumn">
        <asp:Label runat="server" ID="PostsTitle" ></asp:Label>
        <div id="TopPagerDiv" class="Centered" style="text-align:center">
           <Jiyuu:Pager runat="server" id="TopPager" ></Jiyuu:Pager>
        </div>
        <asp:Repeater runat="server" ID="PostsRepeater">
            <ItemTemplate>
            <table style="width:650px;margin-left:auto;margin-right:auto;border: solid 1px black;">
            <tr><td rowspan="2" style="width:52px;padding: 0px;"><img alt="logo" src="/images/logos/<%#Eval("Blog.BlogId")%>.png" class="BlogLogo" /></td><td  style="text-align:center;font-weight:bold;" ><span  style="margin-right:-52px">כותרת: <a class="loud" href="<%#Eval("Link") %>"><%#Eval("Title") %></a></span></td></tr>
            <tr><td ><div style="float:right;width:50%;">ע"י: <%#Eval("PostAuthor.AuthorName")%><span style="font-size:x-small"> מהבלוג: <a href="<%#Eval("Blog.HomepageURL")%>"> <%#Eval("Blog.BlogName")%></a></span></div>
                     <div style="float:right;">בתאריך: <%#Eval("PublicationTS")%></div></td></tr>
            <tr><td colspan="2" >מתוייג כ: <%#getCategories() %></td></tr>
            <tr id="PostSummary_<%#Eval("PostID")%>"><td colspan="2" ><%#getSummary()%> [<a href="<%#Eval("Link") %>">...</a>]</td></tr>
            <%--<tr><td colspan="2" style="display:none;"></td></tr>
            <tr id="PostContent_<%#Eval("PostID")%>" style="display:none;"><td colspan="2"><%#Eval("Content")%></td></tr>
            <tr><td colspan="2" style="text-align:center;"><input type="button" id="ShowContentBtn_<%#Eval("PostID")%>" onclick="showContent(event,this)" value="Show Content" style="margin-left:auto;margin-right:auto;" /></td></tr>--%>
            </table>
            </ItemTemplate>
        </asp:Repeater>
        <div id="lowerPager" class="Centered" style="text-align:center">
           <Jiyuu:Pager runat="server" id="FooterPager" ></Jiyuu:Pager>
        </div>
        
        </div>
        </div>
</asp:Content>