<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="SC.LoggingPortal.Web.Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div style="border:solid black 1px; width:500px; float:left;margin-right: 20px;">
                        <asp:Repeater ID="rptFacets" runat="server" OnItemDataBound="rptFacets_ItemDataBound">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <h3>
                                    <asp:Literal ID="litFacetName" runat="server" /></h3>
                                <asp:CheckBoxList ID="cblFacetOptions" runat="server" DataTextFormatString="" AutoPostBack="true" />
                            </ItemTemplate>
                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>


                   <div style="border:solid black 1px; width:1000px; float:left;">
                       <asp:Repeater ID="rptLogMessages" runat="server" OnItemDataBound="rptLogMessages_ItemDataBound">
                           <HeaderTemplate>

                           </HeaderTemplate>
                           <ItemTemplate>
                               <asp:Literal ID="litMessage" runat="server"></asp:Literal><br />
                           </ItemTemplate>
                           <FooterTemplate>

                           </FooterTemplate>
                       </asp:Repeater>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
