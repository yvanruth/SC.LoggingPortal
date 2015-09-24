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
                <asp:Repeater ID="rptFacets" runat="server" OnItemDataBound="rptFacets_ItemDataBound">
                    <HeaderTemplate>

                    </HeaderTemplate>
                    <ItemTemplate>
                        <h3><asp:Literal ID="litFacetName" runat="server" /></h3>
                        <asp:CheckBoxList ID="cblFacetOptions" runat="server" DataTextFormatString="" AutoPostBack="true" OnSelectedIndexChanged="cblFacetOptions_SelectedIndexChanged" /> 
                    </ItemTemplate>
                    <FooterTemplate>

                    </FooterTemplate>
                </asp:Repeater>
            </ContentTemplate>

        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
